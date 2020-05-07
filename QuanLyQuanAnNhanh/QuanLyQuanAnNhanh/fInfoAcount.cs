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
    public partial class fInfoAcount : Form
    {
        public string Username;
        public fInfoAcount()
        {
            InitializeComponent();
        }

        private void fInfoAcount_Load(object sender, EventArgs e)
        {
            textMaNV.Text = NhanVien.Instance.MaNV.ToString();
            textHoTen.Text = NhanVien.Instance.HoTenNV;
            textUserName.Text = NhanVien.Instance.TaiKhoan.ToString();
            btnThoatIfAC.Focus();
        }

        private void btnSuaIfAC_Click(object sender, EventArgs e)
        {
            this.Hide();
            fEditAcount f = new fEditAcount();
            f.ShowDialog();
            this.Close();
        }

        private void btnThoatIfAC_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
