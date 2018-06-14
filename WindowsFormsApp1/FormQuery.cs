using Adam.Util;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class FormQuery : Form
    {
        public FormQuery()
        {
            InitializeComponent();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            DBUtil dBUtil = new DBUtil();
            StringBuilder sql = new StringBuilder();
            sql.Append("\n SELECT user_id, user_name, user_group_id");
            sql.Append("\n   FROM account ");
            //set parameter
            Dictionary<string, object> param = new Dictionary<string, object>();
            //Query
            DataTableReader rs = dBUtil.GetDataReader(sql.ToString(), param);
            MySqlDataAdapter adapter  = dBUtil.GetDataAdapter(sql.ToString());
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dgvResult.DataSource = dataTable;
        }
    }
}
