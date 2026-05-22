using QuanLyQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace QuanLyQuanCaPhe.DAL
{
    public class FoodDAL
    {
        private static FoodDAL instance;
        public static FoodDAL Instance
        {
            get
            {
                if (instance == null)
                    instance = new FoodDAL();
                return FoodDAL.instance;
            }
            private set => instance = value;
        }

        private FoodDAL() { }

        public List<FoodDTO> GetListFoodByCategoryID(int id)
        {
            List<FoodDTO> lstFood = new List<FoodDTO>();
            string query = "select * from Food where idCategory = " + id;
            DataTable dt = DataProvider.Instance.ExcuteQuery(query);

            foreach (DataRow item in dt.Rows)
            {
                FoodDTO food = new FoodDTO(item);
                lstFood.Add(food);
            }

            return lstFood;
        }

        public List<FoodDTO> GetListFood()
        {
            List<FoodDTO> lstFood = new List<FoodDTO>();
            //string query = "select id, name , idCategory , price  from Food";
            DataTable dt = DataProvider.Instance.ExcuteQuery("select * from Food");


            foreach (DataRow item in dt.Rows)
            {
                FoodDTO food = new FoodDTO(item);
                lstFood.Add(food);
            }

            return lstFood;
        }

        public bool InsertFood(string name, int id, float price) //hàm thêm thức ăn trong phần admin
        {
            string query = "insert into Food(name, idCategory, price) values (N'" + name + "'" + ", " + id + ", " + price + ")";
            int rs = DataProvider.Instance.ExcuteNonQuery(query);

            return rs > 0;
        }
        public bool UpdateFood(int idFood, string name, int id, float price) //hàm thêm thức ăn trong phần admin
        {
            string query = string.Format("update Food SET name = N'{0}', idCategory = {1}, price = {2} where id = {3}", name, id, price, idFood);
            int rs = DataProvider.Instance.ExcuteNonQuery(query);

            return rs > 0;
        }

        public bool DeleteFood(int idFood) //hàm xóa thức ăn trong phần admin
        {
            BillInfoDAL.Instance.DeleteBillInfoByIdFood(idFood);

            string query = string.Format("delete Food where id = {0}", idFood);
            int rs = DataProvider.Instance.ExcuteNonQuery(query);
            return rs > 0;

        }

        public void DeleteFoodByIdCategory(int idCategory) //hàm xóa food thuộc category phần admin trong Danh mục
        {
            DataProvider.Instance.ExcuteQuery("delete Food where idCategory = " + idCategory);
        }

        public List<FoodDTO> SearchFoodByName(string name) 
        {
            List<FoodDTO> lstFood = new List<FoodDTO>();
            string query = string.Format("select * from Food where name like N'%{0}%'", name);
            DataTable data = DataProvider.Instance.ExcuteQuery(query);

            foreach (DataRow item in data.Rows) 
            {
                FoodDTO food = new FoodDTO(item);
                lstFood.Add(food);
            }

            return lstFood;
        }
    }
}
