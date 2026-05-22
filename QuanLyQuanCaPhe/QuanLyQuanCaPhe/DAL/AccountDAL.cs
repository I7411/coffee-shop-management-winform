using QuanLyQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCaPhe.DAL
{
    public class AccountDAL
    {
        public static AccountDAL instance;
     
        public static  AccountDAL Instance
        {
            get { if(instance == null) 
                    instance = new AccountDAL();
                return AccountDAL.instance; } 
            private set => instance = value;
        }
        private AccountDAL() { }

        public bool CheckLogin(string username, string password) {
            string query = "USP_Login @userName , @passWord";

            DataTable result = DataProvider.Instance.ExcuteQuery(query, new object[] {username, password});
            return result.Rows.Count > 0;
        }

        public bool UpdateAccount(string userName, string displayName, string password, string newPass)
        {
            int count = DataProvider.Instance.ExcuteNonQuery("exec  USP_UpdateAccount @userName , @displayName , @passWord , @newPassWord", new object[] { userName, displayName, password, newPass });
       
            return count > 0;
        }

        public AccountDTO GetAccountByUserName(string userName)
        {
            DataTable dt = DataProvider.Instance.ExcuteQuery("select * from Account where UserName = '" + userName + "'");
            foreach (DataRow item in dt.Rows) 
            {
                return new AccountDTO(item); //Trường hợp nếu có username sẽ return lại account
            }
            return null; //không có trả về null
        }
        

        //Chức năng của Account
        public DataTable GetListAccount()
        {
            return DataProvider.Instance.ExcuteQuery("select UserName, DisplayName, Type from Account");
        }

        public bool InsertAccountForUser(string name, string displayName, string password, int type)
        {
            string query = string.Format("insert into Account(UserName, DisplayName, Password, Type) values(N'{0}', N'{1}', N'{2}', {3})", name, displayName, password,type);
            int rs = DataProvider.Instance.ExcuteNonQuery(query);

            return rs > 0;
        }
        public bool UpdateAccountForUser(string name, string displayName, int type) //hàm thêm tài khoản trong phần admin
        {
            string query = string.Format("update Account set DisplayName = N'{0}', Type = {1} where UserName = N'{2}'", displayName, type, name);
            int rs = DataProvider.Instance.ExcuteNonQuery(query);

            return rs > 0;
        }

        public bool DeleteAccountForUser(string name) //hàm xóa tài khoản trong phần admin
        {
      
            string query = string.Format("delete from Account where UserName = N'{0}'", name);
            int rs = DataProvider.Instance.ExcuteNonQuery(query);
            return rs > 0;

        }
        public bool ResetPassword(string name)
        {
            string query = string.Format("update Account set Password = N'0' where UserName = N'{0}'", name);
            int rs = DataProvider.Instance.ExcuteNonQuery(query);
            return rs > 0;
        }
    }
}
