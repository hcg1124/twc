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
    public partial class ucDevRequestDetail : UserControl
    {
        private int _requestNo;
        private List<string> _mainRequestSolutionCodes = new List<string>();
        private bool _isDataLoading = false; // 데이터 로딩 중인지 확인하는 플래그
    

        public ucDevRequestDetail()
        {
            InitializeComponent();
            _requestNo = 0; // 0이면 '신규' 모드
        }

        public ucDevRequestDetail(int requestNo)
        {
            InitializeComponent();
            _requestNo = requestNo; // 0보다 크면 '수정/조회' 모드
        }

        private void ucDevRequestDetail_Load(object sender, EventArgs e)
        {
            SetupProgressGrid();
            SetupOutputsGrid();
            BindComboBoxes();
            this.cmbDeveloper.SelectedIndexChanged += new System.EventHandler(this.cmbDeveloper_SelectedIndexChanged);

            if (_requestNo > 0)
            {
                LoadRequestData();
            }
            else
            {
                // '신규' 모드일 때 화면을 깨끗하게 비웁니다.
                ClearControls();
            }
        }
        /// <summary>
        /// '개발담당' 콤보박스의 선택이 변경될 때마다 실행됩니다.
        /// </summary>
        private void cmbDeveloper_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 데이터가 코드에 의해 로딩되는 중에는 이벤트를 무시합니다.
            if (_isDataLoading) return;

            // 사용자가 직접 개발자를 변경한 경우, 관련 컨트롤을 초기화합니다.
            txtProgressDescription.Text = "";
            foreach (CheckBox chk in flowLayoutPanel2.Controls.OfType<CheckBox>())
            {
                chk.Checked = false;
            }
        }
        // '신규' 버튼 클릭 이벤트
        private void btnNew_Click(object sender, EventArgs e)
        {
            _requestNo = 0; // 모드를 '신규'로 변경
            ClearControls();  // 화면 초기화
        }

        // 메인 '저장' 버튼 클릭 이벤트
        private void btnSaveRequest_Click(object sender, EventArgs e)
        {
            SaveRequestData();
        }

        // 상세진행 '저장' 버튼 클릭 이벤트
        private void btnSaveProgress_Click(object sender, EventArgs e)
        {
            SaveProgressData();
        }
        // 상세진행 '삭제' 버튼
        private void btnDeleteProgress_Click(object sender, EventArgs e)
        {
            // 1. 표에서 선택된 행이 있는지 확인
            if (dgvProgress.SelectedRows.Count == 0)
            {
                MessageBox.Show("삭제할 진행 내역을 목록에서 선택해주세요.");
                return;
            }

            // 2. 정말로 삭제할 것인지 사용자에게 확인
            if (MessageBox.Show("선택한 개발 진행 내역을 정말로 삭제하시겠습니까?", "삭제 확인", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }

            // 3. 선택된 행에서 RequestNo와 DeveloperID를 가져옴
            string developerId = dgvProgress.SelectedRows[0].Cells["DeveloperID"].Value.ToString();

            string connString = ConfigurationManager.ConnectionStrings["db_conn"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();

                try
                {
                    // 4. 자식 테이블(DevProgressSolutions)의 데이터를 먼저 삭제
                    string sqlDeleteSolutions = "DELETE FROM DevProgressSolutions WHERE RequestNo = @RequestNo AND DeveloperID = @DeveloperID";
                    using (SqlCommand cmd = new SqlCommand(sqlDeleteSolutions, conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@RequestNo", _requestNo);
                        cmd.Parameters.AddWithValue("@DeveloperID", developerId);
                        cmd.ExecuteNonQuery();
                    }

                    // 5. 부모 테이블(DevRequestsDetail)의 데이터를 삭제
                    string sqlDeleteDetail = "DELETE FROM DevRequestsDetail WHERE RequestNo = @RequestNo AND DeveloperID = @DeveloperID";
                    using (SqlCommand cmd = new SqlCommand(sqlDeleteDetail, conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@RequestNo", _requestNo);
                        cmd.Parameters.AddWithValue("@DeveloperID", developerId);
                        cmd.ExecuteNonQuery();
                    }

                    tran.Commit(); // 모든 삭제가 성공하면 최종 저장
                    MessageBox.Show("성공적으로 삭제되었습니다.");
                    LoadProgressGrid(); // 삭제 후 그리드 새로고침
                }
                catch (Exception ex)
                {
                    tran.Rollback(); // 하나라도 실패하면 모든 변경사항 되돌리기
                    MessageBox.Show("삭제 중 오류가 발생했습니다.\n" + ex.Message, "DB 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        // '검색' (목록으로 돌아가기) 버튼 클릭 이벤트
        private void btnSearchCustomer_Click(object sender, EventArgs e)
        {
            MessageBox.Show("버튼 눌렸다!"); // <-- 이 줄 추가
            SaveProgressData();
            MainForm main = this.ParentForm as MainForm;
            if (main != null)
            {
                main.ShowControl(new ucDevRequestList());
            }
        }

        /// <summary>
        /// 화면의 모든 컨트롤을 초기 상태로 되돌립니다.
        /// </summary>
        private void ClearControls()
        {
            txtCustomerName.Text = "";
            cmbRequester.SelectedIndex = 0;
            dtpRequestDate.Value = DateTime.Now;
            dtpCompletionDate.Value = DateTime.Now;
            cmbStatus.SelectedIndex = 0;
            txtTitle.Text = "";
            txtDescription.Text = "";

            foreach (CheckBox chk in flowLayoutPanel1.Controls.OfType<CheckBox>())
            {
                chk.Checked = false;
            }

            numEstimateAmount.Value = 0;
            numContractAmount.Value = 0;
            numDevCost.Value = 0;

            dgvProgress.DataSource = null;
            dgvOutputs.DataSource = null;

            ClearProgressDetailControls();
        }

        /// <summary>
        /// '상세진행' 입력창만 비웁니다.
        /// </summary>
        private void ClearProgressDetailControls()
        {
            cmbDeveloper.SelectedIndex = 0;
            dtpProgressStart.Value = DateTime.Now;
            dtpProgressEnd.Value = DateTime.Now;
            txtProgressDescription.Text = "";
            foreach (CheckBox chk in flowLayoutPanel2.Controls.OfType<CheckBox>())
            {
                chk.Checked = false;
            }
        }

        /// <summary>
        /// DevRequests 테이블과 관련 솔루션을 저장합니다.
        /// </summary>
        private void SaveRequestData()
        {
            if (string.IsNullOrWhiteSpace(txtCustomerName.Text)) { MessageBox.Show("거래처명을 입력해주세요."); return; }
            if (cmbRequester.SelectedIndex <= 0) { MessageBox.Show("요청자를 선택해주세요."); return; }
            if (cmbStatus.SelectedIndex <= 0) { MessageBox.Show("진행상태를 선택해주세요."); return; }

            string connString = ConfigurationManager.ConnectionStrings["db_conn"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction(); // 트랜잭션 시작

                try
                {
                    string sql = "";
                    if (_requestNo == 0) // 신규 등록
                    {
                        sql = @"INSERT INTO DevRequests 
                                    (CustomerName, Title, Description, RequestDate, CompletionHopeDate, Status, RequesterID, EstimateAmount, ContractAmount, DevelopmentCost) 
                                VALUES 
                                    (@CustomerName, @Title, @Description, @RequestDate, @CompletionHopeDate, @Status, @RequesterID, @EstimateAmount, @ContractAmount, @DevelopmentCost);
                                SELECT SCOPE_IDENTITY();";
                    }
                    else // 수정
                    {
                        sql = @"UPDATE DevRequests SET 
                                    CustomerName = @CustomerName, Title = @Title, Description = @Description, RequestDate = @RequestDate, 
                                    CompletionHopeDate = @CompletionHopeDate, Status = @Status, RequesterID = @RequesterID, 
                                    EstimateAmount = @EstimateAmount, ContractAmount = @ContractAmount, DevelopmentCost = @DevelopmentCost
                                WHERE RequestNo = @RequestNo";
                    }

                    using (SqlCommand cmd = new SqlCommand(sql, conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@CustomerName", txtCustomerName.Text);
                        cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
                        cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                        cmd.Parameters.AddWithValue("@RequestDate", dtpRequestDate.Value);
                        cmd.Parameters.AddWithValue("@CompletionHopeDate", dtpCompletionDate.Value);
                        cmd.Parameters.AddWithValue("@Status", cmbStatus.SelectedValue);
                        cmd.Parameters.AddWithValue("@RequesterID", cmbRequester.SelectedValue);
                        cmd.Parameters.AddWithValue("@EstimateAmount", numEstimateAmount.Value);
                        cmd.Parameters.AddWithValue("@ContractAmount", numContractAmount.Value);
                        cmd.Parameters.AddWithValue("@DevelopmentCost", numDevCost.Value);

                        if (_requestNo > 0)
                        {
                            cmd.Parameters.AddWithValue("@RequestNo", _requestNo);
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            _requestNo = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                    }

                    // 솔루션 저장 로직
                    DevRequestSolutions(conn, tran);

                    tran.Commit(); // 모든 작업이 성공하면 최종 저장
                    MessageBox.Show("요청 정보가 성공적으로 저장되었습니다.");
                    LoadRequestData(); // 저장 후 데이터 다시 불러오기
                }
                catch (Exception ex)
                {
                    tran.Rollback(); // 오류 발생 시 모든 변경사항 되돌리기
                    MessageBox.Show("데이터 저장 중 오류가 발생했습니다.\n" + ex.Message, "DB 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// DevRequestSolutions 테이블에 데이터를 저장합니다.
        /// </summary>
        private void DevRequestSolutions(SqlConnection conn, SqlTransaction tran)
        {
            // 1. 기존 솔루션 정보 삭제
            string sqlDelete = "DELETE FROM DevRequestSolutions WHERE RequestNo = @RequestNo";
            using (SqlCommand cmd = new SqlCommand(sqlDelete, conn, tran))
            {
                cmd.Parameters.AddWithValue("@RequestNo", _requestNo);
                cmd.ExecuteNonQuery();
            }

            // 2. 선택된 솔루션 정보 새로 추가
            string sqlInsert = "INSERT INTO DevRequestSolutions (RequestNo, SolutionCode) VALUES (@RequestNo, @SolutionCode)";
            foreach (CheckBox chk in flowLayoutPanel1.Controls.OfType<CheckBox>())
            {
                if (chk.Checked)
                {
                    using (SqlCommand cmd = new SqlCommand(sqlInsert, conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@RequestNo", _requestNo);
                        cmd.Parameters.AddWithValue("@SolutionCode", GetCodeFromCheckbox(chk));
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        /// 선택된 솔루션 코드들이 CommonCodes 테이블에 모두 존재하는지 확인합니다.
        /// </summary>


        /// <summary>
        /// DevRequestsDetail 테이블과 관련 솔루션을 저장합니다.
        /// </summary>
        private void SaveProgressData()
        {
            if (_requestNo == 0) { MessageBox.Show("기본 요청 정보를 먼저 저장해주세요."); return; }
            if (cmbDeveloper.SelectedIndex <= 0) { MessageBox.Show("개발담당자를 선택해주세요."); return; }

            string developerId = cmbDeveloper.SelectedValue.ToString();
            // '상세진행'에서 선택된 솔루션 목록을 미리 준비합니다.
            var solutionsToSave = flowLayoutPanel2.Controls.OfType<CheckBox>()
                                                  .Where(chk => chk.Checked)
                                                  .Select(chk => GetCodeFromCheckbox(chk))
                                                  .ToList();
            string connString = ConfigurationManager.ConnectionStrings["db_conn"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();

                try
                {
                    if (!AreSolutionCodesValid(solutionsToSave, conn, tran))
                    {
                        tran.Rollback();
                        return;
                    }
                    // MERGE 문을 사용하여 INSERT 또는 UPDATE를 한 번에 처리
                    string sql = @"
                        MERGE DevRequestsDetail AS target
                        USING (SELECT @RequestNo AS RequestNo, @DeveloperID AS DeveloperID) AS source
                        ON (target.RequestNo = source.RequestNo AND target.DeveloperID = source.DeveloperID)
                        WHEN MATCHED THEN
                            UPDATE SET ProgressDescription = @ProgressDescription, StartDate = @StartDate, CompletionTargetDate = @CompletionTargetDate
                        WHEN NOT MATCHED THEN
                            INSERT (RequestNo, DeveloperID, ProgressDescription, StartDate, CompletionTargetDate)
                            VALUES (@RequestNo, @DeveloperID, @ProgressDescription, @StartDate, @CompletionTargetDate);";

                    using (SqlCommand cmd = new SqlCommand(sql, conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@RequestNo", _requestNo);
                        cmd.Parameters.AddWithValue("@DeveloperID", developerId);
                        cmd.Parameters.AddWithValue("@ProgressDescription", txtProgressDescription.Text);
                        cmd.Parameters.AddWithValue("@StartDate", dtpProgressStart.Value);
                        cmd.Parameters.AddWithValue("@CompletionTargetDate", dtpProgressEnd.Value);
                        cmd.ExecuteNonQuery();
                    }

                    // 상세 진행 솔루션 저장 로직
                    SaveProgressSolutions(conn, tran, developerId);

                    tran.Commit();
                    MessageBox.Show("개발 진행 내역이 성공적으로 저장되었습니다.");
                    LoadProgressGrid(); // 저장 후 그리드 새로고침
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    MessageBox.Show("개발 진행 내역 저장 중 오류가 발생했습니다.\n" + ex.Message, "DB 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// DevProgressSolutions 테이블에 데이터를 저장합니다.
        /// </summary>
        private void SaveProgressSolutions(SqlConnection conn, SqlTransaction tran, string developerId)
        {
            string sqlDelete = "DELETE FROM DevProgressSolutions WHERE RequestNo = @RequestNo AND DeveloperID = @DeveloperID";
            using (SqlCommand cmd = new SqlCommand(sqlDelete, conn, tran))
            {
                cmd.Parameters.AddWithValue("@RequestNo", _requestNo);
                cmd.Parameters.AddWithValue("@DeveloperID", developerId);
                cmd.ExecuteNonQuery();
            }

            string sqlInsert = "INSERT INTO DevProgressSolutions (RequestNo, DeveloperID, SolutionCode) VALUES (@RequestNo, @DeveloperID, @SolutionCode)";
            foreach (CheckBox chk in flowLayoutPanel2.Controls.OfType<CheckBox>())
            {
                if (chk.Checked)
                {
                    using (SqlCommand cmd = new SqlCommand(sqlInsert, conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@RequestNo", _requestNo);
                        cmd.Parameters.AddWithValue("@DeveloperID", developerId);
                        cmd.Parameters.AddWithValue("@SolutionCode", GetCodeFromCheckbox(chk));
                        cmd.ExecuteNonQuery();
                    }
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
                    string sql = "SELECT * FROM DevRequests WHERE RequestNo = @RequestNo";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@RequestNo", _requestNo);
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            txtCustomerName.Text = reader["CustomerName"].ToString();
                            cmbRequester.SelectedValue = reader["RequesterID"].ToString();
                            dtpRequestDate.Value = reader["RequestDate"] is DBNull ? DateTime.Now : Convert.ToDateTime(reader["RequestDate"]);
                            dtpCompletionDate.Value = reader["CompletionHopeDate"] is DBNull ? DateTime.Now : Convert.ToDateTime(reader["CompletionHopeDate"]);
                            cmbStatus.SelectedValue = reader["Status"].ToString();
                            txtTitle.Text = reader["Title"].ToString();
                            txtDescription.Text = reader["Description"].ToString();

                            numEstimateAmount.Value = reader["EstimateAmount"] is DBNull ? 0 : Convert.ToDecimal(reader["EstimateAmount"]);
                            numContractAmount.Value = reader["ContractAmount"] is DBNull ? 0 : Convert.ToDecimal(reader["ContractAmount"]);
                            numDevCost.Value = reader["DevelopmentCost"] is DBNull ? 0 : Convert.ToDecimal(reader["DevelopmentCost"]);
                        }
                        reader.Close();
                    }
                }
                LoadSolutionsForRequest();
                LoadProgressGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("요청 데이터를 불러오는 중 오류가 발생했습니다.\n" + ex.Message, "DB 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSolutionsForRequest()
        {
            _mainRequestSolutionCodes.Clear(); // 메인 솔루션 목록 초기화
            string connString = ConfigurationManager.ConnectionStrings["db_conn"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string sql = "SELECT SolutionCode FROM DevRequestSolutions WHERE RequestNo = @RequestNo";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@RequestNo", _requestNo);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        _mainRequestSolutionCodes.Add(reader["SolutionCode"].ToString());
                    }
                    reader.Close();
                }
            }
            // 모든 체크박스를 순회하며 상태 설정
            foreach (CheckBox chk in flowLayoutPanel1.Controls.OfType<CheckBox>())
            {
                string code = GetCodeFromCheckbox(chk);
                // 메인 솔루션 목록에 포함되어 있는지 확인
                if (_mainRequestSolutionCodes.Contains(code))
                {
                    chk.Checked = true;
                    chk.Enabled = true; // 포함된 솔루션은 활성화
                }
                else
                {
                    chk.Checked = false;
                    chk.Enabled = true; // 포함되지 않은 솔루션은 비활성화
                }
            }
        
            // 모든 체크박스를 순회하며 상태 설정
            foreach (CheckBox chk in flowLayoutPanel2.Controls.OfType<CheckBox>())
            {
                string code = GetCodeFromCheckbox(chk);
                // 메인 솔루션 목록에 포함되어 있는지 확인
                if (_mainRequestSolutionCodes.Contains(code))
                {
                    chk.Checked = true;
                    chk.Enabled = true; // 포함된 솔루션은 활성화
                }
                else
                {
                    chk.Checked = false;
                    chk.Enabled = false; // 포함되지 않은 솔루션은 비활성화
                }
            }
        }

        private void SetupProgressGrid()
        {
            dgvProgress.AutoGenerateColumns = false;
            dgvProgress.Columns.Clear();
            dgvProgress.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProgress.MultiSelect = false;
            dgvProgress.ReadOnly = true;
            dgvProgress.AllowUserToAddRows = false;
            dgvProgress.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvProgress.Columns.Add(new DataGridViewTextBoxColumn { Name = "DeveloperID", DataPropertyName = "DeveloperID", Visible = false });
            dgvProgress.Columns.Add(new DataGridViewTextBoxColumn { Name = "DeveloperName", HeaderText = "개발담당", DataPropertyName = "DeveloperName" });
            dgvProgress.Columns.Add(new DataGridViewTextBoxColumn { Name = "Solutions", DataPropertyName = "Solutions"});
            dgvProgress.Columns.Add(new DataGridViewTextBoxColumn { Name = "ProgressDescription", HeaderText = "진행 내용", DataPropertyName = "ProgressDescription" });
            dgvProgress.Columns.Add(new DataGridViewTextBoxColumn { Name = "StartDate", HeaderText = "시작일", DataPropertyName = "StartDate" });
            dgvProgress.Columns.Add(new DataGridViewTextBoxColumn { Name = "CompletionTargetDate", HeaderText = "완료 예정일", DataPropertyName = "CompletionTargetDate" });

            foreach (DataGridViewColumn col in dgvProgress.Columns)
            {
                col.ReadOnly = true;
            }
            dgvProgress.Columns["ProgressDescription"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void LoadProgressGrid()
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["db_conn"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string sql = @"
                        SELECT
                            det.DeveloperID,
                            usr.UserName as DeveloperName,
                            det.ProgressDescription,
                            det.StartDate,
                            det.CompletionTargetDate,
                            STUFF(
                                (SELECT ',' + cc.CodeName -- Code 대신 CodeName을 가져옵니다.
                                 FROM DevProgressSolutions sol
                                 JOIN CommonCodes cc ON sol.SolutionCode = cc.Code AND cc.Category = 'L'
                                 WHERE sol.RequestNo = det.RequestNo AND sol.DeveloperID = det.DeveloperID
                                 FOR XML PATH('')), 1, 1, ''
                            ) AS Solutions
                        FROM DevRequestsDetail det
                        LEFT JOIN Users usr ON det.DeveloperID = usr.UserID
                        WHERE det.RequestNo = @RequestNo";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@RequestNo", _requestNo);
                        DataTable dt = new DataTable();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dt);
                        dgvProgress.DataSource = dt;
                    }
                }

                // 그리드 로드 후, 첫 번째 행을 자동으로 선택하고 상세 정보를 표시합니다.
                if (dgvProgress.Rows.Count > 0)
                {
                    dgvProgress.Rows[0].Selected = true;
                    DisplayProgressDetail(dgvProgress.Rows[0]);
                }
                else
                {
                    // 진행 내역이 없으면 상세 정보창을 비웁니다.
                    ClearProgressDetailControls();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("개발 진행 내역을 불러오는 중 오류가 발생했습니다.\n" + ex.Message, "DB 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayProgressDetail(DataGridViewRow row)
        {
            if (row == null || row.Cells["DeveloperID"].Value == null) return;

            string developerId = row.Cells["DeveloperID"].Value.ToString();

            cmbDeveloper.SelectedValue = developerId;
            dtpProgressStart.Value = row.Cells["StartDate"].Value is DBNull ? DateTime.Now : Convert.ToDateTime(row.Cells["StartDate"].Value);
            dtpProgressEnd.Value = row.Cells["CompletionTargetDate"].Value is DBNull ? DateTime.Now : Convert.ToDateTime(row.Cells["CompletionTargetDate"].Value);
            txtProgressDescription.Text = row.Cells["ProgressDescription"].Value.ToString();

            LoadSolutionsForProgress(developerId);
        }

        private void LoadSolutionsForProgress(string developerId)
        {
            var solutionCodes = new List<string>();
            string connString = ConfigurationManager.ConnectionStrings["db_conn"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string sql = "SELECT SolutionCode FROM DevProgressSolutions WHERE RequestNo = @RequestNo AND DeveloperID = @DeveloperID";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@RequestNo", _requestNo);
                    cmd.Parameters.AddWithValue("@DeveloperID", developerId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        solutionCodes.Add(reader["SolutionCode"].ToString());
                    }
                    reader.Close();
                }
            }

            foreach (CheckBox chk in flowLayoutPanel2.Controls.OfType<CheckBox>())
            {
                string code = GetCodeFromCheckbox(chk);
                chk.Checked = solutionCodes.Contains(code);
            }
        }

        private void dgvProgress_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProgress.SelectedRows.Count > 0)
            {
                DisplayProgressDetail(dgvProgress.SelectedRows[0]);
            }
        }

        private void BindComboBoxes()
        {
            BindComboBox(cmbRequester, "U", "- 요청자 선택 -", "sales");
            BindComboBox(cmbStatus, "T", "- 상태 선택 -");
            BindComboBox(cmbDeveloper, "U", "- 개발담당 선택 -", "dev");
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
                    if (category == "U") // Users 테이블에서 가져올 경우
                    {
                        sql = "SELECT UserID as Code, UserName as CodeName FROM Users WHERE 1=1";
                        if (!string.IsNullOrEmpty(userPrefix))
                        {
                            sql += " AND UserID LIKE @Prefix";
                        }
                    }
                    else // CommonCodes 테이블에서 가져올 경우
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
        private bool AreSolutionCodesValid(List<string> solutionCodes, SqlConnection conn, SqlTransaction tran)
        {
            if (solutionCodes.Count == 0) return true; // 검사할 코드가 없으면 통과

            var paramNames = solutionCodes.Select((s, i) => "@p" + i.ToString()).ToArray();
            var inClause = string.Join(",", paramNames);

            string sql = string.Format("SELECT COUNT(*) FROM CommonCodes WHERE Category = 'L' AND Code IN ({0})", inClause);

            using (SqlCommand cmd = new SqlCommand(sql, conn, tran))
            {
                for (int i = 0; i < paramNames.Length; i++)
                {
                    cmd.Parameters.AddWithValue(paramNames[i], solutionCodes[i]);
                }

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count == solutionCodes.Count)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("선택된 솔루션 중 유효하지 않은 코드가 포함되어 있습니다.\n공통 코드를 확인해주세요.", "저장 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
        }
        private void SetupOutputsGrid()
        {
            dgvOutputs.AutoGenerateColumns = false;
            dgvOutputs.Columns.Clear();
            dgvOutputs.AllowUserToAddRows = false;
            dgvOutputs.RowHeadersVisible = false;

            DataGridViewComboBoxColumn cmbCol = new DataGridViewComboBoxColumn();
            cmbCol.Name = "FileType";
            cmbCol.HeaderText = "자료구분";
            cmbCol.DataPropertyName = "FileType";
            cmbCol.FlatStyle = FlatStyle.Flat;
            cmbCol.DataSource = GetCommonCodes("O");
            cmbCol.DisplayMember = "CodeName";
            cmbCol.ValueMember = "Code";
            dgvOutputs.Columns.Add(cmbCol);

            dgvOutputs.Columns.Add(new DataGridViewTextBoxColumn { Name = "FileName", HeaderText = "파일명", DataPropertyName = "FileName", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvOutputs.Columns.Add(new DataGridViewButtonColumn { Name = "SelectFile", HeaderText = "파일선택", Text = "파일 선택", UseColumnTextForButtonValue = true });
            dgvOutputs.Columns.Add(new DataGridViewTextBoxColumn { Name = "AttachedFile", HeaderText = "첨부파일", ReadOnly = true });
        }

        private DataTable GetCommonCodes(string category)
        {
            DataTable dt = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["db_conn"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string sql = "SELECT Code, CodeName FROM CommonCodes WHERE Category = @Category ORDER BY CodeID";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Category", category);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(category + " 공통코드를 불러오는 중 오류가 발생했습니다.\n" + ex.Message, "DB 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        private string GetCodeFromCheckbox(CheckBox chk)
        {
            // 이제 체크박스의 Text 속성을 기준으로 코드를 찾습니다.
            string text = chk.Text.ToUpper().Trim();

            var replacements = new Dictionary<string, string>
            {
                { "POPS+", "POP" },
                { "WAVEKIOSK", "WVK" },
                { "POPSCLOUD", "CLD" },
                { "SMARTORDER", "SMO"},
                { "POSMASTER1.0", "PM1" },
                { "POSMASTER KIOSK 1.0", "PK1" },
                { "ACORN", "ACN" },
                { "POSMASTER2.0", "PM2" },
                { "OKASP2.0", "OA2" },
                { "OKPOS1.0", "OK1" },
                { "OKPOS+", "OKP" },
                { "ETC", "ETC" }
            };

            if (replacements.ContainsKey(text))
            {
                return replacements[text];
            }
            return text;
        }

    }
}
