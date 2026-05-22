using QuanLyQuanCaPhe.DAL;
using QuanLyQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanCaPhe
{
    public partial class ButtonAddCategoryFood : Form
    {
        public ButtonAddCategoryFood()
        {
            InitializeComponent();
        }
        #region methods
      
        #endregion


        #region events
        private void btnExitCategoryFood_Click(object sender, EventArgs e)
        {
            this.Close();
        }
       
        private event EventHandler insertFoodCategory;
        public event EventHandler InsertFoodCategory
        {
            add { insertFoodCategory += value; }
            remove { insertFoodCategory -= value; }
        }
        private void btnBtnAddCategoryFood_Click(object sender, EventArgs e)
        {
            string name = txbBtnAddFoodCategoryName.Text;
            
            if (CategoryDAL.Instance.InsertCategory(name) == true)
            {
                MessageBox.Show("Thêm loại món thành công!");
                Admin ad = new Admin();
                ad.LoadListFood();  //Load lại danh sách thức ăn               
                ad.LoadCategoryAdminForTabAdmin(); //Loại lại danh sách danh mục
                if (insertFoodCategory != null)
                    insertFoodCategory(this, new EventArgs());

                this.Close();
            }
            else
            {
                MessageBox.Show("Có lỗi khi thực hiện thêm loại món!");
            }
        }
        #endregion


    }
}
