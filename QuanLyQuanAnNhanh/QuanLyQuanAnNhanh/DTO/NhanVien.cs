using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanAnNhanh.DTO
{
    class NhanVien
    {
        private static NhanVien instance;
        private int maNV;
        private int idAcount;
        private string hoTenNV;
        private DateTime ngaySinhNV;
        private string soDTNV;
        private string taiKhoan;
        private string matKhau;
        private string quyenHan;
        
        
        private NhanVien() { }

        public static NhanVien Instance
        {
            get
            {
                if (instance == null) instance = new NhanVien();
                return instance;
            }

            set
            {
                instance = value;
            }
        }

        public int MaNV
        {
            get { return maNV; }
            set { maNV = value; }
        }

        public int IdAcount
        {
            get { return idAcount; }
            set { idAcount = value; }
        }

        public string HoTenNV
        {
            get { return hoTenNV; }
            set { hoTenNV = value; }
        }

        public DateTime NgaySinhNV
        {
            get { return ngaySinhNV; }
            set { ngaySinhNV = value; }
        }


        public string SoDTNV
        {
            get { return soDTNV; }
            set { soDTNV = value; }
        }

        public string TaiKhoan
        {
            get { return taiKhoan; }
            set { taiKhoan = value; }
        }

        public string MatKhau
        {
            get { return matKhau; }
            set { matKhau = value; }
        }

        public string QuyenHan
        {
            get { return quyenHan; }
            set { quyenHan = value; }
        }
    }
}
