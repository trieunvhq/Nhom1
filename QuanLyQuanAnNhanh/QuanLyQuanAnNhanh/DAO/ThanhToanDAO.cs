using QuanLyQuanAnNhanh.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanAnNhanh.DAO
{
    class ThanhToanDAO
    {
        private static ThanhToanDAO instance;

        internal static ThanhToanDAO Instance
        {
            get { if (instance == null) instance = new ThanhToanDAO(); return ThanhToanDAO.instance; }
            private set { ThanhToanDAO.instance = value; }
        }

        private ThanhToanDAO() { }

        //Load data từ SQL lên dtgv
        public DataTable LoadThanhToan()
        {
            string query = "SELECT maOrder AS [Mã Order], maNV AS [Mã nhân viên], giamGia AS [Giảm giá], tongTien AS [Tổng tiền], timeThanhToan AS [Thời gian] FROM dbo.THANHTOAN";

            return DataProvider.Instance.ExecuteQuery(query);
        }

        //Update Đã thanh toán đơn hàng
        public bool ThanhToanXong(int maOrder)
        {
            string query = "UPDATE dbo.tabORDER SET status = N'Đã thanh toán' WHERE maOrder = '" + maOrder + "' ";

            return DataProvider.Instance.ExecuteNonQuery(query) > 0;
        }

        //Update Hủy thanh toán đơn hàng
        public bool HuyThanhToan(int maOrder)
        {
            string query = "UPDATE dbo.tabORDER SET status = N'Chưa thanh toán' WHERE maOrder = '" + maOrder + "' ";

            return DataProvider.Instance.ExecuteNonQuery(query) > 0;
        }

        //Thêm thanh toán đơn hàng
        public bool AddThanhtoan(int maOrder, int maNV, int giamGia, Decimal tongTien, DateTime timeThanhToan)
        {
            if (DataProvider.Instance.ExecuteQuery("SELECT* From dbo.THANHTOAN where maOrder = '" + maOrder + "' ").Rows.Count > 0)
            {
                return false;
            }
            else
            {
                string query = string.Format("INSERT dbo.THANHTOAN ( maOrder, maNV, giamGia, tongTien, timeThanhToan ) " +
                    " values ('{0}', '{1}', '{2}', '{3}', '{4}')", maOrder, maNV, giamGia, tongTien, timeThanhToan);
                return DataProvider.Instance.ExecuteNonQuery(query) > 0;
            }
        }

        //Sửa thanh toán
        public bool EditThanhtoan(int maOrder, int maNV, int giamGia, Decimal tongTien, DateTime timeThanhToan)
        {
            if (DataProvider.Instance.ExecuteQuery("SELECT* From dbo.THANHTOAN where maOrder = '" + maOrder + "' ").Rows.Count == 0)
            {
                return false;
            }
            else
            {
                string query = "UPDATE dbo.THANHTOAN SET maNV = '" + maNV + "', giamGia = '" + giamGia + "', tongTien = '" + tongTien + "', timeThanhToan = '" + timeThanhToan + "' where maOrder = '" + maOrder + "' ";
                int result = DataProvider.Instance.ExecuteNonQuery(query);
                return result > 0;
            }
        }

        // Xóa thanh toán
        public bool DeleteThanhtoan(int maOrder)
        {
            if (DataProvider.Instance.ExecuteQuery("SELECT* From dbo.THANHTOAN where maOrder = '" + maOrder + "' ").Rows.Count == 0)
            {
                return false;
            }
            else
            {
                string query = "DELETE dbo.THANHTOAN WHERE maOrder = '" + maOrder + "' ";
                return DataProvider.Instance.ExecuteNonQuery(query) > 0;
            }

        }

        // Tìm kiếm thanh toán hiển thị thông tin lên datagridview
        public DataTable SearchThanhtoan(string keySearch)
        {
            string query = "SELECT * FROM dbo.THANHTOAN where maOrder like '%" + keySearch + "%' or " +
                " maNV like '%" + keySearch + "%' or giamGia like '%" + keySearch + "%' or tongTien like '%" + keySearch + "%' or timeThanhToan like '%" + keySearch + "%' ";
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);
            return dt;
        }


        // Hàm lấy thông tin thanh toán
        public ThanhToan GetInfoThanhToan(int maOrder)
        {
            DataTable dt = new DataTable();
            string query = "SELECT tt.maOrder, od.tenOrder, tt.maNV, nv.hoTenNV, tt.giamGia, tt.timeThanhToan, " +
                " tt.tongTien FROM dbo.NHANVIEN AS nv, dbo.tabORDER AS od, dbo.THANHTOAN AS tt WHERE tt.maOrder " +
            " = od.maOrder AND tt.maNV = nv.maNV AND tt.maOrder = '" + maOrder + "' ";
            dt = DataProvider.Instance.ExecuteQuery(query);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                ThanhToan.Instance.MaOrder = maOrder;
                ThanhToan.Instance.TenOrder = dr["tenOrder"].ToString();
                ThanhToan.Instance.MaNV = Int16.Parse(dr["maNV"].ToString());
                ThanhToan.Instance.HoTenNV = dr["hoTenNV"].ToString();
                ThanhToan.Instance.GiamGia = Int16.Parse(dr["giamGia"].ToString());
                ThanhToan.Instance.TimeThanhToan = (DateTime)dr["timeThanhToan"];
                ThanhToan.Instance.TongTien = Decimal.Parse(dr["tongTien"].ToString());
            }
            return ThanhToan.Instance;
        }
    }
}
