using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Adam.Util;
using System.Linq;
using System.IO.Ports;
using System.Diagnostics;

namespace Adam.Menu.Communications
{
    public partial class FormCommunications : Adam.Menu.FormFrame
    {
        public FormCommunications()
        {
            InitializeComponent();
        }

        private DataTable dtController = new DataTable();
        private DataTable dtNode = new DataTable();
        private string Vendor = string.Empty;
        private string ControllerID = string.Empty;

        private void FormCommunications_Load(object sender, EventArgs e)
        {
            string strSql = string.Empty;
            DBUtil dBUtil = new DBUtil();
            //DataTable dtList = new DataTable();
            Dictionary<string, object> keyValues = new Dictionary<string, object>();

            try
            {
                strSql = "SELECT A.* " +
                            "FROM config_node A " +
                            "INNER JOIN equipment_model B ON A.equipment_model_id = B.equipment_model_id " +
                            "WHERE B.enable_flg = @enable_flg " +
                            "AND B.equipment_model_id = @equipment_model_id ";

                keyValues.Add("@enable_flg", "Y");
                keyValues.Add("@equipment_model_id", SANWA.Utility.Config.SystemConfig.Get().SystemMode);

                dtNode = dBUtil.GetDataTable(strSql, keyValues);

                if (dtNode.Rows.Count > 0)
                {
                    libDeviceList.DataSource = dtNode;
                    libDeviceList.DisplayMember = "node_name";
                    libDeviceList.ValueMember = "node_id";
                    libDeviceList.SelectedIndex = -1;
                }

                strSql = "SELECT* FROM config_controller ";
                dtController = dBUtil.GetDataTable(strSql, null);

                CleanConnectMode();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// COM port scan
        /// </summary>
        private void COMPortScan()
        {
            cmbPortName.Items.Clear();

            try
            {
                string[] ports = SerialPort.GetPortNames();

                foreach (string port in ports)
                {
                    cmbPortName.Items.Add(port);
                    cmbPortName.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        private void btnTCPIP_Click(object sender, EventArgs e)
        {
            if (libDeviceList.SelectedIndex < 0)
                return;

            DataTable dtTemp = new DataTable();

            try
            {
                if (btnTCPIP.BackColor == Color.White)
                {
                    gbTCPIPSetting.Dock = DockStyle.Fill;
                    gbTCPIPSetting.Visible = true;
                    gbRS232CSetting.Dock = DockStyle.None;
                    gbRS232CSetting.Visible = false;
                    btnTCPIP.BackColor = Color.DodgerBlue;
                    btnRS232C.BackColor = Color.White;

                    txbCommunicatoyTypeNotice.Text = "The Internet protocol suite is the conceptual model and set of communications protocols used on the Internet and similar computer networks. It is commonly known as TCP/IP because the foundational protocols in the suite are the Transmission Control Protocol (TCP) and the Internet Protocol (IP). It is occasionally known as the Department of Defense (DoD) model because the development of the networking method was funded by the United States Department of Defense through DARPA.";
                }
                else if (btnTCPIP.BackColor == Color.DodgerBlue)
                {
                    gbTCPIPSetting.Dock = DockStyle.None;
                    gbTCPIPSetting.Visible = false;
                    gbRS232CSetting.Dock = DockStyle.None;
                    gbRS232CSetting.Visible = false;
                    btnTCPIP.BackColor = Color.White;
                    btnRS232C.BackColor = Color.White;
                    txbCommunicatoyTypeNotice.Text = string.Empty;
                }

                if (dtController.Rows.Count == 0)
                {
                    nudIP01.Value = 0;
                    nudIP02.Value = 0;
                    nudIP03.Value = 0;
                    nudIP04.Value = 0;
                    nudIPPort.Value = 0;
                }
                else
                {
                    var query = (from a in dtController.AsEnumerable()
                                 where a.Field<string>("node_id") == libDeviceList.SelectedValue.ToString()
                                    && a.Field<string>("conn_type") == btnTCPIP.Tag.ToString()
                                 select a).ToList();

                    if (query.Count > 0)
                    {
                        dtTemp = query.CopyToDataTable();

                        nudIP01.Value = Convert.ToInt32(dtTemp.Rows[0]["conn_address"].ToString().Split('.')[0].ToString());
                        nudIP02.Value = Convert.ToInt32(dtTemp.Rows[0]["conn_address"].ToString().Split('.')[1].ToString());
                        nudIP03.Value = Convert.ToInt32(dtTemp.Rows[0]["conn_address"].ToString().Split('.')[2].ToString());
                        nudIP04.Value = Convert.ToInt32(dtTemp.Rows[0]["conn_address"].ToString().Split('.')[3].ToString());
                        nudIPPort.Value = Convert.ToInt32(dtTemp.Rows[0]["conn_prot"].ToString());
                        chbTCPIPActive.Checked = dtTemp.Rows[0]["enable_flg"].ToString() == "Y" ? true : false;
                        txbInformation.Text = dtTemp.Rows[0]["create_user"].ToString() + "," + Convert.ToDateTime(dtTemp.Rows[0]["create_timestamp"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else
                    {
                        nudIP01.Value = 0;
                        nudIP02.Value = 0;
                        nudIP03.Value = 0;
                        nudIP04.Value = 0;
                        nudIPPort.Value = 0;
                        chbTCPIPActive.Checked = true;
                        txbInformation.Text = string.Empty;
                    }
                }

                txbConnectType.Text = btnTCPIP.Tag.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        private void btnRS232C_Click(object sender, EventArgs e)
        {
            if (libDeviceList.SelectedIndex < 0)
                return;

            DataTable dtTemp = new DataTable();

            if (btnRS232C.BackColor == Color.White)
            {
                gbTCPIPSetting.Dock = DockStyle.None;
                gbTCPIPSetting.Visible = false;
                gbRS232CSetting.Dock = DockStyle.Fill;
                gbRS232CSetting.Visible = true;
                btnRS232C.BackColor = Color.DodgerBlue;
                btnTCPIP.BackColor = Color.White;
                txbCommunicatoyTypeNotice.Text = "An RS-232 serial port was once a standard feature of a personal computer, used for connections to modems, printers, mice, data storage, uninterruptible power supplies, and other peripheral devices. RS-232, when compared to later interfaces such as RS-422, RS-485 and Ethernet, has lower transmission speed, short maximum cable length, large voltage swing, large standard connectors, no multipoint capability and limited multidrop capability. In modern personal computers, USB has displaced RS-232 from most of its peripheral interface roles. Many computers no longer come equipped with RS-232 ports (although some motherboards come equipped with a COM port header that allows the user to install a bracket with a DE-9 port) and must use either an external USB-to-RS-232 converter or an internal expansion card with one or more serial ports to connect to RS-232 peripherals. Nevertheless, thanks to their simplicity and past ubiquity, RS-232 interfaces are still used—particularly in industrial machines, networking equipment, and scientific instruments where a short-range, point-to-point, low-speed wired data connection is adequate.";
            }
            else if (btnRS232C.BackColor == Color.DodgerBlue)
            {
                gbTCPIPSetting.Dock = DockStyle.None;
                gbTCPIPSetting.Visible = false;
                gbRS232CSetting.Dock = DockStyle.None;
                gbRS232CSetting.Visible = false;
                btnRS232C.BackColor = Color.White;
                btnTCPIP.BackColor = Color.White;
                txbCommunicatoyTypeNotice.Text = string.Empty;
            }

            if (dtController.Rows.Count == 0)
            {
                COMPortScan();
                nudBaudRate.Value = 0;
                nudDataBits.Value = 0;
                txbParityBit.Text = "None";
                txbStopBit.Text = "One";
                txbConnectTypeCOM.Text = "ComPort";
            }
            else
            {
                var query = (from a in dtController.AsEnumerable()
                             where a.Field<string>("node_id") == libDeviceList.SelectedValue.ToString()
                                && a.Field<string>("conn_type") == btnRS232C.Tag.ToString()
                select a).ToList();

                if (query.Count > 0)
                {
                    dtTemp = query.CopyToDataTable();
                    COMPortScan();
                    cmbPortName.SelectedItem = dtTemp.Rows[0]["conn_address"].ToString();
                    nudBaudRate.Value = Convert.ToInt32(dtTemp.Rows[0]["conn_prot"].ToString());
                    nudDataBits.Value = Convert.ToInt32(dtTemp.Rows[0]["com_data_bits"].ToString());
                    txbParityBit.Text = dtTemp.Rows[0]["com_parity_bit"].ToString();
                    txbStopBit.Text = dtTemp.Rows[0]["com_stop_bit"].ToString();
                    chbRS232CActive.Checked = dtTemp.Rows[0]["enable_flg"].ToString() == "Y" ? true : false;
                    txbInformation.Text = dtTemp.Rows[0]["create_user"].ToString() + "," + Convert.ToDateTime(dtTemp.Rows[0]["create_timestamp"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    COMPortScan();
                    nudBaudRate.Value = 0;
                    nudDataBits.Value = 0;
                    txbParityBit.Text = "None";
                    txbStopBit.Text = "One";
                    chbRS232CActive.Checked = true;
                    txbInformation.Text = string.Empty;
                }

                txbConnectTypeCOM.Text = btnRS232C.Tag.ToString();
            }
        }

        private void btnRenew_Click(object sender, EventArgs e)
        {
            COMPortScan();
            cmbPortName.DroppedDown = true;
        }

        private void nudIP01_Click(object sender, EventArgs e)
        {

        }

        private void libDeviceList_Click(object sender, EventArgs e)
        {
            DataTable dtTemp = new DataTable();

            try
            {
                if (libDeviceList.SelectedIndex >= 0)
                {
                    var query = (from a in dtController.AsEnumerable()
                                 join b in dtNode.AsEnumerable() on a.Field<string>("node_id") equals b.Field<string>("node_id")
                                 where a.Field<string>("node_id") == libDeviceList.SelectedValue.ToString()
                                 //select  new { a, b }).ToList();
                                 select new
                                 {
                                     Vendor = b.Field<string>("vendor"),
                                     ControllerID = b.Field<string>("controller_id"),
                                     ConnType = a.Field<string>("conn_type")

                                 }).ToList();

                    

                    if (query.Count > 0)
                    {
                        Vendor = query[0].Vendor.ToString();
                        ControllerID = query[0].ControllerID.ToString();

                        btnTCPIP.BackColor = Color.White;
                        btnRS232C.BackColor = Color.White;

                        switch (query[0].ConnType.ToString())
                        {
                            case "Socket":
                                btnTCPIP_Click(this, null);
                                break;

                            case "ComPort":
                                btnRS232C_Click(this, null);
                                break;
                        }
                    }
                    else
                    {
                        CleanConnectMode();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        private void CleanConnectMode()
        {
            gbTCPIPSetting.Dock = DockStyle.None;
            gbTCPIPSetting.Visible = false;
            gbRS232CSetting.Dock = DockStyle.None;
            gbRS232CSetting.Visible = false;
            btnRS232C.BackColor = Color.White;
            btnTCPIP.BackColor = Color.White;
            txbParityBit.Text = "None";
            txbStopBit.Text = "One";
            txbConnectTypeCOM.Text = "ComPort";
            nudBaudRate.Value = 0;
            nudDataBits.Value = 0;
            nudIP01.Value = 0;
            nudIP02.Value = 0;
            nudIP03.Value = 0;
            nudIP04.Value = 0;
            nudIPPort.Value = 0;
            txbConnectType.Text = "Socket";
            txbCommunicatoyTypeNotice.Text = string.Empty;
            txbSettingandMaintainNotice.Text = string.Empty;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string strReplaceSql = string.Empty;

            DBUtil dBUtil = new DBUtil();
            Dictionary<string, object> keyValues = new Dictionary<string, object>();

            try
            {
                strReplaceSql = "REPLACE INTO controller " +
                    "(node_id, node_function_name, node_function_type, conn_address, conn_type, conn_prot, " +
                    "com_parity_bit, com_data_bits, com_stop_bit, enable_flg, " +
                    "create_user, create_timestamp, modify_user, modify_timestamp) " +
                    "VALUES (" +
                    "@node_id, " +
                    "@node_function_name, " +
                    "@node_function_type, " +
                    "@conn_address, " +
                    "@conn_type, " +
                    "@conn_prot, " +
                    "@com_parity_bit, " +
                    "@com_data_bits, " +
                    "@com_stop_bit, " +
                    "@enable_flg, " +
                    "@create_user, " +
                    "@create_timestamp, " +
                    "@modify_user, " +
                    "NOW() " +
                    ") ";

                keyValues.Add("@node_id", libDeviceList.SelectedValue.ToString());
                keyValues.Add("@node_function_name", ControllerID);
                keyValues.Add("@node_function_type", Vendor);

                if (btnTCPIP.BackColor == Color.DodgerBlue)
                {
                    keyValues.Add("@conn_address", string.Format("{0}.{1}.{2}.{3}", nudIP01.Value.ToString(), nudIP02.Value.ToString(), nudIP03.Value.ToString(), nudIP04.Value.ToString()));
                    keyValues.Add("@conn_type", btnTCPIP.Tag.ToString());
                    keyValues.Add("@conn_prot", nudIPPort.Value.ToString());
                    keyValues.Add("@com_parity_bit", string.Empty);
                    keyValues.Add("@com_data_bits", string.Empty);
                    keyValues.Add("@com_stop_bit", string.Empty);
                    keyValues.Add("@enable_flg", chbTCPIPActive.Checked ? "Y" : "N");
                }

                if (btnRS232C.BackColor == Color.DodgerBlue)
                {
                    keyValues.Add("@conn_address", cmbPortName.Text);
                    keyValues.Add("@conn_type", btnRS232C.Tag.ToString());
                    keyValues.Add("@conn_prot", nudBaudRate.Value.ToString());
                    keyValues.Add("@com_parity_bit", txbParityBit.Text.Trim());
                    keyValues.Add("@com_data_bits", nudDataBits.Value.ToString());
                    keyValues.Add("@com_stop_bit", txbStopBit.Text.Trim());
                    keyValues.Add("@enable_flg", chbRS232CActive.Checked ? "Y" : "N");
                }

                Form form = Application.OpenForms["FormMain"];
                Label Signal = form.Controls.Find("lbl_login_id", true).FirstOrDefault() as Label;

                if (txbInformation.Text.Split(',').Length > 1)
                {
                    keyValues.Add("@create_user", txbInformation.Text.Split(',')[0].ToString().Equals(string.Empty) ? string.Empty : txbInformation.Text.Split(',')[0].ToString());
                    keyValues.Add("@create_timestamp", txbInformation.Text.Split(',')[1].ToString().Equals(string.Empty) ? string.Empty : txbInformation.Text.Split(',')[1].ToString());
                }
                else
                {
                    keyValues.Add("@create_user", Signal.Text);
                    strReplaceSql = strReplaceSql.Replace("@create_timestamp,", "NOW(),");
                }

                keyValues.Add("@modify_user", Signal.Text);

                dBUtil.ExecuteNonQuery(strReplaceSql, keyValues);

                MessageBox.Show("Done it.", "Save", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                FormCommunications_Load(sender, e);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        private void nudIP01_Enter(object sender, EventArgs e)
        {
            ((NumericUpDown)sender).Select(0, ((NumericUpDown)sender).Text.Length);
        }
    }
}
