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

internal partial class SiteEditForm : Form
{
    public ConfigSite? ConfigSite { get; private set; } = null;

    public SiteEditForm()
    {
        InitializeComponent();
        CmbUnitLetter.SelectedIndex = 0;
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        var auth = ChkEnableAuth.Checked;
        var res = DialogResult.OK;
        try
        {
            Uri uri = new(TxtAddress.Text);
            if (uri.Scheme != "http" && uri.Scheme != "https")
            {
                MessageBox.Show($"Invalid schema: {uri.Scheme}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                res = DialogResult.Cancel;
            }
        }
        catch (Exception)
        {
            MessageBox.Show($"Invalid address: {TxtAddress.Text}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            res = DialogResult.Cancel;
        }

        if (string.IsNullOrEmpty(TxtName.Text))
        {
            MessageBox.Show($"Site name cannot be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            res = DialogResult.Cancel;
        }
        if (auth && string.IsNullOrEmpty(TxtUsername.Text))
        {
            MessageBox.Show($"Username cannot be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            res = DialogResult.Cancel;
        }

        if (string.IsNullOrEmpty(LblMountPoint.Text))
        {
            MessageBox.Show($"Invalid or unset mount point", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            res = DialogResult.Cancel;
        }

        if (res == DialogResult.OK)
        {
            ConfigSite = new()
            {
                Label = TxtName.Text,
                Address = TxtAddress.Text,
                Username = auth ? TxtUsername.Text : null,
                Password = auth ? TxtPassword.Text : null
            };
        }
        else
        {
            ConfigSite = null;
        }
        DialogResult = res;
        Close();
    }

    private void ChkEnableAuth_CheckedChanged(object sender, EventArgs e)
        => TxtUsername.ReadOnly = TxtPassword.ReadOnly = !ChkEnableAuth.Checked;

    private void SiteEditForm_Shown(object sender, EventArgs e)
    {
        CmbUnitLetter.SuspendLayout();
        CmbUnitLetter.Items.Clear();
        for (var c = 'F'; c <= 'Z'; c++)
        {
            CmbUnitLetter.Items.Add($"{c}:\\".ToString());
        }
        CmbUnitLetter.ResumeLayout();
    }

    private void UpdateConfigSiteLabel()
        => LblMountPoint.Text = ConfigSite.MountPoint;

    private void CmbUnitLetter_SelectedIndexChanged(object sender, EventArgs e)
        => CmbUnitLetter_SelectedIndexChanged();

    private void CmbUnitLetter_SelectedIndexChanged()
    {
        string? selected = CmbUnitLetter.SelectedItem?.ToString();
        if (selected != null)
            ConfigSite.MountPoint = selected;
        else
            ConfigSite.MountPoint = string.Empty;
        UpdateConfigSiteLabel();
    }

    private void RbMountPoint_CheckedChanged(object sender, EventArgs e)
    {
        if (RbMountPointUnit.Checked)
            CmbUnitLetter_SelectedIndexChanged();
        if (RbMountPointFolder.Checked)
            ConfigSite.MountPoint = string.Empty;
        UpdateConfigSiteLabel();
    }

    [STAThread]
    private void BtnMountPointBrowse_Click(object sender, EventArgs e)
    {
        using var folderDialog = new FolderBrowserDialog();
        folderDialog.Description = "Select a folder";
        folderDialog.ShowNewFolderButton = true;

        if (folderDialog.ShowDialog() == DialogResult.OK)
        {
            string selectedPath = folderDialog.SelectedPath;
            ConfigSite.MountPoint = selectedPath;
            UpdateConfigSiteLabel();
        }
    }
}
