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
    public partial class AccountProfile : Form
    {
        private AccountDTO loginAccount; 

        public AccountDTO LoginAccount
        {
            get { return loginAccount; }
            set
            {
                loginAccount = value;
                ControlAccount(LoginAccount);    
            }
        }
        public AccountProfile(AccountDTO account)
        {
            InitializeComponent();

            this.LoginAccount = account;
        }

        void ControlAccount(AccountDTO account) //Xử lý phần hiện thông tin trong Thông tin cá nhân 
        {
            txtBoxUserName.Text = LoginAccount.UserName;
            txbDisplayName.Text = LoginAccount.DisplayName;
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void UpdateAccountInfo() //Cập nhập phần Thông tin cá nhân
        {
            string userName = txtBoxUserName.Text;
            string password = txbPassword.Text;
            string newpass = txbNewPass.Text;
            string repassword = txbReEnterPass.Text;
            string displayName = txbDisplayName.Text;

            if (String.Compare(newpass, repassword) == 1)
            {
                MessageBox.Show("Mật khẩu nhập lại không khớp với mật khẩu mới!");
            }
            else
            {
                if (AccountDAL.Instance.UpdateAccount(userName, displayName, password, newpass))
                {
                    MessageBox.Show("Cập nhập thành công!");
                    if (updateAccount != null) 
                    {
                        updateAccount(this, new AccountEvent(AccountDAL.Instance.GetAccountByUserName(userName)));
                    }
                }
                else 
                {
                    MessageBox.Show("Vui lòng điền đúng mật khẩu");
                }
            }
        }
        private event EventHandler<AccountEvent> updateAccount;
        public event EventHandler<AccountEvent> UpdateAccount
        {
            add { updateAccount += value; }
            remove { updateAccount -= value; }
        }
        private void btnUpdate_Click(object sender, EventArgs e) //Xử lí button của cập nhập Ttin cá nhân
        {
            UpdateAccountInfo();
        }

    }

    public class AccountEvent : EventArgs //Tạo 1 event hiển thị thông tin tài khoản
    {
        private AccountDTO acc;
        public AccountDTO Acc { get => acc; set => acc = value; }

        public AccountEvent(AccountDTO acc)
        {
            this.Acc= acc;
        }
    }

}
