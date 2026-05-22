using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanCaPhe.DAL
{
    public class DataProvider
    {
        private static DataProvider instance;
        public static DataProvider Instance 
        {
            get { if (instance == null) 
                    instance = new DataProvider();
                 return DataProvider.instance; }
            private set => DataProvider.instance = value; 
        }

        private DataProvider() { }

        string connectionSTR = "Data Source=DESKTOP-F81P7JH\\MINHPHUC;Initial Catalog=QL_QuanCaFe;Integrated Security=True;Encrypt=False";
 

        public DataTable ExcuteQuery(string query, object[] parameter = null) // dữ liệu trả về dạng bảng
        {
            DataTable dt = new DataTable();

            

            using (SqlConnection connection = new SqlConnection(connectionSTR)) // dữ liệu đc khai báo tự giải phóng khi kết thúc khối lệnh 
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                if (parameter != null) {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara) { 
                        if(item.Contains('@') )
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }    
                    }
                }

               // SqlDataAdapter adapter = new SqlDataAdapter(command);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);

                connection.Close();
            }

            return dt;
            
        }


        public int ExcuteNonQuery(string query, object[] parameter = null) //dùng để thực thi câu lệnh INSERT, UPDATE, DELETE, CREATE, ALTER…
        {
            int data = 0;

            using (SqlConnection connection = new SqlConnection(connectionSTR)) // dữ liệu đc khai báo tự giải phóng khi kết thúc khối lệnh 
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                data = command.ExecuteNonQuery();

                connection.Close();
            }

            return data;
        }

        public object ExcuteScalar(string query, object[] parameter = null) //lấy một giá trị duy nhất(Min, max, sum, count)
        {
            object data = 0;



            using (SqlConnection connection = new SqlConnection(connectionSTR)) // dữ liệu đc khai báo tự giải phóng khi kết thúc khối lệnh 
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                data = command.ExecuteScalar();

                connection.Close();
            }

            return data;

        }

    }
}
