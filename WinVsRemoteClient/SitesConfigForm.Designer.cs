namespace WinVsRemoteClient
{
    partial class SitesConfigForm
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(SitesConfigForm));
            NewSiteOrFolderStrip = new ContextMenuStrip(components);
            NewFolderToolStripMenuItem = new ToolStripMenuItem();
            NewSiteToolStripMenuItem = new ToolStripMenuItem();
            RemoveSelectedToolStripMenuItem = new ToolStripMenuItem();
            LblSites = new Label();
            SitesTreeview = new TreeView();
            TreeIcons = new ImageList(components);
            NewSiteOrFolderStrip.SuspendLayout();
            SuspendLayout();
            // 
            // NewSiteOrFolderStrip
            // 
            NewSiteOrFolderStrip.ImageScalingSize = new Size(24, 24);
            NewSiteOrFolderStrip.Items.AddRange(new ToolStripItem[] { NewFolderToolStripMenuItem, NewSiteToolStripMenuItem, RemoveSelectedToolStripMenuItem });
            NewSiteOrFolderStrip.Name = "newSiteOrFolderStrip";
            NewSiteOrFolderStrip.Size = new Size(175, 100);
            // 
            // NewFolderToolStripMenuItem
            // 
            NewFolderToolStripMenuItem.Name = "NewFolderToolStripMenuItem";
            NewFolderToolStripMenuItem.Size = new Size(174, 32);
            NewFolderToolStripMenuItem.Text = "New Folder";
            NewFolderToolStripMenuItem.Click += NewFolderToolStripMenuItem_Click;
            // 
            // NewSiteToolStripMenuItem
            // 
            NewSiteToolStripMenuItem.Name = "NewSiteToolStripMenuItem";
            NewSiteToolStripMenuItem.Size = new Size(174, 32);
            NewSiteToolStripMenuItem.Text = "New Site";
            NewSiteToolStripMenuItem.Click += NewSiteToolStripMenuItem_Click;
            // 
            // RemoveSelectedToolStripMenuItem
            // 
            RemoveSelectedToolStripMenuItem.Name = "RemoveSelectedToolStripMenuItem";
            RemoveSelectedToolStripMenuItem.Size = new Size(174, 32);
            RemoveSelectedToolStripMenuItem.Text = "Remove";
            RemoveSelectedToolStripMenuItem.Click += RemoveSelectedToolStripMenuItem_Click;
            // 
            // LblSites
            // 
            LblSites.AutoSize = true;
            LblSites.Location = new Point(12, 9);
            LblSites.Name = "LblSites";
            LblSites.Size = new Size(143, 25);
            LblSites.TabIndex = 0;
            LblSites.Text = "Configured Sites";
            // 
            // SitesTreeview
            // 
            SitesTreeview.ImageIndex = 0;
            SitesTreeview.ImageList = TreeIcons;
            SitesTreeview.LabelEdit = true;
            SitesTreeview.Location = new Point(12, 37);
            SitesTreeview.Name = "SitesTreeview";
            SitesTreeview.SelectedImageIndex = 0;
            SitesTreeview.ShowPlusMinus = false;
            SitesTreeview.Size = new Size(312, 530);
            SitesTreeview.TabIndex = 1;
            SitesTreeview.AfterLabelEdit += SitesTreeview_AfterLabelEdit;
            SitesTreeview.BeforeCollapse += SitesTreeview_BeforeCollapse;
            SitesTreeview.MouseDown += SitesTreeview_MouseDown;
            // 
            // TreeIcons
            // 
            TreeIcons.ColorDepth = ColorDepth.Depth32Bit;
            TreeIcons.ImageStream = (ImageListStreamer)resources.GetObject("TreeIcons.ImageStream");
            TreeIcons.TransparentColor = Color.Transparent;
            TreeIcons.Images.SetKeyName(0, "VsRemoteFolder.png");
            TreeIcons.Images.SetKeyName(1, "VsRemoteSite.png");
            // 
            // SitesConfigForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(336, 579);
            Controls.Add(SitesTreeview);
            Controls.Add(LblSites);
            Name = "SitesConfigForm";
            Text = "VsRemote Client";
            NewSiteOrFolderStrip.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label LblSites;
        private TreeView SitesTreeview;
        private ContextMenuStrip NewSiteOrFolderStrip;
        private ToolStripMenuItem NewFolderToolStripMenuItem;
        private ToolStripMenuItem NewSiteToolStripMenuItem;
        private ToolStripMenuItem RemoveSelectedToolStripMenuItem;
        private ImageList TreeIcons;
    }
}