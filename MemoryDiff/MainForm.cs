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

        int LastFoundPosition { get; set; } = -1;

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
            GameProcess = Process.GetProcessesByName("notepad").FirstOrDefault();
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

            targetTitleToolStripStatusLabel.Text = $"PID {GameProcess.Id}: {GameProcess.MainWindowTitle}";

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

            foreach (ListViewItem item in items)
            {
                item.BackColor = Color.Transparent;
            }

            ListViewItem found = null;

        find:
            for (var i = LastFoundPosition + 1; i < items.Count; ++i)
            {
                var item = items[i];
                if (item.SubItems[1].Text == queryTextBox.Text)
                {
                    if (found == null || i > LastFoundPosition)
                    {
                        found = item;
                        LastFoundPosition = i;
                        break;
                    }
                }
            }

            if (found == null && LastFoundPosition != -1)
            {
                LastFoundPosition = -1;
                goto find;
            }

            if (found != null)
            {
                if (found.SubItems[1].Text == query)
                {
                    found.BackColor = Color.FromArgb(255, 255, 190);
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
            if (query == null)
                return;

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
            await MemoryScanner.Watch(Watches, 500, Cancellation.Token, t: query, handler: (k, v) =>
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
                    if (!byte.TryParse(text, out byte br))
                    {
                        break;
                    }
                    return br;
                case TypeCode.Int16:
                    if (!short.TryParse(text, out short sr))
                    {
                        break;
                    }
                    return sr;
                case TypeCode.UInt16:
                    if (!ushort.TryParse(text, out ushort usr))
                    {
                        break;
                    }
                    return usr;
                case TypeCode.Int32:
                    if (!int.TryParse(text, out int ir))
                    {
                        break;
                    }
                    return ir;
                case TypeCode.UInt32:
                    if (!uint.TryParse(text, out uint uir))
                    {
                        break;
                    }
                    return uir;
                case TypeCode.Int64:
                    if (!long.TryParse(text, out long lr))
                    {
                        break;
                    }
                    return lr;
                case TypeCode.UInt64:
                    if (!ulong.TryParse(text, out ulong ulr))
                    {
                        break;
                    }
                    return ulr;
                case TypeCode.Single:
                    if (!float.TryParse(text, out float fr))
                    {
                        break;
                    }
                    return fr;
                case TypeCode.Double:
                    if (!double.TryParse(text, out double dr))
                    {
                        break;
                    }
                    return dr;
                default:
                    // TODO: Support Strings, Bit arrays, byte arrays and other types
                    MessageBox.Show(this,
                        $"型 {type.Name} は現在サポートされていません", Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return null;
            }

            MessageBox.Show(this,
                $"入力 {text} は型 {type.Name} に変換できませんでした", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }

    }
}
