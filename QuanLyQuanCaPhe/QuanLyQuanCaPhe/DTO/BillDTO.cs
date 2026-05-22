using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCaPhe.DTO
{
    public class BillDTO
    {
        private int id;

        public int Id { get => id; set => id = value; }
       

        private DateTime? dateCheckIn; // DateTime là kiểu dữ liệu kh cho phép null; nếu muốn cho null dc thì phải thêm "?" ở cuối chữ

        public DateTime? DateCheckIn { get => dateCheckIn; set => dateCheckIn = value; }

        private DateTime? dateCheckOut;

        public DateTime? DateCheckOut { get => dateCheckOut; set => dateCheckOut = value; }
        
        private int status;
        public int Status { get => status; set => status = value; }

        private int discount;
        public int Discount { get => discount; set => discount = value; }

        public BillDTO(int id, DateTime? dateCheckIn, DateTime? dateCheckOut, int status, int discount = 0) 
        {
            this.Id = id;
            this.DateCheckIn = dateCheckIn;
            this.DateCheckOut = dateCheckOut;
            this.Status = status;
            this.Discount = discount;
        }

        public BillDTO(DataRow dtr)
        {
            this.Id = (int)dtr["id"];
            this.DateCheckIn = (DateTime?)dtr["dateCheckIn"];
            var checkOutTmp = dtr["dateCheckOut"];
            if (checkOutTmp.ToString() != "") 
            {
                this.DateCheckOut = (DateTime?)checkOutTmp;
            }
            
            this.Status = (int)dtr["status"];
            if(dtr["discount"].ToString() != "")
                this.Discount = (int)dtr["discount"];

        }
    }
}
