﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoryDiff
{
    public partial class MainForm : Form
    {
        string Title;

        Process GameProcess { get; set; }
        Scanner MemoryScanner { get; set; }
        IList<IntPtr> Watches { get; set; }
        CancellationTokenSource Cancellation { get; set; }

        ISet<IntPtr> Exclusions { get; } = new HashSet<IntPtr>();
        IDictionary<RadioButton, Type> Radios { get; } = new Dictionary<RadioButton, Type>();

        int LastFoundPosition { get; set; } = 0;

        public MainForm()
        {
            InitializeComponent();
            Radios.Add(byteRadioButton, typeof(byte));
            Radios.Add(wordRadioButton, typeof(short));
            Radios.Add(dwordRadioButton, typeof(int));
            Radios.Add(qwordRadioButton, typeof(long));
            Radios.Add(floatRadioButton, typeof(float));
            Radios.Add(doubleRadioButton, typeof(double));
            Radios.Add(asciiRadioButton, typeof(string));
            Radios.Add(cp932RadioButton, typeof(string));
            Radios.Add(unicodeLERadioButton, typeof(string));
            Radios.Add(unicodeBERadioButton, typeof(string));
            Radios.Add(utf8RadioButton, typeof(string));
            Radios.Add(byteArrayRadioButton, typeof(byte[]));
            Radios.Add(bitArrayRadioButton, typeof(byte[]));
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Title = Text;
            GameProcess = Process.GetProcessesByName("MonsterHunterWorld").FirstOrDefault();
            if (GameProcess == default(Process))
            {
                MessageBox.Show(
                    "MONSTER HUNTER: WORLD のプロセスが見つかりませんでした。",
                    "MemoryDiff - エラー",
                    MessageBoxButtons.OK, MessageBoxIcon.Error
                );
                return;
            }

            foreach (var control in Radios.Keys)
            {
                control.CheckedChanged += (s, ev) =>
                {
                    if (!(s is RadioButton))
                        return;

                    if (!((RadioButton)s).Checked)
                        return;

                    foreach (var radio in Radios.Keys)
                    {
                        if (radio != s)
                        {
                            radio.Checked = false;
                        }
                    }
                };
            }

            targetTitleToolStripStatusLabel.Text = GameProcess.MainWindowTitle;

            MemoryScanner = new Scanner(GameProcess);
        }

        private void excludeAllButton_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in addressListView.Items)
            {
                Exclusions.Add((IntPtr)ulong.Parse(item.SubItems[0].Text, System.Globalization.NumberStyles.HexNumber));
            }
            addressListView.Items.Clear();
            Cancellation?.Cancel();
        }

        private async void refreshButton_Click(object sender, EventArgs e)
        {
            await Scan().ConfigureAwait(false);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cancellation?.Cancel();
        }

        private void findFromListButton_Click(object sender, EventArgs e)
        {
            FindNext();
        }

        void FindNext()
        {
            var query = queryTextBox.Text;
            var items = addressListView.Items;
            ListViewItem found = null;

        find:
            for (var i = LastFoundPosition; i < items.Count; ++i)
            {
                var item = items[i];
                if (found.SubItems[1].Text == queryTextBox.Text)
                {
                    if (found == null || i > LastFoundPosition)
                    {
                        found = item;
                        LastFoundPosition = i;
                        break;
                    }
                }
            }

            if (found == null && LastFoundPosition != 0)
            {
                LastFoundPosition = 0;
                goto find;
            }

            if (found != null)
            {
                if (found.SubItems[1].Text == query)
                {
                    found.Focused = true;
                }
            }
            else
            {
                MessageBox.Show($"条件 {query} に合う項目は見つかりませんでした", "検索", MessageBoxButtons.OK);
            }
        }

        async Task Scan()
        {
            addressListView.Items.Clear();
            var count = 0;

            Invoke((MethodInvoker)(() =>
            {
                readyToolStripStatusLabel.Text = ScanState.Scanning.ToString(count);
                Text = Title + " (検索中...)";
            }));

            var items = new Dictionary<ulong, ListViewItem>();

            var query = GetQuery();

            Watches = await MemoryScanner.FindMatches(query, exclusions: Exclusions, handler: (address, found) =>
            {
                BeginInvoke((MethodInvoker)(() =>
                {
                    var c = Interlocked.Increment(ref count);
                    Text = $"{Title} (検索中: {c})";
                    readyToolStripStatusLabel.Text = ScanState.Scanning.ToString(c);
                    items[address] = addressListView.Items.Add(new ListViewItem(new[] {
                        address.ToString("X8"), "", found.ToString()
                    }));
                }));
            });

            await Task.Run(async () =>
            {
                await Console.Out.WriteLineAsync($"Matches ({Watches.Count}):");
                foreach (var address in Watches)
                {
                    await Console.Out.WriteLineAsync("0x" + address.ToString("X8"));
                }
            });

            Invoke((MethodInvoker)(() =>
            {
                Text = $"{Title} ({Watches.Count})";
                readyToolStripStatusLabel.Text = ScanState.Complete.ToString(Watches.Count);
            }));

            Cancellation?.Cancel();
            Cancellation = new CancellationTokenSource();
            await MemoryScanner.Watch(Watches, 500, Cancellation.Token, t: query, allHandler: dict =>
           {
               foreach (var (k, v) in dict)
               {
                   Invoke((MethodInvoker)(() =>
                   {
                       if (items.ContainsKey(k))
                       {
                           items[k].SubItems[1].Text = v.ToString();
                       }
                       else
                       {
                           items[k] = addressListView.Items.Add(new ListViewItem(new[]{
                                k.ToString("X8"), v.ToString(), v.ToString()
                           }));
                       }
                   }));
               }
           });
        }

        object GetQuery()
        {
            object query = null;
            foreach (var radio in Radios)
            {
                if (!radio.Key.Checked) continue;
                query = Parse(radio.Value, queryTextBox.Text);
                break;
            }
            return query;
        }

        object Parse(Type type, string text)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                    return byte.Parse(text);
                case TypeCode.Int16:
                    return short.Parse(text);
                case TypeCode.UInt16:
                    return ushort.Parse(text);
                case TypeCode.Int32:
                    return int.Parse(text);
                case TypeCode.UInt32:
                    return uint.Parse(text);
                case TypeCode.Int64:
                    return long.Parse(text);
                case TypeCode.UInt64:
                    return ulong.Parse(text);
                case TypeCode.Single:
                    return float.Parse(text);
                case TypeCode.Double:
                    return double.Parse(text);
                default:
                    // TODO: Support Strings, Bit arrays, byte arrays and other types
                    return text;
            }
        }

    }
}
