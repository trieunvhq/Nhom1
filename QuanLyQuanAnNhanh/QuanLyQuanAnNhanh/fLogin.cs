using QuanLyQuanAnNhanh.DAO;
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
    public partial class fLogin : Form
    {
        private bool loginCheck;

        public bool LoginCheck
        {
            get { return loginCheck; }
            set { loginCheck = value; }
        }

        public fLogin()
        {
            InitializeComponent();
            LoginCheck = false;
        }

        private void butLogin_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textUser.Text))
            {
                MessageBox.Show("Bạn chưa nhập tên đăng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textUser.Focus();
            }
            else if (string.IsNullOrWhiteSpace(textPassword.Text))
            {
                MessageBox.Show("Bạn chưa nhập mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textPassword.Focus();
            }
            else
            {
                if (Login(textUser.Text, textPassword.Text))
                {
                    NhanVienDAO.Instance.LayThongTinNhanVien(textUser.Text, textPassword.Text);
                    loginCheck = true;
                    //this.Close();
                    this.Hide();
                    fMain fm = new fMain();
                    fm.ShowDialog();
                    this.Show();
                }
                else
                {
                    textUser.Text = "";
                    textPassword.Text = "";
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu sai. Kiểm tra lại!");
                    textUser.Focus();
                }
            }
        }

        bool Login(string username, string password)
        {
            return LoginDAO.Instance.Login(username, password);
        }

        private void butCancall_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát không?", "Thông báo!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                this.Close();
            }
        }
    }
}
