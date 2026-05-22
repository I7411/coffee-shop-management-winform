using QuanLyQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCaPhe.DAL
{
    public class BillDAL
    {
        private static BillDAL instance;
        public static BillDAL Instance
        {
            get {
                if (instance == null)
                    instance = new BillDAL();
                return BillDAL.instance; }
            private set => instance = value; 
        }

        private BillDAL() { }

        /// Thành công: -> Bill id
        /// Thất bại: -1
        public int GetUncheckBillIdByTableId(int id)
        {
            DataTable dt = DataProvider.Instance.ExcuteQuery("select * from Bill where idTable = "+ id +" and status = 0");
            if (dt.Rows.Count > 0) 
            {
                BillDTO bill = new BillDTO(dt.Rows[0]);
                return bill.Id;
            }
            return -1;
        }

        public void CheckOut(int id, int discount, float totalPrice, float finalTotalPrice)
        {
            string query = "update Bill set status = 1, DateCheckOut = GETDATE(), " + " discount = " + discount + ", totalPrice = " + totalPrice + ", finaltotalPrice = " + finalTotalPrice + " where id = " + id;
            DataProvider.Instance.ExcuteNonQuery(query);
        }

        public void InsertBill(int id)
        {
            DataProvider.Instance.ExcuteNonQuery("exec USP_InsertBill @idTable", new object[] {id});
        }

        public int GetMaxBillID()
        {
            try
            {
                return (int)DataProvider.Instance.ExcuteScalar("select max(id) from Bill");
            }
            catch
            {
                return 1;
            }
        }

        public DataTable GetListBillRevenueByDate(DateTime checkIn, DateTime checkOut)
        {

            return DataProvider.Instance.ExcuteQuery("exec USP_GetListBillRevenueByDate @checkIn , @checkOut", new object[]{checkIn, checkOut});        
        }
        public void DeleteIdTableFromBill(int idTable) 
        {
            DataProvider.Instance.ExcuteQuery("delete Bill where idTable = " + idTable);
        }
    }
}
