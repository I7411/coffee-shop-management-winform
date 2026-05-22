namespace QuanLyQuanCaPhe
{
    partial class ButtonAddCategoryFood
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
            this.btnExitCategoryFood = new System.Windows.Forms.Button();
            this.txbBtnAddFoodCategoryName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnBtnAddCategoryFood = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExitCategoryFood
            // 
            this.btnExitCategoryFood.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExitCategoryFood.Location = new System.Drawing.Point(580, 134);
            this.btnExitCategoryFood.Name = "btnExitCategoryFood";
            this.btnExitCategoryFood.Size = new System.Drawing.Size(131, 62);
            this.btnExitCategoryFood.TabIndex = 19;
            this.btnExitCategoryFood.Text = "Thoát";
            this.btnExitCategoryFood.UseVisualStyleBackColor = true;
            this.btnExitCategoryFood.Click += new System.EventHandler(this.btnExitCategoryFood_Click);
            // 
            // txbBtnAddFoodCategoryName
            // 
            this.txbBtnAddFoodCategoryName.Location = new System.Drawing.Point(196, 22);
            this.txbBtnAddFoodCategoryName.Name = "txbBtnAddFoodCategoryName";
            this.txbBtnAddFoodCategoryName.Size = new System.Drawing.Size(455, 26);
            this.txbBtnAddFoodCategoryName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(20, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(170, 29);
            this.label2.TabIndex = 0;
            this.label2.Text = "Tên loại món:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txbBtnAddFoodCategoryName);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(31, 44);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(680, 60);
            this.panel1.TabIndex = 16;
            // 
            // btnBtnAddCategoryFood
            // 
            this.btnBtnAddCategoryFood.Location = new System.Drawing.Point(438, 134);
            this.btnBtnAddCategoryFood.Name = "btnBtnAddCategoryFood";
            this.btnBtnAddCategoryFood.Size = new System.Drawing.Size(131, 62);
            this.btnBtnAddCategoryFood.TabIndex = 18;
            this.btnBtnAddCategoryFood.Text = "Xác nhận thêm";
            this.btnBtnAddCategoryFood.UseVisualStyleBackColor = true;
            this.btnBtnAddCategoryFood.Click += new System.EventHandler(this.btnBtnAddCategoryFood_Click);
            // 
            // ButtonAddCategoryFood
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 224);
            this.Controls.Add(this.btnExitCategoryFood);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnBtnAddCategoryFood);
            this.Name = "ButtonAddCategoryFood";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Trang Thêm Thể Loại Món Ăn";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExitCategoryFood;
        private System.Windows.Forms.TextBox txbBtnAddFoodCategoryName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnBtnAddCategoryFood;
    }
}