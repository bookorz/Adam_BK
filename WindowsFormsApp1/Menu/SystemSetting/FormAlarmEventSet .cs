using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Adam.Util;

namespace Adam.Menu.SystemSetting
{
    public partial class FormAlarmEventSet : Form
    {
        public FormAlarmEventSet()
        {
            InitializeComponent();
        }

        private DBUtil dBUtil = new DBUtil();

        private void FormAlarmEventSet_Load(object sender, EventArgs e)
        {
            string strSql = string.Empty;
            DataTable dtTemp;
            Dictionary<string, object> keyValues = new Dictionary<string, object>();

            try
            {
                dtTemp = new DataTable();

                strSql = "SELECT list_type, list_id, CONCAT(list_name, ' - ', list_name_en) name, list_name, list_name_en, list_value, sort_sequence, active " +
                         "  FROM list_item " +
                         " WHERE list_type = @list_type" +
                         "   AND active = @active " +
                         "   AND list_id <> @list_id " +
                         " ORDER BY sort_sequence ASC ";


                keyValues.Add("@list_type", "VENDOR_CODE");
                keyValues.Add("@active", "Y");
                keyValues.Add("@list_id", "NA");

                dtTemp = dBUtil.GetDataTable(strSql, keyValues);


                if (dtTemp.Rows.Count > 0)
                {
                    lsbAlarmConditionVendorCode.DataSource = dtTemp;
                    lsbAlarmConditionVendorCode.DisplayMember = "name";
                    lsbAlarmConditionVendorCode.ValueMember = "list_value";
                    lsbAlarmConditionVendorCode.SelectedIndex = -1;
                }
                else
                {
                    MessageBox.Show("list_item : VENDOR_CODE does not exist.", this.Name, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        private void lsbAlarmConditionVendorCode_Click(object sender, EventArgs e)
        {
            string strSql = string.Empty;
            DataTable dtTemp;
            Dictionary<string, object> keyValues = new Dictionary<string, object>();

            try
            {
                if (lsbAlarmConditionVendorCode.SelectedIndex >= 0)
                {
                    dtTemp = new DataTable();

                    strSql = "SELECT * " +
                             " FROM alarm_event_config " +
                             "WHERE Device_Name = @Device_Name " +
                             "ORDER BY alarm_no ASC";

                    keyValues.Add("@Device_Name", lsbAlarmConditionVendorCode.SelectedValue.ToString());

                    dtTemp = dBUtil.GetDataTable(strSql, keyValues);
                    dgvlsbAlarmData.AutoGenerateColumns = false;
                    dgvlsbAlarmData.DataSource = dtTemp;
                }
                else
                {
                    dgvlsbAlarmData.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }
    }
}
