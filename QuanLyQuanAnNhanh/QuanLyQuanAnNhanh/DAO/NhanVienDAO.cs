using QuanLyQuanAnNhanh.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanAnNhanh.DAO
{
    class NhanVienDAO
    {
        private static NhanVienDAO instance;

        internal static NhanVienDAO Instance
        {
            get { if (instance == null) instance = new NhanVienDAO(); return NhanVienDAO.instance; }
            private set { NhanVienDAO.instance = value; }
        }

        private NhanVienDAO() { }

        //Load data từ SQL lên dtgv
        public DataTable LoadNhanVien()
        {
            string query = "SELECT maNV AS [Mã nhân viên], hoTenNV AS [Họ tên], ngaySinhNV AS [Ngày sinh], soDTNV AS [Số ĐT] FROM dbo.NHANVIEN";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        // Hàm lấy thông tin nhân viên khi đăng nhập
        public NhanVien LayThongTinNhanVien(string taiKhoan, string matKhau)
        {
            DataTable dt = new DataTable();
            string query = "SELECT nv.maNV, nv.hoTenNV, nv.ngaySinhNV, nv.soDTNV, ac.ID, ac.UserName, ac.Pasword, ac.typeAcount "
            + " FROM dbo.NHANVIEN AS nv, dbo.ACOUNT AS ac WHERE ac.maNV = nv.maNV AND UserName = N'" + taiKhoan + "' AND Pasword = N'" + matKhau + "' ";
            dt = DataProvider.Instance.ExecuteQuery(query);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                NhanVien.Instance.MaNV = Int16.Parse(dr["maNV"].ToString());
                NhanVien.Instance.IdAcount = Int16.Parse(dr["ID"].ToString());
                NhanVien.Instance.HoTenNV = dr["hoTenNV"].ToString();
                NhanVien.Instance.NgaySinhNV = (DateTime)dr["ngaySinhNV"];
                NhanVien.Instance.SoDTNV = dr["soDTNV"].ToString();
                NhanVien.Instance.TaiKhoan = taiKhoan;
                NhanVien.Instance.MatKhau = matKhau;
                NhanVien.Instance.QuyenHan = dr["typeAcount"].ToString();
            }
            return NhanVien.Instance;
        }

        //Thêm Nhân viên
        public bool AddNhanVien(string hoTenNV, DateTime ngaySinhNV, string soDTNV)
        {
            if (DataProvider.Instance.ExecuteQuery("SELECT* From dbo.NHANVIEN where hoTenNV = N'" + hoTenNV + "' and ngaySinhNV = '" + ngaySinhNV + "' and soDTNV = N'" + soDTNV + "' ").Rows.Count > 0)
            {
                return false;
            }
            else
            {
                string query = string.Format("INSERT INTO dbo.NHANVIEN ( hoTenNV, ngaySinhNV, soDTNV ) values (N'{0}', N'{1}', N'{2}')", hoTenNV, ngaySinhNV, soDTNV);
                int result = DataProvider.Instance.ExecuteNonQuery(query);
                return result > 0;
            }
        }

        //Sửa Nhân viên
        public bool EditNhanVien(int maNV, string hoTenNV, DateTime ngaySinhNV, string soDTNV)
        {
            if (DataProvider.Instance.ExecuteQuery("SELECT* From dbo.NHANVIEN where maNV = '" + maNV + "' ").Rows.Count == 0)
            {
                return false;
            }
            else
            {
                string query = "UPDATE dbo.NHANVIEN SET hoTenNV = N'" + hoTenNV + "', ngaySinhNV = '" + ngaySinhNV + "', soDTNV = N'" + soDTNV + "' where maNV = '" + maNV + "' ";
                int result = DataProvider.Instance.ExecuteNonQuery(query);
                return result > 0;
            }
        }

        // Xóa Nhân viên
        public bool DeleteNhanVien(int maNV)
        {
            if (DataProvider.Instance.ExecuteQuery("SELECT* From dbo.NHANVIEN where maNV = '" + maNV + "' ").Rows.Count == 0)
            {
                return false;
            }
            else
            {
                string query = "DELETE dbo.NHANVIEN WHERE maNV = '" + maNV + "' ";
                return DataProvider.Instance.ExecuteNonQuery(query) > 0;
            }
        }

        // Tìm kiếm Nhân viên hiển thị thông tin lên datagridview //(int maNV, string hoTenNV, DateTime ngaySinhNV, string soDTNV)
        public DataTable SearchNhanVien(string keySearch)
        {
            DataTable dt = DataProvider.Instance.ExecuteQuery("SELECT * FROM dbo.NHANVIEN where hoTenNV like N'%" + keySearch + "%' or maNV like '%" + keySearch + "%' or ngaySinhNV like '%" + keySearch + "%' or soDTNV like N'%" + keySearch + "%'");
            return dt;
        }

        // Check mã nhân viên có tồn tại trong hệ thống không
        public bool CheckMaNhanVien(int maNV)
        {
            return DataProvider.Instance.ExecuteQuery("SELECT* From dbo.NHANVIEN where maNV = '" + maNV + "' ").Rows.Count > 0;
        }
    }
}
