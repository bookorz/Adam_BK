namespace Adam.Menu.SystemSetting
{
    partial class FormSignalTtower
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
            this.gbSignalTower = new System.Windows.Forms.GroupBox();
            this.spcSignalTowerSetting = new System.Windows.Forms.SplitContainer();
            this.gbCondition = new System.Windows.Forms.GroupBox();
            this.gbSettingData = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.dgvSignalTower = new System.Windows.Forms.DataGridView();
            this.lsbCondition = new System.Windows.Forms.ListBox();
            this.gbSignalTower.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spcSignalTowerSetting)).BeginInit();
            this.spcSignalTowerSetting.Panel1.SuspendLayout();
            this.spcSignalTowerSetting.Panel2.SuspendLayout();
            this.spcSignalTowerSetting.SuspendLayout();
            this.gbCondition.SuspendLayout();
            this.gbSettingData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSignalTower)).BeginInit();
            this.SuspendLayout();
            // 
            // gbSignalTower
            // 
            this.gbSignalTower.Controls.Add(this.spcSignalTowerSetting);
            this.gbSignalTower.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbSignalTower.Location = new System.Drawing.Point(0, 0);
            this.gbSignalTower.Name = "gbSignalTower";
            this.gbSignalTower.Size = new System.Drawing.Size(1420, 740);
            this.gbSignalTower.TabIndex = 3;
            this.gbSignalTower.TabStop = false;
            this.gbSignalTower.Text = "Signal tower setting";
            // 
            // spcSignalTowerSetting
            // 
            this.spcSignalTowerSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcSignalTowerSetting.Location = new System.Drawing.Point(3, 25);
            this.spcSignalTowerSetting.Name = "spcSignalTowerSetting";
            // 
            // spcSignalTowerSetting.Panel1
            // 
            this.spcSignalTowerSetting.Panel1.Controls.Add(this.gbCondition);
            // 
            // spcSignalTowerSetting.Panel2
            // 
            this.spcSignalTowerSetting.Panel2.Controls.Add(this.gbSettingData);
            this.spcSignalTowerSetting.Size = new System.Drawing.Size(1414, 712);
            this.spcSignalTowerSetting.SplitterDistance = 333;
            this.spcSignalTowerSetting.TabIndex = 0;
            // 
            // gbCondition
            // 
            this.gbCondition.Controls.Add(this.lsbCondition);
            this.gbCondition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbCondition.Location = new System.Drawing.Point(0, 0);
            this.gbCondition.Name = "gbCondition";
            this.gbCondition.Size = new System.Drawing.Size(333, 712);
            this.gbCondition.TabIndex = 0;
            this.gbCondition.TabStop = false;
            this.gbCondition.Text = "Condition";
            // 
            // gbSettingData
            // 
            this.gbSettingData.Controls.Add(this.btnSave);
            this.gbSettingData.Controls.Add(this.dgvSignalTower);
            this.gbSettingData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbSettingData.Location = new System.Drawing.Point(0, 0);
            this.gbSettingData.Name = "gbSettingData";
            this.gbSettingData.Size = new System.Drawing.Size(1077, 712);
            this.gbSettingData.TabIndex = 0;
            this.gbSettingData.TabStop = false;
            this.gbSettingData.Text = "Data";
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
            this.btnSave.Visible = false;
            // 
            // dgvSignalTower
            // 
            this.dgvSignalTower.AllowUserToAddRows = false;
            this.dgvSignalTower.AllowUserToDeleteRows = false;
            this.dgvSignalTower.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvSignalTower.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvSignalTower.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSignalTower.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvSignalTower.Location = new System.Drawing.Point(3, 25);
            this.dgvSignalTower.Name = "dgvSignalTower";
            this.dgvSignalTower.ReadOnly = true;
            this.dgvSignalTower.RowHeadersVisible = false;
            this.dgvSignalTower.RowTemplate.Height = 24;
            this.dgvSignalTower.Size = new System.Drawing.Size(1071, 625);
            this.dgvSignalTower.TabIndex = 5;
            // 
            // lsbCondition
            // 
            this.lsbCondition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsbCondition.FormattingEnabled = true;
            this.lsbCondition.ItemHeight = 20;
            this.lsbCondition.Location = new System.Drawing.Point(3, 25);
            this.lsbCondition.Name = "lsbCondition";
            this.lsbCondition.Size = new System.Drawing.Size(327, 684);
            this.lsbCondition.TabIndex = 5;
            // 
            // FormSignalTtower
            // 
            this.ClientSize = new System.Drawing.Size(1420, 740);
            this.Controls.Add(this.gbSignalTower);
            this.Name = "FormSignalTtower";
            this.Load += new System.EventHandler(this.FormSignalTtower_Load);
            this.gbSignalTower.ResumeLayout(false);
            this.spcSignalTowerSetting.Panel1.ResumeLayout(false);
            this.spcSignalTowerSetting.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spcSignalTowerSetting)).EndInit();
            this.spcSignalTowerSetting.ResumeLayout(false);
            this.gbCondition.ResumeLayout(false);
            this.gbSettingData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSignalTower)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbSignalTower;
        private System.Windows.Forms.SplitContainer spcSignalTowerSetting;
        private System.Windows.Forms.GroupBox gbCondition;
        private System.Windows.Forms.GroupBox gbSettingData;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView dgvSignalTower;
        private System.Windows.Forms.ListBox lsbCondition;
    }
}
