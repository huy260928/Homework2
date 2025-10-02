using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BAITAP05
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void tạoVănBảnMớiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Hỏi người dùng xác nhận nếu văn bản hiện tại chưa được lưu
            if (MessageBox.Show("Bạn có muốn lưu tài liệu hiện tại không?", "Tạo Trang Mới", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                // Gọi lại chức năng Lưu
                lưuNộiDungVănBảnToolStripMenuItem_Click(sender, e);

                // Sau khi lưu xong (hoặc nếu người dùng chọn No) thì xóa nội dung
                richTextBoxMain.Clear();
                this.Text = "Soạn thảo văn bản"; // Đặt lại tiêu đề
            }
            else if (MessageBox.Show("Bạn có muốn lưu tài liệu hiện tại không?", "Tạo Trang Mới", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.No)
            {
                richTextBoxMain.Clear();
                this.Text = "Soạn thảo văn bản";
            }
            // Nếu chọn Cancel, không làm gì cả
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 1. Điền danh sách Font
            foreach (FontFamily font in FontFamily.Families)
            {
                toolStripComboFont.Items.Add(font.Name);
            }
            // Chọn Font mặc định (Tahoma)
            toolStripComboFont.Text = "Tahoma";

            // 2. Điền danh sách Kích thước
            for (int i = 8; i <= 72; i += 2) // Thêm kích thước từ 8 đến 72
            {
                toolStripComboSize.Items.Add(i.ToString());
            }
            // Chọn kích thước mặc định (14)
            toolStripComboSize.Text = "14";

            // 3. Đặt RichTextBox ở chế độ Multiline
            richTextBoxMain.Multiline = true;
        }

        private void toolStripButtonBold_Click(object sender, EventArgs e)
        {
            ToggleFontStyle(FontStyle.Bold);
        }

        private void toolStripButtonItalic_Click(object sender, EventArgs e)
        {
            ToggleFontStyle(FontStyle.Italic);
        }

        private void toolStripButtonUnderline_Click(object sender, EventArgs e)
        {
            ToggleFontStyle(FontStyle.Underline);
        }
        private void ToggleFontStyle(FontStyle style)
        {
            // Chỉ hoạt động nếu có văn bản được chọn
            if (richTextBoxMain.SelectionLength > 0)
            {
                Font currentFont = richTextBoxMain.SelectionFont;
                FontStyle newStyle = currentFont.Style ^ style; // Toán tử XOR để bật/tắt

                richTextBoxMain.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newStyle);
            }
        }

        private void lưuNộiDungVănBảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            // Thiết lập các bộ lọc file (chỉ cho phép lưu file RTF)
            sfd.Filter = "Rich Text Format (*.rtf)|*.rtf|Text File (*.txt)|*.txt|All Files (*.*)|*.*";
            sfd.Title = "Lưu tài liệu";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Kiểm tra kiểu file người dùng chọn
                    if (sfd.FilterIndex == 1) // Rich Text Format (*.rtf)
                    {
                        // Lưu file RTF để giữ lại định dạng (in đậm, in nghiêng, v.v.)
                        richTextBoxMain.SaveFile(sfd.FileName, RichTextBoxStreamType.RichText);
                    }
                    else // File Text hoặc All Files
                    {
                        // Lưu file Text thường (sẽ mất định dạng)
                        richTextBoxMain.SaveFile(sfd.FileName, RichTextBoxStreamType.PlainText);
                    }

                    MessageBox.Show("Đã lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Cập nhật tiêu đề Form với tên file
                    this.Text = "Soạn thảo văn bản - " + sfd.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lưu tệp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void toolStripComboFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFontChange();
        }

        private void toolStripComboFont_KeyUp(object sender, KeyEventArgs e)
        {
            // Cần xử lý khi người dùng tự gõ tên font và nhấn Enter
            if (e.KeyCode == Keys.Enter)
            {
                ApplyFontChange();
            }
        }

        private void toolStripComboSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFontChange();
        }

        private void toolStripComboSize_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ApplyFontChange();
            }
        }
        private void ApplyFontChange()
        {
            // Lấy tên Font và Kích thước hiện tại từ ToolStrip
            string fontName = toolStripComboFont.Text;
            string fontSizeString = toolStripComboSize.Text;

            // Lấy Font hiện tại của vùng chọn
            Font currentSelectionFont = richTextBoxMain.SelectionFont;

            // Nếu chưa có vùng chọn (SelectionFont là null), lấy Font mặc định của RichTextBox
            if (currentSelectionFont == null)
            {
                currentSelectionFont = richTextBoxMain.Font;
            }

            // 1. Xử lý Kích thước
            if (float.TryParse(fontSizeString, out float newSize))
            {
                if (newSize < 1 || newSize > 72) // Giới hạn hợp lý
                {
                    MessageBox.Show("Kích thước font không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                // Nếu không phải số hợp lệ, dùng kích thước hiện tại
                newSize = currentSelectionFont.Size;
            }

            // 2. Áp dụng Font mới
            try
            {
                // Tạo Font mới với tên, kích thước và kiểu dáng (Bold/Italic/Underline) hiện tại
                richTextBoxMain.SelectionFont = new Font(
                    fontName,
                    newSize,
                    currentSelectionFont.Style
                );
            }
            catch
            {
                // Xử lý nếu tên font không tồn tại trên hệ thống
                MessageBox.Show("Font '" + fontName + "' không tồn tại.", "Lỗi Font", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            tạoVănBảnMớiToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            lưuNộiDungVănBảnToolStripMenuItem_Click(sender, e);
        }

        private void địnhDạngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontDlg = new FontDialog();
            fontDlg.ShowColor = true;
            fontDlg.ShowApply = true;
            fontDlg.ShowEffects = true;
            fontDlg.ShowHelp = true;
            if (fontDlg.ShowDialog() != DialogResult.Cancel)
            {
                richTextBoxMain.ForeColor = fontDlg.Color;
                richTextBoxMain.Font = fontDlg.Font;
            }
        }
    }
}
