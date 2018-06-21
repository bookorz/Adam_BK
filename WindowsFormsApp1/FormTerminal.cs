using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SANWA.Utility;
using TransferControl.Management;
using System.Threading;
using System.Collections;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Adam
{
    public partial class FormTerminal : Form
    {
        private DataTable dtDeviceList = new DataTable();
        private DataTable dtCommandType = new DataTable();
        private DataTable dtCommand = new DataTable();
        private DataTable dtCommandParameter = new DataTable();
        private DataTable dtCommandAssembly = new DataTable();

        string strSql = string.Empty;

        private Thread trRun;
        private ArrayList alRun = new ArrayList();

        public FormTerminal()
        {
            InitializeComponent();
        }

        private void FormTerminal_Load(object sender, EventArgs e)
        {
            Util.DBUtil dBUtil = new Util.DBUtil();

            try
            {
                strSql = "SELECT CONCAT(node_id, ',', conn_address) as node_id , node_name, node_type, sn_no, CONCAT(vendor, ',' ,node_type, ',', controller_id) as vendor, model_no, firmware_ver, conn_address, controller_id " +
                            "FROM node " +
                            "WHERE enable_flg = 'Y' AND node_type IN('ALIGNER', 'LOADPORT', 'ROBOT', 'OCR') " +
                            "ORDER BY node_id, sn_no";

                dtDeviceList = dBUtil.GetDataTable(strSql, null);

                if (dtDeviceList.Rows.Count == 0)
                {
                    MessageBox.Show("", "", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    this.Close();
                }
                else
                {
                    lsbDeviceName.DataSource = dtDeviceList;
                    lsbDeviceName.DisplayMember = "node_id";
                    lsbDeviceName.ValueMember = "vendor";
                    lsbDeviceName.SelectedIndex = -1;
                }

                dtCommandAssembly.Columns.Add("No");
                dtCommandAssembly.Columns.Add("Device_Name");
                dtCommandAssembly.Columns.Add("Controller_ID");
                dtCommandAssembly.Columns.Add("Vendor");
                dtCommandAssembly.Columns.Add("CommandShow");
                dtCommandAssembly.Columns.Add("Command");

                dgvCommandList.AutoGenerateColumns = false;
                dgvCommandList.DataSource = dtCommandAssembly;

                trRun = new Thread(CommandRun);
                trRun.IsBackground = true;

                trRun.Start();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        private void lsbDeviceName_Click(object sender, EventArgs e)
        {
            tlpAssemblyUI.Controls.Clear();
            txbManually.Text = string.Empty;

            if (lsbDeviceName.SelectedIndex < 0)
            {
                lsbCommandType.DataSource = null;
                lsbCommand.DataSource = null;
                txbNotice.Text = string.Empty;
                return;
            }

            lsbCommand.DataSource = null;
            txbNotice.Text = string.Empty;

            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            Util.DBUtil dBUtil = new Util.DBUtil();

            try
            {
                if (dtCommandType == null)
                    dtCommandType = new DataTable();

                strSql = "SELECT DISTINCT code_type " +
                            "FROM device_code_params " +
                            "WHERE node_type = @node_type " +
                            "AND vendor = @vendor " +
                            "ORDER BY code_type, code_order, Parameters_Amount";

                keyValues.Add("@node_type", lsbDeviceName.SelectedValue.ToString().Split(',')[1].ToString());
                keyValues.Add("@vendor", lsbDeviceName.SelectedValue.ToString().Split(',')[0].ToString());

                dtCommandType = dBUtil.GetDataTable(strSql, keyValues);

                if (dtCommandType.Rows.Count == 0)
                    throw new DataException();
                else
                {
                    lsbCommandType.DataSource = dtCommandType;
                    lsbCommandType.DisplayMember = "code_type";
                    lsbCommandType.ValueMember = "code_type";
                    lsbCommandType.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        private void lsbCommandType_Click(object sender, EventArgs e)
        {
            tlpAssemblyUI.Controls.Clear();
            txbManually.Text = string.Empty;

            if (lsbCommandType.SelectedIndex < 0)
            {
                lsbCommand.DataSource = null;
                txbNotice.Text = string.Empty;
                return;
            }

            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            Util.DBUtil dBUtil = new Util.DBUtil();

            try
            {
                if (dtCommand == null)
                    dtCommand = new DataTable();

                strSql = "SELECT DISTINCT code_id, code_name " +
                            "FROM device_code_params " +
                            "WHERE node_type = @node_type " +
                            "AND vendor = @vendor " +
                            "AND code_type = @code_type " +
                            "ORDER BY code_type, code_order, Parameters_Amount";

                keyValues.Add("@node_type", lsbDeviceName.SelectedValue.ToString().Split(',')[1].ToString());
                keyValues.Add("@vendor", lsbDeviceName.SelectedValue.ToString().Split(',')[0].ToString());
                keyValues.Add("@code_type", lsbCommandType.SelectedValue.ToString());

                dtCommand = dBUtil.GetDataTable(strSql, keyValues);

                if (dtCommand.Rows.Count == 0)
                    throw new DataException();
                else
                {
                    lsbCommand.DataSource = dtCommand;
                    lsbCommand.DisplayMember = "code_id";
                    lsbCommand.ValueMember = "code_name";
                    lsbCommand.SelectedIndex = -1;
                    txbNotice.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        private void lsbCommand_Click(object sender, EventArgs e)
        {
            if (lsbCommand.SelectedIndex < 0)
            {
                txbNotice.Text = string.Empty;
                return;
            }

            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            Util.DBUtil dBUtil = new Util.DBUtil();

            try
            {
                txbNotice.Text = lsbCommand.SelectedValue.ToString();

                if (dtCommandParameter == null)
                    dtCommandParameter = new DataTable();

                strSql = "SELECT * , CONCAT(node_type, '_', vendor, '_', code_type, '_', code_id, '_', Parameter_Order) as UI_Name " +
                            "FROM device_code_params " +
                            "WHERE node_type = @node_type " +
                            "AND vendor = @vendor " +
                            "AND code_type = @code_type " +
                            "AND code_id = @code_id " +
                            "ORDER BY code_type, code_order, Parameters_Amount";

                keyValues.Add("@node_type", lsbDeviceName.SelectedValue.ToString().Split(',')[1].ToString());
                keyValues.Add("@vendor", lsbDeviceName.SelectedValue.ToString().Split(',')[0].ToString());
                keyValues.Add("@code_type", lsbCommandType.SelectedValue.ToString());
                keyValues.Add("@code_id", lsbCommand.Text);

                dtCommandParameter = dBUtil.GetDataTable(strSql, keyValues);

                if (dtCommandParameter.Rows.Count == 0)
                    throw new DataException();
                else
                {
                    CreateUI();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        private void CreateUI()
        {
            tlpAssemblyUI.Controls.Clear();
            txbManually.Text = string.Empty;

            if (dtCommandParameter.Rows.Count == 0)
                return;

            tlpAssemblyUI.Controls.Clear();
            txbManually.Text = string.Empty;
            Label lbTemp;
            ComboBox cmbTemp;
            NumericUpDown nudTemp;
            string[] strsTemp;
            StringBuilder sbTemp;
            ContainerSet container = new ContainerSet();
            string strCommandFormatParameter = string.Empty;
            ToolTip toolTip = new ToolTip();

            try
            {
                sbTemp = new StringBuilder();

                foreach (DataRow dr in dtCommandParameter.Rows)
                {
                    if (dr["Parameter_ID"].ToString().Equals("Null"))
                    {
                        break;
                    }

                    lbTemp = new Label();
                    lbTemp.Name = dr["UI_Name"].ToString();
                    lbTemp.Dock = DockStyle.Fill;
                    lbTemp.Text = dr["Parameter_ID"].ToString();
                    lbTemp.TextAlign = ContentAlignment.MiddleRight;

                    toolTip.SetToolTip(lbTemp, dr["Parameter_Description"].ToString());
                    tlpAssemblyUI.Controls.Add(lbTemp);

                    if (dr["Data_Value"].ToString().Equals(string.Empty))
                    {
                        nudTemp = new NumericUpDown();
                        nudTemp.Value = Convert.ToInt32(dr["Default_Value"].ToString());
                        nudTemp.Maximum = Convert.ToInt32(dr["Max_Value"].ToString());
                        nudTemp.Minimum = Convert.ToInt32(dr["Min_Value"].ToString());
                        nudTemp.Dock = DockStyle.Fill;

                        if (Convert.ToInt32(dr["Values_length"].ToString()) > 1 && dr["Is_Fill"].ToString() == "Y")
                        {
                            sbTemp.Append(Convert.ToInt32(dr["Default_Value"].ToString()).ToString("D" + Convert.ToInt32(dr["Values_length"].ToString()).ToString()));
                            sbTemp.Append(",");
                            nudTemp.Tag = Convert.ToInt32(dr["Values_length"].ToString());
                        }
                        else
                        {
                            sbTemp.Append(dr["Default_Value"].ToString());
                            sbTemp.Append(",");
                        }

                        nudTemp.ValueChanged += new EventHandler(numericUpDown_ValueChanged);
                        toolTip.SetToolTip(nudTemp, dr["Parameter_Description"].ToString());
                        tlpAssemblyUI.Controls.Add(nudTemp);
                    }
                    else
                    {
                        strsTemp = dr["Min_Value"].ToString().Split(',');
                        cmbTemp = new ComboBox();
                        cmbTemp.Items.AddRange(strsTemp);
                        cmbTemp.Dock = DockStyle.Fill;
                        cmbTemp.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        cmbTemp.AutoCompleteSource = AutoCompleteSource.ListItems;
                        sbTemp.Append(cmbTemp.Text);
                        sbTemp.Append(",");
                        cmbTemp.DropDownClosed += new EventHandler(comboBox_DropDownClosed);
                        toolTip.SetToolTip(cmbTemp, dr["Parameter_Description"].ToString());
                        tlpAssemblyUI.Controls.Add(cmbTemp);

                    }
                }

                if (tlpAssemblyUI.Controls.Count == 0)
                {
                    switch (lsbDeviceName.SelectedValue.ToString().Split(',')[0].ToString())
                    {
                        case "SANWA":
                            txbManually.Text = string.Format(dtCommandParameter.Rows[0]["code_format"].ToString(), lsbDeviceName.Text.Split(',')[1].ToString(), string.Empty);
                            break;

                        case "TDK":
                            txbManually.Text = dtCommandParameter.Rows[0]["code_format"].ToString();
                            break;
                    }
                }
                else
                {
                    switch (lsbDeviceName.SelectedValue.ToString().Split(',')[0].ToString())
                    {
                        case "SANWA":
                            txbManually.Text = string.Format(dtCommandParameter.Rows[0]["code_format"].ToString(), lsbDeviceName.Text.Split(',')[1].ToString(), string.Empty) + container.StringFormat(dtCommandParameter.Select(), sbTemp.ToString().TrimEnd(',').Split(','));
                            break;

                        case "TDK":
                            txbManually.Text = dtCommandParameter.Rows[0]["code_format"].ToString();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        private void comboBox_DropDownClosed(object sender, EventArgs e)
        {
            FormulaCalculation();
        }

        private void numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            FormulaCalculation();
        }

        private void FormulaCalculation()
        {
            StringBuilder sbTemp;
            ContainerSet container = new ContainerSet();

            try
            {
                sbTemp = new StringBuilder();
                foreach (var item in tlpAssemblyUI.Controls)
                {
                    switch (item.GetType().FullName.Split('.')[item.GetType().FullName.Split('.').Length - 1].ToString())
                    {
                        case "ComboBox":
                            sbTemp.Append(((ComboBox)item).Text);
                            sbTemp.Append(",");
                            break;

                        case "NumericUpDown":
                            if (((NumericUpDown)item).Tag == null)
                            {
                                sbTemp.Append(((NumericUpDown)item).Value.ToString());
                            }
                            else
                            {
                                sbTemp.Append(Convert.ToInt32(((NumericUpDown)item).Value).ToString("D" + ((NumericUpDown)item).Tag.ToString()));
                            }

                            sbTemp.Append(",");
                            break;
                    }
                }

                switch (lsbDeviceName.SelectedValue.ToString().Split(',')[0].ToString())
                {
                    case "SANWA":
                        txbManually.Text = string.Format(dtCommandParameter.Rows[0]["code_format"].ToString(), lsbDeviceName.Text.Split(',')[1].ToString(), string.Empty) + container.StringFormat(dtCommandParameter.Select(), sbTemp.ToString().TrimEnd(',').Split(','));
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (txbManually.Text.Equals(string.Empty))
                return;

            string strDevice = string.Empty;
            string strReturnMsg = string.Empty;
            SANWA.Utility.Encoder encoder;

            try
            {
                //strDevice = lsbDeviceName.Text.Split(',')[0].ToString();

                strDevice = lsbDeviceName.SelectedValue.ToString().Split(',')[2].ToString();

                encoder = new SANWA.Utility.Encoder(lsbDeviceName.SelectedValue.ToString().Split(',')[0].ToString());

                //btnSend.Enabled = false;

                switch (lsbDeviceName.SelectedValue.ToString().Split(',')[0].ToString())
                {
                    case "SANWA":

                        //Adam.UI_Update.Terminal.TerminalUpdate.UpdateReturnList("lsbHistory", DateTime.Now.ToLongTimeString() + strDevice + " <<  " + txbManually.Text, false);
                        //strReturnMsg = ControllerManagement.Get(strDevice).DoWorkSync(txbManually.Text + "\r");

                        alRun.Add(strDevice + "&" + txbManually.Text + "\r");

                        break;

                    case "KAWASAKI":

                        //Adam.UI_Update.Terminal.TerminalUpdate.UpdateReturnList("lsbHistory", DateTime.Now.ToLongTimeString() + strDevice + " <<  " + txbManually.Text + encoder.Robot.KawasakiCheckSum(txbManually.Text.Substring(1, txbManually.Text.IndexOf('>') + 1)), false);
                        //strReturnMsg = ControllerManagement.Get(strDevice).DoWorkSync(txbManually.Text + encoder.Robot.KawasakiCheckSum(txbManually.Text.Substring(1, txbManually.Text.IndexOf('>')+1)));

                        alRun.Add(strDevice + "&" + txbManually.Text + encoder.Robot.KawasakiCheckSum(txbManually.Text.Substring(1, txbManually.Text.IndexOf('>') + 1)) + "\r\n");

                        break;

                    case "TDK":

                        //Adam.UI_Update.Terminal.TerminalUpdate.UpdateReturnList("lsbHistory", DateTime.Now.ToLongTimeString() + strDevice + " <<  " + encoder.LoadPort.TDK_A(txbManually.Text), false);
                        //strReturnMsg = ControllerManagement.Get(strDevice).DoWorkSync(encoder.LoadPort.TDK_A(txbManually.Text));

                        alRun.Add(strDevice + "&" + encoder.LoadPort.TDK_A(txbManually.Text));

                        break;

                    default:
                        alRun.Add(strDevice + "&" + txbManually.Text);
                        break;
                }

                //Adam.UI_Update.Terminal.TerminalUpdate.UpdateReturnList("lsbHistory", strDevice + " >>  " + strReturnMsg, false);

                //MessageBox.Show("Send to queue", "", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                btnSend.Enabled = true;
            }
        }

        private void btnClearHistory_Click(object sender, EventArgs e)
        {
            Adam.UI_Update.Terminal.TerminalUpdate.UpdateReturnList("lsbHistory", string.Empty, true);
        }

        private void btnAppendlist_Click(object sender, EventArgs e)
        {
            if (txbManually.Text.Equals(string.Empty))
                return;

            DataRow drTemp;
            string strDevice = string.Empty;
            SANWA.Utility.Encoder encoder;

            try
            {
                encoder = new SANWA.Utility.Encoder(lsbDeviceName.SelectedValue.ToString().Split(',')[0].ToString());

                strDevice = lsbDeviceName.Text.ToUpper().Split(',')[0].ToString();
                drTemp = dtCommandAssembly.NewRow();
                drTemp["No"] = (dtCommandAssembly.Rows.Count + 1).ToString("D2"); ;
                drTemp["Device_Name"] = lsbDeviceName.Text.Split(',')[0].ToString();
                drTemp["Vendor"] = lsbDeviceName.SelectedValue.ToString();
                drTemp["CommandShow"] = txbManually.Text;
                drTemp["Controller_ID"] = lsbDeviceName.SelectedValue.ToString().Split(',')[2].ToString();

                switch (lsbDeviceName.SelectedValue.ToString().Split(',')[0].ToString())
                {
                    case "SANWA":

                        drTemp["Command"] = txbManually.Text + "\r";

                        break;

                    case "KAWASAKI":

                        drTemp["Command"] = txbManually.Text + encoder.Robot.KawasakiCheckSum(txbManually.Text.Substring(1, txbManually.Text.IndexOf('>') + 1));

                        break;

                    case "TDK":

                        drTemp["Command"] = encoder.LoadPort.TDK_A(txbManually.Text);
                         
                        break;
                }

                dtCommandAssembly.Rows.Add(drTemp);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dtCommandAssembly.Rows.Count > 0)
            {
                if (MessageBox.Show("Whether remove the list??", "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    dtCommandAssembly.Clear();
                }
            }
        }

        private void CommandListReNo()
        {
            try
            {
                for (int i = 0; i < dtCommandAssembly.Rows.Count; i++)
                {
                    dtCommandAssembly.Rows[i]["No"] = (i + 1).ToString("D2");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        private void btnUP_Click(object sender, EventArgs e)
        {
            if (dgvCommandList.SelectedCells.Count == 0)
                return;

            ContainerSet containerSet;
            DataRowView currentDataRowView;

            int ii = 0;

            try
            {
                containerSet = new ContainerSet();

                ii = dgvCommandList.SelectedCells[0].OwningRow.Index;

                currentDataRowView = (DataRowView)dgvCommandList.Rows[ii].DataBoundItem;

                switch (((Button)sender).Tag.ToString())
                {
                    case "Down":
                        containerSet.MoveRow(ref dtCommandAssembly, currentDataRowView.Row, ContainerSet.DataTableMoveRow.Down);

                        dgvCommandList.ClearSelection();

                        if (ii + 1 < dgvCommandList.Rows.Count)
                        {
                            dgvCommandList.Rows[ii+1].Selected = true;
                        }
                        else
                        {
                            dgvCommandList.Rows[ii].Selected = true;
                        }
                        break;

                    case "UP":
                        containerSet.MoveRow(ref dtCommandAssembly, currentDataRowView.Row, ContainerSet.DataTableMoveRow.Up);

                        dgvCommandList.ClearSelection();

                        if (ii - 1 >= 0)
                        {
                            dgvCommandList.Rows[ii - 1].Selected = true;
                        }
                        else
                        {
                            dgvCommandList.Rows[ii].Selected = true;
                        }
                        break;
                }

                CommandListReNo();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int ii = 0;

            try
            {
                if (dgvCommandList.SelectedCells.Count > 0)
                {
                    ii = dgvCommandList.SelectedCells[0].OwningRow.Index;

                    if (ii >= 0)
                    {
                        dtCommandAssembly.Rows.RemoveAt(ii);
                        CommandListReNo();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        private void CommandRun()
        {
            string strDevice = string.Empty;
            string strCommand = string.Empty;
            string strReturnMsg = string.Empty;

            strDevice = "RobotController02";

            try
            {
                while (true)
                {
                    if (alRun.Count > 0)
                    {
                        strDevice = alRun[0].ToString().Split('&')[0];
                        strCommand = alRun[0].ToString().Split('&')[1];

                        Adam.UI_Update.Terminal.TerminalUpdate.UpdateLabelText("lbQueue", "Queue:" + alRun.Count.ToString());
                        Adam.UI_Update.Terminal.TerminalUpdate.UpdateReturnList("lsbHistory", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss ffffff") + "    " + strDevice + " <<  " + strCommand, false);
                        strReturnMsg = ControllerManagement.Get(strDevice).DoWorkSync(strCommand, strCommand.IndexOf("CMD") > 0 ? "CMD" : string.Empty);                  
                        Adam.UI_Update.Terminal.TerminalUpdate.UpdateReturnList("lsbHistory", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss ffffff") + "   " + strDevice + " >>  " + strReturnMsg, false);
                        alRun.RemoveAt(0);

                        if (alRun.Count > 0)
                        {
                            Adam.UI_Update.Terminal.TerminalUpdate.UpdateLabelText("lbQueue", "Queue:" + alRun.Count.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        private void tsmtRunList_Click(object sender, EventArgs e)
        {
            if (dtCommandAssembly.Rows.Count == 0)
                return;

            alRun.Clear();

            try
            {

                foreach (DataRow dr in dtCommandAssembly.Rows)
                {
                    alRun.Add(dr["Controller_ID"].ToString() + "&" + dr["Command"].ToString());
                }

                MessageBox.Show("Send to queue.", "tsmtRunList_Click");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        private void tsmtExcute_Click(object sender, EventArgs e)
        {
            if (dtCommandAssembly.Rows.Count == 0)
                return;

            alRun.Clear();

            int resultInt = 0;

            try
            {
                if(Int32.TryParse(tstbfrequency.Text, out resultInt))
                {
                    for (int i = 0; i < resultInt; i++)
                    {
                        foreach (DataRow dr in dtCommandAssembly.Rows)
                        {
                            alRun.Add(dr["Controller_ID"].ToString() + "&" + dr["Command"].ToString());
                        }
                    }
                }

                MessageBox.Show("Send to queue.", "tsmtExcute_Click");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        private void tsmtRunStep_Click(object sender, EventArgs e)
        {
            int ii = 0;

            try
            {
                if (dgvCommandList.SelectedCells.Count > 0)
                {
                    ii = dgvCommandList.SelectedCells[0].OwningRow.Index;

                    if (ii >= 0)
                    {
                        dgvCommandList.ClearSelection();

                        alRun.Add(dtCommandAssembly.Rows[ii]["Controller_ID"].ToString() +"&" + dtCommandAssembly.Rows[ii]["Command"].ToString());

                        if (ii + 1 < dgvCommandList.Rows.Count)
                        {
                            dgvCommandList.Rows[ii + 1].Selected = true;
                        }
                        else
                        {
                            dgvCommandList.Rows[ii].Selected = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dgvCommandList.Rows.Count == 0)
            {
                return;
            }

            SaveFileDialog saveFileDialog1;
            StreamWriter sw;

            try
            { 
                saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Title = "Save file";
                saveFileDialog1.InitialDirectory = ".\\";
                saveFileDialog1.Filter = "json files (*.json)|*.json";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    sw = new StreamWriter(saveFileDialog1.FileName.ToString());

                    sw.WriteLine(JsonConvert.SerializeObject(dtCommandAssembly, Formatting.Indented));

                    sw.Close();

                    MessageBox.Show("Done it.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Exception Message", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1;
            StreamReader myStream = null;

            try
            {
                openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "json files (*.json)|*.json";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.RestoreDirectory = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string line = string.Empty;

                    using (myStream = new StreamReader(openFileDialog1.FileName))
                    {
                        line = myStream.ReadToEnd();
                    }

                    dtCommandAssembly = (DataTable)Newtonsoft.Json.JsonConvert.DeserializeObject(line, (typeof(DataTable)));
                    dgvCommandList.DataSource = dtCommandAssembly;

                    MessageBox.Show("Done it.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Exception Message", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
    }
}