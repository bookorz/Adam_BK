using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adam
{
    public partial class FormTerminal : Form
    {
        private DataTable dtDeviceList = new DataTable();
        private DataTable dtCommandType = new DataTable();
        private DataTable dtCommand = new DataTable();

        string strSql = string.Empty;

        public FormTerminal()
        {
            InitializeComponent();
        }

        private void FormTerminal_Load(object sender, EventArgs e)
        {
            Util.DBUtil dBUtil = new Util.DBUtil();

            try
            {
                strSql = "SELECT node_id, node_name, node_type, sn_no, CONCAT(vendor, ',' ,node_type) as vendor, model_no, firmware_ver, conn_address, controller_id " +
                            "FROM node " +
                            "WHERE enable_flg = 'Y' AND node_type IN('ALIGNER', 'LOADPORT', 'ROBOT') " +
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
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        private void lsbDeviceName_Click(object sender, EventArgs e)
        {
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

                //if (dtCommand == null)
                //    dtCommand = new DataTable();

                //strSql = "SELECT DISTINCT code_id, code_name " +
                //            "FROM device_code_params " +
                //            "WHERE node_type = @node_type " +
                //            "AND vendor = @vendor " +
                //            "AND code_type = @code_type " +
                //            "ORDER BY code_type, code_order, Parameters_Amount";

                //keyValues.Add("@node_type", lsbDeviceName.SelectedValue.ToString().Split(',')[1].ToString());
                //keyValues.Add("@vendor", lsbDeviceName.SelectedValue.ToString().Split(',')[0].ToString());
                //keyValues.Add("@code_type", lsbCommandType.SelectedValue.ToString());

                //dtCommand = dBUtil.GetDataTable(strSql, keyValues);

                //if (dtCommand.Rows.Count == 0)
                //    throw new DataException();
                //else
                //{
                //    lsbCommand.DataSource = dtCommand;
                //    lsbCommand.DisplayMember = "code_id";
                //    lsbCommand.ValueMember = "code_id";
                //    lsbCommand.SelectedIndex = -1;
                //    txbNotice.Text = string.Empty;
                //}
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
