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
    public partial class fAdmin : Form
    {
        private string flag;
        private int index;

        public fAdmin()
        {
            InitializeComponent();
            LoadDonHangAdmin();
            LoadAcountAdmin();
            LoadMonAnAdmin();
            LoadNhanVienAdmin();
            LoadThanhToanAdmin();
        }

        


        private void fAdmin_Load(object sender, EventArgs e)
        {
            cmbTypeAC.Items.Add("Admin");
            cmbTypeAC.Items.Add("Tài khoản thường");
            //lockControlAcount();
            //lockControlNV();
            //lockControlMonAn();
            //lockControlThanhtoan();
            //lockControlDonHang();
            //lockControlDonHang();
        }

        //================================ CÁC HÀM XỬ LÝ TAB ACOUNT/ADMIN ==================================

        void LoadAcountAdmin()
        {
            dtgvAcount.DataSource = AcountDAO.Instance.LoadAcount();
            lockControlAcount();
        }

        // Lựa chọn Acount theo Click chuột, hiển thị thông tin trên Textbox
        private void dtgvAcount_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index = dtgvAcount.CurrentCell == null ? -1 : dtgvAcount.CurrentCell.RowIndex;
            if (index != -1)
            {
                textIDAC.Text = dtgvAcount.Rows[index].Cells[0].Value.ToString();
                textMaNVAC.Text = dtgvAcount.Rows[index].Cells[1].Value.ToString();
                textUserNameAC.Text = dtgvAcount.Rows[index].Cells[2].Value.ToString();
                cmbTypeAC.Text = dtgvAcount.Rows[index].Cells[3].Value.ToString();
            }
        }

        //Ham kiem tra thong tin nhap vao
        private bool checkDataAcount()
        {
            if (string.IsNullOrWhiteSpace(textMaNVAC.Text))
            {
                MessageBox.Show("Bạn chưa nhập mã nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textMaNVAC.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(textUserNameAC.Text))
            {
                MessageBox.Show("Bạn chưa nhập UserName!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textUserNameAC.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(textPassAcount.Text))
            {
                MessageBox.Show("Bạn chưa nhập Mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textPassAcount.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(cmbTypeAC.Text))
            {
                MessageBox.Show("Bạn chưa chọn Loại tài khoản!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbTypeAC.Focus();
                return false;
            }
            return true;
        }

        // Các hàm điều khiển khi thêm, sửa, xóa
        private void lockControlAcount()
        {
            textMaNVAC.ReadOnly = true;
            textUserNameAC.ReadOnly = true;
            textPassAcount.ReadOnly = true;
            textRePassAC.ReadOnly = true;
            cmbTypeAC.Enabled = false;
        }

        private void UnlockControlAcount()
        {
            textMaNVAC.ReadOnly = false;
            textUserNameAC.ReadOnly = false;
            textPassAcount.ReadOnly = false;
            textRePassAC.ReadOnly = false;
            cmbTypeAC.Enabled = true;
        }

        //Xoa noi dung trong text box
        private void clearTextBoxAcount()
        {
            textIDAC.Text = "";
            textMaNVAC.Text = "";
            textUserNameAC.Text = "";
            textPassAcount.Text = "";
            textRePassAC.Text = "";
            cmbTypeAC.SelectedIndex = 1;
        }
        // Các Hàm Thêm, Reset, xóa, tìm kiếm Thông tin tài khoản
        private void btnThemAC_Click(object sender, EventArgs e)
        {
            flag = "addAcount";
            UnlockControlAcount();
            clearTextBoxAcount();
        }

        private void btnResetPassAC_Click(object sender, EventArgs e)
        {
            if (textIDAC.Text == "")
            {
                MessageBox.Show("Chưa chọn tài khoản cần Reset Passwords. Kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if(MessageBox.Show("Bạn thực sự muốn Reset mật khẩu tài khoản này?", "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        if (AcountDAO.Instance.ResetPassAcount(Convert.ToInt32(textIDAC.Text)))
                        {
                            MessageBox.Show("Reset Mật khẩu tài khoản thành công ! Mật khẩu mới = '123456'", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadAcountAdmin();
                        }
                        else MessageBox.Show("Lỗi Reset Mật khẩu. Kiểm tra lại kết nối!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch
                    {
                        MessageBox.Show("Lỗi Reset Mật khẩu. ID không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void btnXoaAC_Click(object sender, EventArgs e)
        {
            if (textIDAC.Text == "")
            {
                MessageBox.Show("Chưa chọn tài khoản cần xóa. Kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (MessageBox.Show("Bạn thực sự muốn tài khoản này?", "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        if (AcountDAO.Instance.DeleteAcount(Convert.ToInt32(textIDAC.Text)))
                        {
                            MessageBox.Show("Xóa tài khoản thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadAcountAdmin();
                            clearTextBoxAcount();
                        }
                        else MessageBox.Show("Lỗi xóa tài khoản. Kiểm tra lại kết nối!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch
                    {
                        MessageBox.Show("Lỗi xóa tài khoản. ID không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void btnGhiAC_Click(object sender, EventArgs e)
        {
            if (flag == "addAcount")
            {
                if (checkDataAcount())
                {
                    if(textPassAcount.Text != textRePassAC.Text)
                    {
                        MessageBox.Show("Nhập lại mật khẩu sai. Kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        try
                        {
                            if (AcountDAO.Instance.AddAcount(Convert.ToInt32(textMaNVAC.Text), textUserNameAC.Text, textPassAcount.Text, cmbTypeAC.Text))
                            {
                                MessageBox.Show("Thêm mới tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadAcountAdmin();
                                flag = "";
                            }
                            else MessageBox.Show("Tên đăng nhập đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        catch
                        {
                            MessageBox.Show("Lỗi tạo tài khoản!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }

        // Button Hủy Click
        private void btnHuyAC_Click(object sender, EventArgs e)
        {
            lockControlAcount();
            LoadAcountAdmin();
            //textSearchAC.Text = "";
            clearTextBoxAcount();
            flag = "";
        }

        // Các Hàm tìm kiếm
        private void textSearchAC_TextChanged(object sender, EventArgs e)
        {
            if (textSearchAC.Text == "")
            {
                LoadAcountAdmin();
            }
            else
            {
                dtgvAcount.DataSource = AcountDAO.Instance.SearchAcount(textSearchAC.Text);
            }
        }

        private void btnSearchAC_Click(object sender, EventArgs e)
        {
            if (textSearchAC.Text == "")
            {
                LoadAcountAdmin();
            }
            else
            {
                dtgvAcount.DataSource = AcountDAO.Instance.SearchAcount(textSearchAC.Text);
            }
        }



        //============================== CÁC HÀM XỬ LÝ TAB NHÂN VIÊN/ADMIN ================================

        void LoadNhanVienAdmin()
        {
            dtgvNhanVien.DataSource = NhanVienDAO.Instance.LoadNhanVien();
            lockControlNV();
        }

        // Lựa chọn Nhân viên theo Click chuột, hiển thị thông tin trên Textbox
        private void dtgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index = dtgvNhanVien.CurrentCell == null ? -1 : dtgvNhanVien.CurrentCell.RowIndex;
            if (index != -1)
            {
                textMaNV.Text = dtgvNhanVien.Rows[index].Cells[0].Value.ToString();
                textHoTenNV.Text = dtgvNhanVien.Rows[index].Cells[1].Value.ToString();
                dateNgaySinhNV.Text = dtgvNhanVien.Rows[index].Cells[2].Value.ToString();
                textSoDTNV.Text = dtgvNhanVien.Rows[index].Cells[3].Value.ToString();
            }
        }

        
        //Ham kiem tra thong tin nhap vao
        private bool checkDataNhanVien()
        {
            if (string.IsNullOrWhiteSpace(textHoTenNV.Text))
            {
                MessageBox.Show("Bạn chưa nhập họ tên nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textHoTenNV.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(dateNgaySinhNV.Text))
            {
                MessageBox.Show("Bạn chưa nhập ngày sinh nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dateNgaySinhNV.Focus();
                return false;
            }
            return true;
        }

        // Các hàm điều khiển khi thêm, sửa, xóa
        private void lockControlNV()
        {
            textHoTenNV.ReadOnly = true;
            dateNgaySinhNV.Enabled = false;
            textSoDTNV.ReadOnly = true;
        }

        private void UnlockControlNV()
        {
            textHoTenNV.ReadOnly = false;
            dateNgaySinhNV.Enabled = true;
            textSoDTNV.ReadOnly = false;
        }

        //Xoa noi dung trong text box
        private void clearTextBoxNV()
        {
            textMaNV.Text = "";
            textHoTenNV.Text = "";
            dateNgaySinhNV.Text = "";
            textSoDTNV.Text = "";
            textSearchNV.Text = "";
        }

        // Các Hàm Thêm, Sửa, xóa, tìm kiếm Thông tin nhân viên
        private void btnThemNV_Click(object sender, EventArgs e)
        {
            flag = "addNV";
            UnlockControlNV();
            clearTextBoxNV();
        }

        private void btnSuaNV_Click(object sender, EventArgs e)
        {
            if(textMaNV.Text == "")
            {
                MessageBox.Show("Chưa chọn nhân viên cần sửa. Kiểm tra lại!");
            }
            else
            {
                flag = "editNV";
                UnlockControlNV();
            }
        }

        private void btnXoaNV_Click(object sender, EventArgs e)
        {
            if (textMaNV.Text == "")
            {
                MessageBox.Show("Chưa chọn nhân viên cần xóa. Kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (MessageBox.Show("Bạn thực sự muốn xóa nhân viên này?", "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        if (NhanVienDAO.Instance.DeleteNhanVien(Convert.ToInt32(textMaNV.Text)))
                        {
                            MessageBox.Show("Xóa nhân viên thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadNhanVienAdmin();
                            clearTextBoxNV();
                        }
                        else MessageBox.Show("Lỗi xóa nhân viên. Kiểm tra lại kết nối!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch
                    {
                        MessageBox.Show("Lỗi xóa nhân viên. ID không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void btnGhiNV_Click(object sender, EventArgs e)
        {
            if (flag == "addNV")
            {
                if (checkDataNhanVien())
                {
                    try
                    {
                        if (NhanVienDAO.Instance.AddNhanVien(textHoTenNV.Text, Convert.ToDateTime(dateNgaySinhNV.Text), textSoDTNV.Text))
                        {
                            MessageBox.Show("Thêm mới nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadNhanVienAdmin();
                            flag = "";
                        }
                        else MessageBox.Show("Nhân viên đã tồn tại. Kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch
                    {
                        MessageBox.Show("Lỗi thêm mới nhân viên. Kiểm tra lại kết nối!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else if (flag == "editNV")
            {
                if (checkDataNhanVien())
                {
                    try
                    {
                        if (NhanVienDAO.Instance.EditNhanVien(Convert.ToInt16(textMaNV.Text), textHoTenNV.Text, Convert.ToDateTime(dateNgaySinhNV.Text), textSoDTNV.Text))
                        {
                            MessageBox.Show("Sửa thông tin nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadNhanVienAdmin();
                            flag = "";
                        }
                        else MessageBox.Show("Nhân viên không tồn tại. Kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch
                    {
                        MessageBox.Show("Lỗi sửa thông tin nhân viên. Kiểm tra lại kết nối!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        //Các hàm tìm kiếm nhân viên theo họ tên
        private void btnHuyNhanVien_Click(object sender, EventArgs e)
        {
            lockControlNV();
            LoadNhanVienAdmin();
            //textSearchNV.Text = "";
            clearTextBoxNV();
            flag = "";
        }

        private void btnSearchNV_Click(object sender, EventArgs e)
        {
            if (textSearchNV.Text == "")
            {
                LoadNhanVienAdmin();
            }
            else
            {
                dtgvNhanVien.DataSource = NhanVienDAO.Instance.SearchNhanVien(textSearchNV.Text);
            }
        }

        private void textSearchNV_TextChanged(object sender, EventArgs e)
        {
            if (textSearchNV.Text == "")
            {
                LoadNhanVienAdmin();
            }
            else
            {
                dtgvNhanVien.DataSource = NhanVienDAO.Instance.SearchNhanVien(textSearchNV.Text);
            }
        }

       
        
        //============================== CÁC HÀM XỬ LÝ TAB MÓN ĂN/ADMIN ================================

        void LoadMonAnAdmin()
        {
            dtgvMonAn.DataSource = MonAnDAO.Instance.LoadMonAn();
            lockControlMonAn();
        }

        // Lựa chọn Món ăn theo Click chuột, hiển thị thông tin trên Textbox
        private void dtgvMonAn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index = dtgvMonAn.CurrentCell == null ? -1 : dtgvMonAn.CurrentCell.RowIndex;
            if (index != -1)
            {
                textMaMon.Text = dtgvMonAn.Rows[index].Cells[0].Value.ToString();
                textTenMon.Text = dtgvMonAn.Rows[index].Cells[1].Value.ToString();
                textDonGiaMon.Text = dtgvMonAn.Rows[index].Cells[2].Value.ToString();
            }
        }


        //Ham kiem tra thong tin nhap vao
        private bool checkDataMonAn()
        {
            if (string.IsNullOrWhiteSpace(textTenMon.Text))
            {
                MessageBox.Show("Bạn chưa nhập tên món!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textTenMon.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(textDonGiaMon.Text))
            {
                MessageBox.Show("Bạn chưa nhập đơn giá!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textDonGiaMon.Focus();
                return false;
            }
            return true;
        }

        // Các hàm điều khiển khi thêm, sửa, xóa
        private void lockControlMonAn()
        {
            textTenMon.ReadOnly = true;
            textDonGiaMon.ReadOnly = true;
        }

        private void UnlockControlMonAn()
        {
            textTenMon.ReadOnly = false;
            textDonGiaMon.ReadOnly = false;
        }

        //Xoa noi dung trong text box
        private void clearTextBoxMonAn()
        {
            textTenMon.Text = "";
            textDonGiaMon.Text = "";
            textMaMon.Text = "";
        }

        // Các Hàm Thêm, Sửa, xóa, tìm kiếm Thông tin Món ăn

        private void btnThemMon_Click(object sender, EventArgs e)
        {
            flag = "addMA";
            UnlockControlMonAn();
            clearTextBoxMonAn();
        }

        private void btnSuaMon_Click(object sender, EventArgs e)
        {
            if (textMaMon.Text == "")
            {
                MessageBox.Show("Chưa chọn Món ăn cần sửa. Kiểm tra lại!");
            }
            else
            {
                flag = "editMA";
                UnlockControlMonAn();
            }
        }

        private void btnXoaMon_Click(object sender, EventArgs e)
        {
            if (textMaMon.Text == "")
            {
                MessageBox.Show("Chưa chọn món ăn cần xóa. Kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (MessageBox.Show("Bạn thực sự muốn xóa món ăn này?", "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        if (MonAnDAO.Instance.DeleteMonAn(Convert.ToInt32(textMaMon.Text)))
                        {
                            MessageBox.Show("Xóa món ăn thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadMonAnAdmin();
                            clearTextBoxMonAn();
                            lockControlMonAn();
                        }
                        else MessageBox.Show("Lỗi xóa món ăn. Kiểm tra lại kết nối!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch
                    {
                        MessageBox.Show("Lỗi xóa món ăn. Bạn cần xóa 'Đơn hàng' trước rồi mới có thể xóa món ăn này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void btnGhiMon_Click(object sender, EventArgs e)
        {
            if (flag == "addMA")
            {
                if (checkDataMonAn())
                {
                    try
                    {
                        if (MonAnDAO.Instance.AddMonAn(textTenMon.Text, Convert.ToDecimal(textDonGiaMon.Text)))
                        {
                            MessageBox.Show("Thêm mới Món ăn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadMonAnAdmin();
                            flag = "";
                        }
                        else MessageBox.Show("Món ăn đã tồn tại. Kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch
                    {
                        MessageBox.Show("Lỗi thêm mới Món ăn. Kiểm tra lại kết nối!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else if (flag == "editMA")
            {
                if (checkDataMonAn())
                {
                    try
                    {
                        if (MonAnDAO.Instance.EditMonAn(Convert.ToInt16(textMaMon.Text), textTenMon.Text, Convert.ToDecimal(textDonGiaMon.Text)))
                        {
                            MessageBox.Show("Sửa thông tin Món ăn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadMonAnAdmin();
                            flag = "";
                        }
                        else MessageBox.Show("Món ăn không tồn tại. Kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch
                    {
                        MessageBox.Show("Lỗi sửa thông tin Món ăn. Kiểm tra lại kết nối!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void btnHuyMonAn_Click(object sender, EventArgs e)
        {
            lockControlMonAn();
            LoadMonAnAdmin();
            //textSearchMon.Text = "";
            clearTextBoxMonAn();
            flag = "";
        }

        private void textSearchMon_TextChanged(object sender, EventArgs e)
        {
            if (textSearchMon.Text == "")
            {
                LoadMonAnAdmin();
            }
            else
            {
                dtgvMonAn.DataSource = MonAnDAO.Instance.SearchMonAn(textSearchMon.Text);
            }
        }

        private void btnSearchMon_Click(object sender, EventArgs e)
        {
            if (textSearchMon.Text == "")
            {
                LoadMonAnAdmin();
            }
            else
            {
                dtgvMonAn.DataSource = MonAnDAO.Instance.SearchMonAn(textSearchMon.Text);
            }
        }

        //============================== CÁC HÀM XỬ LÝ TAB THANH TOÁN/ADMIN ================================

        void LoadThanhToanAdmin()
        {
            dtgvThanhToan.DataSource = ThanhToanDAO.Instance.LoadThanhToan();
            lockControlThanhtoan();
        }


        // Lựa chọn Thanh toán theo Click chuột, hiển thị thông tin trên Textbox
        private void dtgvThanhToan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index = dtgvThanhToan.CurrentCell == null ? -1 : dtgvThanhToan.CurrentCell.RowIndex;
            if (index != -1)
            {
                textMaOrderThanhToan.Text = dtgvThanhToan.Rows[index].Cells[0].Value.ToString();
                textMaNVThanhToan.Text = dtgvThanhToan.Rows[index].Cells[1].Value.ToString();
                UpdGiamgiaTT.Text = dtgvThanhToan.Rows[index].Cells[2].Value.ToString();
                textTongTienThanhtoan.Text = dtgvThanhToan.Rows[index].Cells[3].Value.ToString();
                dateThanhtoan.Text = dtgvThanhToan.Rows[index].Cells[4].Value.ToString();
            }
        }


        //Ham kiem tra thong tin nhap vao
        private bool checkDataAddThanhtoan()
        {
            if (string.IsNullOrWhiteSpace(textMaOrderThanhToan.Text))
            {
                MessageBox.Show("Bạn chưa nhập mã Order!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textMaOrderThanhToan.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(UpdGiamgiaTT.Text))
            {
                MessageBox.Show("Bạn chưa nhập giảm giá!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                UpdGiamgiaTT.Focus();
                return false;
            }
            return true;
        }

        private bool checkDataEditThanhtoan()
        {
            if (string.IsNullOrWhiteSpace(textMaOrderThanhToan.Text))
            {
                MessageBox.Show("Bạn chưa nhập mã Order!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textMaOrderThanhToan.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(textMaNVThanhToan.Text))
            {
                MessageBox.Show("Bạn chưa nhập mã nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textMaNVThanhToan.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(UpdGiamgiaTT.Text))
            {
                MessageBox.Show("Bạn chưa nhập giảm giá!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                UpdGiamgiaTT.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(textTongTienThanhtoan.Text))
            {
                MessageBox.Show("Bạn chưa nhập giảm giá!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textTongTienThanhtoan.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(dateThanhtoan.Text))
            {
                MessageBox.Show("Bạn chưa nhập giảm giá!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dateThanhtoan.Focus();
                return false;
            }
            return true;
        }

        // Các hàm điều khiển khi thêm, sửa, xóa
        private void lockControlThanhtoan()
        {
            textMaOrderThanhToan.ReadOnly = true;
            textMaNVThanhToan.ReadOnly = true;
            UpdGiamgiaTT.Enabled = false;
            textTongTienThanhtoan.ReadOnly = true;
            dateThanhtoan.Enabled = false;
        }

        private void UnlockControlThanhtoan()
        {
            textMaOrderThanhToan.ReadOnly = false;
            textMaNVThanhToan.ReadOnly = false;
            UpdGiamgiaTT.Enabled = true;
            textTongTienThanhtoan.ReadOnly = false;
            dateThanhtoan.Enabled = true;
        }

        //Xoa noi dung trong text box
        private void clearTextBoxThanhtoan()
        {
            textMaOrderThanhToan.Text = "";
            textMaNVThanhToan.Text = "";
            UpdGiamgiaTT.Text = "0";
            textTongTienThanhtoan.Text = "";
            dateThanhtoan.Text = DateTime.Now.ToString();
        }


        // Các Hàm Thêm, Sửa, xóa, tìm kiếm Thông tin Thanh toán
        private void btnThemThanhToan_Click(object sender, EventArgs e)
        {
            flag = "addTT";
            textMaOrderThanhToan.ReadOnly = false;
            UpdGiamgiaTT.Enabled = true;
            clearTextBoxThanhtoan();
        }

        private void btnSuaThanhToan_Click(object sender, EventArgs e)
        {
            if (textMaOrderThanhToan.Text == "")
            {
                MessageBox.Show("Chưa chọn Order cần sửa. Kiểm tra lại!");
            }
            else
            {
                flag = "editTT";
                UnlockControlThanhtoan();
            }
        }

        private void btnXoaThanhToan_Click(object sender, EventArgs e)
        {
            if (textMaOrderThanhToan.Text == "")
            {
                MessageBox.Show("Chưa chọn Thanh toán cần xóa. Kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (MessageBox.Show("Bạn thực sự muốn xóa Thanh toán này?", "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        if (ThanhToanDAO.Instance.DeleteThanhtoan(Convert.ToInt32(textMaOrderThanhToan.Text)))
                        {
                            ThanhToanDAO.Instance.HuyThanhToan(Convert.ToInt32(textMaOrderThanhToan.Text));
                            MessageBox.Show("Xóa Thanh toán thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadThanhToanAdmin();
                            clearTextBoxThanhtoan();
                            lockControlThanhtoan();
                        }
                        else MessageBox.Show("Lỗi xóa Thanh toán. Kiểm tra lại kết nối!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch
                    {
                        MessageBox.Show("Lỗi xóa Thanh toán. Kiểm tra lại kết nối!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void btnGhiThanhToan_Click(object sender, EventArgs e)
        {
            if (flag == "addTT")
            {
                if (checkDataAddThanhtoan())
                {
                    try
                    {
                        if (OrderDAO.Instance.GetInfoOrder(Convert.ToInt16(textMaOrderThanhToan.Text)).Status == "Đã thanh toán")
                        {
                            MessageBox.Show("Khách hàng đang chọn đã thanh toán. Kiểm tra lại!");
                        }
                        else
                        {
                            fThanhToan fTT = new fThanhToan();
                            fTT.maOrderTT = Convert.ToInt16(textMaOrderThanhToan.Text);
                            fTT.giamGiaTT = (int)UpdGiamgiaTT.Value;
                            fTT.ShowDialog();
                            LoadThanhToanAdmin();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Order không tồn tại!");
                    }
                }
            }
            else if (flag == "editTT")
            {
                if (checkDataEditThanhtoan())
                {
                    try
                    {
                        int maOrder = Convert.ToInt16(textMaOrderThanhToan.Text);
                        int maNV = Convert.ToInt16(textMaNVThanhToan.Text);
                        int giamGia = (int)UpdGiamgiaTT.Value;
                        Decimal tongTien = Convert.ToDecimal(textTongTienThanhtoan.Text);
                        DateTime thoiGianTT = Convert.ToDateTime(dateThanhtoan.Text);
                        if (NhanVienDAO.Instance.CheckMaNhanVien(maNV))
                        {
                            if (ThanhToanDAO.Instance.EditThanhtoan(maOrder, maNV, giamGia, tongTien, thoiGianTT))
                            {
                                MessageBox.Show("Sửa thông tin Thanh toán thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadThanhToanAdmin();
                                flag = "";
                            }
                            else MessageBox.Show("Thanh toán không tồn tại. Kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else MessageBox.Show("Nhân viên không tồn tại. Kiểm tra lại!");
                    }
                    catch
                    {
                        MessageBox.Show("Lỗi sửa thông tin Thanh toán. Kiểm tra lại kết nối!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void btnHuyThanhtoan_Click(object sender, EventArgs e)
        {
            lockControlThanhtoan();
            LoadThanhToanAdmin();
            clearTextBoxThanhtoan();
            flag = "";
        }

        private void btnSearchThanhToan_Click(object sender, EventArgs e)
        {
            if (textSearchThanhToan.Text == "")
            {
                LoadThanhToanAdmin();
            }
            else
            {
                dtgvThanhToan.DataSource = ThanhToanDAO.Instance.SearchThanhtoan(textSearchThanhToan.Text);
            }
        }

        private void textSearchThanhToan_TextChanged(object sender, EventArgs e)
        {
            if (textSearchThanhToan.Text == "")
            {
                LoadThanhToanAdmin();
            }
            else
            {
                dtgvThanhToan.DataSource = ThanhToanDAO.Instance.SearchThanhtoan(textSearchThanhToan.Text);
            }
        }



        //============================== CÁC HÀM XỬ LÝ TAB ĐƠN HÀNG/ADMIN ================================

        void LoadDonHangAdmin()
        {
            dtgvDonHang.DataSource = DonHangDAO.Instance.LoadDonHang();
            lockControlDonHang();
        }

        // Lựa chọn Đơn hàng theo Click chuột, hiển thị thông tin trên Textbox
        private void dtgvDonHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index = dtgvDonHang.CurrentCell == null ? -1 : dtgvDonHang.CurrentCell.RowIndex;
            if (index != -1)
            {
                textMaOrderDonHang.Text = dtgvDonHang.Rows[index].Cells[0].Value.ToString();
                textMamonAdmin.Text = dtgvDonHang.Rows[index].Cells[1].Value.ToString();
                UpdSoLuongMonAnAdmin.Text = dtgvDonHang.Rows[index].Cells[2].Value.ToString();
                textDonGiaDonHang.Text = dtgvDonHang.Rows[index].Cells[3].Value.ToString();
                textThanhtieDonHang.Text = dtgvDonHang.Rows[index].Cells[4].Value.ToString();
                textGhiChuDonHang.Text = dtgvDonHang.Rows[index].Cells[5].Value.ToString();
                cmbMonAnAdmin.Text = MonAnDAO.Instance.GetInfoMonAnFromComboBox(Convert.ToInt16(dtgvDonHang.Rows[index].Cells[1].Value.ToString())).TenMonAn;
            }
        }

        //Load danh sách món ăn lên ComboBox
        private void LoadMonAnCmboxAdmin()
        {
            cmbMonAnAdmin.DataSource = MainDAO.Instance.LoadMonAnCmbox();
            cmbMonAnAdmin.DisplayMember = "tenMOn";
            cmbMonAnAdmin.ValueMember = "maMon";
        }

        //Lấy giá trị mã món của món ăn tương ứng được chọn
        private void cmbMonAnAdmin_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbMonAnAdmin.SelectedValue != null)
            {
                textMamonAdmin.Text = cmbMonAnAdmin.SelectedValue.ToString();
            }
        }


        //Ham kiem tra thong tin nhap vao
        private bool checkDataDonHang()
        {
            if (string.IsNullOrWhiteSpace(textMaOrderDonHang.Text))
            {
                MessageBox.Show("Bạn chưa nhập Mã Order!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textMaOrderDonHang.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(textMamonAdmin.Text))
            {
                MessageBox.Show("Bạn chưa nhập Mã món ăn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textMamonAdmin.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(UpdSoLuongMonAnAdmin.Text))
            {
                MessageBox.Show("Bạn chưa nhập Số lượng món ăn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                UpdSoLuongMonAnAdmin.Focus();
                return false;
            }
            return true;
        }

        // Các hàm điều khiển khi thêm, sửa, xóa
        private void lockControlDonHang()
        {
            textMaOrderDonHang.ReadOnly = true;
            cmbMonAnAdmin.Enabled = false;
            UpdSoLuongMonAnAdmin.ReadOnly = true;
            textGhiChuDHAdmin.ReadOnly = true;
        }

        private void UnlockControlDonHang()
        {
            textMaOrderDonHang.ReadOnly = false;
            cmbMonAnAdmin.Enabled = true;
            UpdSoLuongMonAnAdmin.ReadOnly = false;
            textGhiChuDHAdmin.ReadOnly = false;
        }

        //Xoa noi dung trong text box
        private void clearTextBoxDonHang()
        {
            textMaOrderDonHang.Text = "";
            textMamonAdmin.Text = "";
            UpdSoLuongMonAnAdmin.Text = "";
            textDonGiaDonHang.Text = "";
            textThanhtieDonHang.Text = "";
            textGhiChuDHAdmin.Text = "";
        }

        // Các Hàm Thêm, Sửa, xóa, tìm kiếm Thông tin Đơn hàng
        private void btnThemDonHang_Click(object sender, EventArgs e)
        {
            flag = "addDH";
            UnlockControlDonHang();
            LoadMonAnCmboxAdmin();
            clearTextBoxDonHang();
        }

        private void btnSuaDonHang_Click(object sender, EventArgs e)
        {
            if (textMaOrderDonHang.Text == "")
            {
                MessageBox.Show("Chưa chọn Đơn hàng cần sửa. Kiểm tra lại!");
            }
            else
            {
                flag = "editDH";
                textMaOrderDonHang.ReadOnly = true;
                UpdSoLuongMonAnAdmin.ReadOnly = false;
                textGhiChuDHAdmin.ReadOnly = false;
                textDonGiaDonHang.Text = "";
                textThanhtieDonHang.Text = "";
            }
        }

        private void btnXoaDonHang_Click(object sender, EventArgs e)
        {
            if (textMaOrderDonHang.Text == "" || textMamonAdmin.Text == "")
            {
                MessageBox.Show("Chưa chọn Đơn hàng. Kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (MessageBox.Show("Bạn thực sự muốn xóa Đơn hàng này?", "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        if (DonHangDAO.Instance.DeleteDonHang(Convert.ToInt32(textMaOrderDonHang.Text), Convert.ToInt32(textMamonAdmin.Text)))
                        {
                            MessageBox.Show("Xóa Đơn hàng thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDonHangAdmin();
                            clearTextBoxDonHang();
                        }
                        else MessageBox.Show("Lỗi xóa Đơn hàng. Kiểm tra lại kết nối!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch
                    {
                        MessageBox.Show("Lỗi xóa Đơn hàng. Bạn cần xóa 'Thanh toán' trước rồi mới có thể xóa Đơn hàng này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void btnGhiDonHang_Click(object sender, EventArgs e)
        {
            if (flag == "addDH")
            {
                if (checkDataDonHang())
                {
                    try
                    {
                        int soLuong = (int)UpdSoLuongMonAnAdmin.Value;
                        if (soLuong != 0)
                        {
                            if (OrderDAO.Instance.GetInfoOrder(Convert.ToInt16(textMaOrderDonHang.Text)).Status == "Đã thanh toán")
                            {
                                MessageBox.Show("Khách hàng này đã thanh toán. Đề nghị nhập vào Order khác!");
                            }
                            else
                            {
                                int maOrder = Convert.ToInt16(textMaOrderDonHang.Text);
                                int maMonM = Convert.ToInt32(textMamonAdmin.Text);
                                Decimal donGiaMon = MonAnDAO.Instance.GetInfoMonAnFromComboBox(maMonM).DonGiaMonAn;
                                Decimal thanhTien = donGiaMon * soLuong;
                                DonHangDAO.Instance.ThemDonHang(maOrder, maMonM, soLuong, donGiaMon, thanhTien);
                                LoadDonHangAdmin();
                                flag = "";
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Order không tồn tại!");
                    }
                }
            }
            else if (flag == "editDH")
            {
                if (checkDataDonHang())
                {
                    try
                    {
                        int soLuong = (int)UpdSoLuongMonAnAdmin.Value;
                        if (soLuong > 0)
                        {
                            int maOrder = Convert.ToInt16(textMaOrderDonHang.Text);
                            int maMonM = Convert.ToInt32(textMamonAdmin.Text);
                            Decimal donGiaMon = MonAnDAO.Instance.GetInfoMonAnFromComboBox(maMonM).DonGiaMonAn;
                            Decimal thanhTien = donGiaMon * soLuong;
                            DonHangDAO.Instance.EditDonHang(maOrder, maMonM, soLuong, donGiaMon, thanhTien);
                            LoadDonHangAdmin();
                            flag = "";
                        }
                        else MessageBox.Show("Số lượng nhập vào phải > 0");

                    }
                    catch
                    {
                        MessageBox.Show("Lỗi sửa thông tin Thanh toán. Kiểm tra lại kết nối!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        

        private void btnHuyDonHang_Click(object sender, EventArgs e)
        {
            lockControlDonHang();
            LoadDonHangAdmin();
            clearTextBoxDonHang();
            flag = "";
        }

        private void btnSearchDonHang_Click(object sender, EventArgs e)
        {
            if (textSearchDonHang.Text == "")
            {
                LoadDonHangAdmin();
            }
            else
            {
                dtgvDonHang.DataSource = DonHangDAO.Instance.SearchDonHang(textSearchDonHang.Text);
            }
        }

        private void textSearchDonHang_TextChanged(object sender, EventArgs e)
        {
            if (textSearchDonHang.Text == "")
            {
                LoadDonHangAdmin();
            }
            else
            {
                dtgvDonHang.DataSource = DonHangDAO.Instance.SearchDonHang(textSearchDonHang.Text);
            }
        }

        private void btnXemTTCTKH_Click(object sender, EventArgs e)
        {
            try
            {
                if (textMaOrderThanhToan.Text == "")
                {
                    MessageBox.Show("Vui lòng chọn Khách hàng đã thanh toán trước!");
                }
                else
                {
                    fInfoKHDaThanhToan fTT = new fInfoKHDaThanhToan();
                    fTT.maOrderTT = Convert.ToInt16(textMaOrderThanhToan.Text);
                    fTT.ShowDialog();
                }
            }
            catch
            {
                MessageBox.Show("Thông tin thanh toán không tồn tại!");
            }
        }

        
    }
}
