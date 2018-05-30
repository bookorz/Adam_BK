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
        DBUtil dBUtil = new DBUtil();

        public FormAlarmEventSet()
        {
            InitializeComponent();
        }

        private void FormAlarmEventSet_Load(object sender, EventArgs e)
        {
            string strSql = string.Empty;
            DataTable dtTemp;

            try
            {
                dtTemp = new DataTable();

                strSql = "SELECT list_type, list_id, list_name, list_name_en, list_value, sort_sequence, active " +
                         "  FROM list_item " +
                         " WHERE list_type = 'VENDOR_CODE' " +
                         "   AND active = 'Y' " +
                         "   AND list_id<> 'NA' " +
                         " ORDER BY sort_sequence ASC ";

                //DataTable = dBUtil.GetDataReader()
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
