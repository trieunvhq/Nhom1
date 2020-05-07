using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanAnNhanh.DTO
{
    class MonAn
    {
        private static MonAn instance;
        private int maMonAn;
        private string tenMonAn;
        private Decimal donGiaMonAn;
       

        private MonAn() { }

        public static MonAn Instance
        {
            get
            {
                if (instance == null) instance = new MonAn();
                return instance;
            }

            set
            {
                instance = value;
            }
        }

        public int MaMonAn
        {
            get { return maMonAn; }
            set { maMonAn = value; }
        }


        public string TenMonAn
        {
            get { return tenMonAn; }
            set { tenMonAn = value; }
        }


        public Decimal DonGiaMonAn
        {
            get { return donGiaMonAn; }
            set { donGiaMonAn = value; }
        }
    }
}
