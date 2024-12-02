namespace WinVsRemoteClient
{
    partial class SiteRunnerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            StatusBar = new StatusStrip();
            LblStatus = new ToolStripStatusLabel();
            BtnMountUnmount = new Button();
            MainTabControl = new TabControl();
            Page = new TabPage();
            LogViewer = new RichTextBox();
            BtnExplore = new Button();
            LogTimer = new System.Windows.Forms.Timer(components);
            ChkDebug = new CheckBox();
            ControlsPanel = new Panel();
            TxtClear = new Button();
            LblFilter = new Label();
            TxtGrep = new TextBox();
            ChkFollowTail = new CheckBox();
            BtnSaveLogs = new Button();
            StatusBar.SuspendLayout();
            MainTabControl.SuspendLayout();
            Page.SuspendLayout();
            ControlsPanel.SuspendLayout();
            SuspendLayout();
            // 
            // StatusBar
            // 
            StatusBar.ImageScalingSize = new Size(24, 24);
            StatusBar.Items.AddRange(new ToolStripItem[] { LblStatus });
            StatusBar.Location = new Point(0, 594);
            StatusBar.Name = "StatusBar";
            StatusBar.Size = new Size(834, 32);
            StatusBar.TabIndex = 0;
            StatusBar.Text = "statusStrip1";
            // 
            // LblStatus
            // 
            LblStatus.Name = "LblStatus";
            LblStatus.Size = new Size(123, 25);
            LblStatus.Text = "Disconnected.";
            // 
            // BtnMountUnmount
            // 
            BtnMountUnmount.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            BtnMountUnmount.Location = new Point(655, 38);
            BtnMountUnmount.Name = "BtnMountUnmount";
            BtnMountUnmount.Size = new Size(142, 34);
            BtnMountUnmount.TabIndex = 2;
            BtnMountUnmount.Text = "Mount";
            BtnMountUnmount.UseVisualStyleBackColor = true;
            BtnMountUnmount.Click += BtnMountUnmount_Click;
            // 
            // MainTabControl
            // 
            MainTabControl.Controls.Add(Page);
            MainTabControl.Location = new Point(12, 12);
            MainTabControl.Name = "MainTabControl";
            MainTabControl.SelectedIndex = 0;
            MainTabControl.Size = new Size(810, 493);
            MainTabControl.TabIndex = 3;
            // 
            // Page
            // 
            Page.Controls.Add(LogViewer);
            Page.Location = new Point(4, 34);
            Page.Name = "Page";
            Page.Padding = new Padding(3);
            Page.Size = new Size(802, 455);
            Page.TabIndex = 0;
            Page.Text = "Logs";
            Page.UseVisualStyleBackColor = true;
            // 
            // LogViewer
            // 
            LogViewer.BackColor = Color.White;
            LogViewer.Dock = DockStyle.Fill;
            LogViewer.Location = new Point(3, 3);
            LogViewer.Name = "LogViewer";
            LogViewer.ReadOnly = true;
            LogViewer.Size = new Size(796, 449);
            LogViewer.TabIndex = 0;
            LogViewer.Text = "";
            LogViewer.WordWrap = false;
            // 
            // BtnExplore
            // 
            BtnExplore.Enabled = false;
            BtnExplore.Location = new Point(3, 38);
            BtnExplore.Name = "BtnExplore";
            BtnExplore.Size = new Size(112, 34);
            BtnExplore.TabIndex = 4;
            BtnExplore.Text = "Explore";
            BtnExplore.UseVisualStyleBackColor = true;
            BtnExplore.Click += BtnExplore_Click;
            // 
            // LogTimer
            // 
            LogTimer.Interval = 500;
            LogTimer.Tick += LogTimer_Tick;
            // 
            // ChkDebug
            // 
            ChkDebug.AutoSize = true;
            ChkDebug.Location = new Point(3, 3);
            ChkDebug.Name = "ChkDebug";
            ChkDebug.Size = new Size(208, 29);
            ChkDebug.TabIndex = 5;
            ChkDebug.Text = "Enable Debug output";
            ChkDebug.UseVisualStyleBackColor = true;
            ChkDebug.CheckedChanged += ChkDebug_CheckedChanged;
            // 
            // ControlsPanel
            // 
            ControlsPanel.Controls.Add(TxtClear);
            ControlsPanel.Controls.Add(LblFilter);
            ControlsPanel.Controls.Add(TxtGrep);
            ControlsPanel.Controls.Add(ChkFollowTail);
            ControlsPanel.Controls.Add(BtnSaveLogs);
            ControlsPanel.Controls.Add(ChkDebug);
            ControlsPanel.Controls.Add(BtnExplore);
            ControlsPanel.Controls.Add(BtnMountUnmount);
            ControlsPanel.Location = new Point(12, 511);
            ControlsPanel.Name = "ControlsPanel";
            ControlsPanel.Size = new Size(803, 80);
            ControlsPanel.TabIndex = 6;
            // 
            // TxtClear
            // 
            TxtClear.Location = new Point(239, 38);
            TxtClear.Name = "TxtClear";
            TxtClear.Size = new Size(112, 34);
            TxtClear.TabIndex = 10;
            TxtClear.Text = "Clear";
            TxtClear.UseVisualStyleBackColor = true;
            TxtClear.Click += TxtClear_Click;
            // 
            // LblFilter
            // 
            LblFilter.AutoSize = true;
            LblFilter.Location = new Point(373, 4);
            LblFilter.Name = "LblFilter";
            LblFilter.Size = new Size(50, 25);
            LblFilter.TabIndex = 9;
            LblFilter.Text = "Grep";
            // 
            // TxtGrep
            // 
            TxtGrep.Location = new Point(429, 3);
            TxtGrep.Name = "TxtGrep";
            TxtGrep.Size = new Size(216, 31);
            TxtGrep.TabIndex = 8;
            // 
            // ChkFollowTail
            // 
            ChkFollowTail.AutoSize = true;
            ChkFollowTail.Location = new Point(232, 3);
            ChkFollowTail.Name = "ChkFollowTail";
            ChkFollowTail.Size = new Size(118, 29);
            ChkFollowTail.TabIndex = 7;
            ChkFollowTail.Text = "Follow tail";
            ChkFollowTail.UseVisualStyleBackColor = true;
            // 
            // BtnSaveLogs
            // 
            BtnSaveLogs.Location = new Point(121, 38);
            BtnSaveLogs.Name = "BtnSaveLogs";
            BtnSaveLogs.Size = new Size(112, 34);
            BtnSaveLogs.TabIndex = 6;
            BtnSaveLogs.Text = "Save";
            BtnSaveLogs.UseVisualStyleBackColor = true;
            // 
            // SiteRunnerForm
            // 
            AcceptButton = BtnMountUnmount;
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(834, 626);
            Controls.Add(ControlsPanel);
            Controls.Add(MainTabControl);
            Controls.Add(StatusBar);
            MinimumSize = new Size(500, 500);
            Name = "SiteRunnerForm";
            ShowIcon = false;
            Text = "Connection Name - https://127.0.0.1/asdf";
            Resize += SiteRunnerForm_Resize;
            StatusBar.ResumeLayout(false);
            StatusBar.PerformLayout();
            MainTabControl.ResumeLayout(false);
            Page.ResumeLayout(false);
            ControlsPanel.ResumeLayout(false);
            ControlsPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private StatusStrip StatusBar;
        private ToolStripStatusLabel LblStatus;
        private Button BtnMountUnmount;
        private TabControl MainTabControl;
        private TabPage Page;
        private RichTextBox LogViewer;
        private Button BtnExplore;
        private System.Windows.Forms.Timer LogTimer;
        private CheckBox ChkDebug;
        private Panel ControlsPanel;
        private Button BtnSaveLogs;
        private CheckBox ChkFollowTail;
        private Button TxtClear;
        private Label LblFilter;
        private TextBox TxtGrep;
    }
}