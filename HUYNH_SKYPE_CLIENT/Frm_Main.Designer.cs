namespace HUYNH_SKYPE_CLIENT
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Main));
            this.label_ho_ten = new System.Windows.Forms.Label();
            this.avatar = new System.Windows.Forms.PictureBox();
            this.btn_change_info = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.TabC1 = new System.Windows.Forms.TabControl();
            this.TabP1P1 = new System.Windows.Forms.TabPage();
            this.btn_remove_msg_public = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_msg_public = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_send_msg_public = new System.Windows.Forms.Button();
            this.list_msg_public = new System.Windows.Forms.DataGridView();
            this.Tab1P2 = new System.Windows.Forms.TabPage();
            this.list_msg_private = new System.Windows.Forms.DataGridView();
            this.TabC2 = new System.Windows.Forms.TabControl();
            this.Tab2P1 = new System.Windows.Forms.TabPage();
            this.list_friends = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_acc_friend = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_remove_friend = new System.Windows.Forms.Button();
            this.btn_add_friend = new System.Windows.Forms.Button();
            this.btn_get_list_friend = new System.Windows.Forms.Button();
            this.Tab2P2 = new System.Windows.Forms.TabPage();
            this.btn_get_list_not_friend = new System.Windows.Forms.Button();
            this.list_not_friends_send_msg = new System.Windows.Forms.DataGridView();
            this.label10 = new System.Windows.Forms.Label();
            this.check_choose_file_attachment_private = new System.Windows.Forms.CheckBox();
            this.btn_download_attachment_private = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_msg_private = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btn_remove_msg_private = new System.Windows.Forms.Button();
            this.btn_send_msg_private = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.avatar)).BeginInit();
            this.TabC1.SuspendLayout();
            this.TabP1P1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.list_msg_public)).BeginInit();
            this.Tab1P2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.list_msg_private)).BeginInit();
            this.TabC2.SuspendLayout();
            this.Tab2P1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.list_friends)).BeginInit();
            this.Tab2P2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.list_not_friends_send_msg)).BeginInit();
            this.SuspendLayout();
            // 
            // label_ho_ten
            // 
            this.label_ho_ten.AutoSize = true;
            this.label_ho_ten.BackColor = System.Drawing.Color.Transparent;
            this.label_ho_ten.ForeColor = System.Drawing.Color.White;
            this.label_ho_ten.Location = new System.Drawing.Point(98, 31);
            this.label_ho_ten.Name = "label_ho_ten";
            this.label_ho_ten.Size = new System.Drawing.Size(83, 16);
            this.label_ho_ten.TabIndex = 2;
            this.label_ho_ten.Text = "<Full Name>";
            // 
            // avatar
            // 
            this.avatar.Image = global::HUYNH_SKYPE_CLIENT.Properties.Resources.No_Avatar_80x80;
            this.avatar.Location = new System.Drawing.Point(12, 12);
            this.avatar.Name = "avatar";
            this.avatar.Size = new System.Drawing.Size(80, 80);
            this.avatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.avatar.TabIndex = 1;
            this.avatar.TabStop = false;
            // 
            // btn_change_info
            // 
            this.btn_change_info.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_change_info.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_change_info.Location = new System.Drawing.Point(101, 59);
            this.btn_change_info.Name = "btn_change_info";
            this.btn_change_info.Size = new System.Drawing.Size(200, 33);
            this.btn_change_info.TabIndex = 0;
            this.btn_change_info.Text = "CẬP NHẬT THÔNG TIN TÀI KHOẢN";
            this.btn_change_info.UseVisualStyleBackColor = true;
            this.btn_change_info.Click += new System.EventHandler(this.btn_change_info_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(98, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "HỌ VÀ TÊN";
            // 
            // TabC1
            // 
            this.TabC1.Controls.Add(this.TabP1P1);
            this.TabC1.Controls.Add(this.Tab1P2);
            this.TabC1.Location = new System.Drawing.Point(12, 98);
            this.TabC1.Name = "TabC1";
            this.TabC1.SelectedIndex = 0;
            this.TabC1.Size = new System.Drawing.Size(894, 421);
            this.TabC1.TabIndex = 6;
            // 
            // TabP1P1
            // 
            this.TabP1P1.Controls.Add(this.btn_remove_msg_public);
            this.TabP1P1.Controls.Add(this.label6);
            this.TabP1P1.Controls.Add(this.txt_msg_public);
            this.TabP1P1.Controls.Add(this.label4);
            this.TabP1P1.Controls.Add(this.btn_send_msg_public);
            this.TabP1P1.Controls.Add(this.list_msg_public);
            this.TabP1P1.Location = new System.Drawing.Point(4, 25);
            this.TabP1P1.Name = "TabP1P1";
            this.TabP1P1.Padding = new System.Windows.Forms.Padding(3);
            this.TabP1P1.Size = new System.Drawing.Size(886, 392);
            this.TabP1P1.TabIndex = 1;
            this.TabP1P1.Text = "Công cộng";
            this.TabP1P1.UseVisualStyleBackColor = true;
            // 
            // btn_remove_msg_public
            // 
            this.btn_remove_msg_public.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_remove_msg_public.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_remove_msg_public.Location = new System.Drawing.Point(742, 356);
            this.btn_remove_msg_public.Name = "btn_remove_msg_public";
            this.btn_remove_msg_public.Size = new System.Drawing.Size(138, 30);
            this.btn_remove_msg_public.TabIndex = 20;
            this.btn_remove_msg_public.Text = "XÓA TIN ĐANG CHỌN";
            this.btn_remove_msg_public.UseVisualStyleBackColor = true;
            this.btn_remove_msg_public.Click += new System.EventHandler(this.btn_remove_msg_public_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Blue;
            this.label6.Location = new System.Drawing.Point(3, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(126, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "DANH SÁCH TIN NHẮN";
            // 
            // txt_msg_public
            // 
            this.txt_msg_public.Location = new System.Drawing.Point(6, 323);
            this.txt_msg_public.Multiline = true;
            this.txt_msg_public.Name = "txt_msg_public";
            this.txt_msg_public.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_msg_public.Size = new System.Drawing.Size(730, 63);
            this.txt_msg_public.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(3, 307);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(149, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "NHẬP NỘI DUNG TIN NHẮN";
            // 
            // btn_send_msg_public
            // 
            this.btn_send_msg_public.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_send_msg_public.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_send_msg_public.Location = new System.Drawing.Point(742, 321);
            this.btn_send_msg_public.Name = "btn_send_msg_public";
            this.btn_send_msg_public.Size = new System.Drawing.Size(138, 29);
            this.btn_send_msg_public.TabIndex = 11;
            this.btn_send_msg_public.Text = "GỬI TIN";
            this.btn_send_msg_public.UseVisualStyleBackColor = true;
            this.btn_send_msg_public.Click += new System.EventHandler(this.btn_send_msg_public_Click);
            // 
            // list_msg_public
            // 
            this.list_msg_public.AllowUserToAddRows = false;
            this.list_msg_public.AllowUserToDeleteRows = false;
            this.list_msg_public.AllowUserToResizeColumns = false;
            this.list_msg_public.AllowUserToResizeRows = false;
            this.list_msg_public.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.list_msg_public.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.list_msg_public.BackgroundColor = System.Drawing.Color.White;
            this.list_msg_public.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.list_msg_public.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.list_msg_public.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.list_msg_public.ColumnHeadersVisible = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.list_msg_public.DefaultCellStyle = dataGridViewCellStyle1;
            this.list_msg_public.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.list_msg_public.Location = new System.Drawing.Point(6, 19);
            this.list_msg_public.MultiSelect = false;
            this.list_msg_public.Name = "list_msg_public";
            this.list_msg_public.ReadOnly = true;
            this.list_msg_public.RowHeadersVisible = false;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.list_msg_public.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.list_msg_public.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.list_msg_public.Size = new System.Drawing.Size(874, 285);
            this.list_msg_public.TabIndex = 7;
            // 
            // Tab1P2
            // 
            this.Tab1P2.Controls.Add(this.list_msg_private);
            this.Tab1P2.Controls.Add(this.TabC2);
            this.Tab1P2.Controls.Add(this.check_choose_file_attachment_private);
            this.Tab1P2.Controls.Add(this.btn_download_attachment_private);
            this.Tab1P2.Controls.Add(this.label7);
            this.Tab1P2.Controls.Add(this.txt_msg_private);
            this.Tab1P2.Controls.Add(this.label8);
            this.Tab1P2.Controls.Add(this.btn_remove_msg_private);
            this.Tab1P2.Controls.Add(this.btn_send_msg_private);
            this.Tab1P2.Location = new System.Drawing.Point(4, 25);
            this.Tab1P2.Name = "Tab1P2";
            this.Tab1P2.Padding = new System.Windows.Forms.Padding(3);
            this.Tab1P2.Size = new System.Drawing.Size(886, 392);
            this.Tab1P2.TabIndex = 0;
            this.Tab1P2.Text = "Cá nhân";
            this.Tab1P2.UseVisualStyleBackColor = true;
            // 
            // list_msg_private
            // 
            this.list_msg_private.AllowUserToAddRows = false;
            this.list_msg_private.AllowUserToDeleteRows = false;
            this.list_msg_private.AllowUserToResizeColumns = false;
            this.list_msg_private.AllowUserToResizeRows = false;
            this.list_msg_private.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.list_msg_private.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.list_msg_private.BackgroundColor = System.Drawing.Color.White;
            this.list_msg_private.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.list_msg_private.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.list_msg_private.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.list_msg_private.ColumnHeadersVisible = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.list_msg_private.DefaultCellStyle = dataGridViewCellStyle3;
            this.list_msg_private.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.list_msg_private.Location = new System.Drawing.Point(294, 22);
            this.list_msg_private.MultiSelect = false;
            this.list_msg_private.Name = "list_msg_private";
            this.list_msg_private.ReadOnly = true;
            this.list_msg_private.RowHeadersVisible = false;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.list_msg_private.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.list_msg_private.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.list_msg_private.Size = new System.Drawing.Size(586, 258);
            this.list_msg_private.TabIndex = 26;
            this.list_msg_private.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.list_msg_private_CellEnter);
            // 
            // TabC2
            // 
            this.TabC2.Controls.Add(this.Tab2P1);
            this.TabC2.Controls.Add(this.Tab2P2);
            this.TabC2.Location = new System.Drawing.Point(6, 6);
            this.TabC2.Name = "TabC2";
            this.TabC2.SelectedIndex = 0;
            this.TabC2.Size = new System.Drawing.Size(279, 380);
            this.TabC2.TabIndex = 25;
            // 
            // Tab2P1
            // 
            this.Tab2P1.Controls.Add(this.list_friends);
            this.Tab2P1.Controls.Add(this.label3);
            this.Tab2P1.Controls.Add(this.txt_acc_friend);
            this.Tab2P1.Controls.Add(this.label2);
            this.Tab2P1.Controls.Add(this.btn_remove_friend);
            this.Tab2P1.Controls.Add(this.btn_add_friend);
            this.Tab2P1.Controls.Add(this.btn_get_list_friend);
            this.Tab2P1.Location = new System.Drawing.Point(4, 25);
            this.Tab2P1.Name = "Tab2P1";
            this.Tab2P1.Padding = new System.Windows.Forms.Padding(3);
            this.Tab2P1.Size = new System.Drawing.Size(271, 351);
            this.Tab2P1.TabIndex = 0;
            this.Tab2P1.Text = "Danh sách bạn bè";
            this.Tab2P1.UseVisualStyleBackColor = true;
            // 
            // list_friends
            // 
            this.list_friends.AllowUserToAddRows = false;
            this.list_friends.AllowUserToDeleteRows = false;
            this.list_friends.AllowUserToOrderColumns = true;
            this.list_friends.AllowUserToResizeRows = false;
            this.list_friends.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.list_friends.BackgroundColor = System.Drawing.Color.White;
            this.list_friends.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.list_friends.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.list_friends.Location = new System.Drawing.Point(6, 19);
            this.list_friends.MultiSelect = false;
            this.list_friends.Name = "list_friends";
            this.list_friends.ReadOnly = true;
            this.list_friends.RowHeadersVisible = false;
            this.list_friends.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.list_friends.Size = new System.Drawing.Size(259, 210);
            this.list_friends.TabIndex = 19;
            this.list_friends.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.list_friends_CellClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(3, 268);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "ACCOUNT CẦN THÊM";
            // 
            // txt_acc_friend
            // 
            this.txt_acc_friend.Location = new System.Drawing.Point(6, 284);
            this.txt_acc_friend.MaxLength = 50;
            this.txt_acc_friend.Name = "txt_acc_friend";
            this.txt_acc_friend.Size = new System.Drawing.Size(259, 23);
            this.txt_acc_friend.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "DANH SÁCH BẠN BÈ";
            // 
            // btn_remove_friend
            // 
            this.btn_remove_friend.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_remove_friend.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_remove_friend.Location = new System.Drawing.Point(106, 313);
            this.btn_remove_friend.Name = "btn_remove_friend";
            this.btn_remove_friend.Size = new System.Drawing.Size(159, 32);
            this.btn_remove_friend.TabIndex = 13;
            this.btn_remove_friend.Text = "XÓA BẠN ĐANG CHỌN";
            this.btn_remove_friend.UseVisualStyleBackColor = true;
            this.btn_remove_friend.Click += new System.EventHandler(this.btn_remove_friend_Click);
            // 
            // btn_add_friend
            // 
            this.btn_add_friend.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_add_friend.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_add_friend.Location = new System.Drawing.Point(6, 313);
            this.btn_add_friend.Name = "btn_add_friend";
            this.btn_add_friend.Size = new System.Drawing.Size(94, 32);
            this.btn_add_friend.TabIndex = 14;
            this.btn_add_friend.Text = "THÊM BẠN";
            this.btn_add_friend.UseVisualStyleBackColor = true;
            this.btn_add_friend.Click += new System.EventHandler(this.btn_add_friend_Click);
            // 
            // btn_get_list_friend
            // 
            this.btn_get_list_friend.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_get_list_friend.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_get_list_friend.Location = new System.Drawing.Point(6, 235);
            this.btn_get_list_friend.Name = "btn_get_list_friend";
            this.btn_get_list_friend.Size = new System.Drawing.Size(259, 30);
            this.btn_get_list_friend.TabIndex = 15;
            this.btn_get_list_friend.Text = "CẬP NHẬT DANH SÁCH BẠN BÈ";
            this.btn_get_list_friend.UseVisualStyleBackColor = true;
            this.btn_get_list_friend.Click += new System.EventHandler(this.btn_get_list_friend_Click);
            // 
            // Tab2P2
            // 
            this.Tab2P2.Controls.Add(this.btn_get_list_not_friend);
            this.Tab2P2.Controls.Add(this.list_not_friends_send_msg);
            this.Tab2P2.Controls.Add(this.label10);
            this.Tab2P2.Location = new System.Drawing.Point(4, 25);
            this.Tab2P2.Name = "Tab2P2";
            this.Tab2P2.Padding = new System.Windows.Forms.Padding(3);
            this.Tab2P2.Size = new System.Drawing.Size(271, 351);
            this.Tab2P2.TabIndex = 1;
            this.Tab2P2.Text = "Tin nhắn của người lạ";
            this.Tab2P2.UseVisualStyleBackColor = true;
            // 
            // btn_get_list_not_friend
            // 
            this.btn_get_list_not_friend.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_get_list_not_friend.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_get_list_not_friend.Location = new System.Drawing.Point(6, 313);
            this.btn_get_list_not_friend.Name = "btn_get_list_not_friend";
            this.btn_get_list_not_friend.Size = new System.Drawing.Size(259, 30);
            this.btn_get_list_not_friend.TabIndex = 22;
            this.btn_get_list_not_friend.Text = "CẬP NHẬT DANH SÁCH NGƯỜI LẠ";
            this.btn_get_list_not_friend.UseVisualStyleBackColor = true;
            this.btn_get_list_not_friend.Click += new System.EventHandler(this.btn_get_list_not_friend_Click);
            // 
            // list_not_friends_send_msg
            // 
            this.list_not_friends_send_msg.AllowUserToAddRows = false;
            this.list_not_friends_send_msg.AllowUserToDeleteRows = false;
            this.list_not_friends_send_msg.AllowUserToOrderColumns = true;
            this.list_not_friends_send_msg.AllowUserToResizeRows = false;
            this.list_not_friends_send_msg.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.list_not_friends_send_msg.BackgroundColor = System.Drawing.Color.White;
            this.list_not_friends_send_msg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.list_not_friends_send_msg.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.list_not_friends_send_msg.Location = new System.Drawing.Point(6, 19);
            this.list_not_friends_send_msg.MultiSelect = false;
            this.list_not_friends_send_msg.Name = "list_not_friends_send_msg";
            this.list_not_friends_send_msg.ReadOnly = true;
            this.list_not_friends_send_msg.RowHeadersVisible = false;
            this.list_not_friends_send_msg.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.list_not_friends_send_msg.Size = new System.Drawing.Size(259, 288);
            this.list_not_friends_send_msg.TabIndex = 21;
            this.list_not_friends_send_msg.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.list_not_friends_send_msg_CellClick);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Blue;
            this.label10.Location = new System.Drawing.Point(6, 3);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(209, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "DANH SÁCH NGƯỜI LẠ GỬI TIN NHẮN";
            // 
            // check_choose_file_attachment_private
            // 
            this.check_choose_file_attachment_private.AutoSize = true;
            this.check_choose_file_attachment_private.BackColor = System.Drawing.Color.Transparent;
            this.check_choose_file_attachment_private.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check_choose_file_attachment_private.ForeColor = System.Drawing.Color.Purple;
            this.check_choose_file_attachment_private.Location = new System.Drawing.Point(671, 299);
            this.check_choose_file_attachment_private.Name = "check_choose_file_attachment_private";
            this.check_choose_file_attachment_private.Size = new System.Drawing.Size(162, 17);
            this.check_choose_file_attachment_private.TabIndex = 24;
            this.check_choose_file_attachment_private.Text = "CHỌN DỮ LIỆU ĐÍNH KÈM";
            this.check_choose_file_attachment_private.UseVisualStyleBackColor = false;
            this.check_choose_file_attachment_private.CheckedChanged += new System.EventHandler(this.check_choose_file_attachment_private_CheckedChanged);
            // 
            // btn_download_attachment_private
            // 
            this.btn_download_attachment_private.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_download_attachment_private.Enabled = false;
            this.btn_download_attachment_private.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_download_attachment_private.Location = new System.Drawing.Point(671, 356);
            this.btn_download_attachment_private.Name = "btn_download_attachment_private";
            this.btn_download_attachment_private.Size = new System.Drawing.Size(209, 30);
            this.btn_download_attachment_private.TabIndex = 23;
            this.btn_download_attachment_private.Text = "TẢI DỮ LIỆU ĐÍNH KÈM";
            this.btn_download_attachment_private.UseVisualStyleBackColor = true;
            this.btn_download_attachment_private.Click += new System.EventHandler(this.btn_download_attachment_private_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Blue;
            this.label7.Location = new System.Drawing.Point(291, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(126, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "DANH SÁCH TIN NHẮN";
            // 
            // txt_msg_private
            // 
            this.txt_msg_private.Location = new System.Drawing.Point(294, 299);
            this.txt_msg_private.Multiline = true;
            this.txt_msg_private.Name = "txt_msg_private";
            this.txt_msg_private.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_msg_private.Size = new System.Drawing.Size(371, 87);
            this.txt_msg_private.TabIndex = 21;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Blue;
            this.label8.Location = new System.Drawing.Point(291, 283);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(149, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "NHẬP NỘI DUNG TIN NHẮN";
            // 
            // btn_remove_msg_private
            // 
            this.btn_remove_msg_private.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_remove_msg_private.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_remove_msg_private.Location = new System.Drawing.Point(744, 322);
            this.btn_remove_msg_private.Name = "btn_remove_msg_private";
            this.btn_remove_msg_private.Size = new System.Drawing.Size(136, 28);
            this.btn_remove_msg_private.TabIndex = 18;
            this.btn_remove_msg_private.Text = "XÓA TIN ĐANG CHỌN";
            this.btn_remove_msg_private.UseVisualStyleBackColor = true;
            this.btn_remove_msg_private.Click += new System.EventHandler(this.btn_remove_msg_private_Click);
            // 
            // btn_send_msg_private
            // 
            this.btn_send_msg_private.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_send_msg_private.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_send_msg_private.Location = new System.Drawing.Point(671, 322);
            this.btn_send_msg_private.Name = "btn_send_msg_private";
            this.btn_send_msg_private.Size = new System.Drawing.Size(67, 28);
            this.btn_send_msg_private.TabIndex = 18;
            this.btn_send_msg_private.Text = "GỬI TIN";
            this.btn_send_msg_private.UseVisualStyleBackColor = true;
            this.btn_send_msg_private.Click += new System.EventHandler(this.btn_send_msg_private_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 3000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 3000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Frm_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::HUYNH_SKYPE_CLIENT.Properties.Resources.Background;
            this.ClientSize = new System.Drawing.Size(918, 531);
            this.Controls.Add(this.btn_change_info);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label_ho_ten);
            this.Controls.Add(this.avatar);
            this.Controls.Add(this.TabC1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "Frm_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HUỲNH SKYPE - CLIENT";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_Main_FormClosing);
            this.Load += new System.EventHandler(this.Frm_Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.avatar)).EndInit();
            this.TabC1.ResumeLayout(false);
            this.TabP1P1.ResumeLayout(false);
            this.TabP1P1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.list_msg_public)).EndInit();
            this.Tab1P2.ResumeLayout(false);
            this.Tab1P2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.list_msg_private)).EndInit();
            this.TabC2.ResumeLayout(false);
            this.Tab2P1.ResumeLayout(false);
            this.Tab2P1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.list_friends)).EndInit();
            this.Tab2P2.ResumeLayout(false);
            this.Tab2P2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.list_not_friends_send_msg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox avatar;
        private System.Windows.Forms.Label label_ho_ten;
        private System.Windows.Forms.Button btn_change_info;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl TabC1;
        private System.Windows.Forms.TabPage Tab1P2;
        private System.Windows.Forms.TabPage TabP1P1;
        private System.Windows.Forms.DataGridView list_msg_public;
        private System.Windows.Forms.TextBox txt_msg_public;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_send_msg_public;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_msg_private;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btn_send_msg_private;
        private System.Windows.Forms.Button btn_remove_msg_private;
        private System.Windows.Forms.Button btn_remove_msg_public;
        private System.Windows.Forms.Button btn_download_attachment_private;
        private System.Windows.Forms.CheckBox check_choose_file_attachment_private;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.TabControl TabC2;
        private System.Windows.Forms.TabPage Tab2P1;
        private System.Windows.Forms.TabPage Tab2P2;
        private System.Windows.Forms.DataGridView list_friends;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_acc_friend;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_remove_friend;
        private System.Windows.Forms.Button btn_add_friend;
        private System.Windows.Forms.Button btn_get_list_friend;
        private System.Windows.Forms.DataGridView list_not_friends_send_msg;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btn_get_list_not_friend;
        private System.Windows.Forms.DataGridView list_msg_private;
    }
}

