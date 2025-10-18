namespace Lab07.GUI
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvStudent = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnUpload = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnX = new System.Windows.Forms.Button();
            this.btnAdd_Update = new System.Windows.Forms.Button();
            this.picAvatar = new System.Windows.Forms.PictureBox();
            this.txtAverageScore = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.cmbFaculty = new System.Windows.Forms.ComboBox();
            this.chkUnregisterMajor = new System.Windows.Forms.CheckBox();
            this.StudentID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FullName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FacultyID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AverageScore = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MajorID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Avatar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.chứcNăngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.đăngKíChuyênNgànhToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudent)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAvatar)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvStudent
            // 
            this.dgvStudent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStudent.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.StudentID,
            this.FullName,
            this.FacultyID,
            this.AverageScore,
            this.MajorID,
            this.Avatar});
            this.dgvStudent.Location = new System.Drawing.Point(299, 91);
            this.dgvStudent.Name = "dgvStudent";
            this.dgvStudent.RowHeadersWidth = 51;
            this.dgvStudent.RowTemplate.Height = 24;
            this.dgvStudent.Size = new System.Drawing.Size(628, 347);
            this.dgvStudent.TabIndex = 0;
            this.dgvStudent.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnUpload);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnX);
            this.groupBox1.Controls.Add(this.btnAdd_Update);
            this.groupBox1.Controls.Add(this.picAvatar);
            this.groupBox1.Controls.Add(this.txtAverageScore);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.txtID);
            this.groupBox1.Controls.Add(this.cmbFaculty);
            this.groupBox1.Location = new System.Drawing.Point(12, 91);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(281, 347);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông Tin Sinh Viên";
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(224, 233);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(50, 30);
            this.btnUpload.TabIndex = 12;
            this.btnUpload.Text = "...";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 205);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 16);
            this.label5.TabIndex = 11;
            this.label5.Text = "Ảnh Đại Diện";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 162);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "Điểm Trung Bình";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 16);
            this.label3.TabIndex = 9;
            this.label3.Text = "Khoa";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 16);
            this.label2.TabIndex = 8;
            this.label2.Text = "Họ Tên";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "Mã Sinh Viên";
            // 
            // btnX
            // 
            this.btnX.Location = new System.Drawing.Point(172, 301);
            this.btnX.Name = "btnX";
            this.btnX.Size = new System.Drawing.Size(75, 30);
            this.btnX.TabIndex = 6;
            this.btnX.Text = "Delete";
            this.btnX.UseVisualStyleBackColor = true;
            this.btnX.Click += new System.EventHandler(this.btnX_Click);
            // 
            // btnAdd_Update
            // 
            this.btnAdd_Update.Location = new System.Drawing.Point(71, 301);
            this.btnAdd_Update.Name = "btnAdd_Update";
            this.btnAdd_Update.Size = new System.Drawing.Size(95, 30);
            this.btnAdd_Update.TabIndex = 5;
            this.btnAdd_Update.Text = "Add/Update";
            this.btnAdd_Update.UseVisualStyleBackColor = true;
            this.btnAdd_Update.Click += new System.EventHandler(this.btnAdd_Update_Click);
            // 
            // picAvatar
            // 
            this.picAvatar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picAvatar.Location = new System.Drawing.Point(118, 205);
            this.picAvatar.Name = "picAvatar";
            this.picAvatar.Size = new System.Drawing.Size(100, 90);
            this.picAvatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picAvatar.TabIndex = 4;
            this.picAvatar.TabStop = false;
            // 
            // txtAverageScore
            // 
            this.txtAverageScore.Location = new System.Drawing.Point(141, 162);
            this.txtAverageScore.Name = "txtAverageScore";
            this.txtAverageScore.Size = new System.Drawing.Size(119, 22);
            this.txtAverageScore.TabIndex = 3;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(118, 73);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(142, 22);
            this.txtName.TabIndex = 2;
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(118, 27);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(142, 22);
            this.txtID.TabIndex = 1;
            // 
            // cmbFaculty
            // 
            this.cmbFaculty.FormattingEnabled = true;
            this.cmbFaculty.Location = new System.Drawing.Point(118, 119);
            this.cmbFaculty.Name = "cmbFaculty";
            this.cmbFaculty.Size = new System.Drawing.Size(142, 24);
            this.cmbFaculty.TabIndex = 0;
            // 
            // chkUnregisterMajor
            // 
            this.chkUnregisterMajor.AutoSize = true;
            this.chkUnregisterMajor.Location = new System.Drawing.Point(756, 65);
            this.chkUnregisterMajor.Name = "chkUnregisterMajor";
            this.chkUnregisterMajor.Size = new System.Drawing.Size(171, 20);
            this.chkUnregisterMajor.TabIndex = 2;
            this.chkUnregisterMajor.Text = "Chưa ĐK Chuyên Ngành";
            this.chkUnregisterMajor.UseVisualStyleBackColor = true;
            this.chkUnregisterMajor.CheckedChanged += new System.EventHandler(this.chkUnregisterMajor_CheckedChanged);
            // 
            // StudentID
            // 
            this.StudentID.HeaderText = "MSSV";
            this.StudentID.MinimumWidth = 6;
            this.StudentID.Name = "StudentID";
            this.StudentID.Width = 125;
            // 
            // FullName
            // 
            this.FullName.HeaderText = "Họ Tên";
            this.FullName.MinimumWidth = 6;
            this.FullName.Name = "FullName";
            this.FullName.Width = 125;
            // 
            // FacultyID
            // 
            this.FacultyID.HeaderText = "Khoa";
            this.FacultyID.MinimumWidth = 6;
            this.FacultyID.Name = "FacultyID";
            this.FacultyID.Width = 125;
            // 
            // AverageScore
            // 
            this.AverageScore.HeaderText = "Điểm Trung Bình";
            this.AverageScore.MinimumWidth = 6;
            this.AverageScore.Name = "AverageScore";
            this.AverageScore.Width = 125;
            // 
            // MajorID
            // 
            this.MajorID.HeaderText = "Chuyên Ngành";
            this.MajorID.MinimumWidth = 6;
            this.MajorID.Name = "MajorID";
            this.MajorID.Width = 125;
            // 
            // Avatar
            // 
            this.Avatar.HeaderText = "Link Avatar";
            this.Avatar.MinimumWidth = 6;
            this.Avatar.Name = "Avatar";
            this.Avatar.Width = 125;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chứcNăngToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(939, 28);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // chứcNăngToolStripMenuItem
            // 
            this.chứcNăngToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.đăngKíChuyênNgànhToolStripMenuItem});
            this.chứcNăngToolStripMenuItem.Name = "chứcNăngToolStripMenuItem";
            this.chứcNăngToolStripMenuItem.Size = new System.Drawing.Size(96, 24);
            this.chứcNăngToolStripMenuItem.Text = "Chức Năng";
            // 
            // đăngKíChuyênNgànhToolStripMenuItem
            // 
            this.đăngKíChuyênNgànhToolStripMenuItem.Name = "đăngKíChuyênNgànhToolStripMenuItem";
            this.đăngKíChuyênNgànhToolStripMenuItem.Size = new System.Drawing.Size(238, 26);
            this.đăngKíChuyênNgànhToolStripMenuItem.Text = "Đăng kí chuyên ngành";
            this.đăngKíChuyênNgànhToolStripMenuItem.Click += new System.EventHandler(this.đăngKíChuyênNgànhToolStripMenuItem_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(341, 41);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(221, 29);
            this.label6.TabIndex = 4;
            this.label6.Text = "Quản Lí Sinh Viên";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(939, 450);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.chkUnregisterMajor);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvStudent);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudent)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAvatar)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvStudent;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnX;
        private System.Windows.Forms.Button btnAdd_Update;
        private System.Windows.Forms.PictureBox picAvatar;
        private System.Windows.Forms.TextBox txtAverageScore;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.ComboBox cmbFaculty;
        private System.Windows.Forms.CheckBox chkUnregisterMajor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.DataGridViewTextBoxColumn StudentID;
        private System.Windows.Forms.DataGridViewTextBoxColumn FullName;
        private System.Windows.Forms.DataGridViewTextBoxColumn FacultyID;
        private System.Windows.Forms.DataGridViewTextBoxColumn AverageScore;
        private System.Windows.Forms.DataGridViewTextBoxColumn MajorID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Avatar;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem chứcNăngToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem đăngKíChuyênNgànhToolStripMenuItem;
        private System.Windows.Forms.Label label6;
    }
}

