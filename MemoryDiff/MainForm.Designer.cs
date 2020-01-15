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
            this.addressListView = new System.Windows.Forms.ListView();
            this.columnAddress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnCurrent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnPrevious = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.excludeAllButton = new System.Windows.Forms.Button();
            this.refreshButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // addressListView
            // 
            this.addressListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnAddress,
            this.columnCurrent,
            this.columnPrevious});
            this.addressListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addressListView.HideSelection = false;
            this.addressListView.Location = new System.Drawing.Point(0, 24);
            this.addressListView.Name = "addressListView";
            this.addressListView.Size = new System.Drawing.Size(484, 237);
            this.addressListView.TabIndex = 0;
            this.addressListView.UseCompatibleStateImageBehavior = false;
            this.addressListView.View = System.Windows.Forms.View.Details;
            // 
            // columnAddress
            // 
            this.columnAddress.Text = "アドレス";
            // 
            // columnCurrent
            // 
            this.columnCurrent.Text = "現在の値";
            this.columnCurrent.Width = 191;
            // 
            // columnPrevious
            // 
            this.columnPrevious.Text = "最初の値";
            this.columnPrevious.Width = 159;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.excludeAllButton);
            this.panel1.Controls.Add(this.refreshButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(484, 24);
            this.panel1.TabIndex = 2;
            // 
            // excludeAllButton
            // 
            this.excludeAllButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.excludeAllButton.Location = new System.Drawing.Point(75, 0);
            this.excludeAllButton.Name = "excludeAllButton";
            this.excludeAllButton.Size = new System.Drawing.Size(150, 24);
            this.excludeAllButton.TabIndex = 1;
            this.excludeAllButton.Text = "現在の結果をすべて除外";
            this.excludeAllButton.UseVisualStyleBackColor = true;
            this.excludeAllButton.Click += new System.EventHandler(this.excludeAllButton_Click);
            // 
            // refreshButton
            // 
            this.refreshButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.refreshButton.Location = new System.Drawing.Point(0, 0);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(75, 24);
            this.refreshButton.TabIndex = 0;
            this.refreshButton.Text = "再検索";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 261);
            this.Controls.Add(this.addressListView);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Meiryo UI", 9F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.Text = "MemoryDiff";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView addressListView;
        private System.Windows.Forms.ColumnHeader columnAddress;
        private System.Windows.Forms.ColumnHeader columnCurrent;
        private System.Windows.Forms.ColumnHeader columnPrevious;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button excludeAllButton;
        private System.Windows.Forms.Button refreshButton;
    }
}

