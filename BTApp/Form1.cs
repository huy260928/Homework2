using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        private void CapNhatThongKe()
        {
            int nam = 0;
            int nu = 0;
            foreach (DataGridViewRow row in dgvSinhVien.Rows)
            {
                if (row.Cells["GioiTinh"].Value?.ToString() == "Nam")
                {
                    nam++;
                }
                else
                {
                    nu++;
                }
            }
            lblTongSVNam.Text = nam.ToString();
            lblTongSVNu.Text = nu.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 2.1 Khi mới Load Form
            // Khởi tạo các khoa cho ComboBox
            cboKhoa.Items.Add("QTKD");
            cboKhoa.Items.Add("CNTT");
            cboKhoa.Items.Add("NNA");
            cboKhoa.SelectedIndex = 0; // Mặc định chọn QTKD

            // Mặc định Giới tính Nữ được checked
            rdoNu.Checked = true;

            // Thiết lập DataGridView
            dgvSinhVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Chọn toàn bộ dòng
            dgvSinhVien.AllowUserToAddRows = false;
        }

        private void btnThemSua_Click(object sender, EventArgs e)
        {
            // Kiểm tra các thông tin bắt buộc
            if (string.IsNullOrWhiteSpace(txtMaSV.Text) || string.IsNullOrWhiteSpace(txtHoTen.Text) || string.IsNullOrWhiteSpace(txtDiemTB.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kiểm tra điểm TB có phải là số
            double diemTB;
            if (!double.TryParse(txtDiemTB.Text, out diemTB))
            {
                MessageBox.Show("Điểm trung bình phải là một số!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Lấy thông tin từ các control
            string maSV = txtMaSV.Text.Trim();
            string hoTen = txtHoTen.Text.Trim();
            string gioiTinh = rdoNam.Checked ? "Nam" : "Nữ";
            string tenKhoa = cboKhoa.SelectedItem.ToString();

            // Tìm kiếm MSSV trong DataGridView để kiểm tra thêm hay sửa
            DataGridViewRow rowToUpdate = null;
            foreach (DataGridViewRow row in dgvSinhVien.Rows)
            {
                if (row.Cells["MaSV"].Value?.ToString() == maSV)
                {
                    rowToUpdate = row;
                    break;
                }
            }

            if (rowToUpdate != null)
            {
                // Sửa: Nếu đã có MSSV thì cập nhật dữ liệu
                rowToUpdate.Cells["HoTen"].Value = hoTen;
                rowToUpdate.Cells["GioiTinh"].Value = gioiTinh;
                rowToUpdate.Cells["DiemTB"].Value = diemTB;
                rowToUpdate.Cells["TenKhoa"].Value = tenKhoa;
                MessageBox.Show("Cập nhật dữ liệu thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Thêm: Nếu chưa có MSSV thì thêm mới
                dgvSinhVien.Rows.Add(maSV, hoTen, gioiTinh, diemTB, tenKhoa);
                MessageBox.Show("Thêm mới dữ liệu thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // Cập nhật lại số lượng sinh viên Nam/Nữ
            CapNhatThongKe();

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maSV = txtMaSV.Text.Trim();

            DataGridViewRow rowToDelete = null;
            foreach (DataGridViewRow row in dgvSinhVien.Rows)
            {
                if (row.Cells["MaSV"].Value?.ToString() == maSV)
                {
                    rowToDelete = row;
                    break;
                }
            }

            if (rowToDelete == null)
            {
                MessageBox.Show("Không tìm thấy MSSV cần xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa sinh viên có MSSV: {maSV}?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                dgvSinhVien.Rows.Remove(rowToDelete);
                MessageBox.Show("Xóa sinh viên thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CapNhatThongKe();
            }
        }

        private void dgvSinhVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSinhVien.Rows[e.RowIndex];

                txtMaSV.Text = row.Cells["MaSV"].Value?.ToString();
                txtHoTen.Text = row.Cells["HoTen"].Value?.ToString();
                txtDiemTB.Text = row.Cells["DiemTB"].Value?.ToString();

                string gioiTinh = row.Cells["GioiTinh"].Value?.ToString();
                if (gioiTinh == "Nam")
                {
                    rdoNam.Checked = true;
                }
                else
                {
                    rdoNu.Checked = true;
                }

                string tenKhoa = row.Cells["TenKhoa"].Value?.ToString();
                cboKhoa.SelectedItem = tenKhoa;
            }
        }
    }
}
