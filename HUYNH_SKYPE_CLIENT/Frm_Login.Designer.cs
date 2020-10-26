namespace HUYNH_SKYPE_CLIENT
{
    partial class Frm_Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Login));
            this.logo = new System.Windows.Forms.PictureBox();
            this.txt_account = new System.Windows.Forms.TextBox();
            this.txt_password = new System.Windows.Forms.TextBox();
            this.btn_login = new System.Windows.Forms.Button();
            this.btn_register = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_ip_server = new System.Windows.Forms.TextBox();
            this.txt_port_server = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
            this.SuspendLayout();
            // 
            // logo
            // 
            this.logo.Image = ((System.Drawing.Image)(resources.GetObject("logo.Image")));
            this.logo.Location = new System.Drawing.Point(158, 27);
            this.logo.Name = "logo";
            this.logo.Size = new System.Drawing.Size(136, 60);
            this.logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.logo.TabIndex = 0;
            this.logo.TabStop = false;
            // 
            // txt_account
            // 
            this.txt_account.Location = new System.Drawing.Point(158, 111);
            this.txt_account.MaxLength = 50;
            this.txt_account.Name = "txt_account";
            this.txt_account.Size = new System.Drawing.Size(256, 23);
            this.txt_account.TabIndex = 0;
            this.txt_account.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_password
            // 
            this.txt_password.Location = new System.Drawing.Point(158, 140);
            this.txt_password.MaxLength = 50;
            this.txt_password.Name = "txt_password";
            this.txt_password.Size = new System.Drawing.Size(256, 23);
            this.txt_password.TabIndex = 1;
            this.txt_password.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btn_login
            // 
            this.btn_login.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_login.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_login.Location = new System.Drawing.Point(158, 249);
            this.btn_login.Name = "btn_login";
            this.btn_login.Size = new System.Drawing.Size(256, 39);
            this.btn_login.TabIndex = 4;
            this.btn_login.Text = "ĐĂNG NHẬP";
            this.btn_login.UseVisualStyleBackColor = true;
            this.btn_login.Click += new System.EventHandler(this.btn_login_Click);
            // 
            // btn_register
            // 
            this.btn_register.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_register.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_register.Location = new System.Drawing.Point(158, 294);
            this.btn_register.Name = "btn_register";
            this.btn_register.Size = new System.Drawing.Size(256, 39);
            this.btn_register.TabIndex = 5;
            this.btn_register.Text = "ĐĂNG KÝ";
            this.btn_register.UseVisualStyleBackColor = true;
            this.btn_register.Click += new System.EventHandler(this.btn_register_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(74, 143);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "MẬT KHẨU";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(74, 114);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "TÀI KHOẢN";
            // 
            // txt_ip_server
            // 
            this.txt_ip_server.Location = new System.Drawing.Point(158, 179);
            this.txt_ip_server.Name = "txt_ip_server";
            this.txt_ip_server.Size = new System.Drawing.Size(256, 23);
            this.txt_ip_server.TabIndex = 2;
            this.txt_ip_server.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_port_server
            // 
            this.txt_port_server.Location = new System.Drawing.Point(158, 208);
            this.txt_port_server.Name = "txt_port_server";
            this.txt_port_server.Size = new System.Drawing.Size(256, 23);
            this.txt_port_server.TabIndex = 3;
            this.txt_port_server.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(74, 182);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "IP SERVER";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(74, 211);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "PORT";
            // 
            // Frm_Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::HUYNH_SKYPE_CLIENT.Properties.Resources.Background;
            this.ClientSize = new System.Drawing.Size(474, 375);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_register);
            this.Controls.Add(this.btn_login);
            this.Controls.Add(this.txt_port_server);
            this.Controls.Add(this.txt_password);
            this.Controls.Add(this.txt_ip_server);
            this.Controls.Add(this.txt_account);
            this.Controls.Add(this.logo);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "Frm_Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HUỲNH SKYPE - ĐĂNG NHẬP";
            this.Load += new System.EventHandler(this.Frm_Login_Load);
            ((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox logo;
        private System.Windows.Forms.TextBox txt_account;
        private System.Windows.Forms.TextBox txt_password;
        private System.Windows.Forms.Button btn_login;
        private System.Windows.Forms.Button btn_register;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_ip_server;
        private System.Windows.Forms.TextBox txt_port_server;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

