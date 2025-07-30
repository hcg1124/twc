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
            LoadDashboardData();
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
    }
}
