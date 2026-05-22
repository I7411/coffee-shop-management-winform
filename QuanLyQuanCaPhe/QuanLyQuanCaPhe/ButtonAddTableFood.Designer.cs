namespace QuanLyQuanCaPhe
{
    partial class ButtonAddTableFood
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
            this.txbBtnTableStatus = new System.Windows.Forms.TextBox();
            this.txbBtnTableName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnBtnAddFood = new System.Windows.Forms.Button();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Location = new System.Drawing.Point(609, 242);
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
            this.label3.Location = new System.Drawing.Point(20, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(139, 29);
            this.label3.TabIndex = 0;
            this.label3.Text = "Tình trạng:";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txbBtnTableStatus);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Location = new System.Drawing.Point(60, 149);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(680, 60);
            this.panel3.TabIndex = 17;
            // 
            // txbBtnTableStatus
            // 
            this.txbBtnTableStatus.Location = new System.Drawing.Point(178, 21);
            this.txbBtnTableStatus.Name = "txbBtnTableStatus";
            this.txbBtnTableStatus.Size = new System.Drawing.Size(473, 26);
            this.txbBtnTableStatus.TabIndex = 2;
            // 
            // txbBtnTableName
            // 
            this.txbBtnTableName.Location = new System.Drawing.Point(178, 22);
            this.txbBtnTableName.Name = "txbBtnTableName";
            this.txbBtnTableName.Size = new System.Drawing.Size(473, 26);
            this.txbBtnTableName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(20, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 29);
            this.label2.TabIndex = 0;
            this.label2.Text = "Tên bàn:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txbBtnTableName);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(60, 64);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(680, 60);
            this.panel1.TabIndex = 16;
            // 
            // btnBtnAddFood
            // 
            this.btnBtnAddFood.Location = new System.Drawing.Point(462, 242);
            this.btnBtnAddFood.Name = "btnBtnAddFood";
            this.btnBtnAddFood.Size = new System.Drawing.Size(131, 62);
            this.btnBtnAddFood.TabIndex = 18;
            this.btnBtnAddFood.Text = "Xác nhận thêm";
            this.btnBtnAddFood.UseVisualStyleBackColor = true;
            this.btnBtnAddFood.Click += new System.EventHandler(this.btnBtnAddFood_Click);
            // 
            // ButtonAddTableFood
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 340);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnBtnAddFood);
            this.Name = "ButtonAddTableFood";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Trang Thêm Bàn Ăn";
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txbBtnTableName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnBtnAddFood;
        private System.Windows.Forms.TextBox txbBtnTableStatus;
    }
}