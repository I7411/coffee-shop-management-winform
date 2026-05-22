using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCaPhe.DTO
{
    public class CategoryDTO
    {
        private int id;
        public int Id { get => id; set => id = value; }
        
        private string name;
        public string Name { get => name; set => name = value; }

        public CategoryDTO(int id, string name) 
        {
            this.Id = id;
            this.Name = name;   
        }

        public CategoryDTO(DataRow dtr)
        {
            this.Id = (int)dtr["id"];
            this.Name = dtr["name"].ToString();
        }


    }
}
