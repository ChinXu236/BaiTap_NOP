using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;

namespace LAB03_2
{
    public partial class FormSoanThao : Form
    {
        private string savedFilePath = null;

        public FormSoanThao()
        {
            InitializeComponent();
            this.Load += FormSoanThao_Load;
            this.taoVănBanMơiToolStripMenuItem.Click += TaoVanBanMoi_Click;
            this.mơTâpTinToolStripMenuItem.Click += MoTapTin_Click;
            this.lưuNôiDungVănBanToolStripMenuItem.Click += LuuNoiDungVanBan_Click;
            this.thoatToolStripMenuItem.Click += Thoat_Click;
            // Button B/I/U: toolStripButton3/4/5
            this.toolStripButton3.Click += BtnBold_Click;
            this.toolStripButton4.Click += BtnItalic_Click;
            this.toolStripButton5.Click += BtnUnderline_Click;
            this.toolStripComboBox1.SelectedIndexChanged += ToolStripComboBox1_SelectedIndexChanged;
            this.toolStripComboBox2.SelectedIndexChanged += ToolStripComboBox2_SelectedIndexChanged;
        }

        private void FormSoanThao_Load(object sender, EventArgs e)
        {
            // Load font
            toolStripComboBox1.Items.Clear();
            foreach (FontFamily fontFamily in new InstalledFontCollection().Families)
            {
                toolStripComboBox1.Items.Add(fontFamily.Name);
            }
            toolStripComboBox1.SelectedItem = "Tahoma";
            // Load size
            int[] sizeValues = new int[] { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
            toolStripComboBox2.Items.Clear();
            foreach (var size in sizeValues)
                toolStripComboBox2.Items.Add(size);
            toolStripComboBox2.SelectedItem = 14;
            // Set default font
            richTextBox1.Font = new Font("Tahoma", 14, FontStyle.Regular);
        }

        private void TaoVanBanMoi_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            toolStripComboBox1.SelectedItem = "Tahoma";
            toolStripComboBox2.SelectedItem = 14;
            richTextBox1.Font = new Font("Tahoma", 14, FontStyle.Regular);
            savedFilePath = null;
        }

