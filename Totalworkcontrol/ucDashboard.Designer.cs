namespace Totalworkcontrol
{
    partial class ucDashboard
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblInProgress = new System.Windows.Forms.Label();
            this.lblCompleted = new System.Windows.Forms.Label();
            this.lblCancelled = new System.Windows.Forms.Label();
            this.lblWaiting = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.btnDownloadEstimate = new System.Windows.Forms.Button();
            this.btnDownloadInspection = new System.Windows.Forms.Button();
            this.btnDownloadOutputCondition = new System.Windows.Forms.Button();
            this.btnDownloadRequirement = new System.Windows.Forms.Button();
            this.btnDownloadMeetingMinutes = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.CornflowerBlue;
            this.panel2.Controls.Add(this.label5);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(258, 21);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(256, 58);
            this.panel2.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(93, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 24);
            this.label5.TabIndex = 5;
            this.label5.Text = "개발 진행";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(1, 21);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(256, 58);
            this.panel1.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(95, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "요청현황";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label8.Location = new System.Drawing.Point(363, 219);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 17);
            this.label8.TabIndex = 7;
            this.label8.Text = "label8";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label8.Visible = false;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label7.Location = new System.Drawing.Point(363, 159);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 17);
            this.label7.TabIndex = 6;
            this.label7.Text = "label7";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label7.Visible = false;
            // 
            // lblInProgress
            // 
            this.lblInProgress.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblInProgress.AutoSize = true;
            this.lblInProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblInProgress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblInProgress.Location = new System.Drawing.Point(363, 100);
            this.lblInProgress.Name = "lblInProgress";
            this.lblInProgress.Size = new System.Drawing.Size(46, 17);
            this.lblInProgress.TabIndex = 5;
            this.lblInProgress.Text = "label6";
            this.lblInProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCompleted
            // 
            this.lblCompleted.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblCompleted.AutoSize = true;
            this.lblCompleted.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblCompleted.ForeColor = System.Drawing.Color.Blue;
            this.lblCompleted.Location = new System.Drawing.Point(106, 219);
            this.lblCompleted.Name = "lblCompleted";
            this.lblCompleted.Size = new System.Drawing.Size(46, 17);
            this.lblCompleted.TabIndex = 3;
            this.lblCompleted.Text = "label4";
            this.lblCompleted.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCancelled
            // 
            this.lblCancelled.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblCancelled.AutoSize = true;
            this.lblCancelled.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblCancelled.ForeColor = System.Drawing.Color.Red;
            this.lblCancelled.Location = new System.Drawing.Point(106, 159);
            this.lblCancelled.Name = "lblCancelled";
            this.lblCancelled.Size = new System.Drawing.Size(46, 17);
            this.lblCancelled.TabIndex = 2;
            this.lblCancelled.Text = "label3";
            this.lblCancelled.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblWaiting
            // 
            this.lblWaiting.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblWaiting.AutoSize = true;
            this.lblWaiting.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblWaiting.Location = new System.Drawing.Point(106, 100);
            this.lblWaiting.Name = "lblWaiting";
            this.lblWaiting.Size = new System.Drawing.Size(46, 17);
            this.lblWaiting.TabIndex = 1;
            this.lblWaiting.Text = "label2";
            this.lblWaiting.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.lblWaiting, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblCancelled, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.lblCompleted, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.lblInProgress, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label7, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.label8, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(336, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.Padding = new System.Windows.Forms.Padding(0, 20, 20, 0);
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(535, 259);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // chart1
            // 
            this.chart1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(0, 0);
            this.chart1.Margin = new System.Windows.Forms.Padding(30, 3, 3, 3);
            this.chart1.Name = "chart1";
            this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Pastel;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Doughnut;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(336, 259);
            this.chart1.TabIndex = 5;
            this.chart1.Text = "chart1";
            this.chart1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseClick);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.tableLayoutPanel3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 290);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(871, 208);
            this.panel3.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(28, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 21);
            this.label2.TabIndex = 3;
            this.label2.Text = "서식자료";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Outset;
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnDownloadEstimate, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnDownloadInspection, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.btnDownloadOutputCondition, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.btnDownloadRequirement, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnDownloadMeetingMinutes, 3, 1);
            this.tableLayoutPanel3.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.label6, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.label9, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.label10, 2, 1);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(22, 41);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(826, 162);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 21);
            this.label3.Margin = new System.Windows.Forms.Padding(15, 0, 3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "▪️ 견적서";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnDownloadEstimate
            // 
            this.btnDownloadEstimate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnDownloadEstimate.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnDownloadEstimate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownloadEstimate.ForeColor = System.Drawing.Color.White;
            this.btnDownloadEstimate.Location = new System.Drawing.Point(228, 11);
            this.btnDownloadEstimate.Margin = new System.Windows.Forms.Padding(20, 3, 3, 3);
            this.btnDownloadEstimate.Name = "btnDownloadEstimate";
            this.btnDownloadEstimate.Size = new System.Drawing.Size(78, 32);
            this.btnDownloadEstimate.TabIndex = 1;
            this.btnDownloadEstimate.Text = "다운로드";
            this.btnDownloadEstimate.UseVisualStyleBackColor = false;
            // 
            // btnDownloadInspection
            // 
            this.btnDownloadInspection.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnDownloadInspection.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnDownloadInspection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownloadInspection.ForeColor = System.Drawing.Color.White;
            this.btnDownloadInspection.Location = new System.Drawing.Point(228, 63);
            this.btnDownloadInspection.Margin = new System.Windows.Forms.Padding(20, 3, 3, 3);
            this.btnDownloadInspection.Name = "btnDownloadInspection";
            this.btnDownloadInspection.Size = new System.Drawing.Size(78, 32);
            this.btnDownloadInspection.TabIndex = 2;
            this.btnDownloadInspection.Text = "다운로드";
            this.btnDownloadInspection.UseVisualStyleBackColor = false;
            // 
            // btnDownloadOutputCondition
            // 
            this.btnDownloadOutputCondition.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnDownloadOutputCondition.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnDownloadOutputCondition.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownloadOutputCondition.ForeColor = System.Drawing.Color.White;
            this.btnDownloadOutputCondition.Location = new System.Drawing.Point(228, 117);
            this.btnDownloadOutputCondition.Margin = new System.Windows.Forms.Padding(20, 3, 3, 3);
            this.btnDownloadOutputCondition.Name = "btnDownloadOutputCondition";
            this.btnDownloadOutputCondition.Size = new System.Drawing.Size(78, 32);
            this.btnDownloadOutputCondition.TabIndex = 3;
            this.btnDownloadOutputCondition.Text = "다운로드";
            this.btnDownloadOutputCondition.UseVisualStyleBackColor = false;
            // 
            // btnDownloadRequirement
            // 
            this.btnDownloadRequirement.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnDownloadRequirement.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnDownloadRequirement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownloadRequirement.ForeColor = System.Drawing.Color.White;
            this.btnDownloadRequirement.Location = new System.Drawing.Point(640, 11);
            this.btnDownloadRequirement.Margin = new System.Windows.Forms.Padding(20, 3, 3, 3);
            this.btnDownloadRequirement.Name = "btnDownloadRequirement";
            this.btnDownloadRequirement.Size = new System.Drawing.Size(78, 32);
            this.btnDownloadRequirement.TabIndex = 4;
            this.btnDownloadRequirement.Tag = "REQ";
            this.btnDownloadRequirement.Text = "다운로드";
            this.btnDownloadRequirement.UseVisualStyleBackColor = false;
            // 
            // btnDownloadMeetingMinutes
            // 
            this.btnDownloadMeetingMinutes.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnDownloadMeetingMinutes.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnDownloadMeetingMinutes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownloadMeetingMinutes.ForeColor = System.Drawing.Color.White;
            this.btnDownloadMeetingMinutes.Location = new System.Drawing.Point(640, 63);
            this.btnDownloadMeetingMinutes.Margin = new System.Windows.Forms.Padding(20, 3, 3, 3);
            this.btnDownloadMeetingMinutes.Name = "btnDownloadMeetingMinutes";
            this.btnDownloadMeetingMinutes.Size = new System.Drawing.Size(78, 32);
            this.btnDownloadMeetingMinutes.TabIndex = 5;
            this.btnDownloadMeetingMinutes.Text = "다운로드";
            this.btnDownloadMeetingMinutes.UseVisualStyleBackColor = false;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 73);
            this.label4.Margin = new System.Windows.Forms.Padding(15, 0, 3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "▪️ 검수 확인서";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 127);
            this.label6.Margin = new System.Windows.Forms.Padding(15, 0, 3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 12);
            this.label6.TabIndex = 7;
            this.label6.Text = "▪️ 산출물 작성조건";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(429, 21);
            this.label9.Margin = new System.Windows.Forms.Padding(15, 0, 3, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 12);
            this.label9.TabIndex = 8;
            this.label9.Text = "▪️ 요건 정의서";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(429, 73);
            this.label10.Margin = new System.Windows.Forms.Padding(15, 0, 3, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 12);
            this.label10.TabIndex = 9;
            this.label10.Text = "▪️ 업무 회의록";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ucDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.panel3);
            this.Name = "ucDashboard";
            this.Size = new System.Drawing.Size(871, 498);
            this.Load += new System.EventHandler(this.ucDashboard_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblInProgress;
        private System.Windows.Forms.Label lblCompleted;
        private System.Windows.Forms.Label lblCancelled;
        private System.Windows.Forms.Label lblWaiting;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnDownloadEstimate;
        private System.Windows.Forms.Button btnDownloadInspection;
        private System.Windows.Forms.Button btnDownloadOutputCondition;
        private System.Windows.Forms.Button btnDownloadRequirement;
        private System.Windows.Forms.Button btnDownloadMeetingMinutes;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
    }
}
