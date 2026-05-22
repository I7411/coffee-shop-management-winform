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
    public partial class ButtonAddFood : Form
    {
        public ButtonAddFood()
        {
            InitializeComponent();
            LoadCategoryIntoComboBoxCategoryBtnAddFood(cbBtnAddFoodCategory);
        }

        #region methods


        void LoadCategoryIntoComboBoxCategoryBtnAddFood(ComboBox cb)
        {
            cb.DataSource = CategoryDAL.Instance.GetListCategory();
            cbBtnAddFoodCategory.DisplayMember = "Name";
        }
        #endregion

        #region events

        private event EventHandler insertFood;
        public event EventHandler InsertFood
        {
            add { insertFood += value; }
            remove { insertFood -= value; }
        }
        private void btnBtnAddFood_Click(object sender, EventArgs e)
        {
            string name = txbBtnAddFoodName.Text;
            int idCategory = (cbBtnAddFoodCategory.SelectedItem as CategoryDTO).Id;
            float price = (float)nmrBtnAddFoodPrice.Value;
            if (FoodDAL.Instance.InsertFood(name, idCategory, price) == true)
            {
                MessageBox.Show("Thêm món thành công!");
                Admin ad = new Admin();
                ad.LoadListFood(); //Load lại danh sách thức ăn 
                ad.LoadCategoryAdminForTabAdmin(); //Loại lại danh sách danh mục
                if (insertFood != null)
                    insertFood(this, new EventArgs());

                this.Close();
            }
            else
            {
                MessageBox.Show("Có lỗi khi thực hiện thêm món!");
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion


    }
}
