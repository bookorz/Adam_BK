using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Adam.Menu.SystemSetting
{
    public partial class FormSystemSetting : Form
    {
        private FormAccountSetting accountSetting = new FormAccountSetting();
        private FormAlarmEventSet formAlarm = new FormAlarmEventSet();
        private FormConfiguration configuration = new FormConfiguration();
        private FormDeviceManager deviceManager = new FormDeviceManager();
        private FormOnlineSettings onlineSettings = new FormOnlineSettings();
        private FormSECSSet sECSSet = new FormSECSSet();

        public FormSystemSetting()
        {
            InitializeComponent();
        }

        private void tbcSystemSetting_DrawItem(object sender, DrawItemEventArgs e)
        {

            Graphics g = e.Graphics;
            Brush _textBrush;

            // Get the item from the collection.
            TabPage _tabPage = tbcSystemSetting.TabPages[e.Index];

            // Get the real bounds for the tab rectangle.
            Rectangle _tabBounds = tbcSystemSetting.GetTabRect(e.Index);

            if (e.State == DrawItemState.Selected)
            {

                // Draw a different background color, and don't paint a focus rectangle.
                //_textBrush = new SolidBrush(Color.DarkGray);
                //g.FillRectangle(Brushes.Transparent, e.Bounds);
                _textBrush = new System.Drawing.SolidBrush(e.ForeColor);
                g.FillRectangle(Brushes.SkyBlue, e.Bounds);
            }
            else
            {
                _textBrush = new System.Drawing.SolidBrush(e.ForeColor);
                e.DrawBackground();
            }

            // Use our own font.
            Font _tabFont = new Font("微軟正黑體", (float)18.0, FontStyle.Bold, GraphicsUnit.Pixel);

            // Draw string. Center the text.
            StringFormat _stringFlags = new StringFormat();
            _stringFlags.Alignment = StringAlignment.Center;
            _stringFlags.LineAlignment = StringAlignment.Center;
            g.DrawString(_tabPage.Text, _tabFont, _textBrush, _tabBounds, new StringFormat(_stringFlags));

        }

        private void FormSystemSetting_Load(object sender, EventArgs e)
        {
            try
            {
                accountSetting.TopLevel = false;
                formAlarm.TopLevel = false;
                configuration.TopLevel = false;
                deviceManager.TopLevel = false;
                onlineSettings.TopLevel = false;
                sECSSet.TopLevel = false;

                tbpAccountSetting.Controls.Add(accountSetting);
                tbpAlarmEventSet.Controls.Add(formAlarm);
                tbpConfiguration.Controls.Add(configuration);
                tbpDeviceManager.Controls.Add(deviceManager);
                tbpOnlineSettings.Controls.Add(onlineSettings);
                tbpSECSSetting.Controls.Add(sECSSet);

                accountSetting.Show();
                formAlarm.Show();
                configuration.Show();
                deviceManager.Show();
                onlineSettings.Show();
                sECSSet.Show();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
