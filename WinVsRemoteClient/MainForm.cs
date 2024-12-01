namespace WinVsRemoteClient;

public partial class MainForm : Form
{
    private readonly VsRemoteFSManager dokanManager = new();
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
        IconContextMenu.Items.AddRange([ToolStripSeparator, ConfigureSitesToolStripMenuItem, ExitToolStripMenuItem]);
        IconContextMenu.ResumeLayout();
    }

    private void DrawTree(ConfigFolder configFolder, ToolStripMenuItem parent)
    {
        ToolStripMenuItem node;
        foreach (var site in configFolder.Sites)
        {
            node = new(site.Label) { Tag = site };
            node.Click += Site_Click;
            parent.DropDownItems.Add(node);
        }
        foreach (var folder in configFolder.Folders)
        {
            node = new(folder.Label) { Tag = folder };
            DrawTree(folder, node);
            parent.DropDownItems.Add(node);
        }
    }

    private void Site_Click(object? sender, EventArgs e)
    {
        if (sender is ToolStripMenuItem menuItem)
        {
            if (menuItem.Tag is ConfigSite site)
            {
                dokanManager.MountVsRemoteFS(site);
            }
        }
    }

    private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }
}
