using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCaPhe.DTO
{
    public class BillInfoDTO
    {
        private int id;
        public int Id { get => id; set => id = value; }

        private int idBill;
        public int IdBill { get => idBill; set => idBill = value; }
        
        private int idFood;
        public int IdFood { get => idFood; set => idFood = value; }

        private int count; //đếm số lượng món ăn
        public int Count { get => count; set => count = value; }
        
        public BillInfoDTO(int id, int idBill, int idFood, int count) 
        {
            this.Id = id;
            this.IdBill = idBill;
            this.IdFood = idFood;
            this.Count = count;
        }
        public BillInfoDTO(DataRow dtr)
        {
            this.Id = (int)dtr["id"];
            this.IdBill = (int)dtr["idBill"];
            this.IdFood = (int)dtr["idFood"]; 
            this.Count = (int)dtr["count"];
        }
    }
}
