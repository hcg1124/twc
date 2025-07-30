using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Totalworkcontrol
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        // 폼이 처음 로드될 때 실행됩니다.
        private void MainForm_Load(object sender, EventArgs e)
        {
            // 처음에는 대시보드 화면을 보여줍니다.
            ShowControl(new ucDashboard());
        }

        // '메인 페이지' 버튼을 클릭했을 때 실행됩니다.
        private void lblMenuMain_Click(object sender, EventArgs e)
        {
            ShowControl(new ucDashboard());
        }

        // '개발 요청' 버튼을 클릭했을 때 실행됩니다.
        private void lblMenuDev_Click(object sender, EventArgs e)
        {
            // 이 부분은 나중에 '개발 요청 현황' 부품을 만들고 채울 겁니다.
            ShowControl(new ucDevRequestList()); 
        }
        /// <summary>
        /// 오른쪽 메인 패널(pnlMain)에 원하는 부품(UserControl)을 보여주는 함수입니다.
        /// </summary>
        /// <param name="control">보여줄 UserControl 부품<
        private void btnLogout_Click(object sender, EventArgs e)
        {
            // 1. 자동 로그인 정보만 삭제합니다.
            // '아이디 저장' 정보(SavedUserID)는 그대로 둡니다.
            Properties.Settings.Default.AutoLoginToken = "";
            Properties.Settings.Default.AutoLoginExpiry = DateTime.Now.AddDays(-1);
            Properties.Settings.Default.Save();

            // 2. 프로그램을 다시 시작하여 로그인 화면으로 돌아갑니다.
            // Program.cs의 로직에 따라 로그인 화면이 다시 나타납니다.
            Application.Restart();
        }

        /// <summary>
        /// 오른쪽 메인 패널(pnlMain)에 원하는 부품(UserControl)을 보여주는 함수입니다.
        /// </summary>
        public void ShowControl(UserControl control)
        {
            this.pnlMain.Controls.Clear();
            control.Dock = DockStyle.Fill;
            this.pnlMain.Controls.Add(control);
        }

        /*ShowControl 함수는 화면을 바꿀 때 쓰는 기술입니다.

        만약 '대시보드'가 보이는 상태에서 '개발 요청' 메뉴를 누르면, 이 Clear() 명령이 먼저 기존에 있던 '대시보드' 부품을 깨끗하게 치워줍니다.*/

        private void pictureBox1_Click(object sender, EventArgs e)
        {

            ShowControl(new ucDashboard());
        }

    }
}
