using form1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace form1
{
    public partial class frmQuanLyKhoa : Form
    {
        public frmQuanLyKhoa()
        {
            InitializeComponent();
        }

        private void frmQuanLyKhoa_Load(object sender, EventArgs e)
        {
            StudentContextDB contextDB = new StudentContextDB();
            List<Faculty> listKhoa = contextDB.Faculties.ToList();

            BindGrid(listKhoa);
            

        }
     

        private void BindGrid(List<Faculty> listKhoa)
        {

            dgvkhoa.Rows.Clear();
            foreach (var item in listKhoa)
            {
                int index = dgvkhoa.Rows.Add();
                dgvkhoa.Rows[index].Cells[0].Value = item.FacultyID;
                dgvkhoa.Rows[index].Cells[1].Value = item.FacultyName;
               
                

            }
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaKhoa.Text) || string.IsNullOrWhiteSpace(txtTenKhoa.Text))
            {
                MessageBox.Show("Mã Khoa và Tên Khoa không được để trống.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {

                StudentContextDB context = new StudentContextDB();


                int facultyID = int.Parse(txtMaKhoa.Text);
                Faculty existingFaculty = context.Faculties.FirstOrDefault(f => f.FacultyID == facultyID);

                // Giả định: TotalProfessor là số nguyên
                int totalProfessor = 0;
                if (!string.IsNullOrWhiteSpace(txtMaSoGS.Text))
                {
                    totalProfessor = int.Parse(txtMaSoGS.Text);
                }

                if (existingFaculty == null)
                {
                    // THÊM MỚI (Mã Khoa không tồn tại)

                    // 3. Tạo đối tượng Khoa mới
                    var newFaculty = new Faculty
                    {
                        FacultyID = facultyID,
                        FacultyName = txtTenKhoa.Text
                        
                    };

                    // 4. Thêm vào Context và lưu
                    context.Faculties.Add(newFaculty);
                    context.SaveChanges();

                    MessageBox.Show("Thêm Khoa mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {

                    existingFaculty.FacultyName = txtTenKhoa.Text;
                  


                    context.SaveChanges();

                    MessageBox.Show("Cập nhật thông tin Khoa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


                BindGrid(context.Faculties.ToList());
            }
            catch (FormatException)
            {
                MessageBox.Show("Dữ liệu không hợp lệ. Vui lòng kiểm tra lại Mã Khoa và Tổng Số GS phải là số.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi thao tác dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaKhoa.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã Khoa cần xoá.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                StudentContextDB context = new StudentContextDB();


                int facultyIDToDelete = int.Parse(txtMaKhoa.Text);


                Faculty facultyToDelete = context.Faculties.FirstOrDefault(f => f.FacultyID == facultyIDToDelete);

                if (facultyToDelete != null)
                {

                    int studentCount = context.Students.Count(s => s.FacultyID == facultyIDToDelete);
                    if (studentCount > 0)
                    {
                        MessageBox.Show($"Không thể xoá Khoa vì có {studentCount} sinh viên thuộc Khoa này.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Xác nhận xóa
                    DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xoá Khoa {facultyToDelete.FacultyName}?", "Xác nhận xoá", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        context.Faculties.Remove(facultyToDelete);
                        context.SaveChanges();

                        BindGrid(context.Faculties.ToList());
                        MessageBox.Show("Khoa đã được xoá thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Mã Khoa không tìm thấy!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Mã Khoa phải là số nguyên hợp lệ.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Lỗi khi xoá dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            frmsinhvien formQuanLySinhVien = new frmsinhvien();
            formQuanLySinhVien.Show();

            // 2. Đóng form tìm kiếm (frmTimKiem) hiện tại
            // (Đóng form sau khi mở form mới để tránh ứng dụng bị thoát đột ngột nếu đây là form cuối cùng)
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
    }

