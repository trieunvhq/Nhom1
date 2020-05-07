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
    public partial class fMain : Form
    {
        private string flag;
        private int index;
        public fMain()
        {
            InitializeComponent();           
        }

        private string maMonMain;
        private int maOdMain;

        //Load Form Main
        private void fMain_Load(object sender, EventArgs e)
        {
            if (NhanVien.Instance.QuyenHan == "Tài khoản thường")
            {
                MenuAdmin.Enabled = false;            
            }
            helloLogin.Text = "Xin chào: " + NhanVien.Instance.HoTenNV + " !";
            LoadOrderMain();
            LoadMonAnCmbox();

        }

        //người quản trị Admin
        private void MenuAdmin_Click(object sender, EventArgs e)
        {
            fAdmin f = new fAdmin();
            f.ShowDialog();
            LoadOrderMain();

        }

        //Load data Order lên dtgvOrderMain
        public void LoadOrderMain()
        {
            dtgvOrderMain.DataSource = OrderDAO.Instance.LoadOrder();
            lockControlOrder();
        }

        //Load Đơn hàng theo mã Order lên dtgvDonHang
        void LoadDonHangMain(int maOrder)
        {
            dtgvDonHangMain.DataSource = DonHangDAO.Instance.LoadDonHang(maOrder);        
        }

        // Lấy thông tin Order theo vị trí Click chuột
        private void dtgvOrderMain_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index = dtgvOrderMain.CurrentCell == null ? -1 : dtgvOrderMain.CurrentCell.RowIndex;
            if (index != -1)
            {
                textMaOrder.Text = dtgvOrderMain.Rows[index].Cells[0].Value.ToString();
                textTenOrder.Text = dtgvOrderMain.Rows[index].Cells[1].Value.ToString();
                dateOrderMain.Text = dtgvOrderMain.Rows[index].Cells[2].Value.ToString();
                textGhiChu.Text = dtgvOrderMain.Rows[index].Cells[4].Value.ToString();
                maOdMain = Convert.ToInt32(dtgvOrderMain.Rows[index].Cells[0].Value);
                LoadDonHangMain(maOdMain);
            }
        }

        //Load danh sách món ăn lên ComboBox
        private void LoadMonAnCmbox()
        {
            cmbMonAn.DataSource = MainDAO.Instance.LoadMonAnCmbox();
            cmbMonAn.DisplayMember = "tenMOn";
            cmbMonAn.ValueMember = "maMon";
        }

        //Lấy giá trị mã món của món ăn tương ứng được chọn
        private void cmbMonAn_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbMonAn.SelectedValue != null)
            {
                maMonMain = cmbMonAn.SelectedValue.ToString();
            }
            
        }

        // Thêm món ăn vào Order
        private void btnThemMon_Click(object sender, EventArgs e)
        {
            int soLuong = (int)UpdSoLuongMonAn.Value;
            if(textMaOrder.Text == "")
            {
                MessageBox.Show("Vui lòng chọn Khách hàng trước!");
            }
            else
            {
                if (soLuong != 0)
                {
                    if (OrderDAO.Instance.GetInfoOrder(Convert.ToInt16(textMaOrder.Text)).Status == "Đã thanh toán")
                    {
                        MessageBox.Show("Khách hàng đang chọn đã thanh toán. Đề nghị Tạo Order mới cho khách hàng này!");
                    }
                    else
                    {
                        int maMonM = Convert.ToInt32(maMonMain);
                        Decimal donGiaMon = MonAnDAO.Instance.GetInfoMonAnFromComboBox(maMonM).DonGiaMonAn;
                        Decimal thanhTien = donGiaMon * soLuong;
                        DonHangDAO.Instance.ThemDonHang(maOdMain, maMonM, soLuong, donGiaMon, thanhTien);
                        LoadDonHangMain(maOdMain);
                    }
                }
            }
        }

        // Thanh toán Order
        private void btnThanhtoan_Click(object sender, EventArgs e)
        {
            try
            {
                if (textMaOrder.Text == "")
                {
                    MessageBox.Show("Vui lòng chọn Khách hàng trước!");
                }
                else
                {
                    if (OrderDAO.Instance.GetInfoOrder(Convert.ToInt16(textMaOrder.Text)).Status == "Đã thanh toán")
                    {
                        MessageBox.Show("Khách hàng đang chọn đã thanh toán. Kiểm tra lại!");
                    }
                    else
                    {
                        fThanhToan fTT = new fThanhToan();
                        fTT.maOrderTT = maOdMain;
                        fTT.giamGiaTT = (int)UpdGiamGia.Value;
                        fTT.ShowDialog();
                        LoadOrderMain();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Order không tồn tại!");
            }
        }

        //Tìm kiếm Order theo tên
        private void textSearchOrderMain_TextChanged(object sender, EventArgs e)
        {
            if (textSearchOrderMain.Text == "")
            {
                LoadOrderMain();
            }
            else
            {
                dtgvOrderMain.DataSource = OrderDAO.Instance.SearchOrder(textSearchOrderMain.Text);
            }
        }

        private void btnCheckOrder_Click(object sender, EventArgs e)
        {
            if (textSearchOrderMain.Text == "")
            {
                LoadOrderMain();
            }
            else
            {
                dtgvOrderMain.DataSource = OrderDAO.Instance.SearchOrder(textSearchOrderMain.Text);
            }
        }

        // Thêm order
        private void btnThemOrder_Click(object sender, EventArgs e)
        {
            flag = "add";
            UnlockControlOrder();
            clearTextBoxOrder();
            textTenOrder.Focus();
        }

        // Sửa order
        private void btnSuaOrder_Click(object sender, EventArgs e)
        {
            if (textMaOrder.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn Order. Kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                flag = "edit";
                UnlockControlOrder();
            }
        }

        // Xóa order
        private void btnXoaOrder_Click(object sender, EventArgs e)
        {
            if (textMaOrder.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn Order. Kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (MessageBox.Show("Bạn thực sự muốn xóa Order này?", "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        if (OrderDAO.Instance.DeleteOrder(maOdMain))
                        {
                            MessageBox.Show("Xóa Order thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadOrderMain();
                        }
                        else MessageBox.Show("Lỗi xóa Order. Kiểm tra lại kết nối!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    catch
                    {
                        MessageBox.Show("Lỗi xóa Order. Bạn cần xóa 'đơn hàng', 'thanh toán' trước khi xóa Order này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        //Hủy
        private void btnHuyOrder_Click(object sender, EventArgs e)
        {
            lockControlOrder();
            clearTextBoxOrder();
            LoadOrderMain();
            LoadDonHangMain(0);
            flag = "";
        }

        // Ghi nội dung thêm, sửa Order
        private void btnGhiOrder_Click(object sender, EventArgs e)
        {
            if (flag == "add")
            {
                if (checkDataOrder())
                {
                    try
                    {
                        if (OrderDAO.Instance.AddOrder(textTenOrder.Text, Convert.ToDateTime(dateOrderMain.Text), textGhiChu.Text))
                        {
                            MessageBox.Show("Thêm mới Order thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadOrderMain();
                        }
                        else MessageBox.Show("Lỗi thêm Order. Kiểm tra lại kết nối!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    catch
                    {
                        MessageBox.Show("Lỗi thêm Order. Kiểm tra lại kết nối!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else if (flag == "edit")
            {
                if (checkDataOrder())
                {
                    try
                    {
                        if (OrderDAO.Instance.EditOrder(maOdMain, textTenOrder.Text, Convert.ToDateTime(dateOrderMain.Text), textGhiChu.Text))
                        {
                            MessageBox.Show("Sửa thông tin Order thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadOrderMain();
                        }
                        else MessageBox.Show("Lỗi sửa Order. Kiểm tra lại kết nối!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    catch
                    {
                        MessageBox.Show("Lỗi sửa Order. Kiểm tra lại kết nối!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        //Ham kiem tra thong tin nhap vao
        private bool checkDataOrder()
        {
            if (string.IsNullOrWhiteSpace(textTenOrder.Text))
            {
                MessageBox.Show("Bạn chưa nhập tên Order!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textTenOrder.Focus();
                return false;
            }
            return true;
        }

        // Các hàm điều khiển khi thêm, sửa, xóa
        private void lockControlOrder()
        {
            textTenOrder.ReadOnly = true;
            dateOrderMain.Enabled = false;
            textGhiChu.ReadOnly = true;
        }

        private void UnlockControlOrder()
        {
            textTenOrder.ReadOnly = false;
            dateOrderMain.Enabled = true;
            textGhiChu.ReadOnly = false;
        }

        //Xoa noi dung trong text box
        private void clearTextBoxOrder()
        {
            textMaOrder.Text = "";
            textTenOrder.Text = "";
            dateOrderMain.Text = DateTime.Now.ToString();
            textGhiChu.Text = "";
        }

        private void MenuAcountInfo_Click(object sender, EventArgs e)
        {
            fInfoAcount f = new fInfoAcount();
            f.ShowDialog();
        }

        private void MenuAcountDangXuat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn đăng xuất không?", "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                this.Close();
            }
        }





    }
}
