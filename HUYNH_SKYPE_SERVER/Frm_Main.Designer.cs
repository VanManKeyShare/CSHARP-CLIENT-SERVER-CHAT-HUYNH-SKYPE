namespace HUYNH_SKYPE_SERVER
{
    partial class Frm_Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Main));
            this.label1 = new System.Windows.Forms.Label();
            this.txt_ip_server = new System.Windows.Forms.TextBox();
            this.btn_start_server = new System.Windows.Forms.Button();
            this.btn_stop_server = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.list_accounts = new System.Windows.Forms.ListBox();
            this.list_accounts_online = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_port_server = new System.Windows.Forms.TextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.btn_update_list_account = new System.Windows.Forms.Button();
            this.btn_update_list_account_online = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP MÁY CHỦ";
            // 
            // txt_ip_server
            // 
            this.txt_ip_server.Location = new System.Drawing.Point(11, 28);
            this.txt_ip_server.Name = "txt_ip_server";
            this.txt_ip_server.Size = new System.Drawing.Size(146, 23);
            this.txt_ip_server.TabIndex = 0;
            this.txt_ip_server.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btn_start_server
            // 
            this.btn_start_server.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_start_server.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_start_server.Location = new System.Drawing.Point(284, 7);
            this.btn_start_server.Name = "btn_start_server";
            this.btn_start_server.Size = new System.Drawing.Size(153, 44);
            this.btn_start_server.TabIndex = 2;
            this.btn_start_server.Text = "MỞ MÁY CHỦ";
            this.btn_start_server.UseVisualStyleBackColor = true;
            this.btn_start_server.Click += new System.EventHandler(this.btn_start_server_Click);
            // 
            // btn_stop_server
            // 
            this.btn_stop_server.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_stop_server.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_stop_server.Location = new System.Drawing.Point(443, 7);
            this.btn_stop_server.Name = "btn_stop_server";
            this.btn_stop_server.Size = new System.Drawing.Size(162, 44);
            this.btn_stop_server.TabIndex = 3;
            this.btn_stop_server.Text = "ĐÓNG MÁY CHỦ";
            this.btn_stop_server.UseVisualStyleBackColor = true;
            this.btn_stop_server.Click += new System.EventHandler(this.btn_stop_server_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(8, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "DANH SÁCH TÀI KHOẢN";
            // 
            // list_accounts
            // 
            this.list_accounts.FormattingEnabled = true;
            this.list_accounts.ItemHeight = 16;
            this.list_accounts.Location = new System.Drawing.Point(11, 82);
            this.list_accounts.Name = "list_accounts";
            this.list_accounts.Size = new System.Drawing.Size(267, 116);
            this.list_accounts.TabIndex = 4;
            // 
            // list_accounts_online
            // 
            this.list_accounts_online.FormattingEnabled = true;
            this.list_accounts_online.ItemHeight = 16;
            this.list_accounts_online.Location = new System.Drawing.Point(284, 82);
            this.list_accounts_online.Name = "list_accounts_online";
            this.list_accounts_online.Size = new System.Drawing.Size(321, 116);
            this.list_accounts_online.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(281, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(212, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "DANH SÁCH TÀI KHOẢN ĐANG ONLINE";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(160, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "MỞ CỔNG";
            // 
            // txt_port_server
            // 
            this.txt_port_server.Location = new System.Drawing.Point(163, 28);
            this.txt_port_server.Name = "txt_port_server";
            this.txt_port_server.Size = new System.Drawing.Size(115, 23);
            this.txt_port_server.TabIndex = 1;
            this.txt_port_server.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // btn_update_list_account
            // 
            this.btn_update_list_account.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_update_list_account.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_update_list_account.Location = new System.Drawing.Point(11, 204);
            this.btn_update_list_account.Name = "btn_update_list_account";
            this.btn_update_list_account.Size = new System.Drawing.Size(267, 33);
            this.btn_update_list_account.TabIndex = 3;
            this.btn_update_list_account.Text = "CẬP NHẬT DANH SÁCH ACCOUNT";
            this.btn_update_list_account.UseVisualStyleBackColor = true;
            this.btn_update_list_account.Click += new System.EventHandler(this.btn_update_list_account_Click);
            // 
            // btn_update_list_account_online
            // 
            this.btn_update_list_account_online.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_update_list_account_online.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_update_list_account_online.Location = new System.Drawing.Point(284, 204);
            this.btn_update_list_account_online.Name = "btn_update_list_account_online";
            this.btn_update_list_account_online.Size = new System.Drawing.Size(321, 33);
            this.btn_update_list_account_online.TabIndex = 3;
            this.btn_update_list_account_online.Text = "CẬP NHẬT DANH SÁCH ACCOUNT ĐANG ONLINE";
            this.btn_update_list_account_online.UseVisualStyleBackColor = true;
            this.btn_update_list_account_online.Click += new System.EventHandler(this.btn_update_list_account_online_Click);
            // 
            // Frm_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::HUYNH_SKYPE_SERVER.Properties.Resources.Background;
            this.ClientSize = new System.Drawing.Size(617, 249);
            this.Controls.Add(this.list_accounts_online);
            this.Controls.Add(this.list_accounts);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_update_list_account_online);
            this.Controls.Add(this.btn_update_list_account);
            this.Controls.Add(this.btn_stop_server);
            this.Controls.Add(this.btn_start_server);
            this.Controls.Add(this.txt_port_server);
            this.Controls.Add(this.txt_ip_server);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "Frm_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HUỲNH SKYPE - SERVER";
            this.Load += new System.EventHandler(this.Frm_Main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_ip_server;
        private System.Windows.Forms.Button btn_start_server;
        private System.Windows.Forms.Button btn_stop_server;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox list_accounts;
        private System.Windows.Forms.ListBox list_accounts_online;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_port_server;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button btn_update_list_account;
        private System.Windows.Forms.Button btn_update_list_account_online;
    }
}

