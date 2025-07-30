using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace Totalworkcontrol
{
    public partial class MaterialTextBox : UserControl
    {
        // private 멤버 변수
        private bool isFocused = false;
        private Color borderColor = Color.Gray;
        private Color focusBorderColor = Color.DodgerBlue;

        // --- HintText 문제 해결 ---
        private string _hintText;
        [Category("Material Style")]
        public string HintText
        {
            get { return _hintText; }
            set
            {
                _hintText = value;
                this.label1.Text = _hintText;
                Invalidate(); // 디자이너에서 변경 시 바로 반영되도록 추가
            }
        }

        [Category("Material Style")]
        public override string Text
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }

        [Category("Material Style")]
        public char PasswordChar
        {
            get { return textBox1.PasswordChar; }
            set { textBox1.PasswordChar = value; }
        }

        public MaterialTextBox()
        {
            InitializeComponent();
            this.label1.BringToFront();

            // 이벤트 핸들러 연결
            this.textBox1.Enter += TextBox_Enter;
            this.textBox1.Leave += TextBox_Leave;
            this.textBox1.TextChanged += TextBox_TextChanged;
            this.label1.Click += (sender, e) => textBox1.Focus();

            // --- 레이아웃 문제 해결 ---
            this.Resize += new EventHandler(MaterialTextBox_Resize);
            // 초기 로드 시에도 위치를 잡아줍니다.
            MaterialTextBox_Resize(null, null);
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            // 컨트롤이 로드될 때 텍스트가 비어있으면 힌트를 아래로 내립니다.
            if (string.IsNullOrEmpty(this.textBox1.Text))
            {
                MoveLabelDown();
            }
        }
        // --- 레이아웃 문제 해결 ---
        private void MaterialTextBox_Resize(object sender, EventArgs e)
        {
            // 컨트롤 전체 크기가 바뀔 때마다, 내부 텍스트박스 위치를 수동으로 다시 계산합니다.
            textBox1.Location = new Point(5, (this.Height - textBox1.Height) / 2);
            textBox1.Width = this.Width - 10;
        }

        private void TextBox_Enter(object sender, EventArgs e)
        {
            isFocused = true;
            borderColor = focusBorderColor;
            this.Invalidate();

            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MoveLabelUp();
            }
        }

        private void TextBox_Leave(object sender, EventArgs e)
        {
            isFocused = false;
            borderColor = Color.Gray;
            this.Invalidate();

            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MoveLabelDown();
            }
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                MoveLabelUp();
            }
            else if (!isFocused)
            {
                MoveLabelDown();
            }
        }

        private void MoveLabelUp()
        {
            label1.Font = new Font(label1.Font.FontFamily, 8F);
            label1.Location = new Point(5, 2);
            label1.BringToFront();
        }

        private void MoveLabelDown()
        {
            label1.Font = new Font(label1.Font.FontFamily, 11F);
            // --- 레이아웃 문제 해결 ---
            // 라벨도 텍스트박스와 동일하게 세로 중앙 정렬합니다.
            label1.Location = new Point(5, (this.Height - label1.Height) / 2);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (Pen borderPen = new Pen(borderColor, 2))
            {
                e.Graphics.DrawRectangle(borderPen, 0, 0, this.ClientSize.Width - 1, this.ClientSize.Height - 1);
            }
        }
    }
}
