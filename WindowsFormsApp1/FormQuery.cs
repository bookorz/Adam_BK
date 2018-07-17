using MySql.Data.MySqlClient;
using NPOI;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using SANWA.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class FormQuery : Form
    {
        DataTable dataTable;
        string sqlScript = "";
        ToolTip toolTip1 = new ToolTip();
        ToolTip toolTip2 = new ToolTip();
        ToolTip toolTip3 = new ToolTip();

        public string getSqlContent(string scriptName)
        {
            StringBuilder sql = new StringBuilder();
            string[] lines = System.IO.File.ReadAllLines(@".\sql\" + scriptName + ".sql");
            foreach (string line in lines)
            {
                sql.Append(line);
            }

            return sql.ToString();
        }

        public FormQuery()
        {
            InitializeComponent();
        }

        private DataTable dtCondition = new DataTable();

        private void btnQuery_Click(object sender, EventArgs e)
        {
            DBUtil dBUtil = new DBUtil();
            //StringBuilder sql = new StringBuilder();
            //sql.Append("\n SELECT *");
            //sql.Append("\n   FROM device_code_params ");


            //set parameter
            Dictionary<string, object> param = new Dictionary<string, object>();
            //Query
            //DataTableReader rs = dBUtil.GetDataReader(sql.ToString(), param);
            //MySqlDataAdapter adapter  = dBUtil.GetDataAdapter(sql.ToString());
            //MySqlDataAdapter adapter = dBUtil.GetDataAdapter(this.getSqlContent(sqlScript));
            //dataTable = new DataTable();
            //adapter.Fill(dataTable);
            DateTime fromDt = this.dtpFromDate.Value;
            DateTime toDt = this.dtpToDate.Value;

            string cond_1 = txbCondition1.Text.Equals("") ? "%" : txbCondition1.Text;
            string cond_2 = txbCondition2.Text.Equals("") ? "%" : txbCondition2.Text;
            string cond_3 = txbCondition3.Text.Equals("") ? "%" : txbCondition3.Text;
            param.Add("@from_dt", fromDt.ToString("yyyy-MM-dd 00:00:00.000000"));
            param.Add("@to_dt", toDt.ToString("yyyy-MM-dd 23:59:59.999999"));
            param.Add("@cond_1", cond_1);
            param.Add("@cond_2", cond_2);
            param.Add("@cond_3", cond_3);
            dataTable = dBUtil.GetDataTable(getSqlContent(sqlScript), param);
            gdvData.DataSource = dataTable;
            btnExport.Enabled = true;
        }

        private void DataTableToExcelFile(DataTable dt)
        {
            //建立Excel 2003檔案
            HSSFWorkbook wb = new HSSFWorkbook();
            Sheet ws;

            ////建立Excel 2007檔案
            //IWorkbook wb = new XSSFWorkbook();
            //ISheet ws;

            if (dt.TableName != string.Empty)
            {
                ws = wb.CreateSheet(dt.TableName);
            }
            else
            {
                ws = wb.CreateSheet("Sheet1");
            }

            ws.CreateRow(0);//第一行為欄位名稱
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ws.GetRow(0).CreateCell(i).SetCellValue(dt.Columns[i].ColumnName);
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ws.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ws.GetRow(i + 1).CreateCell(j).SetCellValue(dt.Rows[i][j].ToString());
                }
            }

            string fileName =  cbQueryType.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_result.xls";
            FileStream file = new FileStream(@"d:\sanwa\export\" + fileName, FileMode.Create);//產生檔案
            wb.Write(file);
            file.Close();

            Process.Start(@"d:\sanwa\export\" + fileName);
            //OpenFileDialog dialog = new OpenFileDialog();
            //dialog.Title = "Open file";
            //dialog.InitialDirectory = "D:\\";
            //dialog.Filter = "xls files (*.*)|*.xls*";
            //if (dialog.ShowDialog() == DialogResult.OK)
            //{
            //    MessageBox.Show(dialog.FileName);
            //}
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            DataTableToExcelFile(dataTable);
        }

        private void FormQuery_Load(object sender, EventArgs e)
        {
            try
            {
                ClearUI();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        private void ClearUI()
        {
            cbQueryType.Text = string.Empty;
            dtpFromDate.Value = DateTime.Now;
            dtpToDate.Value = DateTime.Now;

            txbCondition1.Text = string.Empty;
            txbCondition2.Text = string.Empty;
            txbCondition3.Text = string.Empty;

            gdvData.DataSource = null;
        }

        private void ConditionTable()
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        private void setToolTip(ToolTip toolTip, Control control, string tip)
        {
            toolTip.SetToolTip(control, tip);
            //以下為提示視窗的設定(通常會設定的部分)
            //停滯多久顯示 Hint
            toolTip.AutomaticDelay = 100;
            //ToolTipIcon：設定顯示在提示視窗的圖示類型。
            toolTip.ToolTipIcon = ToolTipIcon.Info;
            //ForeColor：顧名思義就是前景顏色，你懂的!!XD
            toolTip.ForeColor = Color.Blue;
            //BackColor：顧名思義就是背景顏色，你也懂的!!XD
            toolTip.BackColor = Color.Gray;
            //AutoPopDelay：當游標停滯在控制項，顯示提示視窗的時間。(以毫秒為單位)
            toolTip.AutoPopDelay = 10000;
            //ToolTipTitle：設定提示視窗的標題。
            toolTip.ToolTipTitle = "Hint";
        }
        private void cbQueryType_SelectedValueChanged(object sender, EventArgs e)
        {
            //init
            btnExport.Enabled = false;
            this.labCondition1.Visible = false;
            this.labCondition2.Visible = false;
            this.labCondition3.Visible = false;
            this.txbCondition1.Visible = false;
            this.txbCondition2.Visible = false;
            this.txbCondition3.Visible = false;
            switch (cbQueryType.SelectedItem)
            {
                case "Alarm Log":
                    sqlScript = "Alarm Log";
                    //this.labCondition1.Text = "User ID";
                    //this.txbCondition1.Text = "";
                    //setToolTip(toolTip1, txbCondition1, "Logged user id.");
                    //this.labCondition1.Visible = true;
                    //this.txbCondition1.Visible = true;
                    btnQuery.Enabled = true;
                    break;
                case "Process Job Log":
                    sqlScript = "Process Job Log";
                    //this.labCondition1.Text = "User ID";
                    //this.txbCondition1.Text = "";
                    //setToolTip(toolTip1, txbCondition1, "Logged user id.");
                    //this.labCondition1.Visible = true;
                    //this.txbCondition1.Visible = true;
                    btnQuery.Enabled = true;
                    break;
                case "User Action Log":
                    sqlScript = "User Action Log";
                    this.labCondition1.Text = "User ID";
                    this.txbCondition1.Text = "";
                    setToolTip(toolTip1,txbCondition1, "Logged user id.");
                    this.labCondition1.Visible = true;
                    this.txbCondition1.Visible = true;
                    btnQuery.Enabled = true;
                    break;
                case "Command Log":
                    sqlScript = "Command Log";
                    this.labCondition1.Text = "Device Type";
                    this.txbCondition1.Text = "";
                    setToolTip(toolTip1, txbCondition1, "LoadPort,Robot,Aligner.");
                    this.labCondition2.Text = "Device Name";
                    this.txbCondition2.Text = "";
                    setToolTip(toolTip2, txbCondition2, "LoadPort1~8,Robot1~2,Aligner1~2.");
                    this.labCondition1.Visible = true;
                    this.txbCondition1.Visible = true;
                    this.labCondition2.Visible = true;
                    this.txbCondition2.Visible = true;
                    btnQuery.Enabled = true;
                    break;
                case "DIO Setting Log":
                    sqlScript = "DIO Setting Log";
                    setToolTip(toolTip1, txbCondition1, "Logged user id.");
                    this.labCondition1.Visible = true;
                    this.txbCondition1.Visible = true;
                    btnQuery.Enabled = true;
                    break;
                default:
                    MessageBox.Show("不支援的報表查詢!", "Info");
                    btnQuery.Enabled = false;
                    break;
            }
           
        }

    }
}
