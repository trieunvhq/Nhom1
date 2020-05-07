using QuanLyQuanAnNhanh.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanAnNhanh.DAO
{
    class MonAnDAO
    {
        private static MonAnDAO instance;

        internal static MonAnDAO Instance
        {
            get { if (instance == null) instance = new MonAnDAO(); return MonAnDAO.instance; }
            private set { MonAnDAO.instance = value; }
        }

        private MonAnDAO() { }

        //Load data từ SQL lên dtgv
        public DataTable LoadMonAn()
        {
            string query = "SELECT maMon AS [Mã món], tenMOn AS [Tên món], donGia AS [Đơn giá] FROM dbo.MONAN";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        // Lấy thông tin của Món ăn từ DS cmbBox Món ăn theo mã món
        public MonAn GetInfoMonAnFromComboBox(int maMon)
        {
            string query = "SELECT * FROM dbo.MONAN where maMon = '" + maMon + "'";
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);
            DataRow dr = dt.Rows[0];
            MonAn.Instance.MaMonAn = maMon;
            MonAn.Instance.TenMonAn = dr["tenMOn"].ToString();
            MonAn.Instance.DonGiaMonAn = Convert.ToDecimal(dr["donGia"].ToString());
            return MonAn.Instance;
        }

        //Thêm Món ăn
        public bool AddMonAn(string tenMOn, Decimal donGia)
        {
            if (DataProvider.Instance.ExecuteQuery("SELECT* From dbo.MONAN where tenMOn = N'" + tenMOn + "' ").Rows.Count > 0)
            {
                return false;
            }
            else
            {
                string query = string.Format("INSERT INTO dbo.MONAN ( tenMOn, donGia ) values (N'{0}', '{1}')", tenMOn, donGia);
                int result = DataProvider.Instance.ExecuteNonQuery(query);
                return result > 0;
            }
        }

        //Sửa Món ăn
        public bool EditMonAn(int maMon, string tenMOn, Decimal donGia)
        {
            if (DataProvider.Instance.ExecuteQuery("SELECT* From dbo.MONAN where maMon = '" + maMon + "' ").Rows.Count == 0)
            {
                return false;
            }
            else
            {
                string query = "UPDATE dbo.MONAN SET tenMOn = N'" + tenMOn + "', donGia = '" + donGia + "' where maMon = '" + maMon + "' ";
                int result = DataProvider.Instance.ExecuteNonQuery(query);
                return result > 0;
            }
        }

        // Xóa Món ăn
        public bool DeleteMonAn(int maMon)
        {
            if (DataProvider.Instance.ExecuteQuery("SELECT* From dbo.MONAN where maMon = '" + maMon + "' ").Rows.Count == 0)
            {
                return false;
            }
            else
            {
                string query = "DELETE dbo.MONAN WHERE maMon = '" + maMon + "' ";
                return DataProvider.Instance.ExecuteNonQuery(query) > 0;
            }
            
        }

        // Tìm kiếm Món ăn hiển thị thông tin lên datagridview
        public DataTable SearchMonAn(string keySearch)
        {
            DataTable dt = DataProvider.Instance.ExecuteQuery("SELECT * FROM dbo.MONAN where tenMOn like N'%" + keySearch + "%' or maMon like N'%" + keySearch + "%' ");
            return dt;
        }

    }
}
