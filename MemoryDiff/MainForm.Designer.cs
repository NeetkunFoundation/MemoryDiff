namespace MemoryDiff
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.addressListView = new System.Windows.Forms.ListView();
            this.columnAddress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnCurrent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnPrevious = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.queryTextBox = new System.Windows.Forms.TextBox();
            this.logTextBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.byteArrayRadioButton = new System.Windows.Forms.RadioButton();
            this.bitArrayRadioButton = new System.Windows.Forms.RadioButton();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.asciiRadioButton = new System.Windows.Forms.RadioButton();
            this.cp932RadioButton = new System.Windows.Forms.RadioButton();
            this.unicodeLERadioButton = new System.Windows.Forms.RadioButton();
            this.unicodeBERadioButton = new System.Windows.Forms.RadioButton();
            this.utf8RadioButton = new System.Windows.Forms.RadioButton();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.byteRadioButton = new System.Windows.Forms.RadioButton();
            this.wordRadioButton = new System.Windows.Forms.RadioButton();
            this.dwordRadioButton = new System.Windows.Forms.RadioButton();
            this.qwordRadioButton = new System.Windows.Forms.RadioButton();
            this.floatRadioButton = new System.Windows.Forms.RadioButton();
            this.doubleRadioButton = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pauseButton = new System.Windows.Forms.Button();
            this.findFromListButton = new System.Windows.Forms.Button();
            this.excludeAllButton = new System.Windows.Forms.Button();
            this.refreshButton = new System.Windows.Forms.Button();
            this.mainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.readyToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.springToolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.targetTitleToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.mainStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(4, 4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.addressListView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(900, 588);
            this.splitContainer1.SplitterDistance = 291;
            this.splitContainer1.TabIndex = 0;
            // 
            // addressListView
            // 
            this.addressListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnAddress,
            this.columnCurrent,
            this.columnPrevious});
            this.addressListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addressListView.HideSelection = false;
            this.addressListView.Location = new System.Drawing.Point(0, 0);
            this.addressListView.Name = "addressListView";
            this.addressListView.Size = new System.Drawing.Size(900, 291);
            this.addressListView.TabIndex = 3;
            this.addressListView.UseCompatibleStateImageBehavior = false;
            this.addressListView.View = System.Windows.Forms.View.Details;
            // 
            // columnAddress
            // 
            this.columnAddress.Text = "アドレス";
            this.columnAddress.Width = 136;
            // 
            // columnCurrent
            // 
            this.columnCurrent.Text = "現在の値";
            this.columnCurrent.Width = 246;
            // 
            // columnPrevious
            // 
            this.columnPrevious.Text = "最初の値";
            this.columnPrevious.Width = 91;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(900, 263);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "検索の設定";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.queryTextBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.logTextBox, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 19);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(894, 241);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 106);
            this.label3.TabIndex = 12;
            this.label3.Text = "ログ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 106);
            this.label2.TabIndex = 10;
            this.label2.Text = "型";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 29);
            this.label1.TabIndex = 7;
            this.label1.Text = "検索内容";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // queryTextBox
            // 
            this.queryTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.queryTextBox.Font = new System.Drawing.Font("ＭＳ ゴシック", 12F);
            this.queryTextBox.Location = new System.Drawing.Point(64, 3);
            this.queryTextBox.Name = "queryTextBox";
            this.queryTextBox.Size = new System.Drawing.Size(833, 23);
            this.queryTextBox.TabIndex = 9;
            // 
            // logTextBox
            // 
            this.logTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logTextBox.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.logTextBox.Location = new System.Drawing.Point(64, 138);
            this.logTextBox.Multiline = true;
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.ReadOnly = true;
            this.logTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logTextBox.Size = new System.Drawing.Size(833, 100);
            this.logTextBox.TabIndex = 13;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel3, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(64, 32);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(833, 100);
            this.tableLayoutPanel2.TabIndex = 14;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.byteArrayRadioButton);
            this.flowLayoutPanel3.Controls.Add(this.bitArrayRadioButton);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(3, 63);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(827, 34);
            this.flowLayoutPanel3.TabIndex = 13;
            // 
            // byteArrayRadioButton
            // 
            this.byteArrayRadioButton.AutoSize = true;
            this.byteArrayRadioButton.Location = new System.Drawing.Point(3, 3);
            this.byteArrayRadioButton.Name = "byteArrayRadioButton";
            this.byteArrayRadioButton.Size = new System.Drawing.Size(64, 19);
            this.byteArrayRadioButton.TabIndex = 25;
            this.byteArrayRadioButton.Text = "バイト列";
            this.byteArrayRadioButton.UseVisualStyleBackColor = true;
            // 
            // bitArrayRadioButton
            // 
            this.bitArrayRadioButton.AutoSize = true;
            this.bitArrayRadioButton.Location = new System.Drawing.Point(73, 3);
            this.bitArrayRadioButton.Name = "bitArrayRadioButton";
            this.bitArrayRadioButton.Size = new System.Drawing.Size(60, 19);
            this.bitArrayRadioButton.TabIndex = 26;
            this.bitArrayRadioButton.Text = "ビット列";
            this.bitArrayRadioButton.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.asciiRadioButton);
            this.flowLayoutPanel2.Controls.Add(this.cp932RadioButton);
            this.flowLayoutPanel2.Controls.Add(this.unicodeLERadioButton);
            this.flowLayoutPanel2.Controls.Add(this.unicodeBERadioButton);
            this.flowLayoutPanel2.Controls.Add(this.utf8RadioButton);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 33);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(827, 24);
            this.flowLayoutPanel2.TabIndex = 12;
            // 
            // asciiRadioButton
            // 
            this.asciiRadioButton.AutoSize = true;
            this.asciiRadioButton.Location = new System.Drawing.Point(3, 3);
            this.asciiRadioButton.Name = "asciiRadioButton";
            this.asciiRadioButton.Size = new System.Drawing.Size(59, 19);
            this.asciiRadioButton.TabIndex = 18;
            this.asciiRadioButton.Text = "ASCII";
            this.asciiRadioButton.UseVisualStyleBackColor = true;
            // 
            // cp932RadioButton
            // 
            this.cp932RadioButton.AutoSize = true;
            this.cp932RadioButton.Location = new System.Drawing.Point(68, 3);
            this.cp932RadioButton.Name = "cp932RadioButton";
            this.cp932RadioButton.Size = new System.Drawing.Size(61, 19);
            this.cp932RadioButton.TabIndex = 19;
            this.cp932RadioButton.Text = "CP932";
            this.cp932RadioButton.UseVisualStyleBackColor = true;
            // 
            // unicodeLERadioButton
            // 
            this.unicodeLERadioButton.AutoSize = true;
            this.unicodeLERadioButton.Location = new System.Drawing.Point(135, 3);
            this.unicodeLERadioButton.Name = "unicodeLERadioButton";
            this.unicodeLERadioButton.Size = new System.Drawing.Size(89, 19);
            this.unicodeLERadioButton.TabIndex = 20;
            this.unicodeLERadioButton.Text = "Unicode LE";
            this.unicodeLERadioButton.UseVisualStyleBackColor = true;
            // 
            // unicodeBERadioButton
            // 
            this.unicodeBERadioButton.AutoSize = true;
            this.unicodeBERadioButton.Location = new System.Drawing.Point(230, 3);
            this.unicodeBERadioButton.Name = "unicodeBERadioButton";
            this.unicodeBERadioButton.Size = new System.Drawing.Size(90, 19);
            this.unicodeBERadioButton.TabIndex = 21;
            this.unicodeBERadioButton.Text = "Unicode BE";
            this.unicodeBERadioButton.UseVisualStyleBackColor = true;
            // 
            // utf8RadioButton
            // 
            this.utf8RadioButton.AutoSize = true;
            this.utf8RadioButton.Location = new System.Drawing.Point(326, 3);
            this.utf8RadioButton.Name = "utf8RadioButton";
            this.utf8RadioButton.Size = new System.Drawing.Size(61, 19);
            this.utf8RadioButton.TabIndex = 22;
            this.utf8RadioButton.Text = "UTF-8";
            this.utf8RadioButton.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.byteRadioButton);
            this.flowLayoutPanel1.Controls.Add(this.wordRadioButton);
            this.flowLayoutPanel1.Controls.Add(this.dwordRadioButton);
            this.flowLayoutPanel1.Controls.Add(this.qwordRadioButton);
            this.flowLayoutPanel1.Controls.Add(this.floatRadioButton);
            this.flowLayoutPanel1.Controls.Add(this.doubleRadioButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(827, 24);
            this.flowLayoutPanel1.TabIndex = 11;
            // 
            // byteRadioButton
            // 
            this.byteRadioButton.AutoSize = true;
            this.byteRadioButton.Location = new System.Drawing.Point(3, 3);
            this.byteRadioButton.Name = "byteRadioButton";
            this.byteRadioButton.Size = new System.Drawing.Size(56, 19);
            this.byteRadioButton.TabIndex = 12;
            this.byteRadioButton.Text = "BYTE";
            this.byteRadioButton.UseVisualStyleBackColor = true;
            // 
            // wordRadioButton
            // 
            this.wordRadioButton.AutoSize = true;
            this.wordRadioButton.Location = new System.Drawing.Point(65, 3);
            this.wordRadioButton.Name = "wordRadioButton";
            this.wordRadioButton.Size = new System.Drawing.Size(63, 19);
            this.wordRadioButton.TabIndex = 13;
            this.wordRadioButton.Text = "WORD";
            this.wordRadioButton.UseVisualStyleBackColor = true;
            // 
            // dwordRadioButton
            // 
            this.dwordRadioButton.AutoSize = true;
            this.dwordRadioButton.Checked = true;
            this.dwordRadioButton.Location = new System.Drawing.Point(134, 3);
            this.dwordRadioButton.Name = "dwordRadioButton";
            this.dwordRadioButton.Size = new System.Drawing.Size(72, 19);
            this.dwordRadioButton.TabIndex = 14;
            this.dwordRadioButton.TabStop = true;
            this.dwordRadioButton.Text = "DWORD";
            this.dwordRadioButton.UseVisualStyleBackColor = true;
            // 
            // qwordRadioButton
            // 
            this.qwordRadioButton.AutoSize = true;
            this.qwordRadioButton.Location = new System.Drawing.Point(212, 3);
            this.qwordRadioButton.Name = "qwordRadioButton";
            this.qwordRadioButton.Size = new System.Drawing.Size(72, 19);
            this.qwordRadioButton.TabIndex = 15;
            this.qwordRadioButton.Text = "QWORD";
            this.qwordRadioButton.UseVisualStyleBackColor = true;
            // 
            // floatRadioButton
            // 
            this.floatRadioButton.AutoSize = true;
            this.floatRadioButton.Location = new System.Drawing.Point(290, 3);
            this.floatRadioButton.Name = "floatRadioButton";
            this.floatRadioButton.Size = new System.Drawing.Size(51, 19);
            this.floatRadioButton.TabIndex = 16;
            this.floatRadioButton.Text = "float";
            this.floatRadioButton.UseVisualStyleBackColor = true;
            // 
            // doubleRadioButton
            // 
            this.doubleRadioButton.AutoSize = true;
            this.doubleRadioButton.Location = new System.Drawing.Point(347, 3);
            this.doubleRadioButton.Name = "doubleRadioButton";
            this.doubleRadioButton.Size = new System.Drawing.Size(63, 19);
            this.doubleRadioButton.TabIndex = 17;
            this.doubleRadioButton.Text = "double";
            this.doubleRadioButton.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pauseButton);
            this.panel1.Controls.Add(this.findFromListButton);
            this.panel1.Controls.Add(this.excludeAllButton);
            this.panel1.Controls.Add(this.refreshButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 263);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(900, 30);
            this.panel1.TabIndex = 5;
            // 
            // pauseButton
            // 
            this.pauseButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.pauseButton.Location = new System.Drawing.Point(633, 0);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(96, 30);
            this.pauseButton.TabIndex = 3;
            this.pauseButton.Text = "一時停止";
            this.pauseButton.UseVisualStyleBackColor = true;
            // 
            // findFromListButton
            // 
            this.findFromListButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.findFromListButton.Location = new System.Drawing.Point(729, 0);
            this.findFromListButton.Name = "findFromListButton";
            this.findFromListButton.Size = new System.Drawing.Size(96, 30);
            this.findFromListButton.TabIndex = 2;
            this.findFromListButton.Text = "リストから検索";
            this.findFromListButton.UseVisualStyleBackColor = true;
            this.findFromListButton.Click += new System.EventHandler(this.findFromListButton_Click);
            // 
            // excludeAllButton
            // 
            this.excludeAllButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.excludeAllButton.Location = new System.Drawing.Point(0, 0);
            this.excludeAllButton.Name = "excludeAllButton";
            this.excludeAllButton.Size = new System.Drawing.Size(150, 30);
            this.excludeAllButton.TabIndex = 1;
            this.excludeAllButton.Text = "現在の結果をすべて除外";
            this.excludeAllButton.UseVisualStyleBackColor = true;
            this.excludeAllButton.Click += new System.EventHandler(this.excludeAllButton_Click);
            // 
            // refreshButton
            // 
            this.refreshButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.refreshButton.Location = new System.Drawing.Point(825, 0);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(75, 30);
            this.refreshButton.TabIndex = 0;
            this.refreshButton.Text = "再検索";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // mainStatusStrip
            // 
            this.mainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.readyToolStripStatusLabel,
            this.springToolStripStatusLabel1,
            this.targetTitleToolStripStatusLabel});
            this.mainStatusStrip.Location = new System.Drawing.Point(4, 592);
            this.mainStatusStrip.Name = "mainStatusStrip";
            this.mainStatusStrip.Size = new System.Drawing.Size(900, 22);
            this.mainStatusStrip.TabIndex = 6;
            this.mainStatusStrip.Text = "statusStrip1";
            // 
            // readyToolStripStatusLabel
            // 
            this.readyToolStripStatusLabel.Name = "readyToolStripStatusLabel";
            this.readyToolStripStatusLabel.Size = new System.Drawing.Size(55, 17);
            this.readyToolStripStatusLabel.Text = "準備完了";
            // 
            // springToolStripStatusLabel1
            // 
            this.springToolStripStatusLabel1.Name = "springToolStripStatusLabel1";
            this.springToolStripStatusLabel1.Size = new System.Drawing.Size(830, 17);
            this.springToolStripStatusLabel1.Spring = true;
            // 
            // targetTitleToolStripStatusLabel
            // 
            this.targetTitleToolStripStatusLabel.Name = "targetTitleToolStripStatusLabel";
            this.targetTitleToolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(908, 618);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.mainStatusStrip);
            this.Font = new System.Drawing.Font("Meiryo UI", 9F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.Text = "MemoryDiff";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.mainStatusStrip.ResumeLayout(false);
            this.mainStatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView addressListView;
        private System.Windows.Forms.ColumnHeader columnAddress;
        private System.Windows.Forms.ColumnHeader columnCurrent;
        private System.Windows.Forms.ColumnHeader columnPrevious;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button excludeAllButton;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox queryTextBox;
        private System.Windows.Forms.TextBox logTextBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.RadioButton byteArrayRadioButton;
        private System.Windows.Forms.RadioButton bitArrayRadioButton;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.RadioButton asciiRadioButton;
        private System.Windows.Forms.RadioButton cp932RadioButton;
        private System.Windows.Forms.RadioButton unicodeLERadioButton;
        private System.Windows.Forms.RadioButton unicodeBERadioButton;
        private System.Windows.Forms.RadioButton utf8RadioButton;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.RadioButton byteRadioButton;
        private System.Windows.Forms.RadioButton wordRadioButton;
        private System.Windows.Forms.RadioButton dwordRadioButton;
        private System.Windows.Forms.RadioButton qwordRadioButton;
        private System.Windows.Forms.RadioButton floatRadioButton;
        private System.Windows.Forms.RadioButton doubleRadioButton;
        private System.Windows.Forms.Button findFromListButton;
        private System.Windows.Forms.Button pauseButton;
        private System.Windows.Forms.StatusStrip mainStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel readyToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel springToolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel targetTitleToolStripStatusLabel;
    }
}

