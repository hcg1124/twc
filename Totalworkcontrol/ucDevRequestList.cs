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

namespace Totalworkcontrol
{
    public partial class ucDevRequestList : UserControl
    {
        private const string HINT_CUSTOMER = "거래처명";
        private const string HINT_TITLE = "요청제목";
        private string _initialStatus = null;

        public ucDevRequestList()
        {
            InitializeComponent();
            // 힌트 텍스트 기능을 위한 이벤트 연결
            this.txtCustomerName.Enter += new EventHandler(RemoveHintText);
            this.txtCustomerName.Leave += new EventHandler(SetHintText);
            this.txtTitle.Enter += new EventHandler(RemoveHintText);
            this.txtTitle.Leave += new EventHandler(SetHintText);
        }

        public ucDevRequestList(string initialStatus)
        {
            InitializeComponent();
            _initialStatus = initialStatus;
            // 힌트 텍스트 기능을 위한 이벤트 연결
            this.txtCustomerName.Enter += new EventHandler(RemoveHintText);
            this.txtCustomerName.Leave += new EventHandler(SetHintText);
            this.txtTitle.Enter += new EventHandler(RemoveHintText);
            this.txtTitle.Leave += new EventHandler(SetHintText);
        }

        private void ucDevRequestList_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            BindComboBoxes(); // 콤보박스 데이터 채우기

            // 외부에서 검색어를 받아왔으면 콤보박스에 설정
            if (!string.IsNullOrEmpty(_initialStatus))
            {
                cmbStatus.Text = _initialStatus;
            }

            // 힌트 텍스트 초기화
            SetHintText(txtCustomerName, null);
            SetHintText(txtTitle, null);

            // 데이터 목록 조회
            LoadRequestData();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadRequestData();
        }

        private void btnNewRequest_Click(object sender, EventArgs e)
        {
            MainForm main = this.ParentForm as MainForm;
            if (main != null)
            {
                main.ShowControl(new ucDevRequestDetail());
            }
        }