        private void MoTapTin_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;
            openFileDialog.Filter = "Text files (*.txt)|*.txt|RichText files (*.rtf)|*.rtf";
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFileName = openFileDialog.FileName;
                savedFilePath = selectedFileName;
                try
                {
                    if (Path.GetExtension(selectedFileName).Equals(".txt", StringComparison.OrdinalIgnoreCase))
                        richTextBox1.LoadFile(selectedFileName, RichTextBoxStreamType.PlainText);
                    else
                        richTextBox1.LoadFile(selectedFileName, RichTextBoxStreamType.RichText);
                    MessageBox.Show("Tập tin đã được mở thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi trong quá trình mở tập tin: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LuuNoiDungVanBan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(savedFilePath))
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "RichText files (*.rtf)|*.rtf";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    savedFilePath = saveFileDialog.FileName;
                    richTextBox1.SaveFile(savedFilePath, RichTextBoxStreamType.RichText);
                    MessageBox.Show("Lưu văn bản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                richTextBox1.SaveFile(savedFilePath, RichTextBoxStreamType.RichText);
                MessageBox.Show("Lưu văn bản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Thoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnBold_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont != null)
            {
                FontStyle style = richTextBox1.SelectionFont.Style;
                if (richTextBox1.SelectionFont.Bold)
                    style &= ~FontStyle.Bold;
                else
                    style |= FontStyle.Bold;
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, style);
            }
        }
        private void BtnItalic_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont != null)
            {
                FontStyle style = richTextBox1.SelectionFont.Style;
                if (richTextBox1.SelectionFont.Italic)
                    style &= ~FontStyle.Italic;
                else
                    style |= FontStyle.Italic;
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, style);
            }
        }
        private void BtnUnderline_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont != null)
            {
                FontStyle style = richTextBox1.SelectionFont.Style;
                if (richTextBox1.SelectionFont.Underline)
                    style &= ~FontStyle.Underline;
                else
                    style |= FontStyle.Underline;
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, style);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            // Tạo văn bản mới: xóa nội dung, đặt lại font và size mặc định
            richTextBox1.Clear();
            toolStripComboBox1.SelectedItem = "Tahoma";
            toolStripComboBox2.SelectedItem = 14;
            richTextBox1.Font = new Font("Tahoma", 14, FontStyle.Regular);
            savedFilePath = null;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            // Mở tập tin: hộp thoại chọn file txt/rtf
            richTextBox1.Clear();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;
            openFileDialog.Filter = "Text files (*.txt)|*.txt|RichText files (*.rtf)|*.rtf";
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFileName = openFileDialog.FileName;
                savedFilePath = selectedFileName;
                try
                {
                    if (Path.GetExtension(selectedFileName).Equals(".txt", StringComparison.OrdinalIgnoreCase))
                        richTextBox1.LoadFile(selectedFileName, RichTextBoxStreamType.PlainText);
                    else
                        richTextBox1.LoadFile(selectedFileName, RichTextBoxStreamType.RichText);
                    MessageBox.Show("Tập tin đã được mở thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi trong quá trình mở tập tin: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Button3: Tô đậm
        private void button3_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont != null)
            {
                Font currentFont = richTextBox1.SelectionFont;
                FontStyle newFontStyle;
                // Toggle Bold
                if (currentFont.Bold)
                    newFontStyle = currentFont.Style & ~FontStyle.Bold;
                else
                    newFontStyle = currentFont.Style | FontStyle.Bold;

                richTextBox1.SelectionFont = new Font(currentFont, newFontStyle);
            }
        }

        // Button4: In nghiêng
        private void button4_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont != null)
            {
                Font currentFont = richTextBox1.SelectionFont;
                FontStyle newFontStyle;
                // Toggle Italic
                if (currentFont.Italic)
                    newFontStyle = currentFont.Style & ~FontStyle.Italic;
                else
                    newFontStyle = currentFont.Style | FontStyle.Italic;

                richTextBox1.SelectionFont = new Font(currentFont, newFontStyle);
            }
        }

        // Button5: Gạch chân
        private void button5_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont != null)
            {
                Font currentFont = richTextBox1.SelectionFont;
                FontStyle newFontStyle;
                // Toggle Underline
                if (currentFont.Underline)
                    newFontStyle = currentFont.Style & ~FontStyle.Underline;
                else
                    newFontStyle = currentFont.Style | FontStyle.Underline;

                richTextBox1.SelectionFont = new Font(currentFont, newFontStyle);
            }
        }

        private void ToolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeFontOrSize();
        }

        private void ToolStripComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeFontOrSize();
        }

        private void ChangeFontOrSize()
        {
            string fontName = toolStripComboBox1.SelectedItem?.ToString() ?? richTextBox1.Font.FontFamily.Name;
            float fontSize = Convert.ToSingle(toolStripComboBox2.SelectedItem ?? richTextBox1.Font.Size);
            FontStyle style = richTextBox1.SelectionFont?.Style ?? FontStyle.Regular;
            richTextBox1.SelectionFont = new Font(fontName, fontSize, style);
        }

        private void FormSoanThao_Load_1(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void ĐinhDangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FontDialog fontDialog = new FontDialog())
            {
                // nếu có text được chọn thì đặt font mặc định cho dialog
                if (richTextBox1.SelectionFont != null)
                    fontDialog.Font = richTextBox1.SelectionFont;
                else
                    fontDialog.Font = richTextBox1.Font;

                if (fontDialog.ShowDialog() == DialogResult.OK)
                {
                    if (richTextBox1.SelectionLength > 0)
                    {
                        // áp dụng cho đoạn text được chọn
                        richTextBox1.SelectionFont = fontDialog.Font;
                    }
                    else
                    {
                        // áp dụng cho chữ sẽ gõ tiếp theo
                        richTextBox1.SelectionFont = fontDialog.Font;
                    }

                    // cập nhật lại combobox font + size
                    toolStripComboBox1.SelectedItem = fontDialog.Font.FontFamily.Name;
                    toolStripComboBox2.SelectedItem = (int)fontDialog.Font.Size;
                }
            }
        }
    }
}
