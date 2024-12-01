using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;


namespace WinVsRemoteClient;

internal partial class SitesConfigForm : Form
{
    private readonly ConfigFolder sitesConfig;
    private readonly MainForm mainForm;

    internal SitesConfigForm(ConfigFolder sitesConfig, MainForm mainForm)
    {
        InitializeComponent();
        this.sitesConfig = sitesConfig;
        this.mainForm = mainForm;
        DrawTree();
    }

    private void DrawTreeAndSaveConfig()
    {
        TreeFactory.SaveSitesConfiguration(sitesConfig);
        DrawTree();
    }

    private void DrawTree()
    {
        TreeNode fakeRoot = new();
        DrawTree(fakeRoot);
        var aRoots = new TreeNode[fakeRoot.Nodes.Count];
        fakeRoot.Nodes.CopyTo(aRoots, 0);

        SitesTreeview.SuspendLayout();
        SitesTreeview.Nodes.Clear();
        SitesTreeview.Nodes.AddRange(aRoots);
        SitesTreeview.ExpandAll();
        SitesTreeview.ResumeLayout();

        mainForm.DrawTree();
    }

    private void DrawTree(TreeNode parent)
        => DrawTree(sitesConfig, parent);

    private void DrawTree(ConfigFolder configFolder, TreeNode parent)
    {
        TreeNode node;
        if (configFolder.Sites != null)
        {
            foreach (var site in configFolder.Sites)
            {
                node = new(site.Label, 1, 1);
                node.Tag = site;
                parent.Nodes.Add(node);
            }
        }
        if (configFolder.Folders != null)
        {
            foreach (var folder in configFolder.Folders)
            {
                node = new(folder.Label, 0, 0);
                node.Tag = folder;
                DrawTree(folder, node);
                parent.Nodes.Add(node);
            }
        }
    }

    private void SitesTreeview_MouseDown(object sender, MouseEventArgs e)
    {
        var node = SitesTreeview.GetNodeAt(e.X, e.Y);
        SitesTreeview.SelectedNode = node;

        string str = e.Button.ToString() + " | ";
        str += e.X + ":" + e.Y + " | ";
        str += node?.Text ?? "<none>";
        LblSites.Text = str;

        if (e.Button == MouseButtons.Right)
        {
            RemoveSelectedToolStripMenuItem.Available = node != null;
            NewSiteToolStripMenuItem.Available =
            NewFolderToolStripMenuItem.Available =
                node == null || node.Tag is ConfigFolder;

            NewSiteOrFolderStrip.Tag = node?.Tag;

            NewSiteOrFolderStrip.Show(SitesTreeview, e.Location);
        }

    }

    private void RemoveSelectedToolStripMenuItem_Click(object sender, EventArgs e)
    {
        object? tag = NewSiteOrFolderStrip?.Tag;
        if (tag == null)
            return;
        if (tag is ConfigFolder folder)
        {
            var folderFolder = sitesConfig.FindFolderFolder(folder.Id);

            if (folderFolder != null)
                folderFolder.Folders = folderFolder.Folders.Where(f => f.Id != folder.Id).ToList();
        }
        if (tag is ConfigSite site)
        {
            var siteFolder = sitesConfig.FindSiteFolder(site.Id);

            if (siteFolder != null)
                siteFolder.Sites = siteFolder.Sites.Where(s => s.Id != site.Id).ToList();
        }
        DrawTreeAndSaveConfig();
    }

    private void NewFolderToolStripMenuItem_Click(object sender, EventArgs e)
    {
        ConfigFolder folder = (NewSiteOrFolderStrip?.Tag as ConfigFolder) ?? sitesConfig;
        folder.Folders = folder.Folders.Concat([new ConfigFolder() { Label = "New Folder" }]);
        DrawTreeAndSaveConfig();
    }

    private void NewSiteToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var folder = (NewSiteOrFolderStrip?.Tag as ConfigFolder) ?? sitesConfig;

        SiteEditForm siteEditForm = new();
        var res = siteEditForm.ShowDialog();
        if (res == DialogResult.OK && siteEditForm.ConfigSite != null)
        {
            folder.Sites = folder.Sites.Concat([siteEditForm.ConfigSite]);
        }
        DrawTreeAndSaveConfig();
    }
    private void SitesTreeview_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        => e.Cancel = true;

    private void SitesTreeview_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
    {
        if (e.Label != null)
        {
            var node = SitesTreeview.SelectedNode;
            if (node != null)
            {
                if (node.Tag is ConfigFolder folder && folder.Label != e.Label)
                {
                    folder.Label = e.Label;
                    TreeFactory.SaveSitesConfiguration(sitesConfig);
                }
                if (node.Tag is ConfigSite site && site.Label != e.Label)
                {
                    site.Label = e.Label;
                    TreeFactory.SaveSitesConfiguration(sitesConfig);
                }
            }
        }
    }
}
