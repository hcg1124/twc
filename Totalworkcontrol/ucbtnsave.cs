using System;
using System.Windows.Forms;

namespace Totalworkcontrol
{
    public partial class ucbtnsave : UserControl
    {
        // ▼▼▼ "클릭 신호를 바깥으로 보내겠다"는 선언입니다 ▼▼▼
        public event EventHandler ButtonClick;
        // ▲▲▲ 여기까지 추가 ▲▲▲

        public ucbtnsave()
        {
            InitializeComponent();
            // ▼▼▼ 이 부품 안에 있는 진짜 버튼(예: button1)의 클릭 이벤트를 연결합니다 ▼▼▼
            // (디자인 화면에서 버튼 이름이 button1이 맞는지 확인해주세요)
            this.button1.Click += new EventHandler(InternalButton_Click);
        }

        // ▼▼▼ 진짜 버튼이 눌리면, 바깥으로 신호를 보냅니다 ▼▼▼
        private void InternalButton_Click(object sender, EventArgs e)
        {
            // ButtonClick 신호를 기다리는 누군가가 있다면
            if (this.ButtonClick != null)
            {
                // "나 눌렸다!" 하고 신호를 보냅니다.
                this.ButtonClick(this, e);
            }
        }
    }
}