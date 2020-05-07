using QuanLyQuanAnNhanh.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanAnNhanh.DAO
{
    class DonHangDAO
    {
        private static DonHangDAO instance;

        internal static DonHangDAO Instance
        {
            get { if (instance == null) instance = new DonHangDAO(); return DonHangDAO.instance; }
            private set { DonHangDAO.instance = value; }
        }

        private DonHangDAO() { }

        //Load data từ SQL lên dtgv
        public DataTable LoadDonHang()
        {
            //string query = "SELECT dh.maOrder as [Mã Order], dh.maMon as [Mã Món], tenOrder as [Tên Order], tenMOn as [Tên món], " +
            //    " soLuong as [Số lượng], dh.donGia as [Đơn giá], thanhTien as [Thành tiền], dh.ghiChu as [Ghi chú] FROM dbo.DONHANG AS dh, " +
            //    " dbo.MONAN AS ma, dbo.tabORDER AS od WHERE dh.maOrder = od.maOrder AND dh.maMon = ma.maMon";
            string query = "SELECT * FROM dbo.DONHANG ";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        //Load đơn hàng theo mã Order lên dtgv
        public DataTable LoadDonHang(int maOrder)
        {
            string query = "SELECT m.tenMOn, d.soLuong, d.donGia, d.thanhTien FROM dbo.MONAN AS m, dbo.DONHANG AS d WHERE d.maMon = m.maMon AND d.maOrder = ' " + maOrder + " ' ";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        //Tính tổng tiền theo đơn hàng
        public Decimal TongTienDonHang(int maOrder)
        {
            Decimal tong = 0;
            string query = "SELECT SUM(thanhTien) AS TongTien FROM dbo.DONHANG WHERE maOrder = '" + maOrder + "'";
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);
            DataRow dr = dt.Rows[0];
            tong = Convert.ToDecimal(dr["TongTien"].ToString());
            return tong;
        }

        //Thêm/Cập nhật Đơn hàng
        public bool ThemDonHang(int maOrder, int maMon, int soLuong, Decimal donGia, Decimal thanhTien)
        {
            if (DataProvider.Instance.ExecuteQuery("SELECT* From dbo.DONHANG where maOrder = '" + maOrder + "' and maMon = '" + maMon + "' ").Rows.Count > 0)
            {
                string query = "UPDATE dbo.DONHANG SET soLuong = soLuong + '" + soLuong + "'," +
                    " thanhTien = thanhTien + '" + thanhTien + "' WHERE maOrder = '" + maOrder + "' and maMon = '" + maMon + "' and soLuong + '" + soLuong + "' >= 0 ";
                return DataProvider.Instance.ExecuteNonQuery(query) > 0;
            }
            else
            {
                string query = string.Format("INSERT dbo.DONHANG ( maOrder, maMon, soLuong, donGia, thanhTien ) " +
                    " values ('{0}', '{1}', '{2}', '{3}', '{4}')", maOrder, maMon, soLuong, donGia, thanhTien);
                return DataProvider.Instance.ExecuteNonQuery(query) > 0;
            }
        }

        //Sửa Đơn hàng
        public bool EditDonHang(int maOrder, int maMon, int soLuong, Decimal donGia, Decimal thanhTien)
        {
            if (DataProvider.Instance.ExecuteQuery("SELECT* From dbo.DONHANG where maOrder = '" + maOrder + "' and maMon = '" + maMon + "' ").Rows.Count == 0)
            {
                return false;
            }
            else
            {
                string query = "UPDATE dbo.DONHANG SET soLuong = '" + soLuong + "', donGia = '" + donGia + "', thanhTien = '" + thanhTien + "' where maOrder = '" + maOrder + "' ";
                int result = DataProvider.Instance.ExecuteNonQuery(query);
                return result > 0;
            }
        }

        // Xóa Đơn hàng
        public bool DeleteDonHang(int maOrder, int maMon)
        {
            if (DataProvider.Instance.ExecuteQuery("SELECT* From dbo.DONHANG where maOrder = '" + maOrder + "' and maMon = '" + maMon + "' ").Rows.Count == 0)
            {
                return false;
            }
            else
            {
                string query = "DELETE dbo.DONHANG WHERE maOrder = '" + maOrder + "' and maMon = '" + maMon + "' ";
                return DataProvider.Instance.ExecuteNonQuery(query) > 0;
            }

        }

        // Tìm kiếm Đơn hàng hiển thị thông tin lên datagridview //(int maOrder, int maMon, int soLuong, Decimal donGia, Decimal thanhTien)
        public DataTable SearchDonHang(string keySearch)
        {
            string query = "SELECT * FROM dbo.DONHANG where maOrder like '%" + keySearch + "%' or " +
                " maMon like '%" + keySearch + "%' or soLuong like '%" + keySearch + "%' ";
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);
            return dt;
        }


    }
}
