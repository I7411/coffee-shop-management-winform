namespace QuanLyQuanCaPhe
{
    partial class ButtonAddFood
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
            this.cbBtnAddFoodCategory = new System.Windows.Forms.ComboBox();
            this.txbBtnAddFoodName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnBtnAddFood = new System.Windows.Forms.Button();
            this.panel10 = new System.Windows.Forms.Panel();
            this.nmrBtnAddFoodPrice = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmrBtnAddFoodPrice)).BeginInit();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Location = new System.Drawing.Point(567, 273);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(131, 62);
            this.btnExit.TabIndex = 14;
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
            this.label3.Size = new System.Drawing.Size(136, 29);
            this.label3.TabIndex = 0;
            this.label3.Text = "Danh mục:";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.cbBtnAddFoodCategory);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Location = new System.Drawing.Point(18, 97);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(680, 60);
            this.panel3.TabIndex = 10;
            // 
            // cbBtnAddFoodCategory
            // 
            this.cbBtnAddFoodCategory.FormattingEnabled = true;
            this.cbBtnAddFoodCategory.Location = new System.Drawing.Point(178, 18);
            this.cbBtnAddFoodCategory.Name = "cbBtnAddFoodCategory";
            this.cbBtnAddFoodCategory.Size = new System.Drawing.Size(473, 28);
            this.cbBtnAddFoodCategory.TabIndex = 2;
            // 
            // txbBtnAddFoodName
            // 
            this.txbBtnAddFoodName.Location = new System.Drawing.Point(178, 22);
            this.txbBtnAddFoodName.Name = "txbBtnAddFoodName";
            this.txbBtnAddFoodName.Size = new System.Drawing.Size(473, 26);
            this.txbBtnAddFoodName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(20, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 29);
            this.label2.TabIndex = 0;
            this.label2.Text = "Tên món:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txbBtnAddFoodName);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(18, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(680, 60);
            this.panel1.TabIndex = 9;
            // 
            // btnBtnAddFood
            // 
            this.btnBtnAddFood.Location = new System.Drawing.Point(410, 273);
            this.btnBtnAddFood.Name = "btnBtnAddFood";
            this.btnBtnAddFood.Size = new System.Drawing.Size(131, 62);
            this.btnBtnAddFood.TabIndex = 13;
            this.btnBtnAddFood.Text = "Xác nhận thêm";
            this.btnBtnAddFood.UseVisualStyleBackColor = true;
            this.btnBtnAddFood.Click += new System.EventHandler(this.btnBtnAddFood_Click);
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.nmrBtnAddFoodPrice);
            this.panel10.Controls.Add(this.label4);
            this.panel10.Location = new System.Drawing.Point(18, 187);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(680, 60);
            this.panel10.TabIndex = 15;
            // 
            // nmrBtnAddFoodPrice
            // 
            this.nmrBtnAddFoodPrice.Location = new System.Drawing.Point(178, 18);
            this.nmrBtnAddFoodPrice.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.nmrBtnAddFoodPrice.Name = "nmrBtnAddFoodPrice";
            this.nmrBtnAddFoodPrice.Size = new System.Drawing.Size(473, 26);
            this.nmrBtnAddFoodPrice.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(20, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 29);
            this.label4.TabIndex = 0;
            this.label4.Text = "Giá:";
            // 
            // ButtonAddFood
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 366);
            this.Controls.Add(this.panel10);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnBtnAddFood);
            this.Name = "ButtonAddFood";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Trang Thêm Món Ăn";
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmrBtnAddFoodPrice)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txbBtnAddFoodName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnBtnAddFood;
        private System.Windows.Forms.ComboBox cbBtnAddFoodCategory;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.NumericUpDown nmrBtnAddFoodPrice;
        private System.Windows.Forms.Label label4;
    }
}