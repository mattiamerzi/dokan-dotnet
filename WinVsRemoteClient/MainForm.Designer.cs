namespace WinVsRemoteClient
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            TaskbarIcon = new NotifyIcon(components);
            IconContextMenu = new ContextMenuStrip(components);
            ToolStripSeparator = new ToolStripSeparator();
            ConfigureSitesToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            IconContextMenu.SuspendLayout();
            SuspendLayout();
            // 
            // TaskbarIcon
            // 
            TaskbarIcon.BalloonTipText = "right-click to connect";
            TaskbarIcon.BalloonTipTitle = "VsRemote Client";
            TaskbarIcon.ContextMenuStrip = IconContextMenu;
            TaskbarIcon.Icon = (Icon)resources.GetObject("TaskbarIcon.Icon");
            TaskbarIcon.Text = "VsRemote Client";
            TaskbarIcon.Visible = true;
            // 
            // IconContextMenu
            // 
            IconContextMenu.ImageScalingSize = new Size(24, 24);
            IconContextMenu.Items.AddRange(new ToolStripItem[] { ToolStripSeparator, ConfigureSitesToolStripMenuItem, exitToolStripMenuItem });
            IconContextMenu.Name = "iconContextMenu";
            IconContextMenu.Size = new Size(241, 107);
            // 
            // ToolStripSeparator
            // 
            ToolStripSeparator.Name = "ToolStripSeparator";
            ToolStripSeparator.Size = new Size(237, 6);
            // 
            // ConfigureSitesToolStripMenuItem
            // 
            ConfigureSitesToolStripMenuItem.Name = "ConfigureSitesToolStripMenuItem";
            ConfigureSitesToolStripMenuItem.Size = new Size(240, 32);
            ConfigureSitesToolStripMenuItem.Text = "Configure Sites";
            ConfigureSitesToolStripMenuItem.Click += ConfigureSitesToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(240, 32);
            exitToolStripMenuItem.Text = "Exit";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Name = "MainForm";
            ShowInTaskbar = false;
            Text = "Form1";
            WindowState = FormWindowState.Minimized;
            IconContextMenu.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private NotifyIcon TaskbarIcon;
        private ToolStripMenuItem ConfigureSitesToolStripMenuItem;
        private ToolStripSeparator ToolStripSeparator;
        private ToolStripMenuItem exitToolStripMenuItem;
        internal ContextMenuStrip IconContextMenu;
    }
}
