using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanAnNhanh.DAO
{
    class AcountDAO
    {
        private static AcountDAO instance;

        internal static AcountDAO Instance
        {
            get { if (instance == null) instance = new AcountDAO(); return AcountDAO.instance; }
            private set { AcountDAO.instance = value; }
        }

        private AcountDAO() { }

        //Load data từ SQL lên dtgv
        public DataTable LoadAcount()
        {
            string query = "SELECT ID AS [Mã tài khoản], maNV AS [Mã nhân viên], UserName AS [Tên đăng nhập],typeAcount AS [Loại tài khoản] FROM dbo.ACOUNT";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        //Thêm Tài khoản
        public bool AddAcount(int maNV, string UserName, string Password, string typeAcount)
        {
            if (DataProvider.Instance.ExecuteQuery("SELECT* From dbo.ACOUNT where UserName = N'" + UserName + "' ").Rows.Count > 0)
            {
                return false;
            }
            else
            {
                string query = string.Format("INSERT INTO dbo.ACOUNT ( maNV, UserName, Pasword, typeAcount ) values ('{0}', N'{1}', N'{2}', N'{3}')", maNV, UserName, Password, typeAcount);
                int result = DataProvider.Instance.ExecuteNonQuery(query);
                return result > 0;
            }
        }

        //Sửa Tài khoản
        public bool EditAcount(int ID, string UserName, string Password)
        {
            string query = "UPDATE dbo.ACOUNT SET UserName = N'" + UserName + "', Pasword = N'"+ Password +"' where ID = '" + ID + "' ";
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        //Reset Mật khẩu Tài khoản
        public bool ResetPassAcount(int ID)
        {
            if (DataProvider.Instance.ExecuteQuery("SELECT* From dbo.ACOUNT where ID = '" + ID + "' ").Rows.Count == 0)
            {
                return false;
            }
            else
            {
                string query = "UPDATE dbo.ACOUNT SET Pasword = N'123456' where ID = '" + ID + "' ";
                int result = DataProvider.Instance.ExecuteNonQuery(query);
                return result > 0;
            }
        }

        // Xóa tài khoản
        public bool DeleteAcount(int ID)
        {
            string query = "DELETE dbo.ACOUNT WHERE ID = '" + ID + "' ";
            return DataProvider.Instance.ExecuteNonQuery(query) > 0;
        }

        // Tìm kiếm Acount hiển thị thông tin lên datagridview //( maNV, UserName, Pasword, typeAcount )
        public DataTable SearchAcount(string keySearch)
        {
            DataTable dt = DataProvider.Instance.ExecuteQuery("SELECT * FROM dbo.ACOUNT where UserName like N'%" + keySearch + "%' or ID like '%" + keySearch + "%' or maNV like '%" + keySearch + "%' or typeAcount like N'%" + keySearch + "%' ");
            return dt;
        }

        //Ktra trùng UserName
        public bool checkUserName(string UserName) 
        {
            return DataProvider.Instance.ExecuteQuery("SELECT* From dbo.ACOUNT where UserName = N'" + UserName + "' ").Rows.Count > 0;
        }
    }
}
