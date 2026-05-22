using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCaPhe.DTO
{
    public class AccountDTO
    {
        private int id;
        public int Id { get => id; set => id = value; }
        private string userName;
        public string UserName { get => userName; set => userName = value; }
        
        private string displayName;
        public string DisplayName { get => displayName; set => displayName = value; }
        
        private string password;
        public string Password { get => password; set => password = value; }
        
        private int type; // 1: là admin || 0: là nhân viên
        public int Type { get => type; set => type = value; }
     

        public AccountDTO(int id, string userName, string displayName, int type, string password = null)
        {
            this.Id = id;
            this.UserName = userName;   
            this.DisplayName = displayName;
            this.Type = type;
            this.Password = password;
          
        }
        public AccountDTO(DataRow dtr)
        {
            this.Id = (int)dtr["id"];
            this.UserName = dtr["userName"].ToString();
            this.DisplayName = dtr["displayName"].ToString();
            this.Type = (int)dtr["type"];
            this.Password = dtr["password"].ToString();
           
        }

    }
}
