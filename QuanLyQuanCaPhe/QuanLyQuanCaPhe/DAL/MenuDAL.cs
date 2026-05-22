using QuanLyQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCaPhe.DAL
{
    public class MenuDAL
    {
        private static MenuDAL instance;
        public static MenuDAL Instance 
        {
            get {
                if (instance == null)
                    instance = new MenuDAL();
                return MenuDAL.instance; }
            private set => instance = value; 
        }

        private MenuDAL() { }
        public List<MenuDTO> GetListMenuByTable(int id)
        {
            List<MenuDTO > listMenu = new List<MenuDTO>();

            string query = "select Food.name, BillInfo.count, Food.price, (Food.price*BillInfo.count) as [totalPrice] from Bill, BillInfo, Food\r\nwhere BillInfo.idBill = Bill.id and Food.id = BillInfo.idFood and Bill.status = 0 and Bill.idTable = " + id;
            DataTable dt = DataProvider.Instance.ExcuteQuery(query);

            foreach (DataRow item in dt.Rows) 
            {
                MenuDTO menu = new MenuDTO(item);
                listMenu.Add(menu);
            }
            return listMenu;
        }
    
    }
}
