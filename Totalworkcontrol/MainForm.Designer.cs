namespace Totalworkcontrol
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlTop = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnMyInfo = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblMenuDev = new System.Windows.Forms.Label();
            this.lblMenuMain = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnlLeft.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.White;
            this.pnlTop.Controls.Add(this.panel1);
            this.pnlTop.Controls.Add(this.btnLogout);
            this.pnlTop.Controls.Add(this.btnMyInfo);
            this.pnlTop.Controls.Add(this.label1);
            this.pnlTop.Controls.Add(this.pictureBox1);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1026, 60);
            this.pnlTop.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightGray;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 59);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1026, 1);
            this.panel1.TabIndex = 4;
            // 
            // btnLogout
            // 
            this.btnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogout.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnLogout.FlatAppearance.BorderSize = 2;
            this.btnLogout.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnLogout.ForeColor = System.Drawing.Color.DodgerBlue;
            this.btnLogout.Location = new System.Drawing.Point(912, 16);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(98, 29);
            this.btnLogout.TabIndex = 3;
            this.btnLogout.Text = "로그아웃";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnMyInfo
            // 
            this.btnMyInfo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnMyInfo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnMyInfo.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnMyInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMyInfo.Font = new System.Drawing.Font("굴림", 9F);
            this.btnMyInfo.ForeColor = System.Drawing.Color.White;
            this.btnMyInfo.Location = new System.Drawing.Point(798, 16);
            this.btnMyInfo.Margin = new System.Windows.Forms.Padding(0);
            this.btnMyInfo.Name = "btnMyInfo";
            this.btnMyInfo.Size = new System.Drawing.Size(109, 29);
            this.btnMyInfo.TabIndex = 2;
            this.btnMyInfo.Text = "개인정보변경";
            this.btnMyInfo.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Noto Sans KR Medium", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(194, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 29);
            this.label1.TabIndex = 1;
            this.label1.Text = "통합업무관리";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Totalworkcontrol.Properties.Resources.okpos_logo;
            this.pictureBox1.Location = new System.Drawing.Point(12, 17);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(176, 29);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.Color.White;
            this.pnlLeft.Controls.Add(this.panel2);
            this.pnlLeft.Controls.Add(this.lblMenuDev);
            this.pnlLeft.Controls.Add(this.lblMenuMain);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 60);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(180, 467);
            this.pnlLeft.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LightGray;
            this.panel2.Location = new System.Drawing.Point(179, 0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.panel2.Size = new System.Drawing.Size(1, 600);
            this.panel2.TabIndex = 2;
            // 
            // lblMenuDev
            // 
            this.lblMenuDev.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMenuDev.AutoSize = true;
            this.lblMenuDev.Font = new System.Drawing.Font("Noto Sans KR", 10F, System.Drawing.FontStyle.Bold);
            this.lblMenuDev.Location = new System.Drawing.Point(12, 37);
            this.lblMenuDev.Name = "lblMenuDev";
            this.lblMenuDev.Padding = new System.Windows.Forms.Padding(40, 0, 0, 0);
            this.lblMenuDev.Size = new System.Drawing.Size(105, 20);
            this.lblMenuDev.TabIndex = 1;
            this.lblMenuDev.Text = "개발 요청";
            this.lblMenuDev.Click += new System.EventHandler(this.lblMenuDev_Click);
            // 
            // lblMenuMain
            // 
            this.lblMenuMain.AutoSize = true;
            this.lblMenuMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblMenuMain.Font = new System.Drawing.Font("Noto Sans KR", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMenuMain.Location = new System.Drawing.Point(0, 0);
            this.lblMenuMain.Name = "lblMenuMain";
            this.lblMenuMain.Padding = new System.Windows.Forms.Padding(20, 10, 0, 10);
            this.lblMenuMain.Size = new System.Drawing.Size(109, 44);
            this.lblMenuMain.TabIndex = 0;
            this.lblMenuMain.Text = "메인 페이지";
            this.lblMenuMain.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMenuMain.Click += new System.EventHandler(this.lblMenuMain_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(180, 60);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(846, 467);
            this.pnlMain.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1026, 527);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.pnlTop);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnlLeft.ResumeLayout(false);
            this.pnlLeft.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnMyInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblMenuMain;
        private System.Windows.Forms.Label lblMenuDev;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}