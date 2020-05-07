using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanAnNhanh.DTO
{
    class ThanhToan
    {
        private static ThanhToan instance;
        private int maOrder;
        private string tenOrder;
        private int maNV;
        private string hoTenNV;
        private int giamGia;
        private DateTime timeThanhToan;
        private Decimal tongTien;

        
        


        public ThanhToan() { }

        public static ThanhToan Instance
        {
            get
            {
                if (instance == null) instance = new ThanhToan();
                return instance;
            }

            set
            {
                instance = value;
            }
        }

        public int MaOrder
        {
            get { return maOrder; }
            set { maOrder = value; }
        }


        public int MaNV
        {
            get { return maNV; }
            set { maNV = value; }
        }


        public int GiamGia
        {
            get { return giamGia; }
            set { giamGia = value; }
        }


        public string TenOrder
        {
            get { return tenOrder; }
            set { tenOrder = value; }
        }


        public string HoTenNV
        {
            get { return hoTenNV; }
            set { hoTenNV = value; }
        }

        public DateTime TimeThanhToan
        {
            get { return timeThanhToan; }
            set { timeThanhToan = value; }
        }


        public Decimal TongTien
        {
            get { return tongTien; }
            set { tongTien = value; }
        }
    }
}
