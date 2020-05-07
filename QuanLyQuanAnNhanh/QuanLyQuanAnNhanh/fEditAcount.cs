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
    public partial class fEditAcount : Form
    {
        public fEditAcount()
        {
            InitializeComponent();
        }

        private void fEditAcount_Load(object sender, EventArgs e)
        {
            textNewUserName.Focus();
            btnOKAC.Focus();
        }

        private void btnOKAC_Click(object sender, EventArgs e)
        {
            int ID = NhanVien.Instance.IdAcount;
            string OldUserName = NhanVien.Instance.TaiKhoan;
            string OldPass = textOldPass.Text;
            string NewUserName = textNewUserName.Text;
            string NewPass = textNewPass.Text;
            if (checkDataEditAC())
            {
                if(AcountDAO.Instance.checkUserName(NewUserName))
                {
                    MessageBox.Show("Tài khoản đã tồn tại. Kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textNewUserName.Focus();
                }
                else if (LoginDAO.Instance.Login(OldUserName, OldPass) == false)
                {
                    MessageBox.Show("Mật khẩu cũ sai. Kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textOldPass.Focus();
                }
                else
                {
                    try
                    {
                        if (AcountDAO.Instance.EditAcount(ID, NewUserName, NewPass))
                        {
                            MessageBox.Show("Thay đổi thông tin tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        else MessageBox.Show("Lỗi. Kiểm tra lại kết nối!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch
                    {
                        MessageBox.Show("Lỗi. Kiểm tra lại kết nối!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void btnThoatAC_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool checkDataEditAC()
        {
            if (string.IsNullOrWhiteSpace(textNewUserName.Text))
            {
                MessageBox.Show("Bạn chưa nhập tên đăng nhập mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textNewUserName.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(textOldPass.Text))
            {
                MessageBox.Show("Bạn chưa nhập mật khẩu cũ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textOldPass.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(textNewPass.Text))
            {
                MessageBox.Show("Bạn chưa nhập mật khẩu mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textNewPass.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(textReNewPass.Text))
            {
                MessageBox.Show("Bạn chưa nhập mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textReNewPass.Focus();
                return false;
            }
            else if (textReNewPass.Text != textNewPass.Text)
            {
                MessageBox.Show("Nhập lại mật khẩu chưa chính xác. Kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textReNewPass.Focus();
                return false;
            }
            return true;
        }
    }
}
