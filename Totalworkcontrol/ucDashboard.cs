using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;

namespace Totalworkcontrol
{
    public partial class ucDashboard : UserControl
    {
        private ToolTip chartToolTip = new ToolTip();
        private DataPoint lastHoveredPoint = null;

        public ucDashboard()
        {
            InitializeComponent();
        }

        private void ucDashboard_Load(object sender, EventArgs e)
        {
            this.chart1.MouseMove += new MouseEventHandler(chart1_MouseMove);
            this.chart1.MouseLeave += new EventHandler(chart1_MouseLeave);

            foreach(Label lbl in tableLayoutPanel2.Controls.OfType<Label>())
            {
                lbl.Cursor = Cursors.Hand;
                lbl.Click += new EventHandler(StatusLabel_Click);
            }

            // '서식자료' 그룹박스(groupBox2) 안에 있는 모든 버튼에 다운로드 이벤트를 연결합니다.
            foreach (Button btn in tableLayoutPanel3.Controls.OfType<Button>())
            {
                btn.Click += new EventHandler(btnDownload_Click);
            }
            LoadDashboardData();
        }
        /// '서식자료'의 다운로드 버튼을 클릭했을 때 실행됩니다.
        /// <summary>
        /// '서식자료'의 다운로드 버튼을 클릭했을 때 실행됩니다.
        /// </summary>
        private void btnDownload_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton == null || clickedButton.Tag == null)
            {
                MessageBox.Show("버튼에 다운로드할 파일 코드가 지정되지 않았습니다. (Tag 속성 확인)");
                return;
            }

            string formCode = clickedButton.Tag.ToString();
            byte[] fileData = null;
            string fileName = "downloaded_file";

