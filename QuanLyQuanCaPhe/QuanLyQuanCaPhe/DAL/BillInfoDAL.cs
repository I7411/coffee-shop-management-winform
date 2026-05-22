using QuanLyQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCaPhe.DAL
{
    public class BillInfoDAL
    {
        private static BillInfoDAL instance;

        public static BillInfoDAL Instance
        {
            get
            {
                if (instance == null)
                    instance = new BillInfoDAL();
                return BillInfoDAL.instance;
            }
            private set => instance = value;
        }

        
        private BillInfoDAL() { }

        public List<BillInfoDTO> GetlistBillInfo(int id)
        {
            List<BillInfoDTO> listBillInfo = new List<BillInfoDTO>();

            DataTable dt = DataProvider.Instance.ExcuteQuery("select * from BillInfo where idBill = " + id);

            foreach (DataRow item in dt.Rows) 
            {
                BillInfoDTO info = new BillInfoDTO(item);
                listBillInfo.Add(info);
            }
            
            return listBillInfo;
        }
        public void InsertBillInfo(int idBill, int idFood, int count)
        { 
            DataProvider.Instance.ExcuteNonQuery("USP_InsertBillInfo @idBill , @idFood , @count", new object[] { idBill, idFood, count });
        }

        public void DeleteBillInfoByIdFood(int id)
        {
            DataProvider.Instance.ExcuteQuery("delete BillInfo where idFood = " + id);
        }
    }
}
