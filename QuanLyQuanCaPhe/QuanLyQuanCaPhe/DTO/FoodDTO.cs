using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCaPhe.DTO
{
    public class FoodDTO
    {
        private int id;
        public int Id { get => id; set => id = value; }
        
        private string name;
        public string Name { get => name; set => name = value; }
        
        private int idCategory;
        public int IdCategory { get => idCategory; set => idCategory = value; }
        
        private float price;
        public float Price { get => price; set => price = value; }

        public FoodDTO(int id, string name, int idCategory, float price) 
        {
            this.Id = id;
            this.Name = name;
            this.IdCategory = idCategory;
            this.Price = price;
        }
        public FoodDTO(DataRow dtr) 
        {
            this.Id = (int)dtr["id"];
            this.Name = dtr["name"].ToString();
            this.IdCategory = (int)dtr["idCategory"];
            this.Price = (float)Convert.ToDouble(dtr["price"].ToString());
        }    
    }
}
