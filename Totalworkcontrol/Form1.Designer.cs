namespace Totalworkcontrol
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.chkSaveID = new System.Windows.Forms.CheckBox();
            this.chkAutoLogin = new System.Windows.Forms.CheckBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.txtPassword = new Totalworkcontrol.MaterialTextBox();
            this.txtUserID = new Totalworkcontrol.MaterialTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Noto Sans KR", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblTitle.Location = new System.Drawing.Point(401, 166);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(180, 42);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "통합업무관리";
           // this.lblTitle.Click += new System.EventHandler(this.label1_Click);
            // 
            // chkSaveID
            // 
            this.chkSaveID.AutoSize = true;
            this.chkSaveID.Location = new System.Drawing.Point(362, 335);
            this.chkSaveID.Name = "chkSaveID";
            this.chkSaveID.Size = new System.Drawing.Size(112, 16);
            this.chkSaveID.TabIndex = 3;
            this.chkSaveID.Text = "아이디 저장하기";
            this.chkSaveID.UseVisualStyleBackColor = true;
            // 
            // chkAutoLogin
            // 
            this.chkAutoLogin.AutoSize = true;
            this.chkAutoLogin.Location = new System.Drawing.Point(362, 365);
            this.chkAutoLogin.Name = "chkAutoLogin";
            this.chkAutoLogin.Size = new System.Drawing.Size(128, 16);
            this.chkAutoLogin.TabIndex = 4;
            this.chkAutoLogin.Text = "10일간 자동 로그인";
            this.chkAutoLogin.UseVisualStyleBackColor = true;
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(451, 422);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 5;
            this.btnLogin.Text = "로그인";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // picLogo
            // 
            this.picLogo.Image = global::Totalworkcontrol.Properties.Resources.okpos_logo;
            this.picLogo.Location = new System.Drawing.Point(380, 123);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(216, 29);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLogo.TabIndex = 0;
            this.picLogo.TabStop = false;
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.White;
            this.txtPassword.HintText = "패스워드";
            this.txtPassword.Location = new System.Drawing.Point(362, 263);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(250, 45);
            this.txtPassword.TabIndex = 7;
            // 
            // txtUserID
            // 
            this.txtUserID.BackColor = System.Drawing.Color.White;
            this.txtUserID.HintText = "사용자 ID";
            this.txtUserID.Location = new System.Drawing.Point(362, 220);
            this.txtUserID.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.PasswordChar = '\0';
            this.txtUserID.Size = new System.Drawing.Size(250, 45);
            this.txtUserID.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 527);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUserID);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.chkAutoLogin);
            this.Controls.Add(this.chkSaveID);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.picLogo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "통합업무관리";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.CheckBox chkSaveID;
        private System.Windows.Forms.CheckBox chkAutoLogin;
        private System.Windows.Forms.Button btnLogin;
        private MaterialTextBox txtUserID;
        private MaterialTextBox txtPassword;
    }
}

