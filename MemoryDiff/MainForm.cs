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
        const float SearchTarget = 35000f;

        string Title;

        Process GameProcess { get; set; }
        Scanner MemoryScanner { get; set; }
        IList<IntPtr> Watches { get; set; }
        CancellationTokenSource Cancellation { get; set; }

        ISet<IntPtr> Exclusions { get; } = new HashSet<IntPtr>();
        RadioButton[] Radios => new[] {
            byteRadioButton,
            wordRadioButton,
            dwordRadioButton,
            qwordRadioButton,
            floatRadioButton,
            doubleRadioButton,
            asciiRadioButton,
            cp932RadioButton,
            unicodeLERadioButton,
            unicodeBERadioButton,
            utf8RadioButton,
            byteArrayRadioButton,
            bitArrayRadioButton,
        };

        public MainForm()
        {
            InitializeComponent();
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

            foreach (var control in Radios)
            {
                control.CheckedChanged += (s, ev) =>
                {
                    if (!(s is RadioButton))
                        return;

                    if (!((RadioButton)s).Checked)
                        return;

                    foreach (var radio in Radios)
                    {
                        if (radio != s)
                        {
                            radio.Checked = false;
                        }
                    }
                };
            }
            MemoryScanner = new Scanner(GameProcess);
        }

        async Task Scan()
        {
            Text = Title + " (検索中...)";

            var items = new Dictionary<ulong, ListViewItem>();
            var count = 0;
            Watches = await MemoryScanner.FindMatches(SearchTarget, exclusions: Exclusions, handler: address =>
            {
                Invoke((MethodInvoker)(() =>
                {
                    Text = Title + $" (検索中: {++count})";
                    items[address] = addressListView.Items.Add(new ListViewItem(new[]{
                                address.ToString("X8"), "", SearchTarget.ToString()
                            }));
                }));
            });
            foreach (var exclusion in Exclusions)
            {
                Watches.Remove(exclusion);
            }

            await Console.Out.WriteLineAsync($"Matches ({Watches.Count}):");

            foreach (var address in Watches)
            {
                Console.WriteLine(address);
            }

            Text = Title + " (" + Watches.Count + ")";

            Cancellation?.Cancel();
            Cancellation = new CancellationTokenSource();
            await MemoryScanner.Watch(Watches, 500, Cancellation.Token, allHandler: dict =>
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
    }
}
