namespace Totalworkcontrol
{
    partial class ucDevRequestList
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

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.btnNewRequest = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.roundedPanel5 = new Totalworkcontrol.RoundedPanel();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.roundedPanel4 = new Totalworkcontrol.RoundedPanel();
            this.cmbsolution = new System.Windows.Forms.ComboBox();
            this.roundedPanel3 = new Totalworkcontrol.RoundedPanel();
            this.cmbdamdang = new System.Windows.Forms.ComboBox();
            this.roundedPanel2 = new Totalworkcontrol.RoundedPanel();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.roundedPanel1 = new Totalworkcontrol.RoundedPanel();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dgvRequestList = new System.Windows.Forms.DataGridView();
            this.pnlSearch.SuspendLayout();
            this.roundedPanel5.SuspendLayout();
            this.roundedPanel4.SuspendLayout();
            this.roundedPanel3.SuspendLayout();
            this.roundedPanel2.SuspendLayout();
            this.roundedPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRequestList)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlSearch
            // 
            this.pnlSearch.Controls.Add(this.btnNewRequest);
            this.pnlSearch.Controls.Add(this.panel1);
            this.pnlSearch.Controls.Add(this.roundedPanel5);
            this.pnlSearch.Controls.Add(this.roundedPanel4);
            this.pnlSearch.Controls.Add(this.roundedPanel3);
            this.pnlSearch.Controls.Add(this.roundedPanel2);
            this.pnlSearch.Controls.Add(this.roundedPanel1);
            this.pnlSearch.Controls.Add(this.btnSearch);
            this.pnlSearch.Location = new System.Drawing.Point(0, 0);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(780, 96);
            this.pnlSearch.TabIndex = 0;
            // 
            // btnNewRequest
            // 
            this.btnNewRequest.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnNewRequest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewRequest.Font = new System.Drawing.Font("굴림", 10F);
            this.btnNewRequest.ForeColor = System.Drawing.Color.White;
            this.btnNewRequest.Location = new System.Drawing.Point(699, 11);
            this.btnNewRequest.Name = "btnNewRequest";
            this.btnNewRequest.Size = new System.Drawing.Size(59, 33);
            this.btnNewRequest.TabIndex = 16;
            this.btnNewRequest.Text = "신규";
            this.btnNewRequest.UseVisualStyleBackColor = false;
            this.btnNewRequest.Click += new System.EventHandler(this.btnNewRequest_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightGray;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.ForeColor = System.Drawing.Color.DarkGray;
            this.panel1.Location = new System.Drawing.Point(0, 95);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(780, 1);
            this.panel1.TabIndex = 15;
            this.panel1.Visible = false;
            // 
            // roundedPanel5
            // 
            this.roundedPanel5.BackColor = System.Drawing.Color.White;
            this.roundedPanel5.BorderColor = System.Drawing.Color.LightGray;
            this.roundedPanel5.Controls.Add(this.cmbStatus);
            this.roundedPanel5.CornerRadius = 10;
            this.roundedPanel5.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.roundedPanel5.LabelBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.roundedPanel5.LabelText = "상태";
            this.roundedPanel5.LabelWidth = 80;
            this.roundedPanel5.Location = new System.Drawing.Point(478, 54);
            this.roundedPanel5.Name = "roundedPanel5";
            this.roundedPanel5.Size = new System.Drawing.Size(200, 31);
            this.roundedPanel5.TabIndex = 14;
            // 
            // cmbStatus
            // 
            this.cmbStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(83, 4);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(105, 25);
            this.cmbStatus.TabIndex = 2;
            // 
            // roundedPanel4
            // 
            this.roundedPanel4.BackColor = System.Drawing.Color.White;
            this.roundedPanel4.BorderColor = System.Drawing.Color.LightGray;
            this.roundedPanel4.Controls.Add(this.cmbsolution);
            this.roundedPanel4.CornerRadius = 10;
            this.roundedPanel4.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.roundedPanel4.LabelBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.roundedPanel4.LabelText = "솔루션";
            this.roundedPanel4.LabelWidth = 80;
            this.roundedPanel4.Location = new System.Drawing.Point(251, 55);
            this.roundedPanel4.Name = "roundedPanel4";
            this.roundedPanel4.Size = new System.Drawing.Size(200, 29);
            this.roundedPanel4.TabIndex = 13;
            // 
            // cmbsolution
            // 
            this.cmbsolution.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbsolution.FormattingEnabled = true;
            this.cmbsolution.Location = new System.Drawing.Point(87, 2);
            this.cmbsolution.Name = "cmbsolution";
            this.cmbsolution.Size = new System.Drawing.Size(101, 25);
            this.cmbsolution.TabIndex = 5;
            // 
            // roundedPanel3
            // 
            this.roundedPanel3.BackColor = System.Drawing.Color.White;
            this.roundedPanel3.BorderColor = System.Drawing.Color.LightGray;
            this.roundedPanel3.Controls.Add(this.cmbdamdang);
            this.roundedPanel3.CornerRadius = 10;
            this.roundedPanel3.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.roundedPanel3.LabelBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.roundedPanel3.LabelText = "영업담당";
            this.roundedPanel3.LabelWidth = 80;
            this.roundedPanel3.Location = new System.Drawing.Point(24, 54);
            this.roundedPanel3.Name = "roundedPanel3";
            this.roundedPanel3.Size = new System.Drawing.Size(200, 31);
            this.roundedPanel3.TabIndex = 2;
            // 
            // cmbdamdang
            // 
            this.cmbdamdang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbdamdang.FormattingEnabled = true;
            this.cmbdamdang.Location = new System.Drawing.Point(86, 4);
            this.cmbdamdang.Name = "cmbdamdang";
            this.cmbdamdang.Size = new System.Drawing.Size(110, 25);
            this.cmbdamdang.TabIndex = 14;
            // 
            // roundedPanel2
            // 
            this.roundedPanel2.BackColor = System.Drawing.Color.White;
            this.roundedPanel2.BorderColor = System.Drawing.Color.LightGray;
            this.roundedPanel2.Controls.Add(this.txtTitle);
            this.roundedPanel2.CornerRadius = 5;
            this.roundedPanel2.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.roundedPanel2.LabelBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.roundedPanel2.LabelText = "요청제목";
            this.roundedPanel2.LabelWidth = 80;
            this.roundedPanel2.Location = new System.Drawing.Point(251, 15);
            this.roundedPanel2.Name = "roundedPanel2";
            this.roundedPanel2.Size = new System.Drawing.Size(200, 29);
            this.roundedPanel2.TabIndex = 12;
            // 
            // txtTitle
            // 
            this.txtTitle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTitle.Location = new System.Drawing.Point(87, 7);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(97, 18);
            this.txtTitle.TabIndex = 4;
            // 
            // roundedPanel1
            // 
            this.roundedPanel1.BackColor = System.Drawing.Color.White;
            this.roundedPanel1.BorderColor = System.Drawing.Color.LightGray;
            this.roundedPanel1.Controls.Add(this.txtCustomerName);
            this.roundedPanel1.CornerRadius = 5;
            this.roundedPanel1.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.roundedPanel1.LabelBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.roundedPanel1.LabelText = "거래처명";
            this.roundedPanel1.LabelWidth = 80;
            this.roundedPanel1.Location = new System.Drawing.Point(24, 15);
            this.roundedPanel1.Name = "roundedPanel1";
            this.roundedPanel1.Padding = new System.Windows.Forms.Padding(3);
            this.roundedPanel1.Size = new System.Drawing.Size(200, 31);
            this.roundedPanel1.TabIndex = 11;
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCustomerName.Location = new System.Drawing.Point(88, 9);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Size = new System.Drawing.Size(104, 18);
            this.txtCustomerName.TabIndex = 1;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("굴림", 10F);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(699, 50);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(59, 33);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "검색";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dgvRequestList
            // 
            this.dgvRequestList.AllowUserToResizeColumns = false;
            this.dgvRequestList.AllowUserToResizeRows = false;
            this.dgvRequestList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRequestList.Location = new System.Drawing.Point(0, 96);
            this.dgvRequestList.Name = "dgvRequestList";
            this.dgvRequestList.RowTemplate.Height = 23;
            this.dgvRequestList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvRequestList.Size = new System.Drawing.Size(780, 306);
            this.dgvRequestList.TabIndex = 1;
            this.dgvRequestList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRequestList_CellContentClick);
            this.dgvRequestList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRequestList_CellDoubleClick);
            this.dgvRequestList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvRequestList_CellFormatting);
            // 
            // ucDevRequestList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvRequestList);
            this.Controls.Add(this.pnlSearch);
            this.Name = "ucDevRequestList";
            this.Size = new System.Drawing.Size(780, 408);
            this.Load += new System.EventHandler(this.ucDevRequestList_Load);
            this.pnlSearch.ResumeLayout(false);
            this.roundedPanel5.ResumeLayout(false);
            this.roundedPanel4.ResumeLayout(false);
            this.roundedPanel3.ResumeLayout(false);
            this.roundedPanel2.ResumeLayout(false);
            this.roundedPanel2.PerformLayout();
            this.roundedPanel1.ResumeLayout(false);
            this.roundedPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRequestList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.DataGridView dgvRequestList;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.ComboBox cmbsolution;
        private RoundedPanel roundedPanel1;
        private System.Windows.Forms.TextBox txtCustomerName;
        private RoundedPanel roundedPanel2;
        private RoundedPanel roundedPanel5;
        private System.Windows.Forms.ComboBox cmbStatus;
        private RoundedPanel roundedPanel4;
        private RoundedPanel roundedPanel3;
        private System.Windows.Forms.ComboBox cmbdamdang;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnNewRequest;
    }
}
