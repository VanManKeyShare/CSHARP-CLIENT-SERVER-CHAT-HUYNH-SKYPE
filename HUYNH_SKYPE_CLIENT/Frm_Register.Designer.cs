namespace HUYNH_SKYPE_CLIENT
{
    partial class Frm_Register
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Register));
            this.logo = new System.Windows.Forms.PictureBox();
            this.txt_account = new System.Windows.Forms.TextBox();
            this.txt_password = new System.Windows.Forms.TextBox();
            this.btn_register = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_ho_ten = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_port_server = new System.Windows.Forms.TextBox();
            this.txt_ip_server = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
            this.SuspendLayout();
            // 
            // logo
            // 
            this.logo.Image = ((System.Drawing.Image)(resources.GetObject("logo.Image")));
            this.logo.Location = new System.Drawing.Point(163, 30);
            this.logo.Name = "logo";
            this.logo.Size = new System.Drawing.Size(136, 60);
            this.logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.logo.TabIndex = 0;
            this.logo.TabStop = false;
            // 
            // txt_account
            // 
            this.txt_account.Location = new System.Drawing.Point(163, 118);
            this.txt_account.MaxLength = 50;
            this.txt_account.Name = "txt_account";
            this.txt_account.Size = new System.Drawing.Size(256, 23);
            this.txt_account.TabIndex = 0;
            this.txt_account.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_password
            // 
            this.txt_password.Location = new System.Drawing.Point(163, 147);
            this.txt_password.MaxLength = 50;
            this.txt_password.Name = "txt_password";
            this.txt_password.Size = new System.Drawing.Size(256, 23);
            this.txt_password.TabIndex = 1;
            this.txt_password.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btn_register
            // 
            this.btn_register.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_register.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_register.Location = new System.Drawing.Point(163, 287);
            this.btn_register.Name = "btn_register";
            this.btn_register.Size = new System.Drawing.Size(256, 39);
            this.btn_register.TabIndex = 5;
            this.btn_register.Text = "ĐĂNG KÝ";
            this.btn_register.UseVisualStyleBackColor = true;
            this.btn_register.Click += new System.EventHandler(this.btn_register_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(69, 150);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 16);
            this.label5.TabIndex = 7;
            this.label5.Text = "MẬT KHẨU";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(69, 121);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 16);
            this.label6.TabIndex = 6;
            this.label6.Text = "TÀI KHOẢN";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(69, 179);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "HỌ VÀ TÊN";
            // 
            // txt_ho_ten
            // 
            this.txt_ho_ten.Location = new System.Drawing.Point(163, 176);
            this.txt_ho_ten.MaxLength = 50;
            this.txt_ho_ten.Name = "txt_ho_ten";
            this.txt_ho_ten.Size = new System.Drawing.Size(256, 23);
            this.txt_ho_ten.TabIndex = 2;
            this.txt_ho_ten.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(69, 247);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 16);
            this.label4.TabIndex = 11;
            this.label4.Text = "PORT";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(69, 218);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 16);
            this.label3.TabIndex = 10;
            this.label3.Text = "IP SERVER";
            // 
            // txt_port_server
            // 
            this.txt_port_server.Location = new System.Drawing.Point(163, 244);
            this.txt_port_server.Name = "txt_port_server";
            this.txt_port_server.Size = new System.Drawing.Size(256, 23);
            this.txt_port_server.TabIndex = 4;
            this.txt_port_server.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_ip_server
            // 
            this.txt_ip_server.Location = new System.Drawing.Point(163, 215);
            this.txt_ip_server.Name = "txt_ip_server";
            this.txt_ip_server.Size = new System.Drawing.Size(256, 23);
            this.txt_ip_server.TabIndex = 3;
            this.txt_ip_server.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Frm_Register
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::HUYNH_SKYPE_CLIENT.Properties.Resources.Background;
            this.ClientSize = new System.Drawing.Size(478, 367);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_port_server);
            this.Controls.Add(this.txt_ip_server);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btn_register);
            this.Controls.Add(this.txt_ho_ten);
            this.Controls.Add(this.txt_password);
            this.Controls.Add(this.txt_account);
            this.Controls.Add(this.logo);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "Frm_Register";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HUỲNH SKYPE - ĐĂNG KÝ";
            this.Load += new System.EventHandler(this.Frm_Register_Load);
            ((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox logo;
        private System.Windows.Forms.TextBox txt_account;
        private System.Windows.Forms.TextBox txt_password;
        private System.Windows.Forms.Button btn_register;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_ho_ten;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_port_server;
        private System.Windows.Forms.TextBox txt_ip_server;
    }
}

