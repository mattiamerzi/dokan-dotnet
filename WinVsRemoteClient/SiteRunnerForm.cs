using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinVsRemoteClient;

internal partial class SiteRunnerForm : Form
{
    private Size TcSize;
    private Size PanelSize;
    private readonly Size FormSize;
    private Point PanelLocation;
    private readonly VsRemoteFSManager fsmanager;
    private readonly ConfigSite site;
    private DynaLogger? logger;

    public SiteRunnerForm(VsRemoteFSManager fsmanager, ConfigSite site)
    {
        InitializeComponent();
        this.fsmanager = fsmanager;
        this.site = site;
        FormSize = Size;
        TcSize = MainTabControl.Size;
        PanelSize = ControlsPanel.Size;
        PanelLocation = ControlsPanel.Location;
        this.Text = $"{site.Label} - {site.Address}";
        UpdateControls();
    }

    private void SiteRunnerForm_Resize(object sender, EventArgs e)
    {
        MainTabControl.Size = new(TcSize.Width + (Size.Width - FormSize.Width), TcSize.Height + (Size.Height - FormSize.Height));
        ControlsPanel.Size = new(PanelSize.Width + (Size.Width - FormSize.Width), PanelSize.Height);
        ControlsPanel.Location = new(PanelLocation.X, PanelLocation.Y + (Size.Height - FormSize.Height));
        //BtnMountUnmount.Location = new(BtnLocation.X + (Size.Width - 500), BtnLocation.Y + (Size.Height - 500));
    }

    private bool IsMounted()
        => fsmanager.IsMounted(site);

    private void UpdateControls()
    {
        bool mounted = IsMounted();
        BtnMountUnmount.Text = mounted ? "Unmount" : "Mount";
        BtnExplore.Enabled = mounted;
        LogTimer.Enabled = mounted;
        if (mounted)
            LblStatus.Text = $"Connected to {site.Address}";
    }

    private void BtnMountUnmount_Click(object sender, EventArgs e)
    {
        try
        {
            if (IsMounted())
            {
                fsmanager.UnmountVsRemoteFS(site);
            }
            else
            {
                fsmanager.MountVsRemoteFS(site);
            }
        }
        finally
        {
            UpdateControls();
        }
    }

    private void LogTimer_Tick(object sender, EventArgs e)
    {
        logger ??= fsmanager.GetLogger(site);
        if (logger != null)
        {
            logger.DebugEnabled = ChkDebug.Checked;
            foreach (var logline in logger.GetMessages())
            {
                LogViewer.SelectionColor = logline.LogLevel switch
                {
                    LogLevel.DEBUG => Color.Gray,
                    LogLevel.ERROR => Color.DarkRed,
                    LogLevel.FATAL => Color.Red,
                    LogLevel.WARN => Color.LightGoldenrodYellow,
                    _ => Color.Black,
                };
                if (string.IsNullOrEmpty(TxtGrep.Text) || logline.Log.Contains(TxtGrep.Text))
                {
                    LogViewer.AppendText(logline.Log);
                    LogViewer.AppendText(Environment.NewLine);
                    if (ChkFollowTail.Checked)
                    {
                        LogViewer.SelectionStart = LogViewer.Text.Length;
                        LogViewer.ScrollToCaret();
                    }
                }
            }
        }
    }

    private void ChkDebug_CheckedChanged(object sender, EventArgs e)
    {
        if (logger != null)
            logger.DebugEnabled = ChkDebug.Checked;
    }

    private void TxtClear_Click(object sender, EventArgs e)
        => LogViewer.Clear();

    private void BtnExplore_Click(object sender, EventArgs e)
        => Process.Start("explorer.exe", site.MountPoint);
}
