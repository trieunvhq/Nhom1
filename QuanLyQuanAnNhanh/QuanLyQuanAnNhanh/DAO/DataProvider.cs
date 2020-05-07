using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace QuanLyQuanAnNhanh.DAO
{
    public class DataProvider
    {
        private string Source = @"Data Source=.;Initial Catalog=QuanLyQuanAnNhanh;Integrated Security=True";
        private static DataProvider instance;

        public static DataProvider Instance
        {
            get { if (instance == null) instance = new DataProvider(); return DataProvider.instance; }
            private set { DataProvider.instance = value; }
        }
        private DataProvider() { }

        // Truy vấn data với chuỗi query truyền vào
        public DataTable ExecuteQuery(string query, object[] Parameter = null)
        {
            DataTable data = new DataTable();
            using (SqlConnection connection = new SqlConnection(Source))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                if (Parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, Parameter[i]);
                            i++;
                        }
                    }
                }
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(data);
                connection.Close();
            }
            return data;
        }

        //Truy vấn dữ liệu với mảng parameter truyền vào, kết quả trả về là số rows bị thay đổi thành công
        public int ExecuteNonQuery(string query, object[] Parameter = null)
        {
            int data = 0;
            using (SqlConnection connection = new SqlConnection(Source))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                if(Parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, Parameter[i]);
                            i++;
                        }
                    }
                }
                data = command.ExecuteNonQuery();
                connection.Close();
            }
            return data;
        }

        //Truy vấn dữ liệu với mảng parameter truyền vào, kết quả trả về là số lượng: ô đầu tiên của hàng và cột đầu tiên
        public object ExecuteScalar(string query, object[] Parameter = null)
        {
            object data = 0;
            using (SqlConnection connection = new SqlConnection(Source))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                if (Parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, Parameter[i]);
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
