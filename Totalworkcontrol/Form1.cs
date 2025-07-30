using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace Totalworkcontrol
{
    public partial class Form1 : Form
    {
        private static readonly byte[] s_entropy = { 1, 2, 3, 4, 5, 6, 7, 8 };
        private PrivateFontCollection privateFonts = new PrivateFontCollection();

        public Form1()
        {
            InitializeComponent();
            LoadCustomFont();
        }

        private void LoadCustomFont()
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

        private void btnLogin_Click(object sender, EventArgs e)
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
            if (ValidateUser(userID, password, false))
            {
                HandleManualLogin(userID, password);
            }
        }

        private void HandleManualLogin(string userID, string password)
        {
            Properties.Settings.Default.SavedUserID = this.chkSaveID.Checked ? userID : "";

            if (this.chkAutoLogin.Checked)
            {
                string plainText = string.Format("{0}|{1}", userID, password);
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
                    string sql = "SELECT COUNT(*) FROM Users WHERE UserID = @UserID AND Password = @Password";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userID);
                        cmd.Parameters.AddWithValue("@Password", password);
                        int count = (int)cmd.ExecuteScalar();
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
