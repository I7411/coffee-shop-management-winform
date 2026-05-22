using QuanLyQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace QuanLyQuanCaPhe.DAL
{
    public class CategoryDAL
    {
        private static CategoryDAL instance;
        public static CategoryDAL Instance 
        {
            get {
                if(instance == null)    
                    instance = new CategoryDAL();
                return CategoryDAL.instance; } 
            private set => CategoryDAL.instance = value; 
        }

        private CategoryDAL() { }
        public List<CategoryDTO> GetListCategory()
        {
            List<CategoryDTO> lstCategory = new List<CategoryDTO>();
            string query = "select * from FoodCategory";

            DataTable dt = DataProvider.Instance.ExcuteQuery(query);
            foreach (DataRow item in dt.Rows) 
            {
                CategoryDTO category = new CategoryDTO(item);
                lstCategory.Add(category);
            }

            return lstCategory;
        }

        public CategoryDTO GetCategoryById(int id)
        {
            CategoryDTO cate = null;
            string query = ("select * from FoodCategory where id = " + id);

            DataTable dt = DataProvider.Instance.ExcuteQuery(query);
            foreach (DataRow item in dt.Rows) 
            {
                cate = new CategoryDTO(item);
                return cate;
            }
            return cate;
        }

        public bool InsertCategory(string name) 
        {
            string query = string.Format("insert into FoodCategory(name) values (N'{0}')", name);
            int rs = DataProvider.Instance.ExcuteNonQuery(query);
            
            return rs > 0;
        }
        public bool UpdateCategory(int id, string name)
        {
            string query = string.Format("update FoodCategory set name = N'{0}' where id = {1}", name, id);
            int rs = DataProvider.Instance.ExcuteNonQuery(query);

            return rs > 0;
        }
        public bool DeleteCategory(int id) 
        {
            FoodDAL.Instance.DeleteFoodByIdCategory(id);

            string query = string.Format("delete FoodCategory where id= {0}", id);
            int rs = DataProvider.Instance.ExcuteNonQuery(query);

            return rs > 0;
        }

    }
}
