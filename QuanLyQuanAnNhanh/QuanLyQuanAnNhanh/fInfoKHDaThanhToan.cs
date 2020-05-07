using QuanLyQuanAnNhanh.DAO;
using QuanLyQuanAnNhanh.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanAnNhanh
{
    public partial class fInfoKHDaThanhToan : Form
    {
        public int maOrderTT;

        public fInfoKHDaThanhToan()
        {
            InitializeComponent();
        }

        private void fInfoKHDaThanhToan_Load(object sender, EventArgs e)
        {
            textNVTTKHdaTT.Text = ThanhToanDAO.Instance.GetInfoThanhToan(maOrderTT).HoTenNV;
            textMaOrderKHdaTT.Text = maOrderTT.ToString();
            textTenOrderKHdaTT.Text = ThanhToanDAO.Instance.GetInfoThanhToan(maOrderTT).TenOrder;
            textTimeOrderKHdaTT.Text = OrderDAO.Instance.GetInfoOrder(maOrderTT).TimeOrder.ToString();
            textTimeTTKHdaTT.Text = ThanhToanDAO.Instance.GetInfoThanhToan(maOrderTT).TimeThanhToan.ToString();
            textGiamGiaKHdaTT.Text = ThanhToanDAO.Instance.GetInfoThanhToan(maOrderTT).GiamGia.ToString() + " %";
            textTienDHKHdaTT.Text = DonHangDAO.Instance.TongTienDonHang(maOrderTT).ToString() + " vnđ";
            textTongTienTTKHdaTT.Text = ThanhToanDAO.Instance.GetInfoThanhToan(maOrderTT).TongTien.ToString() + " vnđ";
            dtgvKHdaTT.DataSource = DonHangDAO.Instance.LoadDonHang(maOrderTT);
            btnCancallKHdaTT.Focus();
        }


        private void btnCancallKHdaTT_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
