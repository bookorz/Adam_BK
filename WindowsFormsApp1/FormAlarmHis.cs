using Adam.UI_Update.Alarm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TransferControl.Management;

namespace Adam
{
    public partial class FormAlarmHis : Form
    {
        public FormAlarmHis()
        {
            InitializeComponent();

            
        }

        private void FormAlarmHis_Load(object sender, EventArgs e)
        {
            
            AlarmUpdate.UpdateAlarmHistory(AlarmManagement.GetHistory());
        }
    }
}
