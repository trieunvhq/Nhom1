using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanAnNhanh.DTO
{
    class Order
    {
        private static Order instance;
        private int maOrder;
        private string tenOrder;
        private DateTime timeOrder;
        private string status;
        private string ghiChu;


        public Order() { }

        public static Order Instance
        {
            get
            {
                if (instance == null) instance = new Order();
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


        public string TenOrder
        {
            get { return tenOrder; }
            set { tenOrder = value; }
        }


        public DateTime TimeOrder
        {
            get { return timeOrder; }
            set { timeOrder = value; }
        }


        public string Status
        {
            get { return status; }
            set { status = value; }
        }


        public string GhiChu
        {
            get { return ghiChu; }
            set { ghiChu = value; }
        }
    }
}
