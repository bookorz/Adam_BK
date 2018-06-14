using Adam.Util;
using MySql.Data.MySqlClient;
using NPOI;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
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
        public FormQuery()
        {
            InitializeComponent();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            DBUtil dBUtil = new DBUtil();
            StringBuilder sql = new StringBuilder();
            sql.Append("\n SELECT *");
            sql.Append("\n   FROM device_code_params ");
            //set parameter
            Dictionary<string, object> param = new Dictionary<string, object>();
            //Query
            DataTableReader rs = dBUtil.GetDataReader(sql.ToString(), param);
            MySqlDataAdapter adapter  = dBUtil.GetDataAdapter(sql.ToString());
            dataTable = new DataTable();
            adapter.Fill(dataTable);
            dgvResult.DataSource = dataTable;
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
            FileStream file = new FileStream(@"d:\" + fileName, FileMode.Create);//產生檔案
            wb.Write(file);
            file.Close();

            Process.Start(@"d:\" + fileName);
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
    }
}