            // 1. DB에서 파일 데이터와 파일 이름을 가져옵니다.
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["db_conn"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string sql = "SELECT FileData, FileName FROM FormFiles WHERE FormCode = @FormCode";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@FormCode", formCode);
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            if (reader["FileData"] != DBNull.Value)
                            {
                                fileData = (byte[])reader["FileData"];
                                fileName = reader["FileName"].ToString();
                            }
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("DB에서 파일을 불러오는 중 오류가 발생했습니다.\n" + ex.Message, "DB 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2. DB에 파일이 없으면 사용자에게 알립니다.
            if (fileData == null)
            {
                MessageBox.Show("데이터베이스에 해당 서식이 등록되어 있지 않습니다.\n먼저 파일을 업로드해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 3. 파일 저장 대화상자를 띄웁니다.
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Title = "파일 저장";
            saveDialog.FileName = fileName; // DB에서 가져온 원래 파일 이름
            saveDialog.Filter = "모든 파일 (*.*)|*.*";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // 4. 사용자가 선택한 경로에 파일 데이터를 저장합니다.
                    File.WriteAllBytes(saveDialog.FileName, fileData);
                    MessageBox.Show("파일이 성공적으로 저장되었습니다.", "저장 완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("파일을 저장하는 중 오류가 발생했습니다.\n" + ex.Message, "저장 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void LoadDashboardData()
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["db_conn"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    // ▼▼▼ 이 SQL 쿼리가 수정되었습니다 ▼▼▼
                    // Status 코드 대신 CodeName(한글 이름)으로 그룹화해서 개수를 셉니다.
                    string sql = @"
                        SELECT 
                            ISNULL(cc.CodeName, '미지정') AS StatusName, 
                            COUNT(req.RequestNo) AS StatusCount
                        FROM DevRequests req
                        LEFT JOIN CommonCodes cc ON req.Status = cc.Code AND cc.Category = 'T'
                        GROUP BY cc.CodeName";
                    // ▲▲▲ 여기까지 수정 ▲▲▲

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        Dictionary<string, int> statusData = new Dictionary<string, int>();
                        while (reader.Read())
                        {
                            statusData[reader["StatusName"].ToString()] = Convert.ToInt32(reader["StatusCount"]);
                        }
                        reader.Close();

                        UpdatePieChart(statusData);
                        UpdateStatusLabels(statusData);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("대시보드 데이터를 불러오는 중 오류가 발생했습니다.\n" + ex.Message, "DB 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdatePieChart(Dictionary<string, int> data)
        {
            chart1.Series.Clear();
            chart1.Titles.Clear();
            chart1.Titles.Add("개발 요청 진행 현황");
            chart1.Titles[0].Font = new Font("맑은 고딕", 12F, FontStyle.Bold);

            Series series = new Series("Status")
            {
                ChartType = SeriesChartType.Doughnut,
                Font = new Font("Arial", 10F, FontStyle.Bold),
                LabelForeColor = Color.White
            };
            series.SetCustomProperty("PieDonutRadius", "60");

            foreach (var entry in data)
            {
                DataPoint dp = new DataPoint();
                dp.SetValueY(entry.Value);
                dp.LegendText = entry.Key;
                dp.Label = entry.Value.ToString();
                dp.ToolTip = string.Format("{0}\n{1} ({2:P1})", entry.Key, entry.Value, (double)entry.Value / data.Values.Sum());
                series.Points.Add(dp);
            }
            chart1.Series.Add(series);
        }

        private void UpdateStatusLabels(Dictionary<string, int> data)
        {
            // 이제 data의 Key가 "대기", "진행" 등 한글이므로 정상적으로 값을 찾을 수 있습니다.
            int waiting = data.ContainsKey("대기") ? data["대기"] : 0;
            int inProgress = data.ContainsKey("진행") ? data["진행"] : 0;
            int cancelled = data.ContainsKey("취소") ? data["취소"] : 0; // DB에 '취소' 상태가 없으면 0이 됩니다.
            int completed = data.ContainsKey("완료") ? data["완료"] : 0;

            lblWaiting.Text = string.Format("대기 | {0}", waiting);
            lblInProgress.Text = string.Format("진행 | {0}", inProgress);
            lblCancelled.Text = string.Format("취소 | {0}", cancelled);
            lblCompleted.Text = string.Format("완료 | {0}", completed);
        }

        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {
            var result = chart1.HitTest(e.X, e.Y);
            if (result.ChartElementType == ChartElementType.DataPoint)
            {
                DataPoint currentPoint = (DataPoint)result.Object;
                if (lastHoveredPoint != currentPoint)
                {
                    if (lastHoveredPoint != null)
                    {
                        lastHoveredPoint.SetCustomProperty("Exploded", "false");
                    }
                    currentPoint.SetCustomProperty("Exploded", "true");
                    lastHoveredPoint = currentPoint;
                    chartToolTip.Show(currentPoint.ToolTip, chart1, e.Location.X + 10, e.Location.Y - 15);
                }
            }
            else
            {
                chart1_MouseLeave(sender, e);
                chartToolTip.Hide(chart1);
            }
        }
        private void chart1_MouseLeave(object sender, EventArgs e)
        {
            if (lastHoveredPoint != null)
            {
                lastHoveredPoint.SetCustomProperty("Exploded", "false");
                lastHoveredPoint = null;
            }
        }

        // 디자이너에서 실수로 생성된 이벤트 핸들러
        private void groupBox2_Enter(object sender, EventArgs e) { }
        
        private void chart1_MouseClick(object sender, MouseEventArgs e)
        {
            HitTestResult result = chart1.HitTest(e.X, e.Y);

            if (result.ChartElementType == ChartElementType.DataPoint)
            {
                DataPoint clickedPoint = chart1.Series[0].Points[result.PointIndex];
                string statusName = clickedPoint.LegendText;

                MainForm main = this.ParentForm as MainForm;
                if (main != null)
                {
                    main.ShowControl(new ucDevRequestList(statusName));
                }
            }
        }
        private void StatusLabel_Click(object sender, EventArgs e)
        {
            // C# 5.0 (VS2013) 호환 방식으로 수정
            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {
                // 라벨의 텍스트("대기 | 0")에서 "|" 앞의 글자("대기")만 잘라냅니다.
                string statusName = clickedLabel.Text.Split('|')[0].Trim();

                // 메인 폼을 찾아서 목록 화면으로 전환을 요청합니다.
                MainForm main = this.ParentForm as MainForm;
                if (main != null)
                {
                    main.ShowControl(new ucDevRequestList(statusName));
                }
            }
        }


    }
}
