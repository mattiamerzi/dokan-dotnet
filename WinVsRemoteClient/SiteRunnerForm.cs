using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinVsRemoteClient;

internal partial class SiteRunnerForm : Form
{
    private Size TcSize;
    private Point BtnLocation;
    private readonly VsRemoteFSManager fsmanager;
    private readonly ConfigSite site;
    private DynaLogger? logger;

    public SiteRunnerForm(VsRemoteFSManager fsmanager, ConfigSite site)
    {
        InitializeComponent();
        this.fsmanager = fsmanager;
        this.site = site;
        TcSize = MainTabControl.Size;
        BtnLocation = BtnMountUnmount.Location;
        UpdateControls();
    }

    private void SiteRunnerForm_Resize(object sender, EventArgs e)
    {
        MainTabControl.Size = new(TcSize.Width + (Size.Width - 500), TcSize.Height + (Size.Height - 500));
        BtnMountUnmount.Location = new(BtnLocation.X + (Size.Width - 500), BtnLocation.Y + (Size.Height - 500));
    }

    private bool IsMounted()
        => fsmanager.IsMounted(site);

    private void UpdateControls()
    {
        bool mounted = IsMounted();
        BtnMountUnmount.Text = mounted ? "Unmount" : "Mount";
        BtnExplore.Enabled = mounted;
        LogTimer.Enabled = mounted;
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
            foreach (var logline in logger.GetMessages())
            {
                LogViewer.SelectionColor = logline.LogLevel switch
                {
                    LogLevel.ERROR => Color.DarkRed,
                    LogLevel.FATAL => Color.Red,
                    LogLevel.WARN => Color.LightGoldenrodYellow,
                    _ => Color.Black,
                };
                LogViewer.AppendText(logline.Log);
            }
        }
    }
}
