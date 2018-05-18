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
    public partial class FormAlarmHis : Form
    {
        public FormAlarmHis()
        {
            InitializeComponent();

            for (int i = 0; i < 100; i++)
            {
                this.dg1.Rows.Add("", "", "", "", "");
            }
            dg1.Rows[60].Selected = true;
        }
    }
}
