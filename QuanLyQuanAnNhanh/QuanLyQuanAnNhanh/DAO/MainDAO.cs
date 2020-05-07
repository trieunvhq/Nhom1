using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanAnNhanh.DAO
{
    class MainDAO
    {
        private static MainDAO instance;

        internal static MainDAO Instance
        {
            get { if (instance == null) instance = new MainDAO(); return MainDAO.instance; }
            private set { MainDAO.instance = value; }
        }

        private MainDAO() { }

        

        

        //Load danh sách món ăn lên ComboBox
        public DataTable LoadMonAnCmbox()
        {
            string query = "SELECT * FROM dbo.MONAN";
            return DataProvider.Instance.ExecuteQuery(query);
        }
    }
}
