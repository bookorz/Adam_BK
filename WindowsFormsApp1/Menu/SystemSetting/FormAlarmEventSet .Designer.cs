﻿namespace Adam.Menu.SystemSetting
{
    partial class FormAlarmEventSet
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gbAlarmSetting = new System.Windows.Forms.GroupBox();
            this.spcAlarmSetting = new System.Windows.Forms.SplitContainer();
            this.gbAlarmSettingCondition = new System.Windows.Forms.GroupBox();
            this.tlpAlarmCondition = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel17 = new System.Windows.Forms.TableLayoutPanel();
            this.txbAlarmConditionDeviceType = new System.Windows.Forms.TextBox();
            this.lbAlarmConditionDeviceType = new System.Windows.Forms.Label();
            this.lsbAlarmConditionDeviceType = new System.Windows.Forms.ListBox();
            this.tlpAlarmCondition_1 = new System.Windows.Forms.TableLayoutPanel();
            this.txbAlarmConditionVendorCode = new System.Windows.Forms.TextBox();
            this.lbAlarmConditionVendorCode = new System.Windows.Forms.Label();
            this.lsbAlarmConditionVendorCode = new System.Windows.Forms.ListBox();
            this.gbAlarmSettingData = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.dgvlsbAlarmData = new System.Windows.Forms.DataGridView();
            this.Event_AlarmNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Event_AlarmDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Event_AlarmType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Event_AlarmLevel = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Event_AlarmLED_Red = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Event_AlarmLED_Yellow = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Event_AlarmLED_Green = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Event_AlarmLED_Bule = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Event_AlarmBuzzer_A = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Event_AlarmBuzzer_B = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.gbAlarmSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spcAlarmSetting)).BeginInit();
            this.spcAlarmSetting.Panel1.SuspendLayout();
            this.spcAlarmSetting.Panel2.SuspendLayout();
            this.spcAlarmSetting.SuspendLayout();
            this.gbAlarmSettingCondition.SuspendLayout();
            this.tlpAlarmCondition.SuspendLayout();
            this.tableLayoutPanel17.SuspendLayout();
            this.tlpAlarmCondition_1.SuspendLayout();
            this.gbAlarmSettingData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvlsbAlarmData)).BeginInit();
            this.SuspendLayout();
            // 
            // gbAlarmSetting
            // 
            this.gbAlarmSetting.Controls.Add(this.spcAlarmSetting);
            this.gbAlarmSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbAlarmSetting.Location = new System.Drawing.Point(0, 0);
            this.gbAlarmSetting.Name = "gbAlarmSetting";
            this.gbAlarmSetting.Size = new System.Drawing.Size(1420, 740);
            this.gbAlarmSetting.TabIndex = 2;
            this.gbAlarmSetting.TabStop = false;
            this.gbAlarmSetting.Text = "Alarm Setting";
            // 
            // spcAlarmSetting
            // 
            this.spcAlarmSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcAlarmSetting.Location = new System.Drawing.Point(3, 25);
            this.spcAlarmSetting.Name = "spcAlarmSetting";
            // 
            // spcAlarmSetting.Panel1
            // 
            this.spcAlarmSetting.Panel1.Controls.Add(this.gbAlarmSettingCondition);
            // 
            // spcAlarmSetting.Panel2
            // 
            this.spcAlarmSetting.Panel2.Controls.Add(this.gbAlarmSettingData);
            this.spcAlarmSetting.Size = new System.Drawing.Size(1414, 712);
            this.spcAlarmSetting.SplitterDistance = 333;
            this.spcAlarmSetting.TabIndex = 0;
            // 
            // gbAlarmSettingCondition
            // 
            this.gbAlarmSettingCondition.Controls.Add(this.tlpAlarmCondition);
            this.gbAlarmSettingCondition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbAlarmSettingCondition.Location = new System.Drawing.Point(0, 0);
            this.gbAlarmSettingCondition.Name = "gbAlarmSettingCondition";
            this.gbAlarmSettingCondition.Size = new System.Drawing.Size(333, 712);
            this.gbAlarmSettingCondition.TabIndex = 0;
            this.gbAlarmSettingCondition.TabStop = false;
            this.gbAlarmSettingCondition.Text = "Condition";
            // 
            // tlpAlarmCondition
            // 
            this.tlpAlarmCondition.ColumnCount = 1;
            this.tlpAlarmCondition.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAlarmCondition.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpAlarmCondition.Controls.Add(this.tableLayoutPanel17, 0, 1);
            this.tlpAlarmCondition.Controls.Add(this.tlpAlarmCondition_1, 0, 0);
            this.tlpAlarmCondition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpAlarmCondition.Location = new System.Drawing.Point(3, 25);
            this.tlpAlarmCondition.Name = "tlpAlarmCondition";
            this.tlpAlarmCondition.RowCount = 3;
            this.tlpAlarmCondition.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpAlarmCondition.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpAlarmCondition.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpAlarmCondition.Size = new System.Drawing.Size(327, 684);
            this.tlpAlarmCondition.TabIndex = 0;
            // 
            // tableLayoutPanel17
            // 
            this.tableLayoutPanel17.ColumnCount = 1;
            this.tableLayoutPanel17.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel17.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel17.Controls.Add(this.txbAlarmConditionDeviceType, 0, 2);
            this.tableLayoutPanel17.Controls.Add(this.lbAlarmConditionDeviceType, 0, 0);
            this.tableLayoutPanel17.Controls.Add(this.lsbAlarmConditionDeviceType, 0, 1);
            this.tableLayoutPanel17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel17.Location = new System.Drawing.Point(3, 231);
            this.tableLayoutPanel17.Name = "tableLayoutPanel17";
            this.tableLayoutPanel17.RowCount = 3;
            this.tableLayoutPanel17.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.55605F));
            this.tableLayoutPanel17.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 58.7444F));
            this.tableLayoutPanel17.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28.69955F));
            this.tableLayoutPanel17.Size = new System.Drawing.Size(321, 222);
            this.tableLayoutPanel17.TabIndex = 1;
            // 
            // txbAlarmConditionDeviceType
            // 
            this.txbAlarmConditionDeviceType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbAlarmConditionDeviceType.BackColor = System.Drawing.Color.White;
            this.txbAlarmConditionDeviceType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txbAlarmConditionDeviceType.Location = new System.Drawing.Point(3, 160);
            this.txbAlarmConditionDeviceType.Multiline = true;
            this.txbAlarmConditionDeviceType.Name = "txbAlarmConditionDeviceType";
            this.txbAlarmConditionDeviceType.ReadOnly = true;
            this.txbAlarmConditionDeviceType.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txbAlarmConditionDeviceType.Size = new System.Drawing.Size(315, 50);
            this.txbAlarmConditionDeviceType.TabIndex = 5;
            // 
            // lbAlarmConditionDeviceType
            // 
            this.lbAlarmConditionDeviceType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbAlarmConditionDeviceType.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbAlarmConditionDeviceType.Location = new System.Drawing.Point(3, 0);
            this.lbAlarmConditionDeviceType.Name = "lbAlarmConditionDeviceType";
            this.lbAlarmConditionDeviceType.Size = new System.Drawing.Size(315, 27);
            this.lbAlarmConditionDeviceType.TabIndex = 3;
            this.lbAlarmConditionDeviceType.Text = "Device Type :";
            this.lbAlarmConditionDeviceType.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lsbAlarmConditionDeviceType
            // 
            this.lsbAlarmConditionDeviceType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsbAlarmConditionDeviceType.FormattingEnabled = true;
            this.lsbAlarmConditionDeviceType.ItemHeight = 20;
            this.lsbAlarmConditionDeviceType.Location = new System.Drawing.Point(3, 30);
            this.lsbAlarmConditionDeviceType.Name = "lsbAlarmConditionDeviceType";
            this.lsbAlarmConditionDeviceType.Size = new System.Drawing.Size(315, 124);
            this.lsbAlarmConditionDeviceType.TabIndex = 4;
            // 
            // tlpAlarmCondition_1
            // 
            this.tlpAlarmCondition_1.ColumnCount = 1;
            this.tlpAlarmCondition_1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAlarmCondition_1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpAlarmCondition_1.Controls.Add(this.txbAlarmConditionVendorCode, 0, 2);
            this.tlpAlarmCondition_1.Controls.Add(this.lbAlarmConditionVendorCode, 0, 0);
            this.tlpAlarmCondition_1.Controls.Add(this.lsbAlarmConditionVendorCode, 0, 1);
            this.tlpAlarmCondition_1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpAlarmCondition_1.Location = new System.Drawing.Point(3, 3);
            this.tlpAlarmCondition_1.Name = "tlpAlarmCondition_1";
            this.tlpAlarmCondition_1.RowCount = 3;
            this.tlpAlarmCondition_1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.55605F));
            this.tlpAlarmCondition_1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 58.7444F));
            this.tlpAlarmCondition_1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28.69955F));
            this.tlpAlarmCondition_1.Size = new System.Drawing.Size(321, 222);
            this.tlpAlarmCondition_1.TabIndex = 0;
            // 
            // txbAlarmConditionVendorCode
            // 
            this.txbAlarmConditionVendorCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbAlarmConditionVendorCode.BackColor = System.Drawing.Color.White;
            this.txbAlarmConditionVendorCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txbAlarmConditionVendorCode.Location = new System.Drawing.Point(3, 160);
            this.txbAlarmConditionVendorCode.Multiline = true;
            this.txbAlarmConditionVendorCode.Name = "txbAlarmConditionVendorCode";
            this.txbAlarmConditionVendorCode.ReadOnly = true;
            this.txbAlarmConditionVendorCode.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txbAlarmConditionVendorCode.Size = new System.Drawing.Size(315, 50);
            this.txbAlarmConditionVendorCode.TabIndex = 5;
            // 
            // lbAlarmConditionVendorCode
            // 
            this.lbAlarmConditionVendorCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbAlarmConditionVendorCode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbAlarmConditionVendorCode.Location = new System.Drawing.Point(3, 0);
            this.lbAlarmConditionVendorCode.Name = "lbAlarmConditionVendorCode";
            this.lbAlarmConditionVendorCode.Size = new System.Drawing.Size(315, 27);
            this.lbAlarmConditionVendorCode.TabIndex = 3;
            this.lbAlarmConditionVendorCode.Text = "Vendor Code :";
            this.lbAlarmConditionVendorCode.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lsbAlarmConditionVendorCode
            // 
            this.lsbAlarmConditionVendorCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsbAlarmConditionVendorCode.FormattingEnabled = true;
            this.lsbAlarmConditionVendorCode.ItemHeight = 20;
            this.lsbAlarmConditionVendorCode.Location = new System.Drawing.Point(3, 30);
            this.lsbAlarmConditionVendorCode.Name = "lsbAlarmConditionVendorCode";
            this.lsbAlarmConditionVendorCode.Size = new System.Drawing.Size(315, 124);
            this.lsbAlarmConditionVendorCode.TabIndex = 4;
            // 
            // gbAlarmSettingData
            // 
            this.gbAlarmSettingData.Controls.Add(this.btnSave);
            this.gbAlarmSettingData.Controls.Add(this.dgvlsbAlarmData);
            this.gbAlarmSettingData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbAlarmSettingData.Location = new System.Drawing.Point(0, 0);
            this.gbAlarmSettingData.Name = "gbAlarmSettingData";
            this.gbAlarmSettingData.Size = new System.Drawing.Size(1077, 712);
            this.gbAlarmSettingData.TabIndex = 0;
            this.gbAlarmSettingData.TabStop = false;
            this.gbAlarmSettingData.Text = "Data";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSave.Location = new System.Drawing.Point(848, 658);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(229, 48);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // dgvlsbAlarmData
            // 
            this.dgvlsbAlarmData.AllowUserToAddRows = false;
            this.dgvlsbAlarmData.AllowUserToDeleteRows = false;
            this.dgvlsbAlarmData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvlsbAlarmData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Event_AlarmNo,
            this.Event_AlarmDescription,
            this.Event_AlarmType,
            this.Event_AlarmLevel,
            this.Event_AlarmLED_Red,
            this.Event_AlarmLED_Yellow,
            this.Event_AlarmLED_Green,
            this.Event_AlarmLED_Bule,
            this.Event_AlarmBuzzer_A,
            this.Event_AlarmBuzzer_B});
            this.dgvlsbAlarmData.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvlsbAlarmData.Location = new System.Drawing.Point(3, 25);
            this.dgvlsbAlarmData.Name = "dgvlsbAlarmData";
            this.dgvlsbAlarmData.RowHeadersVisible = false;
            this.dgvlsbAlarmData.RowTemplate.Height = 24;
            this.dgvlsbAlarmData.Size = new System.Drawing.Size(1071, 625);
            this.dgvlsbAlarmData.TabIndex = 5;
            // 
            // Event_AlarmNo
            // 
            this.Event_AlarmNo.HeaderText = "Alarm No";
            this.Event_AlarmNo.Name = "Event_AlarmNo";
            this.Event_AlarmNo.ReadOnly = true;
            // 
            // Event_AlarmDescription
            // 
            this.Event_AlarmDescription.HeaderText = "Alarm Description";
            this.Event_AlarmDescription.Name = "Event_AlarmDescription";
            this.Event_AlarmDescription.ReadOnly = true;
            // 
            // Event_AlarmType
            // 
            this.Event_AlarmType.HeaderText = "Alarm Type";
            this.Event_AlarmType.Name = "Event_AlarmType";
            // 
            // Event_AlarmLevel
            // 
            this.Event_AlarmLevel.HeaderText = "Alarm Level";
            this.Event_AlarmLevel.Name = "Event_AlarmLevel";
            // 
            // Event_AlarmLED_Red
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            this.Event_AlarmLED_Red.DefaultCellStyle = dataGridViewCellStyle1;
            this.Event_AlarmLED_Red.HeaderText = "Red";
            this.Event_AlarmLED_Red.Name = "Event_AlarmLED_Red";
            // 
            // Event_AlarmLED_Yellow
            // 
            this.Event_AlarmLED_Yellow.HeaderText = "Yellow";
            this.Event_AlarmLED_Yellow.Name = "Event_AlarmLED_Yellow";
            // 
            // Event_AlarmLED_Green
            // 
            this.Event_AlarmLED_Green.HeaderText = "Green";
            this.Event_AlarmLED_Green.Name = "Event_AlarmLED_Green";
            // 
            // Event_AlarmLED_Bule
            // 
            this.Event_AlarmLED_Bule.HeaderText = "Green";
            this.Event_AlarmLED_Bule.Name = "Event_AlarmLED_Bule";
            // 
            // Event_AlarmBuzzer_A
            // 
            this.Event_AlarmBuzzer_A.HeaderText = "Buzzer A";
            this.Event_AlarmBuzzer_A.Name = "Event_AlarmBuzzer_A";
            // 
            // Event_AlarmBuzzer_B
            // 
            this.Event_AlarmBuzzer_B.HeaderText = "Buzzer B";
            this.Event_AlarmBuzzer_B.Name = "Event_AlarmBuzzer_B";
            // 
            // FormAlarmEventSet
            // 
            this.ClientSize = new System.Drawing.Size(1420, 740);
            this.Controls.Add(this.gbAlarmSetting);
            this.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormAlarmEventSet";
            this.Text = "Alarm Event Set ";
            this.gbAlarmSetting.ResumeLayout(false);
            this.spcAlarmSetting.Panel1.ResumeLayout(false);
            this.spcAlarmSetting.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spcAlarmSetting)).EndInit();
            this.spcAlarmSetting.ResumeLayout(false);
            this.gbAlarmSettingCondition.ResumeLayout(false);
            this.tlpAlarmCondition.ResumeLayout(false);
            this.tableLayoutPanel17.ResumeLayout(false);
            this.tableLayoutPanel17.PerformLayout();
            this.tlpAlarmCondition_1.ResumeLayout(false);
            this.tlpAlarmCondition_1.PerformLayout();
            this.gbAlarmSettingData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvlsbAlarmData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbAlarmSetting;
        private System.Windows.Forms.SplitContainer spcAlarmSetting;
        private System.Windows.Forms.GroupBox gbAlarmSettingCondition;
        private System.Windows.Forms.TableLayoutPanel tlpAlarmCondition;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel17;
        private System.Windows.Forms.TextBox txbAlarmConditionDeviceType;
        private System.Windows.Forms.Label lbAlarmConditionDeviceType;
        private System.Windows.Forms.ListBox lsbAlarmConditionDeviceType;
        private System.Windows.Forms.TableLayoutPanel tlpAlarmCondition_1;
        private System.Windows.Forms.TextBox txbAlarmConditionVendorCode;
        private System.Windows.Forms.Label lbAlarmConditionVendorCode;
        private System.Windows.Forms.ListBox lsbAlarmConditionVendorCode;
        private System.Windows.Forms.GroupBox gbAlarmSettingData;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView dgvlsbAlarmData;
        private System.Windows.Forms.DataGridViewTextBoxColumn Event_AlarmNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Event_AlarmDescription;
        private System.Windows.Forms.DataGridViewComboBoxColumn Event_AlarmType;
        private System.Windows.Forms.DataGridViewComboBoxColumn Event_AlarmLevel;
        private System.Windows.Forms.DataGridViewComboBoxColumn Event_AlarmLED_Red;
        private System.Windows.Forms.DataGridViewComboBoxColumn Event_AlarmLED_Yellow;
        private System.Windows.Forms.DataGridViewComboBoxColumn Event_AlarmLED_Green;
        private System.Windows.Forms.DataGridViewComboBoxColumn Event_AlarmLED_Bule;
        private System.Windows.Forms.DataGridViewComboBoxColumn Event_AlarmBuzzer_A;
        private System.Windows.Forms.DataGridViewComboBoxColumn Event_AlarmBuzzer_B;
    }
}
