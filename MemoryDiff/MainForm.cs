using System;
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
        IDictionary<RadioButton, ScanType> Radios { get; } = new Dictionary<RadioButton, ScanType>();

        int LastFoundPosition { get; set; } = -1;

        public MainForm()
        {
            InitializeComponent();
            Radios.Add(byteRadioButton, ScanType.BYTE);
            Radios.Add(wordRadioButton, ScanType.WORD);
            Radios.Add(dwordRadioButton, ScanType.DWORD);
            Radios.Add(qwordRadioButton, ScanType.QWORD);
            Radios.Add(floatRadioButton, ScanType.Float);
            Radios.Add(doubleRadioButton, ScanType.Double);
            Radios.Add(asciiRadioButton, ScanType.ASCIIString);
            Radios.Add(cp932RadioButton, ScanType.CP932String);
            Radios.Add(unicodeLERadioButton, ScanType.UnicodeLEString);
            Radios.Add(unicodeBERadioButton, ScanType.UnicodeBEString);
            Radios.Add(utf8RadioButton, ScanType.UTF8String);
            Radios.Add(byteArrayRadioButton, ScanType.ByteArray);
            Radios.Add(bitArrayRadioButton, ScanType.BitArray);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Title = Text;
            Log.OnNext += line =>
            {
                Invoke((MethodInvoker)(() =>
                {
                    logTextBox.AppendText(line + "\r\n");
                    if (logTextBox.Text.Length > 10000)
                    {
                        logTextBox.Text = logTextBox.Text.Substring(logTextBox.Text.IndexOf("\r\n"));
                    }
                }));
            };

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
                await Log.WriteLineAsync($"マッチしたアドレス ({Watches.Count}):");
                foreach (var address in Watches)
                {
                    await Log.WriteLineAsync("0x" + address.ToString("X8"));
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

        object Parse(ScanType type, string text)
        {
            switch (type)
            {
                case ScanType.BYTE:
                    if (!byte.TryParse(text, out byte br))
                    {
                        break;
                    }
                    return br;
                case ScanType.WORD:
                    if (!short.TryParse(text, out short sr))
                    {
                        break;
                    }
                    return sr;
                case ScanType.DWORD:
                    if (!int.TryParse(text, out int ir))
                    {
                        break;
                    }
                    return ir;
                case ScanType.QWORD:
                    if (!long.TryParse(text, out long lr))
                    {
                        break;
                    }
                    return lr;
                case ScanType.Float:
                    if (!float.TryParse(text, out float fr))
                    {
                        break;
                    }
                    return fr;
                case ScanType.Double:
                    if (!double.TryParse(text, out double dr))
                    {
                        break;
                    }
                    return dr;
                case ScanType.ASCIIString:
                    return Encoding.ASCII.GetBytes(text);
                case ScanType.CP932String:
                    return Encoding.GetEncoding(932).GetBytes(text);
                case ScanType.UnicodeLEString:
                    return Encoding.Unicode.GetBytes(text);
                case ScanType.UnicodeBEString:
                    return Encoding.BigEndianUnicode.GetBytes(text);
                case ScanType.UTF8String:
                    return Encoding.UTF8.GetBytes(text);
                case ScanType.ByteArray:
                    var fragments = text.Split(' ');
                    var array = new byte[fragments.Length];
                    int index = 0;
                    foreach (var fragment in fragments)
                    {
                        if (!byte.TryParse(fragment, System.Globalization.NumberStyles.HexNumber,
                            null, out byte parsedByte))
                        {
                            MessageBox.Show(this,
                                $"入力 {fragment} は有効な 16 進 BYTE 型の値ではありません", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                        array[index++] = parsedByte;
                    }
                    return array;
                default:
                    // TODO: Support Strings, Bit arrays, byte arrays and other types
                    MessageBox.Show(this,
                        $"型 {type} は現在サポートされていません", Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return null;
            }

            MessageBox.Show(this,
                $"入力 {text} は型 {type} に変換できませんでした", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }

    }
}
