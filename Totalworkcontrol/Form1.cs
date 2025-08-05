using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;
//이코드에서 사용할 C#의 기본기능, 데이터베이스 연결, 암호화, 위도우폼 등 라이브러리를 불러오는 부분

namespace Totalworkcontrol
{
    public partial class Form1 : Form
    {//## 클래스 변수 (폼이 사용할 재료)
        private static readonly byte[] s_entropy = { 1, 2, 3, 4, 5, 6, 7, 8 }; // 암호화할때 복잡하게 만들기위해 사용
        private PrivateFontCollection privateFonts = new PrivateFontCollection(); // 디자인을 위해 커스텀 폰트를 담아둘 폰트 컬렉션 개체

        public Form1()
        {
            InitializeComponent();//디자이너에서 만든 버튼, 텍스트박스 등을 화면에 그리는 필수 코드입니다.
            LoadCustomFont(); //커스텀 폰트 가져오기
        }

        private void LoadCustomFont() //내장된 ariblk 코드를 읽어서 메모리에 올린다음 lblTitle라벨의 폰트로 지정
        {
            try
            {
                byte[] fontData = Properties.Resources.ariblk;
                IntPtr fontPtr = Marshal.AllocCoTaskMem(fontData.Length);
                Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
                privateFonts.AddMemoryFont(fontPtr, fontData.Length);
                Marshal.FreeCoTaskMem(fontPtr);
                lblTitle.Font = new System.Drawing.Font(privateFonts.Families[0], lblTitle.Font.Size, lblTitle.Font.Style);
            }
            catch (Exception ex)
            {
                MessageBox.Show("커스텀 폰트를 불러오는 데 실패했습니다.\n" + ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string savedID = Properties.Settings.Default.SavedUserID;
            if (!string.IsNullOrEmpty(savedID))
            {
                this.txtUserID.Text = savedID;
                this.chkSaveID.Checked = true;
                this.ActiveControl = this.txtPassword;
            }

            if (!string.IsNullOrEmpty(Properties.Settings.Default.AutoLoginToken) && Properties.Settings.Default.AutoLoginExpiry > DateTime.Now)
            {
                this.chkAutoLogin.Checked = true;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e) //로그인 버튼 클릭시 
        {
            string userID = this.txtUserID.Text;
            string password = this.txtPassword.Text;

            if (string.IsNullOrWhiteSpace(userID) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("사용자 ID와 패스워드를 모두 입력해주세요.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ▼▼▼ 수정된 부분 ▼▼▼
            // 버튼 클릭은 '자동 로그인'이 아니므로, 세 번째 인수로 false를 전달합니다.
            if (ValidateUser(userID, password, false)) //아이디 비번 맞는지 확인
            {
                HandleManualLogin(userID, password); // 로그인 후처리
            }
        }

        private void HandleManualLogin(string userID, string password) //아이디 비번 암호화
        {
            Properties.Settings.Default.SavedUserID = this.chkSaveID.Checked ? userID : "";

            if (this.chkAutoLogin.Checked) //자동체크인
            {
                string plainText = string.Format("{0}|{1}", userID, password); // udsrID가  'admin', password '1234'라면 한문장으로만들어서 plainText 암호화해서 '자동로그인통행증'으로 사용
                byte[] encryptedData = Encrypt(Encoding.UTF8.GetBytes(plainText));
                Properties.Settings.Default.AutoLoginToken = Convert.ToBase64String(encryptedData);
                Properties.Settings.Default.AutoLoginExpiry = DateTime.Now.AddDays(10);
            }
            else
            {
                Properties.Settings.Default.AutoLoginToken = "";
                Properties.Settings.Default.AutoLoginExpiry = DateTime.Now.AddDays(-1);
            }
            Properties.Settings.Default.Save();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public static bool ValidateUser(string userID, string password, bool isAutoLogin)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["db_conn"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string sql = "SELECT COUNT(*) FROM Users WHERE UserID = @UserID AND Password = @Password"; //@붙여서 파라미터처리하면 SQL인젝션 공격막아줌
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userID);
                        cmd.Parameters.AddWithValue("@Password", password);
                        int count = (int)cmd.ExecuteScalar(); //ExecuteScalar 쿼리실행하고 오는 첫번째 열 
                        if (count == 1) return true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (!isAutoLogin) MessageBox.Show("데이터베이스 오류: " + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!isAutoLogin) MessageBox.Show("사용자 ID 또는 패스워드가 올바르지 않습니다.", "로그인 실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        #region 암호화/복호화 도우미 함수 (public static으로 변경)
        public static byte[] Encrypt(byte[] data)
        {
            return ProtectedData.Protect(data, s_entropy, DataProtectionScope.CurrentUser);
        }

        public static byte[] Decrypt(byte[] data)
        {
            return ProtectedData.Unprotect(data, s_entropy, DataProtectionScope.CurrentUser);
        }
        #endregion
    }
}
