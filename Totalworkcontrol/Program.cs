using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing; // Point, Size 사용을 위해 추가
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace Totalworkcontrol
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 1. 먼저 보이지 않는 곳에서 자동 로그인을 시도합니다.
            if (TryAutoLogin())
            {
                // 2. 성공하면 메인 폼을 바로 실행합니다.
                Application.Run(new MainForm());
            }
            else
            {
                // 3. 실패하면, 이전과 같이 로그인 폼을 띄웁니다.
                using (Form1 loginForm = new Form1())
                {
                    if (loginForm.ShowDialog() == DialogResult.OK)
                    {
                        Point startLocation = loginForm.Location;
                        Size startSize = loginForm.Size;
                        MainForm main = new MainForm();
                        main.StartPosition = FormStartPosition.Manual;
                        main.Location = startLocation;
                        main.Size = startSize;
                        Application.Run(main);
                    }
                }
            }
        }

        /// <summary>
        /// 자동 로그인을 시도하고 성공 여부를 반환합니다.
        /// </summary>
        private static bool TryAutoLogin()
        {
            string token = Properties.Settings.Default.AutoLoginToken;
            DateTime expiry = Properties.Settings.Default.AutoLoginExpiry;

            if (!string.IsNullOrEmpty(token) && expiry > DateTime.Now)
            {
                try
                {
                    byte[] encryptedData = Convert.FromBase64String(token);
                    byte[] decryptedData = Form1.Decrypt(encryptedData); // Form1의 공개 함수 사용
                    string plainText = Encoding.UTF8.GetString(decryptedData);
                    string[] parts = plainText.Split('|');
                    if (parts.Length == 2)
                    {
                        string userID = parts[0];
                        string password = parts[1];
                        // DB 확인 후 성공하면 true를 반환
                        if (Form1.ValidateUser(userID, password, true)) // Form1의 공개 함수 사용
                        {
                            return true;
                        }
                    }
                }
                catch { /* 토큰 해석 실패 시 아무것도 안 함 */ }
            }
            return false;
        }
    }
}
