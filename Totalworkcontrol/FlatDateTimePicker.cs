using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace Totalworkcontrol
{
    public partial class FlatDateTimePicker : UserControl
    {
        private Form dropDownForm;
        private MonthCalendar monthCalendar;

        [Category("Data")]
        public DateTime Value
        {
            get
            {
                DateTime result;
                if (DateTime.TryParse(textBox1.Text, out result))
                {
                    return result;
                }
                return DateTime.Now;
            }
            set { textBox1.Text = value.ToString("yyyy년 MM월 dd일"); }
        }

        public FlatDateTimePicker()
        {
            InitializeComponent();
            this.Value = DateTime.Now;

            // 텍스트박스와 버튼에 클릭 이벤트를 연결합니다.
            this.textBox1.Click += new EventHandler(ToggleDropDown);
            this.button1.Click += new EventHandler(ToggleDropDown);
        }

        private void ToggleDropDown(object sender, EventArgs e)
        {
            if (dropDownForm == null || dropDownForm.IsDisposed)
            {
                dropDownForm = new Form();
                dropDownForm.FormBorderStyle = FormBorderStyle.None;
                dropDownForm.StartPosition = FormStartPosition.Manual;
                dropDownForm.ShowInTaskbar = false;
                dropDownForm.BackColor = Color.White;

                monthCalendar = new MonthCalendar();
                monthCalendar.MaxSelectionCount = 1;
                monthCalendar.DateSelected += new DateRangeEventHandler(MonthCalendar_DateSelected);

                dropDownForm.Controls.Add(monthCalendar);
                dropDownForm.Size = monthCalendar.Size;
                dropDownForm.Deactivate += (s, args) => dropDownForm.Close();
            }
            // 1. 컨트롤의 화면상 위치를 가져옵니다.
            Point location = this.Parent.PointToScreen(this.Location);

            // 2. 현재 컨트롤이 있는 화면의 작업 영역을 가져옵니다.
            Rectangle screenArea = Screen.FromControl(this).WorkingArea;

            // 3. 달력을 아래로 띄웠을 때, 화면을 벗어나는지 계산합니다.
            int calendarBottom = location.Y + this.Height + dropDownForm.Height;

            // 4. 만약 화면을 벗어난다면,
            if (calendarBottom > screenArea.Bottom)
            {
                // 컨트롤의 위쪽으로 위치를 잡습니다.
                dropDownForm.Location = new Point(location.X, location.Y - dropDownForm.Height);
            }
            else
            {
                // 공간이 충분하면 원래대로 아래쪽에 위치를 잡습니다.
                dropDownForm.Location = new Point(location.X, location.Y + this.Height);
            }
            dropDownForm.Show();
        }

        private void MonthCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            this.Value = e.Start;
            dropDownForm.Close();
        }

        // 테두리를 그리는 부분
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (Pen borderPen = new Pen(Color.LightGray, 1))
            {
                e.Graphics.DrawRectangle(borderPen, 0, 0, this.ClientSize.Width - 1, this.ClientSize.Height - 1);
            }
        }
    }
}
