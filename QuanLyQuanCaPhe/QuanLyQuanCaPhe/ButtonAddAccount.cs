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
    public partial class ButtonAddAccount : Form
    {
        public ButtonAddAccount()
        {
            InitializeComponent();
        }

        #region methods
        #endregion

        #region events

        private void btnBtnAddAccount_Click(object sender, EventArgs e)
        {
            string name = txbBtnAddUserName.Text;
            string displayName = txbBtnAddDisplayName.Text;
            string password = txbBtnAddPassword.Text; 
            int type = (int)nmrBtnAddType.Value;

            Admin ad = new Admin();
            ad.AddAccount(name, displayName, password, type);
            ad.LoadListAccount();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
