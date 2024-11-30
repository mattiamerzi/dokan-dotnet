namespace WinVsRemoteClient;

public partial class MainForm : Form
{
    private readonly ConfigFolder sitesConfig;
    public MainForm()
    {
        InitializeComponent();
        sitesConfig = TreeFactory.ReadSitesConfiguration();
        DrawTree();
    }

    private void ConfigureSitesToolStripMenuItem_Click(object sender, EventArgs e)
    {
        SitesConfigForm sitesConfigForm = new(sitesConfig, this);
        sitesConfigForm.ShowDialog();
    }

    internal void DrawTree()
    {
        ToolStripMenuItem fakeRoot = new();
        DrawTree(sitesConfig, fakeRoot);
        var aRoots = new ToolStripMenuItem[fakeRoot.DropDownItems.Count];
        fakeRoot.DropDownItems.CopyTo(aRoots, 0);

        IconContextMenu.SuspendLayout();
        IconContextMenu.Items.Clear();
        IconContextMenu.Items.AddRange(aRoots);
        IconContextMenu.Items.AddRange([ToolStripSeparator, ConfigureSitesToolStripMenuItem, exitToolStripMenuItem]);
        IconContextMenu.ResumeLayout();
    }

    private void DrawTree(ConfigFolder configFolder, ToolStripMenuItem parent)
    {
        ToolStripMenuItem node;
        foreach (var site in configFolder.Sites)
        {
            node = new(site.Label);
            node.Tag = site;
            parent.DropDownItems.Add(node);
        }
        foreach (var folder in configFolder.Folders)
        {
            node = new(folder.Label);
            node.Tag = folder;
            DrawTree(folder, node);
            parent.DropDownItems.Add(node);
        }
    }
}
