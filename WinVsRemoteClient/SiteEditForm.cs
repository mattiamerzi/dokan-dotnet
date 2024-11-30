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
    public ConfigSite ConfigSite { get; private set; }

    public SiteEditForm()
    {
        InitializeComponent();
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            Uri uri = new(TxtAddress.Text);
            if (uri.Scheme != "http" && uri.Scheme != "https")
                MessageBox.Show($"Invalid schema: {uri.Scheme}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Invalid address: {TxtAddress.Text}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        if (string.IsNullOrEmpty(TxtName.Text))
            MessageBox.Show($"Site name cannot be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        bool auth = ChkEnableAuth.Checked;

        if (auth && string.IsNullOrEmpty(TxtUsername.Text))
            MessageBox.Show($"Username cannot be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        ConfigSite = new()
        {
            Label = TxtName.Text,
            Address = TxtAddress.Text,
            Username = auth ? TxtUsername.Text : null,
            Password = auth ? TxtPassword.Text : null
        };
        DialogResult = DialogResult.OK;
        Close();
    }

    private void ChkEnableAuth_CheckedChanged(object sender, EventArgs e)
        => TxtUsername.ReadOnly = TxtPassword.ReadOnly = !ChkEnableAuth.Checked;
}