        private void dgvRequestList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvRequestList.Columns[e.ColumnIndex].Name == "Progress" && e.RowIndex >= 0)
            {
                int requestNo = Convert.ToInt32(dgvRequestList.Rows[e.RowIndex].Cells["RequestNo"].Value);
                MainForm main = this.ParentForm as MainForm;
                if (main != null)
                {
                    main.ShowControl(new ucDevRequestDetail(requestNo));
                }
            }
        }

        private void dgvRequestList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvRequestList.Columns[e.ColumnIndex].Name == "StatusName" && e.Value != null)
            {
                string status = e.Value.ToString();
                switch (status)
                {
                    case "진행": e.CellStyle.ForeColor = Color.Green; break;
                    case "취소": e.CellStyle.ForeColor = Color.Red; break;
                    case "완료": e.CellStyle.ForeColor = Color.Blue; break;
                    default: e.CellStyle.ForeColor = Color.Black; break;
                }
            }
        }

        private void SetupDataGridView()
        {
            dgvRequestList.AutoGenerateColumns = false;
            dgvRequestList.Columns.Clear();

            // 스타일 설정
            dgvRequestList.BorderStyle = BorderStyle.None;
            dgvRequestList.BackgroundColor = Color.White;
            dgvRequestList.GridColor = Color.LightGray;
            dgvRequestList.RowHeadersVisible = false;
            dgvRequestList.AllowUserToAddRows = false;
            dgvRequestList.ReadOnly = true;
            dgvRequestList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRequestList.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvRequestList.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dgvRequestList.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvRequestList.ColumnHeadersDefaultCellStyle.Font = new Font("맑은 고딕", 10F, FontStyle.Bold);
            dgvRequestList.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvRequestList.EnableHeadersVisualStyles = false;
            dgvRequestList.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dgvRequestList.DefaultCellStyle.Font = new Font("맑은 고딕", 9.5F);
            dgvRequestList.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvRequestList.RowTemplate.Height = 28;

            // 컬럼 추가
            dgvRequestList.Columns.Add(new DataGridViewTextBoxColumn { Name = "RequestNo", HeaderText = "No", DataPropertyName = "RequestNo" });
            dgvRequestList.Columns.Add(new DataGridViewTextBoxColumn { Name = "CustomerName", HeaderText = "거래처명", DataPropertyName = "CustomerName" });
            dgvRequestList.Columns.Add(new DataGridViewTextBoxColumn { Name = "Title", HeaderText = "요청제목", DataPropertyName = "Title" });
            dgvRequestList.Columns.Add(new DataGridViewTextBoxColumn { Name = "Solutions", HeaderText = "솔루션", DataPropertyName = "Solutions" });
            dgvRequestList.Columns.Add(new DataGridViewTextBoxColumn { Name = "RequesterName", HeaderText = "영업담당", DataPropertyName = "RequesterName" });
            dgvRequestList.Columns.Add(new DataGridViewTextBoxColumn { Name = "RequestDate", HeaderText = "작성일", DataPropertyName = "RequestDate" });
            dgvRequestList.Columns.Add(new DataGridViewTextBoxColumn { Name = "StatusName", HeaderText = "상태", DataPropertyName = "StatusName" });
            dgvRequestList.Columns.Add(new DataGridViewButtonColumn { Name = "Progress", HeaderText = "진행", Text = "현황", UseColumnTextForButtonValue = true });

            // 컬럼별 너비 및 정렬 조절
            dgvRequestList.Columns["RequestNo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvRequestList.Columns["RequesterName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvRequestList.Columns["RequestDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvRequestList.Columns["StatusName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvRequestList.Columns["RequestNo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgvRequestList.Columns["CustomerName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvRequestList.Columns["Title"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvRequestList.Columns["Solutions"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvRequestList.Columns["RequesterName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgvRequestList.Columns["RequestDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgvRequestList.Columns["StatusName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgvRequestList.Columns["Progress"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
        }

        private void BindComboBoxes()
        {
            BindComboBox(cmbSalesperson, "U", "- 영업담당 -", "sales");
            BindComboBox(cmbSolution, "L", "- 솔루션 -");
            BindComboBox(cmbStatus, "T", "- 상태 -");
        }

        private void BindComboBox(ComboBox cmb, string category, string displayName, string userPrefix = null)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["db_conn"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string sql = "";
                    if (category == "U")
                    {
                        sql = "SELECT UserID as Code, UserName as CodeName FROM Users WHERE 1=1";
                        if (!string.IsNullOrEmpty(userPrefix))
                        {
                            sql += " AND UserID LIKE @Prefix";
                        }
                    }
                    else
                    {
                        sql = "SELECT Code, CodeName FROM CommonCodes WHERE Category = @Category ORDER BY CodeID";
                    }

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        if (category != "U") cmd.Parameters.AddWithValue("@Category", category);
                        if (!string.IsNullOrEmpty(userPrefix)) cmd.Parameters.AddWithValue("@Prefix", userPrefix + "%");

                        DataTable dt = new DataTable();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dt);

                        DataRow dr = dt.NewRow();
                        dr["Code"] = "";
                        dr["CodeName"] = displayName;
                        dt.Rows.InsertAt(dr, 0);

                        cmb.DataSource = dt;
                        cmb.DisplayMember = "CodeName";
                        cmb.ValueMember = "Code";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(displayName + " 목록을 불러오는 중 오류가 발생했습니다.\n" + ex.Message, "DB 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadRequestData()
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["db_conn"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();

                    StringBuilder sqlBuilder = new StringBuilder();
                    sqlBuilder.Append(@"
                        SELECT
                            req.RequestNo,
                            req.CustomerName,
                            req.Title,
                            STUFF(
                                (SELECT ',' + s.CodeName
                                 FROM DevRequestSolutions sol
                                 JOIN CommonCodes s ON sol.SolutionCode = s.Code AND s.Category = 'L'
                                 WHERE sol.RequestNo = req.RequestNo
                                 FOR XML PATH('')), 1, 1, ''
                            ) AS Solutions,
                            req.RequestDate,
                            stat.CodeName AS StatusName,
                            req.RequesterID,
                            urs.UserName AS RequesterName
                        FROM
                            DevRequests req
                        LEFT JOIN Users urs ON req.RequesterID = urs.UserID
                        LEFT JOIN CommonCodes stat ON req.Status = stat.Code AND stat.Category = 'T'
                        WHERE 1=1 ");

                    SqlCommand cmd = new SqlCommand();

                    if (!string.IsNullOrWhiteSpace(txtCustomerName.Text) && txtCustomerName.ForeColor == Color.Black)
                    {
                        sqlBuilder.Append(" AND req.CustomerName LIKE @CustomerName");
                        cmd.Parameters.AddWithValue("@CustomerName", "%" + txtCustomerName.Text + "%");
                    }
                    if (!string.IsNullOrWhiteSpace(txtTitle.Text) && txtTitle.ForeColor == Color.Black)
                    {
                        sqlBuilder.Append(" AND req.Title LIKE @Title");
                        cmd.Parameters.AddWithValue("@Title", "%" + txtTitle.Text + "%");
                    }
                    if (cmbSalesperson.SelectedIndex > 0)
                    {
                        sqlBuilder.Append(" AND req.RequesterID = @RequesterID");
                        cmd.Parameters.AddWithValue("@RequesterID", cmbSalesperson.SelectedValue);
                    }
                    if (cmbSolution.SelectedIndex > 0)
                    {
                        sqlBuilder.Append(" AND EXISTS (SELECT 1 FROM DevRequestSolutions WHERE RequestNo = req.RequestNo AND SolutionCode = @SolutionCode)");
                        cmd.Parameters.AddWithValue("@SolutionCode", cmbSolution.SelectedValue);
                    }
                    if (cmbStatus.SelectedIndex > 0)
                    {
                        sqlBuilder.Append(" AND req.Status = @Status");
                        cmd.Parameters.AddWithValue("@Status", cmbStatus.SelectedValue);
                    }

                    cmd.CommandText = sqlBuilder.ToString();
                    cmd.Connection = conn;

                    DataTable dt = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);

                    dgvRequestList.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("데이터를 불러오는 중 오류가 발생했습니다.\n" + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 텍스트박스 힌트 기능
        private void RemoveHintText(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (txt.ForeColor == Color.Gray)
            {
                txt.Text = "";
                txt.ForeColor = Color.Black;
            }
        }

        private void SetHintText(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (string.IsNullOrWhiteSpace(txt.Text))
            {
                if (txt.Name == "txtCustomerName")
                {
                    txt.Text = "거래처명";
                }
                else if (txt.Name == "txtTitle")
                {
                    txt.Text = "요청제목";
                }
                txt.ForeColor = Color.Gray;
            }
        }
    }
}
