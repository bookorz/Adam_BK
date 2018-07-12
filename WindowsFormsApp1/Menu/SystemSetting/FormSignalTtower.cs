using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SANWA.Utility;

namespace Adam.Menu.SystemSetting
{
    public partial class FormSignalTtower : Adam.Menu.SystemSetting.FormSettingFram
    {
        public FormSignalTtower()
        {
            InitializeComponent();
        }

        private DataTable dtSignalTower = new DataTable();
        private DBUtil dBUtil = new DBUtil();

        private void FormSignalTtower_Load(object sender, EventArgs e)
        {

            string strSql = string.Empty;

            try
            {
                strSql = "select * from config_signal_tower order by is_alarm, eqp_status asc";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }
    }
}
