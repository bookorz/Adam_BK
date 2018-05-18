namespace GUI
{
    partial class FormTerminal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.GPBoxCMDSyntax_01 = new System.Windows.Forms.GroupBox();
            this.CboRobot_A01 = new System.Windows.Forms.ComboBox();
            this.CboRobot_A04 = new System.Windows.Forms.ComboBox();
            this.CboRobot_A03 = new System.Windows.Forms.ComboBox();
            this.LabParaA_06 = new System.Windows.Forms.Label();
            this.Button_ADD_SEND_01 = new System.Windows.Forms.Button();
            this.LabParaA_01 = new System.Windows.Forms.Label();
            this.CboRobot_A06 = new System.Windows.Forms.ComboBox();
            this.CboRobot_A02 = new System.Windows.Forms.ComboBox();
            this.LabParaA_05 = new System.Windows.Forms.Label();
            this.LabParaA_02 = new System.Windows.Forms.Label();
            this.CboRobot_A05 = new System.Windows.Forms.ComboBox();
            this.LabParaA_03 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.cbbCommand_01 = new System.Windows.Forms.ComboBox();
            this.LabParaA_04 = new System.Windows.Forms.Label();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.BtnRobot_RESET_01 = new System.Windows.Forms.Button();
            this.BtnRobot_SERVO_01 = new System.Windows.Forms.Button();
            this.BtnRobot_HOME_01 = new System.Windows.Forms.Button();
            this.GPBox_MODE_01 = new System.Windows.Forms.GroupBox();
            this.rBtnSyntaxMode_GET_01 = new System.Windows.Forms.RadioButton();
            this.rBtnSyntaxMode_SET_01 = new System.Windows.Forms.RadioButton();
            this.rBtnSyntaxMode_CMD_01 = new System.Windows.Forms.RadioButton();
            this.BtnRobot_Connect_01 = new System.Windows.Forms.Button();
            this.Button_Clean_Message = new System.Windows.Forms.Button();
            this.RichTextBox_MESSAGE_LOG = new System.Windows.Forms.RichTextBox();
            this.Button_SEND_COMMAND = new System.Windows.Forms.Button();
            this.cbbCommand_SEND = new System.Windows.Forms.ComboBox();
            this.DGV_Script_A = new System.Windows.Forms.DataGridView();
            this.Col_Tag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_ScriptCMD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GroupBox_ATUORUN = new System.Windows.Forms.GroupBox();
            this.Btn_AUTO_RUN_PAUSE_A = new System.Windows.Forms.Button();
            this.Btn_AUTO_RUN_STOP_A = new System.Windows.Forms.Button();
            this.Btn_AUTORUN_STEP_A = new System.Windows.Forms.Button();
            this.Btn_ADD_SCRIPT_A = new System.Windows.Forms.Button();
            this.Btn_Edit_Mode_A = new System.Windows.Forms.Button();
            this.Btn_AUTO_RUN_START_A = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.GPBoxCMDSyntax_01.SuspendLayout();
            this.GroupBox2.SuspendLayout();
            this.GPBox_MODE_01.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Script_A)).BeginInit();
            this.GroupBox_ATUORUN.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // GPBoxCMDSyntax_01
            // 
            this.GPBoxCMDSyntax_01.BackColor = System.Drawing.SystemColors.Control;
            this.GPBoxCMDSyntax_01.Controls.Add(this.CboRobot_A01);
            this.GPBoxCMDSyntax_01.Controls.Add(this.CboRobot_A04);
            this.GPBoxCMDSyntax_01.Controls.Add(this.CboRobot_A03);
            this.GPBoxCMDSyntax_01.Controls.Add(this.LabParaA_06);
            this.GPBoxCMDSyntax_01.Controls.Add(this.Button_ADD_SEND_01);
            this.GPBoxCMDSyntax_01.Controls.Add(this.LabParaA_01);
            this.GPBoxCMDSyntax_01.Controls.Add(this.CboRobot_A06);
            this.GPBoxCMDSyntax_01.Controls.Add(this.CboRobot_A02);
            this.GPBoxCMDSyntax_01.Controls.Add(this.LabParaA_05);
            this.GPBoxCMDSyntax_01.Controls.Add(this.LabParaA_02);
            this.GPBoxCMDSyntax_01.Controls.Add(this.CboRobot_A05);
            this.GPBoxCMDSyntax_01.Controls.Add(this.LabParaA_03);
            this.GPBoxCMDSyntax_01.Controls.Add(this.Label6);
            this.GPBoxCMDSyntax_01.Controls.Add(this.cbbCommand_01);
            this.GPBoxCMDSyntax_01.Controls.Add(this.LabParaA_04);
            this.GPBoxCMDSyntax_01.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.GPBoxCMDSyntax_01.Location = new System.Drawing.Point(135, 98);
            this.GPBoxCMDSyntax_01.Name = "GPBoxCMDSyntax_01";
            this.GPBoxCMDSyntax_01.Size = new System.Drawing.Size(1143, 125);
            this.GPBoxCMDSyntax_01.TabIndex = 325;
            this.GPBoxCMDSyntax_01.TabStop = false;
            this.GPBoxCMDSyntax_01.Tag = "ROBOT";
            this.GPBoxCMDSyntax_01.Text = "Syntax";
            // 
            // CboRobot_A01
            // 
            this.CboRobot_A01.FormattingEnabled = true;
            this.CboRobot_A01.Location = new System.Drawing.Point(154, 72);
            this.CboRobot_A01.Name = "CboRobot_A01";
            this.CboRobot_A01.Size = new System.Drawing.Size(121, 35);
            this.CboRobot_A01.TabIndex = 297;
            this.CboRobot_A01.Tag = "SubCMD";
            this.CboRobot_A01.Visible = false;
            // 
            // CboRobot_A04
            // 
            this.CboRobot_A04.FormattingEnabled = true;
            this.CboRobot_A04.Location = new System.Drawing.Point(540, 72);
            this.CboRobot_A04.Name = "CboRobot_A04";
            this.CboRobot_A04.Size = new System.Drawing.Size(121, 35);
            this.CboRobot_A04.TabIndex = 300;
            this.CboRobot_A04.Tag = "SubCMD";
            this.CboRobot_A04.Visible = false;
            // 
            // CboRobot_A03
            // 
            this.CboRobot_A03.FormattingEnabled = true;
            this.CboRobot_A03.Location = new System.Drawing.Point(410, 72);
            this.CboRobot_A03.Name = "CboRobot_A03";
            this.CboRobot_A03.Size = new System.Drawing.Size(121, 35);
            this.CboRobot_A03.TabIndex = 299;
            this.CboRobot_A03.Tag = "SubCMD";
            this.CboRobot_A03.Visible = false;
            // 
            // LabParaA_06
            // 
            this.LabParaA_06.AutoSize = true;
            this.LabParaA_06.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.LabParaA_06.Location = new System.Drawing.Point(804, 42);
            this.LabParaA_06.Name = "LabParaA_06";
            this.LabParaA_06.Size = new System.Drawing.Size(66, 24);
            this.LabParaA_06.TabIndex = 308;
            this.LabParaA_06.Tag = "DESC_Label";
            this.LabParaA_06.Text = "Param";
            this.LabParaA_06.Visible = false;
            // 
            // Button_ADD_SEND_01
            // 
            this.Button_ADD_SEND_01.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Button_ADD_SEND_01.Location = new System.Drawing.Point(947, 69);
            this.Button_ADD_SEND_01.Name = "Button_ADD_SEND_01";
            this.Button_ADD_SEND_01.Size = new System.Drawing.Size(103, 40);
            this.Button_ADD_SEND_01.TabIndex = 294;
            this.Button_ADD_SEND_01.Tag = "01";
            this.Button_ADD_SEND_01.Text = "Add Send";
            this.Button_ADD_SEND_01.UseVisualStyleBackColor = true;
            // 
            // LabParaA_01
            // 
            this.LabParaA_01.AutoSize = true;
            this.LabParaA_01.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.LabParaA_01.Location = new System.Drawing.Point(155, 42);
            this.LabParaA_01.Name = "LabParaA_01";
            this.LabParaA_01.Size = new System.Drawing.Size(66, 24);
            this.LabParaA_01.TabIndex = 301;
            this.LabParaA_01.Tag = "DESC_Label";
            this.LabParaA_01.Text = "Param";
            this.LabParaA_01.Visible = false;
            // 
            // CboRobot_A06
            // 
            this.CboRobot_A06.FormattingEnabled = true;
            this.CboRobot_A06.Location = new System.Drawing.Point(794, 72);
            this.CboRobot_A06.Name = "CboRobot_A06";
            this.CboRobot_A06.Size = new System.Drawing.Size(121, 35);
            this.CboRobot_A06.TabIndex = 307;
            this.CboRobot_A06.Tag = "SubCMD";
            this.CboRobot_A06.Visible = false;
            // 
            // CboRobot_A02
            // 
            this.CboRobot_A02.FormattingEnabled = true;
            this.CboRobot_A02.Location = new System.Drawing.Point(283, 72);
            this.CboRobot_A02.Name = "CboRobot_A02";
            this.CboRobot_A02.Size = new System.Drawing.Size(121, 35);
            this.CboRobot_A02.TabIndex = 298;
            this.CboRobot_A02.Tag = "SubCMD";
            this.CboRobot_A02.Visible = false;
            // 
            // LabParaA_05
            // 
            this.LabParaA_05.AutoSize = true;
            this.LabParaA_05.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.LabParaA_05.Location = new System.Drawing.Point(670, 42);
            this.LabParaA_05.Name = "LabParaA_05";
            this.LabParaA_05.Size = new System.Drawing.Size(66, 24);
            this.LabParaA_05.TabIndex = 306;
            this.LabParaA_05.Tag = "DESC_Label";
            this.LabParaA_05.Text = "Param";
            this.LabParaA_05.Visible = false;
            // 
            // LabParaA_02
            // 
            this.LabParaA_02.AutoSize = true;
            this.LabParaA_02.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.LabParaA_02.Location = new System.Drawing.Point(289, 42);
            this.LabParaA_02.Name = "LabParaA_02";
            this.LabParaA_02.Size = new System.Drawing.Size(66, 24);
            this.LabParaA_02.TabIndex = 302;
            this.LabParaA_02.Tag = "DESC_Label";
            this.LabParaA_02.Text = "Param";
            this.LabParaA_02.Visible = false;
            // 
            // CboRobot_A05
            // 
            this.CboRobot_A05.FormattingEnabled = true;
            this.CboRobot_A05.Location = new System.Drawing.Point(667, 72);
            this.CboRobot_A05.Name = "CboRobot_A05";
            this.CboRobot_A05.Size = new System.Drawing.Size(121, 35);
            this.CboRobot_A05.TabIndex = 305;
            this.CboRobot_A05.Tag = "SubCMD";
            this.CboRobot_A05.Visible = false;
            // 
            // LabParaA_03
            // 
            this.LabParaA_03.AutoSize = true;
            this.LabParaA_03.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.LabParaA_03.Location = new System.Drawing.Point(419, 42);
            this.LabParaA_03.Name = "LabParaA_03";
            this.LabParaA_03.Size = new System.Drawing.Size(66, 24);
            this.LabParaA_03.TabIndex = 303;
            this.LabParaA_03.Tag = "DESC_Label";
            this.LabParaA_03.Text = "Param";
            this.LabParaA_03.Visible = false;
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Label6.Location = new System.Drawing.Point(18, 42);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(116, 24);
            this.Label6.TabIndex = 296;
            this.Label6.Text = "COMMAND";
            // 
            // cbbCommand_01
            // 
            this.cbbCommand_01.FormattingEnabled = true;
            this.cbbCommand_01.Location = new System.Drawing.Point(23, 72);
            this.cbbCommand_01.Name = "cbbCommand_01";
            this.cbbCommand_01.Size = new System.Drawing.Size(105, 35);
            this.cbbCommand_01.TabIndex = 295;
            this.cbbCommand_01.Tag = "01";
            // 
            // LabParaA_04
            // 
            this.LabParaA_04.AutoSize = true;
            this.LabParaA_04.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.LabParaA_04.Location = new System.Drawing.Point(548, 42);
            this.LabParaA_04.Name = "LabParaA_04";
            this.LabParaA_04.Size = new System.Drawing.Size(66, 24);
            this.LabParaA_04.TabIndex = 304;
            this.LabParaA_04.Tag = "DESC_Label";
            this.LabParaA_04.Text = "Param";
            this.LabParaA_04.Visible = false;
            // 
            // GroupBox2
            // 
            this.GroupBox2.BackColor = System.Drawing.SystemColors.Control;
            this.GroupBox2.Controls.Add(this.BtnRobot_RESET_01);
            this.GroupBox2.Controls.Add(this.BtnRobot_SERVO_01);
            this.GroupBox2.Controls.Add(this.BtnRobot_HOME_01);
            this.GroupBox2.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.GroupBox2.Location = new System.Drawing.Point(135, 9);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(466, 83);
            this.GroupBox2.TabIndex = 324;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = " Immediately Send ";
            // 
            // BtnRobot_RESET_01
            // 
            this.BtnRobot_RESET_01.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.BtnRobot_RESET_01.Location = new System.Drawing.Point(293, 34);
            this.BtnRobot_RESET_01.Name = "BtnRobot_RESET_01";
            this.BtnRobot_RESET_01.Size = new System.Drawing.Size(133, 40);
            this.BtnRobot_RESET_01.TabIndex = 227;
            this.BtnRobot_RESET_01.Tag = "01";
            this.BtnRobot_RESET_01.Text = "Reset";
            this.BtnRobot_RESET_01.UseVisualStyleBackColor = true;
            // 
            // BtnRobot_SERVO_01
            // 
            this.BtnRobot_SERVO_01.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.BtnRobot_SERVO_01.Location = new System.Drawing.Point(13, 34);
            this.BtnRobot_SERVO_01.Name = "BtnRobot_SERVO_01";
            this.BtnRobot_SERVO_01.Size = new System.Drawing.Size(135, 40);
            this.BtnRobot_SERVO_01.TabIndex = 225;
            this.BtnRobot_SERVO_01.Tag = "01";
            this.BtnRobot_SERVO_01.Text = "Servo on";
            this.BtnRobot_SERVO_01.UseVisualStyleBackColor = true;
            // 
            // BtnRobot_HOME_01
            // 
            this.BtnRobot_HOME_01.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.BtnRobot_HOME_01.Location = new System.Drawing.Point(154, 34);
            this.BtnRobot_HOME_01.Name = "BtnRobot_HOME_01";
            this.BtnRobot_HOME_01.Size = new System.Drawing.Size(133, 40);
            this.BtnRobot_HOME_01.TabIndex = 228;
            this.BtnRobot_HOME_01.Tag = "01";
            this.BtnRobot_HOME_01.Text = "HOME";
            this.BtnRobot_HOME_01.UseVisualStyleBackColor = true;
            // 
            // GPBox_MODE_01
            // 
            this.GPBox_MODE_01.Controls.Add(this.rBtnSyntaxMode_GET_01);
            this.GPBox_MODE_01.Controls.Add(this.rBtnSyntaxMode_SET_01);
            this.GPBox_MODE_01.Controls.Add(this.rBtnSyntaxMode_CMD_01);
            this.GPBox_MODE_01.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.GPBox_MODE_01.Location = new System.Drawing.Point(7, 98);
            this.GPBox_MODE_01.Name = "GPBox_MODE_01";
            this.GPBox_MODE_01.Size = new System.Drawing.Size(119, 125);
            this.GPBox_MODE_01.TabIndex = 323;
            this.GPBox_MODE_01.TabStop = false;
            this.GPBox_MODE_01.Text = "Mode";
            // 
            // rBtnSyntaxMode_GET_01
            // 
            this.rBtnSyntaxMode_GET_01.AutoSize = true;
            this.rBtnSyntaxMode_GET_01.Checked = true;
            this.rBtnSyntaxMode_GET_01.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rBtnSyntaxMode_GET_01.Location = new System.Drawing.Point(33, 29);
            this.rBtnSyntaxMode_GET_01.Name = "rBtnSyntaxMode_GET_01";
            this.rBtnSyntaxMode_GET_01.Size = new System.Drawing.Size(63, 28);
            this.rBtnSyntaxMode_GET_01.TabIndex = 287;
            this.rBtnSyntaxMode_GET_01.TabStop = true;
            this.rBtnSyntaxMode_GET_01.Tag = "SYNTAX_MODE_01";
            this.rBtnSyntaxMode_GET_01.Text = "GET";
            this.rBtnSyntaxMode_GET_01.UseVisualStyleBackColor = true;
            // 
            // rBtnSyntaxMode_SET_01
            // 
            this.rBtnSyntaxMode_SET_01.AutoSize = true;
            this.rBtnSyntaxMode_SET_01.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rBtnSyntaxMode_SET_01.Location = new System.Drawing.Point(33, 62);
            this.rBtnSyntaxMode_SET_01.Name = "rBtnSyntaxMode_SET_01";
            this.rBtnSyntaxMode_SET_01.Size = new System.Drawing.Size(60, 28);
            this.rBtnSyntaxMode_SET_01.TabIndex = 288;
            this.rBtnSyntaxMode_SET_01.Tag = "SYNTAX_MODE_01";
            this.rBtnSyntaxMode_SET_01.Text = "SET";
            this.rBtnSyntaxMode_SET_01.UseVisualStyleBackColor = true;
            // 
            // rBtnSyntaxMode_CMD_01
            // 
            this.rBtnSyntaxMode_CMD_01.AutoSize = true;
            this.rBtnSyntaxMode_CMD_01.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rBtnSyntaxMode_CMD_01.Location = new System.Drawing.Point(33, 94);
            this.rBtnSyntaxMode_CMD_01.Name = "rBtnSyntaxMode_CMD_01";
            this.rBtnSyntaxMode_CMD_01.Size = new System.Drawing.Size(73, 28);
            this.rBtnSyntaxMode_CMD_01.TabIndex = 289;
            this.rBtnSyntaxMode_CMD_01.Tag = "SYNTAX_MODE_01";
            this.rBtnSyntaxMode_CMD_01.Text = "CMD";
            this.rBtnSyntaxMode_CMD_01.UseVisualStyleBackColor = true;
            // 
            // BtnRobot_Connect_01
            // 
            this.BtnRobot_Connect_01.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.BtnRobot_Connect_01.Location = new System.Drawing.Point(7, 32);
            this.BtnRobot_Connect_01.Name = "BtnRobot_Connect_01";
            this.BtnRobot_Connect_01.Size = new System.Drawing.Size(119, 51);
            this.BtnRobot_Connect_01.TabIndex = 322;
            this.BtnRobot_Connect_01.Tag = "01";
            this.BtnRobot_Connect_01.Text = "Connect";
            this.BtnRobot_Connect_01.UseVisualStyleBackColor = true;
            // 
            // Button_Clean_Message
            // 
            this.Button_Clean_Message.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Button_Clean_Message.Location = new System.Drawing.Point(1134, 314);
            this.Button_Clean_Message.Name = "Button_Clean_Message";
            this.Button_Clean_Message.Size = new System.Drawing.Size(121, 40);
            this.Button_Clean_Message.TabIndex = 321;
            this.Button_Clean_Message.Text = "Clean Message";
            this.Button_Clean_Message.UseVisualStyleBackColor = true;
            // 
            // RichTextBox_MESSAGE_LOG
            // 
            this.RichTextBox_MESSAGE_LOG.Location = new System.Drawing.Point(581, 360);
            this.RichTextBox_MESSAGE_LOG.Name = "RichTextBox_MESSAGE_LOG";
            this.RichTextBox_MESSAGE_LOG.ReadOnly = true;
            this.RichTextBox_MESSAGE_LOG.Size = new System.Drawing.Size(709, 489);
            this.RichTextBox_MESSAGE_LOG.TabIndex = 320;
            this.RichTextBox_MESSAGE_LOG.Text = "";
            // 
            // Button_SEND_COMMAND
            // 
            this.Button_SEND_COMMAND.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Button_SEND_COMMAND.Location = new System.Drawing.Point(1010, 314);
            this.Button_SEND_COMMAND.Name = "Button_SEND_COMMAND";
            this.Button_SEND_COMMAND.Size = new System.Drawing.Size(118, 40);
            this.Button_SEND_COMMAND.TabIndex = 319;
            this.Button_SEND_COMMAND.Text = "Send Command";
            this.Button_SEND_COMMAND.UseVisualStyleBackColor = true;
            // 
            // cbbCommand_SEND
            // 
            this.cbbCommand_SEND.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cbbCommand_SEND.FormattingEnabled = true;
            this.cbbCommand_SEND.Location = new System.Drawing.Point(581, 314);
            this.cbbCommand_SEND.Name = "cbbCommand_SEND";
            this.cbbCommand_SEND.Size = new System.Drawing.Size(405, 38);
            this.cbbCommand_SEND.TabIndex = 318;
            // 
            // DGV_Script_A
            // 
            this.DGV_Script_A.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_Script_A.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Col_Tag,
            this.Col_ScriptCMD});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGV_Script_A.DefaultCellStyle = dataGridViewCellStyle2;
            this.DGV_Script_A.Enabled = false;
            this.DGV_Script_A.Location = new System.Drawing.Point(14, 86);
            this.DGV_Script_A.Name = "DGV_Script_A";
            this.DGV_Script_A.RowTemplate.Height = 24;
            this.DGV_Script_A.Size = new System.Drawing.Size(533, 464);
            this.DGV_Script_A.TabIndex = 54;
            // 
            // Col_Tag
            // 
            this.Col_Tag.DataPropertyName = "Col_Tag";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Col_Tag.DefaultCellStyle = dataGridViewCellStyle1;
            this.Col_Tag.HeaderText = "Tag";
            this.Col_Tag.Name = "Col_Tag";
            this.Col_Tag.ReadOnly = true;
            this.Col_Tag.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Col_ScriptCMD
            // 
            this.Col_ScriptCMD.DataPropertyName = "Col_ScriptCMD";
            this.Col_ScriptCMD.HeaderText = "Command";
            this.Col_ScriptCMD.Name = "Col_ScriptCMD";
            this.Col_ScriptCMD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Col_ScriptCMD.Width = 300;
            // 
            // GroupBox_ATUORUN
            // 
            this.GroupBox_ATUORUN.Controls.Add(this.Btn_AUTO_RUN_PAUSE_A);
            this.GroupBox_ATUORUN.Controls.Add(this.DGV_Script_A);
            this.GroupBox_ATUORUN.Controls.Add(this.Btn_AUTO_RUN_STOP_A);
            this.GroupBox_ATUORUN.Controls.Add(this.Btn_AUTORUN_STEP_A);
            this.GroupBox_ATUORUN.Controls.Add(this.Btn_ADD_SCRIPT_A);
            this.GroupBox_ATUORUN.Controls.Add(this.Btn_Edit_Mode_A);
            this.GroupBox_ATUORUN.Controls.Add(this.Btn_AUTO_RUN_START_A);
            this.GroupBox_ATUORUN.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.GroupBox_ATUORUN.Location = new System.Drawing.Point(12, 299);
            this.GroupBox_ATUORUN.Name = "GroupBox_ATUORUN";
            this.GroupBox_ATUORUN.Size = new System.Drawing.Size(563, 656);
            this.GroupBox_ATUORUN.TabIndex = 317;
            this.GroupBox_ATUORUN.TabStop = false;
            this.GroupBox_ATUORUN.Text = "Auto Run Script";
            // 
            // Btn_AUTO_RUN_PAUSE_A
            // 
            this.Btn_AUTO_RUN_PAUSE_A.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_AUTO_RUN_PAUSE_A.Location = new System.Drawing.Point(378, 40);
            this.Btn_AUTO_RUN_PAUSE_A.Name = "Btn_AUTO_RUN_PAUSE_A";
            this.Btn_AUTO_RUN_PAUSE_A.Size = new System.Drawing.Size(80, 40);
            this.Btn_AUTO_RUN_PAUSE_A.TabIndex = 55;
            this.Btn_AUTO_RUN_PAUSE_A.Text = "PAUSE";
            this.Btn_AUTO_RUN_PAUSE_A.UseVisualStyleBackColor = true;
            // 
            // Btn_AUTO_RUN_STOP_A
            // 
            this.Btn_AUTO_RUN_STOP_A.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_AUTO_RUN_STOP_A.Location = new System.Drawing.Point(467, 40);
            this.Btn_AUTO_RUN_STOP_A.Name = "Btn_AUTO_RUN_STOP_A";
            this.Btn_AUTO_RUN_STOP_A.Size = new System.Drawing.Size(80, 40);
            this.Btn_AUTO_RUN_STOP_A.TabIndex = 45;
            this.Btn_AUTO_RUN_STOP_A.Text = "STOP";
            this.Btn_AUTO_RUN_STOP_A.UseVisualStyleBackColor = true;
            // 
            // Btn_AUTORUN_STEP_A
            // 
            this.Btn_AUTORUN_STEP_A.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_AUTORUN_STEP_A.Location = new System.Drawing.Point(199, 40);
            this.Btn_AUTORUN_STEP_A.Name = "Btn_AUTORUN_STEP_A";
            this.Btn_AUTORUN_STEP_A.Size = new System.Drawing.Size(80, 40);
            this.Btn_AUTORUN_STEP_A.TabIndex = 42;
            this.Btn_AUTORUN_STEP_A.Text = "STEP";
            this.Btn_AUTORUN_STEP_A.UseVisualStyleBackColor = true;
            // 
            // Btn_ADD_SCRIPT_A
            // 
            this.Btn_ADD_SCRIPT_A.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_ADD_SCRIPT_A.Location = new System.Drawing.Point(111, 40);
            this.Btn_ADD_SCRIPT_A.Name = "Btn_ADD_SCRIPT_A";
            this.Btn_ADD_SCRIPT_A.Size = new System.Drawing.Size(80, 40);
            this.Btn_ADD_SCRIPT_A.TabIndex = 41;
            this.Btn_ADD_SCRIPT_A.Text = "Add";
            this.Btn_ADD_SCRIPT_A.UseVisualStyleBackColor = true;
            // 
            // Btn_Edit_Mode_A
            // 
            this.Btn_Edit_Mode_A.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Edit_Mode_A.Location = new System.Drawing.Point(23, 40);
            this.Btn_Edit_Mode_A.Name = "Btn_Edit_Mode_A";
            this.Btn_Edit_Mode_A.Size = new System.Drawing.Size(80, 40);
            this.Btn_Edit_Mode_A.TabIndex = 37;
            this.Btn_Edit_Mode_A.Text = "EDIT";
            this.Btn_Edit_Mode_A.UseVisualStyleBackColor = true;
            // 
            // Btn_AUTO_RUN_START_A
            // 
            this.Btn_AUTO_RUN_START_A.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_AUTO_RUN_START_A.Location = new System.Drawing.Point(288, 40);
            this.Btn_AUTO_RUN_START_A.Name = "Btn_AUTO_RUN_START_A";
            this.Btn_AUTO_RUN_START_A.Size = new System.Drawing.Size(80, 40);
            this.Btn_AUTO_RUN_START_A.TabIndex = 38;
            this.Btn_AUTO_RUN_START_A.Text = "SATRT";
            this.Btn_AUTO_RUN_START_A.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1292, 281);
            this.tabControl1.TabIndex = 309;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.GroupBox2);
            this.tabPage1.Controls.Add(this.GPBoxCMDSyntax_01);
            this.tabPage1.Controls.Add(this.BtnRobot_Connect_01);
            this.tabPage1.Controls.Add(this.GPBox_MODE_01);
            this.tabPage1.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tabPage1.Location = new System.Drawing.Point(4, 39);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1284, 238);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Robot1";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tabPage2.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tabPage2.Location = new System.Drawing.Point(4, 39);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1284, 238);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Aligner1";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tabPage3.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tabPage3.Location = new System.Drawing.Point(4, 39);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1284, 238);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Aligner2";
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tabPage4.Location = new System.Drawing.Point(4, 39);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(1284, 238);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "LoadPort01";
            // 
            // tabPage5
            // 
            this.tabPage5.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tabPage5.Location = new System.Drawing.Point(4, 39);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(1284, 238);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "LoadPort02";
            // 
            // tabPage6
            // 
            this.tabPage6.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tabPage6.Location = new System.Drawing.Point(4, 39);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(1284, 238);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "LoadPort03";
            // 
            // FormTerminal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(1460, 861);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.Button_Clean_Message);
            this.Controls.Add(this.RichTextBox_MESSAGE_LOG);
            this.Controls.Add(this.Button_SEND_COMMAND);
            this.Controls.Add(this.cbbCommand_SEND);
            this.Controls.Add(this.GroupBox_ATUORUN);
            this.Name = "FormTerminal";
            this.Text = "Terminal";
            this.GPBoxCMDSyntax_01.ResumeLayout(false);
            this.GPBoxCMDSyntax_01.PerformLayout();
            this.GroupBox2.ResumeLayout(false);
            this.GPBox_MODE_01.ResumeLayout(false);
            this.GPBox_MODE_01.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Script_A)).EndInit();
            this.GroupBox_ATUORUN.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.GroupBox GPBoxCMDSyntax_01;
        internal System.Windows.Forms.ComboBox CboRobot_A01;
        internal System.Windows.Forms.ComboBox CboRobot_A04;
        internal System.Windows.Forms.ComboBox CboRobot_A03;
        internal System.Windows.Forms.Label LabParaA_06;
        internal System.Windows.Forms.Button Button_ADD_SEND_01;
        internal System.Windows.Forms.Label LabParaA_01;
        internal System.Windows.Forms.ComboBox CboRobot_A06;
        internal System.Windows.Forms.ComboBox CboRobot_A02;
        internal System.Windows.Forms.Label LabParaA_05;
        internal System.Windows.Forms.Label LabParaA_02;
        internal System.Windows.Forms.ComboBox CboRobot_A05;
        internal System.Windows.Forms.Label LabParaA_03;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.ComboBox cbbCommand_01;
        internal System.Windows.Forms.Label LabParaA_04;
        internal System.Windows.Forms.GroupBox GroupBox2;
        internal System.Windows.Forms.Button BtnRobot_RESET_01;
        internal System.Windows.Forms.Button BtnRobot_SERVO_01;
        internal System.Windows.Forms.Button BtnRobot_HOME_01;
        internal System.Windows.Forms.GroupBox GPBox_MODE_01;
        internal System.Windows.Forms.RadioButton rBtnSyntaxMode_GET_01;
        internal System.Windows.Forms.RadioButton rBtnSyntaxMode_SET_01;
        internal System.Windows.Forms.RadioButton rBtnSyntaxMode_CMD_01;
        internal System.Windows.Forms.Button BtnRobot_Connect_01;
        internal System.Windows.Forms.Button Button_Clean_Message;
        internal System.Windows.Forms.RichTextBox RichTextBox_MESSAGE_LOG;
        internal System.Windows.Forms.Button Button_SEND_COMMAND;
        internal System.Windows.Forms.ComboBox cbbCommand_SEND;
        internal System.Windows.Forms.DataGridView DGV_Script_A;
        internal System.Windows.Forms.DataGridViewTextBoxColumn Col_Tag;
        internal System.Windows.Forms.DataGridViewTextBoxColumn Col_ScriptCMD;
        internal System.Windows.Forms.GroupBox GroupBox_ATUORUN;
        internal System.Windows.Forms.Button Btn_AUTO_RUN_PAUSE_A;
        internal System.Windows.Forms.Button Btn_AUTO_RUN_STOP_A;
        internal System.Windows.Forms.Button Btn_AUTORUN_STEP_A;
        internal System.Windows.Forms.Button Btn_ADD_SCRIPT_A;
        internal System.Windows.Forms.Button Btn_Edit_Mode_A;
        internal System.Windows.Forms.Button Btn_AUTO_RUN_START_A;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage6;
    }
}