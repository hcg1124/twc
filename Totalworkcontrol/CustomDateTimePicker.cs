using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Reflection; // 리플렉션 기능을 사용하기 위해 추가

namespace Totalworkcontrol
{
    public class CustomDateTimePicker : DateTimePicker
    {
        // Windows API 함수
        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        // Windows 스타일 상수
        private const int GWL_STYLE = -16;
        private const int WS_BORDER = 0x00800000; // 테두리 스타일

        public CustomDateTimePicker()
        {
            // 생성자
        }

        /// <summary>
        /// 컨트롤의 핸들이 생성된 직후에 딱 한 번 호출되어 테두리를 제거합니다.
        /// </summary>
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            // 윈도우에게 이 컨트롤의 현재 스타일 정보를 달라고 요청합니다.
            int style = GetWindowLong(this.Handle, GWL_STYLE);

            // 현재 스타일에서 테두리(WS_BORDER) 스타일만 쏙 빼버립니다.
            style &= ~WS_BORDER;

            // 테두리가 없어진 새로운 스타일을 적용하라고 윈도우에게 명령합니다.
            SetWindowLong(this.Handle, GWL_STYLE, style);
        }

        // ▼▼▼ 이 함수가 완전히 새로워졌습니다 ▼▼▼
        /// <summary>
        /// 마우스를 클릭했을 때의 동작을 재정의합니다.
        /// </summary>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            // 오른쪽의 드롭다운 버튼(폭 약 17px)이 아닌, 왼쪽의 날짜 영역을 클릭했다면
            if (e.X < this.Width - 17)
            {
                // SendMessage 대신, 리플렉션을 사용하여 숨겨진 OnDropDown 메서드를 직접 호출합니다.
                // 이것이 훨씬 더 안정적이고 안전한 방법입니다.
                MethodInfo dropDownMethod = typeof(DateTimePicker).GetMethod("OnDropDown", BindingFlags.NonPublic | BindingFlags.Instance);
                if (dropDownMethod != null)
                {
                    dropDownMethod.Invoke(this, new object[] { EventArgs.Empty });
                }
            }

            // 원래 DateTimePicker가 하던 마우스 클릭 동작도 그대로 실행합니다.
            base.OnMouseDown(e);
        }
        // ▲▲▲ 여기까지 수정 ▲▲▲
    }
}
