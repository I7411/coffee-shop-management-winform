using QuanLyQuanCaPhe.DAL;
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
    public partial class ButtonAddTableFood : Form
    {
        public ButtonAddTableFood()
        {
            InitializeComponent();
        }

        #region methods
        #endregion

        #region events
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private event EventHandler insertTable;
        public event EventHandler InsertTable
        {
            add { insertTable += value; }
            remove { insertTable -= value; }
        }

        private void btnBtnAddFood_Click(object sender, EventArgs e)
        {
            string name = txbBtnTableName.Text;
            string status = txbBtnTableStatus.Text;

            if(TableDAL.Instance.InsertTable(name, status) == true)
            {
                MessageBox.Show("Thêm bàn thành công!");
                Admin ad = new Admin();
                ad.LoadListFood();
                ad.LoadCategoryAdminForTabAdmin();
                ad.LoadListTable();
                if(insertTable != null)
                    insertTable(this, new EventArgs());
                this.Close();
            }
            else
            {
                MessageBox.Show("Có lỗi khi thực thêm bàn!");
            }
        }
        #endregion
    }
}
