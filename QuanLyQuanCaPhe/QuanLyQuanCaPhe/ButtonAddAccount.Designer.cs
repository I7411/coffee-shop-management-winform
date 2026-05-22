namespace QuanLyQuanCaPhe
{
    partial class ButtonAddAccount
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
            this.btnExit = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txbBtnAddUserName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel10 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnBtnAddAccount = new System.Windows.Forms.Button();
            this.txbBtnAddDisplayName = new System.Windows.Forms.TextBox();
            this.nmrBtnAddType = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txbBtnAddPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmrBtnAddType)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Location = new System.Drawing.Point(609, 400);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(131, 62);
            this.btnExit.TabIndex = 19;
            this.btnExit.Text = "Thoát";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(25, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(157, 29);
            this.label3.TabIndex = 0;
            this.label3.Text = "Tên hiển thị:";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txbBtnAddDisplayName);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Location = new System.Drawing.Point(60, 149);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(680, 60);
            this.panel3.TabIndex = 17;
            // 
            // txbBtnAddUserName
            // 
            this.txbBtnAddUserName.Location = new System.Drawing.Point(202, 21);
            this.txbBtnAddUserName.Name = "txbBtnAddUserName";
            this.txbBtnAddUserName.Size = new System.Drawing.Size(449, 26);
            this.txbBtnAddUserName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(20, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(176, 29);
            this.label2.TabIndex = 0;
            this.label2.Text = "Tên tài khoản:";
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.nmrBtnAddType);
            this.panel10.Controls.Add(this.label11);
            this.panel10.Location = new System.Drawing.Point(60, 306);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(680, 60);
            this.panel10.TabIndex = 20;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txbBtnAddUserName);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(60, 64);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(680, 60);
            this.panel1.TabIndex = 16;
            // 
            // btnBtnAddAccount
            // 
            this.btnBtnAddAccount.Location = new System.Drawing.Point(450, 400);
            this.btnBtnAddAccount.Name = "btnBtnAddAccount";
            this.btnBtnAddAccount.Size = new System.Drawing.Size(131, 62);
            this.btnBtnAddAccount.TabIndex = 18;
            this.btnBtnAddAccount.Text = "Xác nhận thêm";
            this.btnBtnAddAccount.UseVisualStyleBackColor = true;
            this.btnBtnAddAccount.Click += new System.EventHandler(this.btnBtnAddAccount_Click);
            // 
            // txbBtnAddDisplayName
            // 
            this.txbBtnAddDisplayName.Location = new System.Drawing.Point(202, 22);
            this.txbBtnAddDisplayName.Name = "txbBtnAddDisplayName";
            this.txbBtnAddDisplayName.Size = new System.Drawing.Size(449, 26);
            this.txbBtnAddDisplayName.TabIndex = 2;
            // 
            // nmrBtnAddType
            // 
            this.nmrBtnAddType.Location = new System.Drawing.Point(214, 20);
            this.nmrBtnAddType.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmrBtnAddType.Name = "nmrBtnAddType";
            this.nmrBtnAddType.Size = new System.Drawing.Size(117, 26);
            this.nmrBtnAddType.TabIndex = 3;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(25, 17);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(183, 29);
            this.label11.TabIndex = 2;
            this.label11.Text = "Loại tài khoản:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txbBtnAddPassword);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(60, 228);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(680, 60);
            this.panel2.TabIndex = 21;
            // 
            // txbBtnAddPassword
            // 
            this.txbBtnAddPassword.Location = new System.Drawing.Point(202, 22);
            this.txbBtnAddPassword.Name = "txbBtnAddPassword";
            this.txbBtnAddPassword.Size = new System.Drawing.Size(449, 26);
            this.txbBtnAddPassword.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(25, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mật khẩu:";
            // 
            // ButtonAddAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(812, 489);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel10);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnBtnAddAccount);
            this.Name = "ButtonAddAccount";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Trang Thêm Tài Khoản";
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmrBtnAddType)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txbBtnAddUserName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnBtnAddAccount;
        private System.Windows.Forms.TextBox txbBtnAddDisplayName;
        private System.Windows.Forms.NumericUpDown nmrBtnAddType;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txbBtnAddPassword;
        private System.Windows.Forms.Label label1;
    }
}