namespace WinVsRemoteClient
{
    partial class SiteEditForm
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(SiteEditForm));
            LblSiteName = new Label();
            TxtName = new TextBox();
            LblAddress = new Label();
            TxtAddress = new TextBox();
            LblUsername = new Label();
            TxtUsername = new TextBox();
            ChkEnableAuth = new CheckBox();
            TxtPassword = new TextBox();
            LblPassword = new Label();
            BtnTestConnection = new Button();
            BtnSave = new Button();
            BtnCancel = new Button();
            LblTestResult = new Label();
            LblUnit = new Label();
            CmbUnitLetter = new ComboBox();
            SuspendLayout();
            // 
            // LblSiteName
            // 
            LblSiteName.AutoSize = true;
            LblSiteName.Location = new Point(12, 9);
            LblSiteName.Name = "LblSiteName";
            LblSiteName.Size = new Size(59, 25);
            LblSiteName.TabIndex = 0;
            LblSiteName.Text = "Name";
            // 
            // TxtName
            // 
            TxtName.Location = new Point(12, 37);
            TxtName.Name = "TxtName";
            TxtName.Size = new Size(317, 31);
            TxtName.TabIndex = 1;
            TxtName.Text = "New Site";
            // 
            // LblAddress
            // 
            LblAddress.AutoSize = true;
            LblAddress.Location = new Point(12, 71);
            LblAddress.Name = "LblAddress";
            LblAddress.Size = new Size(77, 25);
            LblAddress.TabIndex = 2;
            LblAddress.Text = "Address";
            // 
            // TxtAddress
            // 
            TxtAddress.Location = new Point(12, 99);
            TxtAddress.Name = "TxtAddress";
            TxtAddress.Size = new Size(317, 31);
            TxtAddress.TabIndex = 3;
            TxtAddress.Text = "http://127.0.0.1:8001";
            // 
            // LblUsername
            // 
            LblUsername.AutoSize = true;
            LblUsername.Location = new Point(11, 232);
            LblUsername.Name = "LblUsername";
            LblUsername.Size = new Size(91, 25);
            LblUsername.TabIndex = 4;
            LblUsername.Text = "Username";
            // 
            // TxtUsername
            // 
            TxtUsername.Location = new Point(12, 260);
            TxtUsername.Name = "TxtUsername";
            TxtUsername.ReadOnly = true;
            TxtUsername.Size = new Size(316, 31);
            TxtUsername.TabIndex = 5;
            // 
            // ChkEnableAuth
            // 
            ChkEnableAuth.AutoSize = true;
            ChkEnableAuth.Location = new Point(12, 200);
            ChkEnableAuth.Name = "ChkEnableAuth";
            ChkEnableAuth.Size = new Size(148, 29);
            ChkEnableAuth.TabIndex = 6;
            ChkEnableAuth.Text = "Authenticated";
            ChkEnableAuth.UseVisualStyleBackColor = true;
            ChkEnableAuth.CheckedChanged += ChkEnableAuth_CheckedChanged;
            // 
            // TxtPassword
            // 
            TxtPassword.Location = new Point(12, 322);
            TxtPassword.Name = "TxtPassword";
            TxtPassword.PasswordChar = '*';
            TxtPassword.ReadOnly = true;
            TxtPassword.Size = new Size(316, 31);
            TxtPassword.TabIndex = 7;
            // 
            // LblPassword
            // 
            LblPassword.AutoSize = true;
            LblPassword.Location = new Point(12, 294);
            LblPassword.Name = "LblPassword";
            LblPassword.Size = new Size(87, 25);
            LblPassword.TabIndex = 8;
            LblPassword.Text = "Password";
            // 
            // BtnTestConnection
            // 
            BtnTestConnection.Location = new Point(12, 359);
            BtnTestConnection.Name = "BtnTestConnection";
            BtnTestConnection.Size = new Size(148, 34);
            BtnTestConnection.TabIndex = 9;
            BtnTestConnection.Text = "Test Connection";
            BtnTestConnection.UseVisualStyleBackColor = true;
            // 
            // BtnSave
            // 
            BtnSave.Location = new Point(217, 417);
            BtnSave.Name = "BtnSave";
            BtnSave.Size = new Size(112, 34);
            BtnSave.TabIndex = 10;
            BtnSave.Text = "Save";
            BtnSave.UseVisualStyleBackColor = true;
            BtnSave.Click += BtnSave_Click;
            // 
            // BtnCancel
            // 
            BtnCancel.Location = new Point(99, 417);
            BtnCancel.Name = "BtnCancel";
            BtnCancel.Size = new Size(112, 34);
            BtnCancel.TabIndex = 11;
            BtnCancel.Text = "Cancel";
            BtnCancel.UseVisualStyleBackColor = true;
            // 
            // LblTestResult
            // 
            LblTestResult.AutoSize = true;
            LblTestResult.Location = new Point(166, 364);
            LblTestResult.Name = "LblTestResult";
            LblTestResult.Size = new Size(145, 25);
            LblTestResult.TabIndex = 12;
            LblTestResult.Text = "-------------------";
            // 
            // LblUnit
            // 
            LblUnit.AutoSize = true;
            LblUnit.Location = new Point(12, 133);
            LblUnit.Name = "LblUnit";
            LblUnit.Size = new Size(89, 25);
            LblUnit.TabIndex = 13;
            LblUnit.Text = "Unit letter";
            // 
            // CmbUnitLetter
            // 
            CmbUnitLetter.DropDownStyle = ComboBoxStyle.DropDownList;
            CmbUnitLetter.FormattingEnabled = true;
            CmbUnitLetter.Location = new Point(12, 161);
            CmbUnitLetter.Name = "CmbUnitLetter";
            CmbUnitLetter.Size = new Size(90, 33);
            CmbUnitLetter.TabIndex = 14;
            // 
            // SiteEditForm
            // 
            AcceptButton = BtnSave;
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = BtnCancel;
            ClientSize = new Size(347, 534);
            Controls.Add(CmbUnitLetter);
            Controls.Add(LblUnit);
            Controls.Add(LblTestResult);
            Controls.Add(BtnCancel);
            Controls.Add(BtnSave);
            Controls.Add(BtnTestConnection);
            Controls.Add(LblPassword);
            Controls.Add(TxtPassword);
            Controls.Add(ChkEnableAuth);
            Controls.Add(TxtUsername);
            Controls.Add(LblUsername);
            Controls.Add(TxtAddress);
            Controls.Add(LblAddress);
            Controls.Add(TxtName);
            Controls.Add(LblSiteName);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SiteEditForm";
            ShowInTaskbar = false;
            Text = "Edit Site";
            Shown += SiteEditForm_Shown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label LblSiteName;
        private TextBox TxtName;
        private Label LblAddress;
        private TextBox TxtAddress;
        private Label LblUsername;
        private TextBox TxtUsername;
        private CheckBox ChkEnableAuth;
        private TextBox TxtPassword;
        private Label LblPassword;
        private Button BtnTestConnection;
        private Button BtnSave;
        private Button BtnCancel;
        private Label LblTestResult;
        private Label LblUnit;
        private ComboBox CmbUnitLetter;
    }
}