using QuanLyQuanAnNhanh.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanAnNhanh.DAO
{
    class OrderDAO
    {
        private static OrderDAO instance;

        internal static OrderDAO Instance
        {
            get { if (instance == null) instance = new OrderDAO(); return OrderDAO.instance; }
            private set { OrderDAO.instance = value; }
        }

        private OrderDAO() { }

        //Load data Order lên dtgv
        public DataTable LoadOrder()
        {
            string query = "SELECT * FROM dbo.tabORDER  ORDER BY status , maOrder DESC";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        // Lấy thông tin của Khách hàng theo mã Order
        public Order GetInfoOrder(int maOrder)
        {
            string query = "SELECT * FROM dbo.tabORDER where maOrder = '" + maOrder + "'";
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);
            DataRow dr = dt.Rows[0];
            Order.Instance.MaOrder = maOrder;
            Order.Instance.TenOrder = dr["tenOrder"].ToString();
            Order.Instance.TimeOrder = (DateTime)dr["timeOrder"];
            Order.Instance.Status = dr["status"].ToString();
            Order.Instance.GhiChu = dr["ghiChu"].ToString();
            return Order.Instance;
        }

        // Tìm kiếm Order hiển thị thông tin lên datagridview
        public List<Order> SearchOrder(string keySearch)
        {
            List<Order> list = new List<Order>();
            DataTable dt = DataProvider.Instance.ExecuteQuery("SELECT * FROM dbo.tabORDER where tenOrder like N'%" + keySearch + "%' ");
            foreach (DataRow dr in dt.Rows)
            {
                Order od = new Order();
                od.MaOrder = Convert.ToInt16(dr["maOrder"].ToString());
                od.TenOrder = dr["tenOrder"].ToString();
                od.TimeOrder = (DateTime)dr["timeOrder"];
                od.Status = dr["status"].ToString();
                od.GhiChu = dr["ghiChu"].ToString();
                list.Add(od);
            }
            return list;
        }

        //Thêm Order
        public bool AddOrder(string tenOrder, DateTime timeOrder, string ghiChu)
        {
            string query = string.Format("INSERT INTO dbo.tabORDER ( tenOrder, timeOrder, ghiChu ) values ( N'{0}', '{1}', N'{2}' )", tenOrder, timeOrder, ghiChu);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        //Sửa Order
        public bool EditOrder(int maOrder, string tenOrder, DateTime timeOrder, string ghiChu)
        {
            string query = "UPDATE dbo.tabORDER SET tenOrder = N'" + tenOrder + "'," +
                " timeOrder = '" + timeOrder + "', ghiChu = N'" + ghiChu + "' WHERE maOrder = '" + maOrder + "' ";
            return DataProvider.Instance.ExecuteNonQuery(query) > 0;
        }

        //Xóa Order
        public bool DeleteOrder(int keySearch)
        {
            string query = "DELETE dbo.tabORDER WHERE maOrder = '" + keySearch + "' or tenOrder = N'" + keySearch + "' or timeOrder = '" + keySearch + "' ";
            return DataProvider.Instance.ExecuteNonQuery(query) > 0;
        }
        

    }
}
