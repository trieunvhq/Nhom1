using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanAnNhanh.DAO
{
    class LoginDAO
    {
        private static LoginDAO instance; //Ctrl + R + E=>

        internal static LoginDAO Instance
        {
            get { if (instance == null) instance = new LoginDAO(); return LoginDAO.instance; }
            private set { LoginDAO.instance = value; }
        }

        private LoginDAO() { }

        // Hàm kiểm tra đăng nhập
        public bool Login(string userName, string password)
        {
            string query = "SELECT* From dbo.ACOUNT where UserName = N'" + userName + "' and pasword = N'" + password + "' ";
            DataTable result = DataProvider.Instance.ExecuteQuery(query);
            return result.Rows.Count >0;
        }

        
    }
}
