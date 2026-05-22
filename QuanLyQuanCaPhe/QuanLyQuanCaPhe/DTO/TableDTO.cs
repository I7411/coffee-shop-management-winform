using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCaPhe.DTO
{
    public class TableDTO
    {
        private int iD;
        public int ID { get => iD; set => iD = value; }
       
        private string name;
        public string Name { get => name; set => name = value; }
        
        private string status;
        public string Status { get => status; set => status = value; }

        public TableDTO(int id, string name, string status) {
            this.ID = id;
            this.Name = name;
            this.Status = status;
        }

        public TableDTO(DataRow row)
        {
            this.ID = (int)row["id"];
            this.Name = row["name"].ToString();
            this.Status = row["status"].ToString();
        }
        public static int TableWidth = 100;
        public static int TableHeight = 100;
    }
}
