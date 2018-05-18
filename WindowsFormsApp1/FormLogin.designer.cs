namespace GUI
{
    partial class FormLogin
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
            this.panel40 = new System.Windows.Forms.Panel();
            this.button33 = new System.Windows.Forms.Button();
            this.label80 = new System.Windows.Forms.Label();
            this.label84 = new System.Windows.Forms.Label();
            this.button26 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.panel40.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel40
            // 
            this.panel40.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel40.Controls.Add(this.textBox2);
            this.panel40.Controls.Add(this.textBox1);
            this.panel40.Controls.Add(this.button33);
            this.panel40.Controls.Add(this.label80);
            this.panel40.Controls.Add(this.label84);
            this.panel40.Controls.Add(this.button26);
            this.panel40.Location = new System.Drawing.Point(12, 12);
            this.panel40.Name = "panel40";
            this.panel40.Size = new System.Drawing.Size(422, 192);
            this.panel40.TabIndex = 65;
            // 
            // button33
            // 
            this.button33.BackColor = System.Drawing.Color.DarkGray;
            this.button33.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button33.FlatAppearance.BorderSize = 2;
            this.button33.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button33.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button33.Location = new System.Drawing.Point(98, 125);
            this.button33.Name = "button33";
            this.button33.Size = new System.Drawing.Size(148, 46);
            this.button33.TabIndex = 67;
            this.button33.Text = "Cancel";
            this.button33.UseVisualStyleBackColor = false;
            // 
            // label80
            // 
            this.label80.AutoSize = true;
            this.label80.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label80.Location = new System.Drawing.Point(8, 66);
            this.label80.Name = "label80";
            this.label80.Size = new System.Drawing.Size(120, 30);
            this.label80.TabIndex = 3;
            this.label80.Text = "Password";
            // 
            // label84
            // 
            this.label84.AutoSize = true;
            this.label84.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label84.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label84.Location = new System.Drawing.Point(32, 12);
            this.label84.Name = "label84";
            this.label84.Size = new System.Drawing.Size(96, 30);
            this.label84.TabIndex = 56;
            this.label84.Text = "User ID";
            // 
            // button26
            // 
            this.button26.BackColor = System.Drawing.Color.DarkGray;
            this.button26.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button26.FlatAppearance.BorderSize = 2;
            this.button26.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button26.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button26.Location = new System.Drawing.Point(252, 125);
            this.button26.Name = "button26";
            this.button26.Size = new System.Drawing.Size(148, 46);
            this.button26.TabIndex = 53;
            this.button26.Text = "Login";
            this.button26.UseVisualStyleBackColor = false;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBox1.Location = new System.Drawing.Point(134, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(266, 39);
            this.textBox1.TabIndex = 68;
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBox2.Location = new System.Drawing.Point(134, 66);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(266, 39);
            this.textBox2.TabIndex = 69;
            // 
            // FormLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(457, 214);
            this.Controls.Add(this.panel40);
            this.Name = "FormLogin";
            this.Text = "Login";
            this.panel40.ResumeLayout(false);
            this.panel40.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel40;
        private System.Windows.Forms.Button button33;
        private System.Windows.Forms.Label label80;
        private System.Windows.Forms.Label label84;
        private System.Windows.Forms.Button button26;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
    }
}