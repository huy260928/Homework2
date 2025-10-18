using Lab07.BLL;
using Lab07.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab07.GUI
{
    public partial class frmRegister : Form
    {
        private readonly StudentService studentService = new StudentService();
        private readonly FacultyService facultyService = new FacultyService();
        private readonly MajorService majorService = new MajorService();
        public frmRegister()
        {
            InitializeComponent();
        }

        private void frmRegister_Load(object sender, EventArgs e)
        {
            try
            {
                EnsureDgvSetup();

                var listFacultys = facultyService.GetAll();
                FillFalcultyCombobox(listFacultys);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void EnsureDgvSetup()
        {
            dgvStudent.AutoGenerateColumns = false;

            // Add checkbox column for selection if missing
            if (!dgvStudent.Columns.Contains("Select"))
            {
                var chk = new DataGridViewCheckBoxColumn
                {
                    Name = "Select",
                    HeaderText = "",
                    Width = 30
                };
                dgvStudent.Columns.Insert(0, chk);
            }

            AddTextColumnIfMissing("StudentID", "StudentID", 100);
            AddTextColumnIfMissing("FullName", "FullName", 200);
            AddTextColumnIfMissing("FacultyName", "FacultyName", 150);
            AddTextColumnIfMissing("AverageScore", "AverageScore", 90);
            AddTextColumnIfMissing("Major", "Major", 150);

            // Events to detect checkbox commit
            dgvStudent.CurrentCellDirtyStateChanged -= dgvStudent_CurrentCellDirtyStateChanged;
            dgvStudent.CurrentCellDirtyStateChanged += dgvStudent_CurrentCellDirtyStateChanged;

            dgvStudent.CellValueChanged -= dgvStudent_CellValueChanged;
            dgvStudent.CellValueChanged += dgvStudent_CellValueChanged;
        }

        private void AddTextColumnIfMissing(string name, string headerText, int width)
        {
            if (!dgvStudent.Columns.Contains(name))
            {
                var col = new DataGridViewTextBoxColumn
                {
                    Name = name,
                    HeaderText = headerText,
                    ReadOnly = true,
                    Width = width
                };
                dgvStudent.Columns.Add(col);
            }
        }
        private void FillFalcultyCombobox(List<Faculty> listFacultys)
        {
            this.cmbFaculty.DataSource = listFacultys;
            this.cmbFaculty.DisplayMember = "FacultyName";
            this.cmbFaculty.ValueMember = "FacultyID";
            this.cmbFaculty.SelectedIndex = -1;

        }
        // Add this method to frmRegister class
        private void FillMajorCombobox(List<Major> listMajors)
        {
            this.cmbMajor.DataSource = listMajors;
            this.cmbMajor.DisplayMember = "Name";
            this.cmbMajor.ValueMember = "MajorID";
            this.cmbMajor.SelectedIndex = -1;

        }
        private void cmbFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            Faculty selectedFaculty = cmbFaculty.SelectedItem as Faculty;
            if (selectedFaculty != null)
            {
                var listMajor = majorService.GetAllByFaculty(selectedFaculty.FacultyID);
                FillMajorCombobox(listMajor);
                var listStudents = studentService.GetAllHasNoMajor(selectedFaculty.FacultyID);
                BindGrid(listStudents);
            }
            else
            {
                // clear grid and majors if no faculty selected
                dgvStudent.Rows.Clear();
                FillMajorCombobox(new List<Major>());
            }

        }
        private void BindGrid(List<Student> listStudent)
        {
            dgvStudent.Rows.Clear();
            foreach (var item in listStudent)
            {
                int index = dgvStudent.Rows.Add();
                var row = dgvStudent.Rows[index];

                if (dgvStudent.Columns.Contains("Select"))
                    row.Cells["Select"].Value = false;

                if (dgvStudent.Columns.Contains("StudentID"))
                    row.Cells["StudentID"].Value = item.StudentID;
                if (dgvStudent.Columns.Contains("FullName"))
                    row.Cells["FullName"].Value = item.FullName;
                if (dgvStudent.Columns.Contains("FacultyName"))
                    row.Cells["FacultyName"].Value = item.Faculty?.FacultyName ?? "";
                if (dgvStudent.Columns.Contains("AverageScore"))
                    row.Cells["AverageScore"].Value = item.AverageScore.ToString("0.##");
                if (dgvStudent.Columns.Contains("Major"))
                    row.Cells["Major"].Value = item.Major?.Name ?? "";
            }

        }

        private void dgvStudent_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvStudent.IsCurrentCellDirty)
                dgvStudent.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dgvStudent_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvStudent.Columns[e.ColumnIndex].Name != "Select") return;

            var chkCell = dgvStudent.Rows[e.RowIndex].Cells["Select"];
            bool isChecked = chkCell.Value != null && Convert.ToBoolean(chkCell.Value);
            if (!isChecked) return; // only act when checked

            // Get student id from the row
            var idCell = dgvStudent.Rows[e.RowIndex].Cells["StudentID"].Value;
            if (idCell == null) return;
            string studentID = idCell.ToString();
            if (string.IsNullOrWhiteSpace(studentID)) return;

            // Validate selected major
            if (cmbMajor.SelectedValue == null || !int.TryParse(cmbMajor.SelectedValue.ToString(), out int majorId))
            {
                MessageBox.Show("Please select a major before registering.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                chkCell.Value = false;
                return;
            }

            try
            {
                var student = studentService.FindById(studentID);
                if (student == null)
                {
                    MessageBox.Show($"Student {studentID} not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    chkCell.Value = false;
                    return;
                }

                // assign major and save
                student.MajorID = majorId;
                studentService.InsertUpdate(student);

                // refresh grid (registered students are removed from "no major" list)
                var selectedFaculty = cmbFaculty.SelectedItem as Faculty;
                if (selectedFaculty != null)
                    BindGrid(studentService.GetAllHasNoMajor(selectedFaculty.FacultyID));
                else
                    BindGrid(studentService.GetAllHasNoMajor());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error registering student: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                chkCell.Value = false;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Validate major selection first
            if (cmbMajor.SelectedValue == null || !int.TryParse(cmbMajor.SelectedValue.ToString(), out int majorId))
            {
                MessageBox.Show("Please select a major before registering.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int registeredCount = 0;
            foreach (DataGridViewRow row in dgvStudent.Rows)
            {
                if (row.IsNewRow) continue;
                if (!dgvStudent.Columns.Contains("Select")) continue;

                var selectCell = row.Cells["Select"];
                bool isChecked = selectCell.Value != null && Convert.ToBoolean(selectCell.Value);
                if (!isChecked) continue;

                var idCell = row.Cells["StudentID"].Value;
                if (idCell == null) continue;

                string studentID = idCell.ToString();
                if (string.IsNullOrWhiteSpace(studentID)) continue;

                var student = studentService.FindById(studentID);
                if (student == null) continue;

                student.MajorID = majorId;
                studentService.InsertUpdate(student);
                registeredCount++;
            }

            if (registeredCount > 0)
            {
                MessageBox.Show($"{registeredCount} student(s) registered to the selected major.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No students selected for registration.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // Signal to Form1 that changes were made and close
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }

}
