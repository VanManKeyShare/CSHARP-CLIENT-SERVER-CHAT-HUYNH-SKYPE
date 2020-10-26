namespace HUYNH_SKYPE_CLIENT
{
    partial class Frm_Update_Account_Info
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Update_Account_Info));
            this.logo = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_account = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_full_name = new System.Windows.Forms.TextBox();
            this.avatar = new System.Windows.Forms.PictureBox();
            this.btn_choose_avatar = new System.Windows.Forms.Button();
            this.btn_update_account_info = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_current_password = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_new_password = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.avatar)).BeginInit();
            this.SuspendLayout();
            // 
            // logo
            // 
            this.logo.Image = ((System.Drawing.Image)(resources.GetObject("logo.Image")));
            this.logo.Location = new System.Drawing.Point(168, 12);
            this.logo.Name = "logo";
            this.logo.Size = new System.Drawing.Size(136, 60);
            this.logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.logo.TabIndex = 1;
            this.logo.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(86, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "ACCOUNT";
            // 
            // txt_account
            // 
            this.txt_account.BackColor = System.Drawing.Color.White;
            this.txt_account.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.txt_account.Location = new System.Drawing.Point(168, 94);
            this.txt_account.Name = "txt_account";
            this.txt_account.ReadOnly = true;
            this.txt_account.Size = new System.Drawing.Size(308, 23);
            this.txt_account.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(86, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "HỌ VÀ TÊN";
            // 
            // txt_full_name
            // 
            this.txt_full_name.BackColor = System.Drawing.Color.White;
            this.txt_full_name.Location = new System.Drawing.Point(168, 123);
            this.txt_full_name.MaxLength = 50;
            this.txt_full_name.Name = "txt_full_name";
            this.txt_full_name.Size = new System.Drawing.Size(308, 23);
            this.txt_full_name.TabIndex = 1;
            // 
            // avatar
            // 
            this.avatar.Image = global::HUYNH_SKYPE_CLIENT.Properties.Resources.No_Avatar_80x80;
            this.avatar.Location = new System.Drawing.Point(168, 267);
            this.avatar.Name = "avatar";
            this.avatar.Size = new System.Drawing.Size(80, 80);
            this.avatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.avatar.TabIndex = 4;
            this.avatar.TabStop = false;
            // 
            // btn_choose_avatar
            // 
            this.btn_choose_avatar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_choose_avatar.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_choose_avatar.Location = new System.Drawing.Point(262, 267);
            this.btn_choose_avatar.Name = "btn_choose_avatar";
            this.btn_choose_avatar.Size = new System.Drawing.Size(214, 35);
            this.btn_choose_avatar.TabIndex = 4;
            this.btn_choose_avatar.Text = "CHỌN AVATAR";
            this.btn_choose_avatar.UseVisualStyleBackColor = true;
            this.btn_choose_avatar.Click += new System.EventHandler(this.btn_choose_avatar_Click);
            // 
            // btn_update_account_info
            // 
            this.btn_update_account_info.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_update_account_info.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_update_account_info.Location = new System.Drawing.Point(262, 308);
            this.btn_update_account_info.Name = "btn_update_account_info";
            this.btn_update_account_info.Size = new System.Drawing.Size(214, 39);
            this.btn_update_account_info.TabIndex = 5;
            this.btn_update_account_info.Text = "LƯU THÔNG TIN ACCOUNT";
            this.btn_update_account_info.UseVisualStyleBackColor = true;
            this.btn_update_account_info.Click += new System.EventHandler(this.btn_update_account_info_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(86, 225);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(145, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "XÁC NHẬN MẬT KHẨU";
            // 
            // txt_current_password
            // 
            this.txt_current_password.BackColor = System.Drawing.Color.White;
            this.txt_current_password.Location = new System.Drawing.Point(262, 222);
            this.txt_current_password.Name = "txt_current_password";
            this.txt_current_password.Size = new System.Drawing.Size(214, 23);
            this.txt_current_password.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(86, 161);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 16);
            this.label4.TabIndex = 2;
            this.label4.Text = "MẬT KHẨU MỚI";
            // 
            // txt_new_password
            // 
            this.txt_new_password.BackColor = System.Drawing.Color.White;
            this.txt_new_password.Location = new System.Drawing.Point(197, 158);
            this.txt_new_password.MaxLength = 50;
            this.txt_new_password.Name = "txt_new_password";
            this.txt_new_password.Size = new System.Drawing.Size(279, 23);
            this.txt_new_password.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(89, 186);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(3);
            this.label5.Size = new System.Drawing.Size(386, 22);
            this.label5.TabIndex = 2;
            this.label5.Text = "*** LƯU Ý - MẬT KHẨU ĐỂ TRỐNG NẾU KHÔNG MUỐN THAY ĐỔI";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Frm_Update_Account_Info
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::HUYNH_SKYPE_CLIENT.Properties.Resources.Background;
            this.ClientSize = new System.Drawing.Size(564, 405);
            this.Controls.Add(this.btn_update_account_info);
            this.Controls.Add(this.btn_choose_avatar);
            this.Controls.Add(this.avatar);
            this.Controls.Add(this.txt_current_password);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_new_password);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_full_name);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_account);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.logo);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "Frm_Update_Account_Info";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HUỲNH SKYPE - CẬP NHẬT THÔNG TIN TÀI KHOẢN";
            this.Load += new System.EventHandler(this.Frm_Update_Account_Info_Load);
            ((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.avatar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox logo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_account;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_full_name;
        private System.Windows.Forms.PictureBox avatar;
        private System.Windows.Forms.Button btn_choose_avatar;
        private System.Windows.Forms.Button btn_update_account_info;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_current_password;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_new_password;
        private System.Windows.Forms.Label label5;
    }
}