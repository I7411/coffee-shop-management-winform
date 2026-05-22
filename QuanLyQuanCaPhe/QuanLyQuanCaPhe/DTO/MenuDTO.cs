using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace QuanLyQuanCaPhe.DTO
{
    public class MenuDTO
    {
        private string foodName;
        public string FoodName { get => foodName; set => foodName = value; }
        
        private int count;
        public int Count { get => count; set => count = value; }

        private float price;
        public float Price { get => price; set => price = value; }
        
        private float totalPrice;
        public float TotalPrice { get => totalPrice; set => totalPrice = value; }

        public MenuDTO(string foodName, int count, float price, float totalPrice =0) 
        {
            this.FoodName = foodName;
            this.Count = count;
            this.Price = price;
            this.TotalPrice = totalPrice;
        }
        public MenuDTO(DataRow dtr)
        {
            this.FoodName = dtr["name"].ToString();
            this.Count = (int)dtr["count"];
            this.Price = (float)Convert.ToDouble(dtr["price"].ToString());
            this.TotalPrice = (float)Convert.ToDouble(dtr["totalPrice"].ToString());


        }
    }
}
