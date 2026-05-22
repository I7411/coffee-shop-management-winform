using QuanLyQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanCaPhe.DAL
{
    public class TableDAL
    {
        private static TableDAL instance;

        public static TableDAL Instance 
        { 
            get {if (instance == null)
                    instance = new TableDAL();
            return TableDAL.instance;
            }
            private set => instance = value; 
        }
        private TableDAL() { }

        public void SwitchTable(int id1, int id2)
        {
            DataProvider.Instance.ExcuteQuery("USP_SwitchTable @idTable1 , @idTable2", new object[] {id1, id2});
        }

        public List<TableDTO> GetTableList()
        {
            List<TableDTO> tableList = new List<TableDTO>();

            DataTable data = DataProvider.Instance.ExcuteQuery("select * from TableFood");

            foreach (DataRow item in data.Rows) 
            {
                TableDTO table = new TableDTO(item);
                tableList.Add(table);
            }

            return tableList;
        }

        public List<TableDTO> GetListTableById(int id)
        {
            List<TableDTO> listtable = new List<TableDTO>();
            string query = ("select * from TableFood where id= " + id);

            DataTable dt = DataProvider.Instance.ExcuteQuery(query);
            foreach (DataRow item in dt.Rows)
            {
                TableDTO table = new TableDTO(item);
                listtable.Add(table);
            }
            return listtable;
        }
       
        public bool InsertTable(string name, string status)
        {
            string query = string.Format("insert into TableFood(name, status) values (N'{0}', N'{1}')", name, status);
            int rs = DataProvider.Instance.ExcuteNonQuery(query);
            return rs > 0;
        }
        public bool UpdateTable(int id, string name, string status)
        {
            string query = string.Format("update TableFood set name = N'{0}', status = N'{1}' where id = {2}", name, status, id);
            int rs = DataProvider.Instance.ExcuteNonQuery(query);
            return rs > 0;
        }
        public bool DeleteTable(int idTable) 
        {
            BillDAL.Instance.DeleteIdTableFromBill(idTable);

            string query=string.Format("delete TableFood where id = {0}", idTable);
            int rs = DataProvider.Instance.ExcuteNonQuery(query);
            return rs > 0;
        }
    }
} 
