using form1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace form1
{
    public partial class frmTimKiem : Form
    {
        public frmTimKiem()
        {
            InitializeComponent();
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmTimKiem_Load(object sender, EventArgs e)
        {
            StudentContextDB contextDB = new StudentContextDB();
            List<Faculty> listKhoa = contextDB.Faculties.ToList();
            List<Student> listStudent = contextDB.Students.ToList();
            cmbKhoa.DataSource = listKhoa;
            cmbKhoa.DisplayMember = "FacultyName";
            cmbKhoa.ValueMember = "FacultyID";
            BindGrid(listStudent);

        }

        private void BindGrid(List<Student> listStudent)
        {// 1. Xóa tất cả các hàng hiện có
            dgvtimkiem.Rows.Clear();

            // 2. Lặp qua danh sách sinh viên và thêm vào lưới
            foreach (var item in listStudent)
            {
                // Lấy chỉ số hàng mới được thêm vào
                int index = dgvtimkiem.Rows.Add();

                // Gán giá trị vào các Cell theo chỉ số cột (0, 1, 2, 3)
                dgvtimkiem.Rows[index].Cells[0].Value = item.StudentID;
                dgvtimkiem.Rows[index].Cells[1].Value = item.FullName;

                // Lấy tên Khoa qua thuộc tính Navigation Property 'Faculty'
                dgvtimkiem.Rows[index].Cells[2].Value = item.Faculty.FacultyName;

                // Điểm trung bình
                dgvtimkiem.Rows[index].Cells[3].Value = item.AverageScore;
            }
        }

        private void btntimkiem_Click(object sender, EventArgs e)
        {
            try
            {
                StudentContextDB context = new StudentContextDB();

                // Bắt đầu truy vấn từ tất cả sinh viên
                IQueryable<Student> studentsQuery = context.Students;

                // Lấy tiêu chí tìm kiếm (Giả định txtMSSV và txtHoTen)
                string studentID = txtStudentId.Text.Trim();
                string fullName = txtFullName.Text.Trim();
                int selectedFacultyID = 0;

                // Xử lý giá trị ComboBox: Lấy FacultyID nếu có mục được chọn
                if (cmbKhoa.SelectedValue != null && int.TryParse(cmbKhoa.SelectedValue.ToString(), out int facultyID))
                {
                    selectedFacultyID = facultyID;
                }

                // Áp dụng Lọc theo Mã Sinh Viên (tìm kiếm chính xác)
                if (!string.IsNullOrEmpty(studentID))
                {
                    studentsQuery = studentsQuery.Where(s => s.StudentID == studentID);
                }

                // Áp dụng Lọc theo Họ Tên (tìm kiếm chứa/gần đúng)
                if (!string.IsNullOrEmpty(fullName))
                {
                    // Sử dụng Contains để tìm kiếm gần đúng
                    studentsQuery = studentsQuery.Where(s => s.FullName.Contains(fullName));
                }

                // Áp dụng Lọc theo Khoa (chỉ lọc nếu một khoa cụ thể được chọn)
                if (selectedFacultyID > 0)
                {
                    studentsQuery = studentsQuery.Where(s => s.FacultyID == selectedFacultyID);
                }

                // Thực thi truy vấn và Bind kết quả lên Grid
                List<Student> listResult = studentsQuery.ToList();
                // *** PHẦN THIẾU ĐƯỢC THÊM VÀO ***
                BindGrid(listResult);
                // ******************************

                if (listResult.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy sinh viên nào phù hợp với tiêu chí.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tìm kiếm dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Sự kiện Xóa (Chắc chắn là xóa tiêu chí tìm kiếm)
        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Xóa nội dung tìm kiếm
            txtStudentId.Text = string.Empty;
            txtFullName.Text = string.Empty;
            cmbKhoa.SelectedIndex = -1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmsinhvien formQuanLySinhVien = new frmsinhvien();
            formQuanLySinhVien.Show();

            // 2. Đóng form tìm kiếm (frmTimKiem) hiện tại
            // (Đóng form sau khi mở form mới để tránh ứng dụng bị thoát đột ngột nếu đây là form cuối cùng)
            this.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dgvtimkiem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
    }

