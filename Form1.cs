using Lab07.BLL;
using Lab07.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Data.Entity.Validation; // <--- Đã thêm
using System.Diagnostics;

namespace Lab07.GUI
{
    public partial class Form1 : Form
    {
        private readonly StudentService studentService = new StudentService();
        private readonly FacultyService facultyService = new FacultyService();
        public Form1()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.Form1_Load);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                setGridViewStyle();
                var listFacultys = facultyService.GetAll();
                var listStudents = studentService.GetAll();
                FillFalcultyCombobox(listFacultys);
                BindGrid(listStudents);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); // <--- Đã sửa
                Debug.WriteLine(ex.ToString());
            }
        }
        //Hàm binding list dữ liệu khoa vào combobox có tên hiện thị là tên khoa,
        private void FillFalcultyCombobox(List<Faculty> listFacultys)
        {
            // Insert a clear placeholder so the user must pick a real faculty
            var placeholder = new Faculty { FacultyID = 0, FacultyName = "-- Select faculty --" };
            listFacultys.Insert(0, placeholder);

            this.cmbFaculty.DataSource = listFacultys;
            this.cmbFaculty.DisplayMember = "FacultyName";
            this.cmbFaculty.ValueMember = "FacultyID";
            this.cmbFaculty.SelectedIndex = 0;

        }
        private void BindGrid(List<Student> ListStudent)
        {
            dgvStudent.Rows.Clear();
            foreach (var item in ListStudent)
            {
                int index = dgvStudent.Rows.Add();
                var row = dgvStudent.Rows[index];

                // Prefer named columns if they exist, fallback to indices (Cải tiến)
                if (dgvStudent.Columns.Contains("StudentID"))
                    row.Cells["StudentID"].Value = item.StudentID;
                else
                    row.Cells[0].Value = item.StudentID;

                if (dgvStudent.Columns.Contains("FullName"))
                    row.Cells["FullName"].Value = item.FullName;
                else
                    row.Cells[1].Value = item.FullName;

                // Store FacultyID (used for cmb selection). Also set FacultyName if column exists.
                if (dgvStudent.Columns.Contains("FacultyID"))
                    row.Cells["FacultyID"].Value = item.FacultyID;
                else
                    row.Cells[2].Value = item.FacultyID;

                if (dgvStudent.Columns.Contains("FacultyName"))
                    row.Cells["FacultyName"].Value = item.Faculty?.FacultyName ?? "";

                // AverageScore -> store as string for display
                if (dgvStudent.Columns.Contains("AverageScore"))
                    row.Cells["AverageScore"].Value = item.AverageScore.ToString("0.##");
                else
                    row.Cells[3].Value = item.AverageScore.ToString("0.##");

                if (dgvStudent.Columns.Contains("Major"))
                    row.Cells["Major"].Value = item.Major?.Name ?? "";
                else
                    row.Cells[4].Value = item.Major?.Name ?? "";

                if (dgvStudent.Columns.Contains("Avatar"))
                    row.Cells["Avatar"].Value = item.Avatar ?? "";
                else
                    row.Cells[5].Value = item.Avatar ?? "";
            }
        }

        public void setGridViewStyle()
        {
            dgvStudent.BorderStyle = BorderStyle.None;
            dgvStudent.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgvStudent.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvStudent.BackgroundColor = Color.White;
            dgvStudent.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvStudent.Rows[e.RowIndex];

                // Set txtID correctly (Ưu tiên tên cột)
                txtID.Text = (dgvStudent.Columns.Contains("StudentID") ?
                    row.Cells["StudentID"].Value : row.Cells[0].Value)?.ToString() ?? "";

                txtName.Text = (dgvStudent.Columns.Contains("FullName") ?
                    row.Cells["FullName"].Value : row.Cells[1].Value)?.ToString() ?? "";

                // Get faculty id from the cell and set combobox selected value safely
                object facultyCellValue = dgvStudent.Columns.Contains("FacultyID") ?
                    row.Cells["FacultyID"].Value : row.Cells[2].Value;

                if (facultyCellValue != null && int.TryParse(facultyCellValue.ToString(), out int facultyId))
                {
                    cmbFaculty.SelectedValue = facultyId;
                }
                else
                {
                    // If no faculty id, reset selection (optional)
                    cmbFaculty.SelectedIndex = 0;
                }

                txtAverageScore.Text = (dgvStudent.Columns.Contains("AverageScore") ?
                    row.Cells["AverageScore"].Value : row.Cells[3].Value)?.ToString() ?? "";

                // Tải ảnh Avatar
                string avatarFile = (dgvStudent.Columns.Contains("Avatar") ?
                    row.Cells["Avatar"].Value : row.Cells[5].Value)?.ToString();

                if (!string.IsNullOrEmpty(avatarFile))
                {
                    string path = Path.Combine(Application.StartupPath, "Images", avatarFile);
                    picAvatar.Image = File.Exists(path) ? Image.FromFile(path) : null;
                }
                else
                {
                    picAvatar.Image = null;
                }
            }

        }
        private string avatarFilePath = string.Empty;
        private void btnUpload_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    avatarFilePath = openFileDialog.FileName;
                    picAvatar.Image = Image.FromFile(avatarFilePath);
                }
            }
        }
        private void LoadAvatar(string studentID)
        {
            // Define folder path and constants
            const string ImagesFolderName = "Images";
            string folderPath = Path.Combine(Application.StartupPath, ImagesFolderName);

            // Retrieve student information
            var student = studentService.FindById(studentID);
            if (student == null || string.IsNullOrEmpty(student.Avatar))
            {
                picAvatar.Image = null;
                return;
            }

            // Build the avatar file path
            string avatarFilePath = Path.Combine(folderPath, student.Avatar);

            // Check if the avatar file exists and load it
            if (File.Exists(avatarFilePath))
            {
                try
                {
                    using (var avatarImage = Image.FromFile(avatarFilePath))
                    {
                        picAvatar.Image = new Bitmap(avatarImage);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading avatar: {ex.Message}\nStack Trace: {ex.StackTrace}");
                    picAvatar.Image = null;
                }
            }
            else
            {
                picAvatar.Image = null;
            }
        }
        private string SaveAvatar(string sourceFilePath, string studentID)
        {
            const string ImagesFolderName = "Images";
            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(sourceFilePath) || string.IsNullOrWhiteSpace(studentID))
                {
                    throw new ArgumentException($"Source file path and student ID must not be null or empty.");
                }

                if (!File.Exists(sourceFilePath))
                {
                    throw new FileNotFoundException($"Source file not found: {sourceFilePath}");
                }

                // Create target folder if it doesn't exist
                string folderPath = Path.Combine(Application.StartupPath, ImagesFolderName);
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                // Construct target file path
                string fileExtension = Path.GetExtension(sourceFilePath);
                string targetFileName = $"{studentID}{fileExtension}";
                string targetFilePath = Path.Combine(folderPath, targetFileName);

                // Copy the file to the target path
                File.Copy(sourceFilePath, targetFilePath, overwrite: true);

                // Return the relative file name (e.g., "studentID.jpg")
                return targetFileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving avatar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }

        private void btnAdd_Update_Click(object sender, EventArgs e)
        {
            try
            {
                // === Basic validations (Kiểm tra dữ liệu đầu vào) ===
                if (string.IsNullOrWhiteSpace(txtID.Text))
                {
                    MessageBox.Show("Student ID is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtID.Text.Length > 10) // <--- Đã thêm: Kiểm tra độ dài ID
                {
                    MessageBox.Show("Student ID must be at most 10 characters.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtName.Text)) // <--- Đã thêm: Kiểm tra Tên
                {
                    MessageBox.Show("Full name is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtName.Text.Length > 255) // <--- Đã thêm: Kiểm tra độ dài Tên
                {
                    MessageBox.Show("Full name is too long.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra ComboBox Khoa
                if (cmbFaculty.SelectedValue == null
                    || !int.TryParse(cmbFaculty.SelectedValue.ToString(), out int facultyId)
                    || facultyId <= 0) // <--- Đã sửa: facultyId <= 0 loại trừ placeholder
                {
                    MessageBox.Show("Please select a valid faculty.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra Điểm Trung Bình
                if (!double.TryParse(txtAverageScore.Text, out double avg))
                {
                    MessageBox.Show("Average score is not a valid number.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // === Create or update student (Tạo/Cập nhật đối tượng) ===
                var student = studentService.FindById(txtID.Text.Trim()) ?? new Student();
                student.StudentID = txtID.Text.Trim();
                student.FullName = txtName.Text.Trim();
                student.AverageScore = avg;
                student.FacultyID = facultyId;

                // Xử lý Avatar
                if (!string.IsNullOrEmpty(avatarFilePath))
                {
                    string avatarFileName = SaveAvatar(avatarFilePath, student.StudentID);
                    if (!string.IsNullOrEmpty(avatarFileName))
                    {
                        student.Avatar = avatarFileName;
                    }
                }

                try
                {
                    // === [Thao tác với Database] ===
                    studentService.InsertUpdate(student);
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx) // <--- Đã thêm: Bắt lỗi EF Validation cụ thể
                {
                    var sb = new StringBuilder();
                    foreach (var eve in dbEx.EntityValidationErrors)
                    {
                        // Thu thập và hiển thị tất cả các lỗi Validation của EF
                        foreach (var ve in eve.ValidationErrors)
                        {
                            sb.AppendLine($"{ve.PropertyName}: {ve.ErrorMessage}");
                        }
                    }
                    MessageBox.Show(sb.ToString(), "Validation errors", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    System.Diagnostics.Debug.WriteLine(dbEx.ToString());
                    return;
                }

                // Cập nhật lưới và reset
                BindGrid(studentService.GetAll());
                avatarFilePath = string.Empty;

                MessageBox.Show("Student saved successfully.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Xử lý các lỗi khác (lỗi SQL, lỗi IO, v.v.)
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        private void chkUnregisterMajor_CheckedChanged(object sender, EventArgs e)
        {
            var listStudents = new List<Student>();
            if (this.chkUnregisterMajor.Checked)
            {
                listStudents = studentService.GetAllHasNoMajor();
            }
            else
            {
                listStudents = studentService.GetAll();
            }
            BindGrid(listStudents);
        }

        private void đăngKíChuyênNgànhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var registerForm = new Lab07.GUI.frmRegister();
            registerForm.ShowDialog(); // Use ShowDialog for modal, or Show for non-modal

        }

        private void btnX_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem có hàng nào đang được chọn không
            if (dgvStudent.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a student to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Lấy StudentID từ hàng được chọn
            // Giả định StudentID là cột đầu tiên (index 0) hoặc có tên là "StudentID"
            string studentID = string.Empty;
            try
            {
                var selectedRow = dgvStudent.SelectedRows[0];

                // Ưu tiên lấy giá trị theo tên cột nếu có
                if (dgvStudent.Columns.Contains("StudentID") && selectedRow.Cells["StudentID"].Value != null)
                {
                    studentID = selectedRow.Cells["StudentID"].Value.ToString();
                }
                else if (selectedRow.Cells.Count > 0 && selectedRow.Cells[0].Value != null)
                {
                    // Fallback: Lấy giá trị từ Cells[0] (cột đầu tiên)
                    studentID = selectedRow.Cells[0].Value.ToString();
                }

                if (string.IsNullOrWhiteSpace(studentID))
                {
                    MessageBox.Show("Could not retrieve Student ID from the selected row.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving student data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 3. Xác nhận xóa
            var confirmResult = MessageBox.Show($"Are you sure you want to delete student with ID: {studentID}?",
                                             "Confirm Delete",
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    // 4. Gọi hàm xóa từ BLL
                    bool isSuccess = studentService.Delete(studentID);

                    if (isSuccess)
                    {
                        MessageBox.Show($"Student {studentID} deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // 5. Cập nhật lại DataGridView
                        BindGrid(studentService.GetAll());

                        // 6. Xóa thông tin trên Form và Avatar
                        txtID.Clear();
                        txtName.Clear();
                        txtAverageScore.Clear();
                        cmbFaculty.SelectedIndex = 0;
                        picAvatar.Image = null;
                        avatarFilePath = string.Empty;
                    }
                    else
                    {
                        MessageBox.Show($"Could not find student {studentID} or deletion failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    // Bắt các lỗi Database hoặc BLL
                    MessageBox.Show($"An error occurred during deletion: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                }
            }
        }
    }
}
