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
        public ucDevRequestList()
        {
            InitializeComponent();
            this.txtCustomerName.Enter += new EventHandler(RemoveHintText);
            this.txtCustomerName.Leave += new EventHandler(SetHintText);
            // 요청제목 텍스트박스
            this.txtTitle.Enter += new EventHandler(RemoveHintText);
            this.txtTitle.Leave += new EventHandler(SetHintText);
        }

        private void ucDevRequestList_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            BindComboBox(cmbdamdang, "P", "영업담당", "S%"); // 'P' 카테고리 중 코드가 'S'로 시작하는 담당자
            BindComboBox(cmbsolution, "L", "솔루션");     // 'L' 카테고리 (솔루션)
            BindComboBox(cmbStatus, "T", "상태");         // 'T' 카테고리 (상태)
            // ▲▲▲ 여기까지 수정 ▲▲▲
            SetHintText(txtCustomerName, null);
            SetHintText(txtTitle, null);
            LoadRequestData();
        }
        /// <summary>
        /// 텍스트박스에 포커스가 들어올 때 힌트를 지웁니다.
        /// </summary>
        private void RemoveHintText(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            // 현재 텍스트가 힌트일 경우에만 내용을 지우고 글자색을 검정으로 바꿉니다.
            if (tb.ForeColor == Color.Gray)
            {
                tb.Text = "";
                tb.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// 텍스트박스에서 포커스가 나갈 때 내용이 없으면 힌트를 보여줍니다.
        /// </summary>
        private void SetHintText(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                // 어떤 텍스트박스인지 이름으로 구분해서 알맞은 힌트를 넣습니다.
                if (tb.Name == "txtCustomerName")
                {
                    tb.Text = HINT_CUSTOMER;
                }
                else if (tb.Name == "txtTitle")
                {
                    tb.Text = HINT_TITLE;
                }
                tb.ForeColor = Color.Gray;
            }
        }

        /// <summary>
        /// 공통 코드 테이블에서 데이터를 가져와 콤보박스에 바인딩합니다.
        /// </summary>
        private void BindComboBox(ComboBox cmb, string category, string displayName, string codeFilter = null)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["db_conn"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    // ▼▼▼ SQL 쿼리에 필터링 기능이 추가되었습니다 ▼▼▼
                    string sql = "SELECT Code, CodeName FROM CommonCodes WHERE Category = @Category";
                    if (!string.IsNullOrEmpty(codeFilter))
                    {
                        sql += " AND Code LIKE @CodeFilter";
                    }
                    sql += " ORDER BY CodeID";
                    // ▲▲▲ 여기까지 수정 ▲▲▲

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Category", category);
                        if (!string.IsNullOrEmpty(codeFilter))
                        {
                            cmd.Parameters.AddWithValue("@CodeFilter", codeFilter);
                        }

                        DataTable dt = new DataTable();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dt);

                        DataRow dr = dt.NewRow();
                        dr["Code"] = ""; // 실제 값은 비워둡니다.
                        dr["CodeName"] = string.Format("- {0} -", displayName);
                        dt.Rows.InsertAt(dr, 0);

                        cmb.DataSource = dt;
                        cmb.DisplayMember = "CodeName"; // 사용자에게 보여줄 값
                        cmb.ValueMember = "Code";       // 실제 코드에서 사용할 값
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("콤보박스 데이터를 불러오는 중 오류가 발생했습니다.\n" + ex.Message, "DB 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadRequestData();
        }

        /// <summary>
        /// DataGridView의 컬럼과 스타일을 설정합니다.
        /// </summary>
        private void SetupDataGridView()
        {
            dgvRequestList.AutoGenerateColumns = false;

            // 스타일 설정
            dgvRequestList.BorderStyle = BorderStyle.None;
            dgvRequestList.BackgroundColor = Color.White;
            dgvRequestList.GridColor = Color.LightGray;
            dgvRequestList.RowHeadersVisible = false;
            dgvRequestList.AllowUserToAddRows = false;
            dgvRequestList.ReadOnly = true;
            dgvRequestList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // 헤더 스타일
            dgvRequestList.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvRequestList.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dgvRequestList.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvRequestList.ColumnHeadersDefaultCellStyle.Font = new Font("맑은 고딕", 10F, FontStyle.Bold);
            dgvRequestList.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvRequestList.EnableHeadersVisualStyles = false;

            dgvRequestList.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            // 행 스타일
            dgvRequestList.DefaultCellStyle.Font = new Font("맑은 고딕", 9.5F);
            dgvRequestList.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvRequestList.RowTemplate.Height = 30;

            // 컬럼 추가
            dgvRequestList.Columns.Clear();
            dgvRequestList.Columns.Add("RequestNo", "No");
            dgvRequestList.Columns.Add("CustomerName", "거래처명");
            dgvRequestList.Columns.Add("Title", "요청제목");
            dgvRequestList.Columns.Add("Solutions", "솔루션");
            dgvRequestList.Columns.Add("RequesterID", "영업담당");
            dgvRequestList.Columns.Add("RequesterName", "영업담당자");
            dgvRequestList.Columns.Add("RequestDate", "작성일");
            dgvRequestList.Columns.Add("StatusName", "상태");
            dgvRequestList.Columns.Add(new DataGridViewButtonColumn { Name = "Progress", HeaderText = "진행", Text = "현황", UseColumnTextForButtonValue = true });

            // 데이터 바인딩
            dgvRequestList.Columns["RequestNo"].DataPropertyName = "RequestNo";
            dgvRequestList.Columns["CustomerName"].DataPropertyName = "CustomerName";
            dgvRequestList.Columns["Title"].DataPropertyName = "Title";
            dgvRequestList.Columns["Solutions"].DataPropertyName = "Solutions";
            dgvRequestList.Columns["RequesterID"].DataPropertyName = "RequesterID";
            dgvRequestList.Columns["RequesterID"].Visible = false;
            dgvRequestList.Columns["RequesterName"].DataPropertyName = "RequesterName";
            dgvRequestList.Columns["RequestDate"].DataPropertyName = "RequestDate";
            dgvRequestList.Columns["StatusName"].DataPropertyName = "StatusName";
            dgvRequestList.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // 컬럼 너비 자동 조절
            foreach (DataGridViewColumn col in dgvRequestList.Columns)
            {
                if (col.Name == "Title")
                {
                    // '요청제목'만 남는 공간을 꽉 채우도록 합니다.
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                else
                {
                    // 나머지 모든 컬럼은 헤더(제목) 길이에 딱 맞춥니다.
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                }
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
                    // ▼▼▼ SQL 쿼리의 JOIN 부분이 수정되었습니다 ▼▼▼
                    sqlBuilder.Append(@"
                       SELECT
                            req.RequestNo,
                            req.CustomerName,
                            req.Title,
                            STUFF(
                                (SELECT ',' + s.CodeName
                                 FROM DevProgressSolutions sol
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
                        WHERE 1=1");
                    // ▲▲▲ 여기까지 수정 ▲▲▲

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
                    // ▲▲▲ 여기까지 추가 ▲▲▲

                    // 검색 조건 추가
                    if (cmbdamdang.SelectedIndex > 0)
                    {
                        sqlBuilder.Append(" AND req.RequesterID = @RequesterID");
                        cmd.Parameters.AddWithValue("@RequesterID", cmbdamdang.SelectedValue);
                    }
                    if (cmbsolution.SelectedIndex > 0)
                    {
                        // 파라미터 이름을 @SolutionCode로 변경하여 충돌 가능성을 제거합니다.
                        sqlBuilder.Append(" AND req.Solutions LIKE @SolutionCode");
                        cmd.Parameters.AddWithValue("@SolutionCode", "%" + cmbsolution.SelectedValue + "%");
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

        private void dgvRequestList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // '상태' 컬럼일 경우에만 실행
            if (dgvRequestList.Columns[e.ColumnIndex].Name == "StatusName" && e.Value != null)
            {
                string status = e.Value.ToString();

                // 상태 값에 따라 글자색을 바꿉니다.
                switch (status)
                {
                    case "진행":
                        e.CellStyle.ForeColor = Color.Green;
                        break;
                    case "취소":
                        e.CellStyle.ForeColor = Color.Red;
                        break;
                    case "완료":
                        e.CellStyle.ForeColor = Color.Blue;
                        break;
                    default:
                        e.CellStyle.ForeColor = Color.Black;
                        break;
                }
            }
        }

        private void dgvRequestList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 클릭된 컬럼이 'Progress'(현황 버튼)이고, 헤더가 아닌 경우에만 실행
            if (dgvRequestList.Columns[e.ColumnIndex].Name == "Progress" && e.RowIndex >= 0)
            {
                // 선택된 행의 'RequestNo' 값을 가져옵니다.
                int requestNo = Convert.ToInt32(dgvRequestList.Rows[e.RowIndex].Cells["RequestNo"].Value);

                // 메인 폼의 화면 전환 함수를 호출하여 상세 화면을 띄웁니다.
                MainForm main = this.ParentForm as MainForm;
                if (main != null)
                {
                    main.ShowControl(new ucDevRequestDetail(requestNo));
                }
            }
        }

        private void btnNewRequest_Click(object sender, EventArgs e)
        {
            MainForm main = this.ParentForm as MainForm;
            if (main != null)
            {
                // RequestNo 없이 Detail 화면을 호출하여 '신규' 모드로 진입합니다.
                main.ShowControl(new ucDevRequestDetail());
            }

        }
    }
}
