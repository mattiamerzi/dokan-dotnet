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
            StatusBar.SuspendLayout();
            MainTabControl.SuspendLayout();
            Page.SuspendLayout();
            SuspendLayout();
            // 
            // StatusBar
            // 
            StatusBar.ImageScalingSize = new Size(24, 24);
            StatusBar.Items.AddRange(new ToolStripItem[] { LblStatus });
            StatusBar.Location = new Point(0, 412);
            StatusBar.Name = "StatusBar";
            StatusBar.Size = new Size(478, 32);
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
            BtnMountUnmount.Location = new Point(317, 371);
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
            MainTabControl.Size = new Size(454, 357);
            MainTabControl.TabIndex = 3;
            // 
            // Page
            // 
            Page.Controls.Add(LogViewer);
            Page.Location = new Point(4, 34);
            Page.Name = "Page";
            Page.Padding = new Padding(3);
            Page.Size = new Size(446, 319);
            Page.TabIndex = 0;
            Page.Text = "Logs";
            Page.UseVisualStyleBackColor = true;
            // 
            // LogViewer
            // 
            LogViewer.Dock = DockStyle.Fill;
            LogViewer.Location = new Point(3, 3);
            LogViewer.Name = "LogViewer";
            LogViewer.Size = new Size(440, 313);
            LogViewer.TabIndex = 0;
            LogViewer.Text = "Hello, World!";
            // 
            // BtnExplore
            // 
            BtnExplore.Enabled = false;
            BtnExplore.Location = new Point(19, 371);
            BtnExplore.Name = "BtnExplore";
            BtnExplore.Size = new Size(112, 34);
            BtnExplore.TabIndex = 4;
            BtnExplore.Text = "Explore";
            BtnExplore.UseVisualStyleBackColor = true;
            // 
            // LogTimer
            // 
            LogTimer.Interval = 500;
            LogTimer.Tick += LogTimer_Tick;
            // 
            // SiteRunnerForm
            // 
            AcceptButton = BtnMountUnmount;
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(478, 444);
            Controls.Add(BtnExplore);
            Controls.Add(MainTabControl);
            Controls.Add(BtnMountUnmount);
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
    }
}