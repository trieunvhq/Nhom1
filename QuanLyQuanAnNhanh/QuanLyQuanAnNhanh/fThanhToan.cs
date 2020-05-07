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
    public partial class fThanhToan : Form
    {
        public int maOrderTT;
        public int giamGiaTT;
        public fThanhToan()
        {
            InitializeComponent();
        }


        private void fThanhToan_Load(object sender, EventArgs e)
        {
            textNVTT.Text = NhanVien.Instance.HoTenNV;
            textMaOrderThanhtoan.Text = maOrderTT.ToString();
            textTenOrderThanhtoan.Text = OrderDAO.Instance.GetInfoOrder(maOrderTT).TenOrder.ToString();
            textTimeOrderThanhtoan.Text = OrderDAO.Instance.GetInfoOrder(maOrderTT).TimeOrder.ToString();
            textTimeTT.Text = DateTime.Now.ToString();
            textGiamGiaThanhtoan.Text = giamGiaTT.ToString() + " %";
            textTienDHThanhtoan.Text = DonHangDAO.Instance.TongTienDonHang(maOrderTT).ToString() + " vnđ";
            textTongTThanhtoan.Text = ((DonHangDAO.Instance.TongTienDonHang(maOrderTT)) - (DonHangDAO.Instance.TongTienDonHang(maOrderTT)) * giamGiaTT / 100).ToString() + " vnđ";
            dtgvDonHangThanhtoan.DataSource = DonHangDAO.Instance.LoadDonHang(maOrderTT);
            btnOKThanhtoan.Focus();
        }

        private void btnOKThanhtoan_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Đồng ý thanh toán không?", "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (ThanhToanDAO.Instance.ThanhToanXong(maOrderTT))
                {
                    int maNV = NhanVien.Instance.MaNV;
                    Decimal TongTienTT = DonHangDAO.Instance.TongTienDonHang(maOrderTT) - (DonHangDAO.Instance.TongTienDonHang(maOrderTT)) * giamGiaTT / 100;
                    DateTime timeTT = DateTime.Now;
                    ThanhToanDAO.Instance.AddThanhtoan(maOrderTT, maNV, giamGiaTT, TongTienTT, timeTT);
                    MessageBox.Show("Thanh toán thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Thanh toán lỗi. Đề nghị kiểm tra lại kết nối !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
                this.Close();
            }
        }

        private void btnCancallThanhtoan_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
