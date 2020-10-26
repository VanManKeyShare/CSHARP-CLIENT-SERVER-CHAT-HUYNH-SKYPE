using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HUYNH_SKYPE_CLIENT
{
    public partial class Frm_Main : Form
    {
        // KHAI BÁO CÁC BIẾN CẦN SỬ DỤNG

        public string IP_Server;
        public Int32 Port_Server;

        public string Account;
        public string Session_Key;
        public bool Status_Logged = false;

        public string Acc2 = "";
        private string File_Name_Attachment = "";
        private string Data_Attachment = "";

        private Dictionary<int, string> List_ID_Msg_Public = new Dictionary<int, string>();
        private Dictionary<int, string> List_ID_Msg_Private = new Dictionary<int, string>();
        private Dictionary<int, string> List_ID_Attachment_Msg_Private = new Dictionary<int, string>();

        // KHAI BÁO BIẾN SỬ DỤNG CLASS FUNCTION

        Functions Class_Func = new Functions();

        public Frm_Main()
        {
            InitializeComponent();
        }

        private void Frm_Main_Load(object sender, EventArgs e)
        {
            Get_Account_Info();

            Get_List_Msg_Public();

            // BẬT TIMER TỰ ĐỘNG LẤY TIN NHẮN CÔNG CỘNG

            timer1.Enabled = true;
        }

        // PHẦN LỆNH CHỨC NĂNG CỦA CONTROL LIST_FRIENDS

        private void list_friends_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // LẤY ACCOUNT BẠN BÈ CẦN CHAT

            if (e.RowIndex != -1)
            {
                list_msg_private.DataSource = null;
                txt_msg_private.Text = "";

                Acc2 = list_friends.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();
                if (Acc2 != "")
                {
                    Get_List_Msg_Private(Acc2);
                    timer2.Enabled = true;
                }
                else
                {
                    timer2.Enabled = false;
                }
            }
        }

        // PHẦN LỆNH CHỨC NĂNG CỦA CONTROL LIST_NOT_FRIENDS_SEND_MSG

        private void list_not_friends_send_msg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // LẤY ACCOUNT NGƯỜI LẠ CẦN CHAT

            if (e.RowIndex != -1)
            {
                list_msg_private.DataSource = null;
                txt_msg_private.Text = "";

                Acc2 = list_not_friends_send_msg.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();
                if (Acc2 != "")
                {
                    Get_List_Msg_Private(Acc2);
                    timer2.Enabled = true;
                }
                else
                {
                    timer2.Enabled = false;
                }
            }
        }

        // PHẦN LỆNH CHỨC NĂNG CỦA CONTROL LIST_MSG_PRIVATE

        private void list_msg_private_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string ID_Attachment = List_ID_Attachment_Msg_Private[e.RowIndex].ToString();
                if (ID_Attachment != "")
                {
                    btn_download_attachment_private.Enabled = true;
                }
                else
                {
                    btn_download_attachment_private.Enabled = false;
                }
            }
        }

        private void list_msg_private_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (list_msg_private.Rows.Count == 0)
            {
                btn_download_attachment_private.Enabled = false;
            }
        }

        // PHẦN LỆNH LẤY THÔNG TIN TÀI KHOẢN, MỞ FORM CẬP NHẬT THÔNG TIN TÀI KHOẢN

        private void Get_Account_Info()
        {
            // TẠO DỮ LIỆU GỬI ĐẾN SERVER

            string[] Header_Data = { "COMMAND", "OBJECT" };
            string[] Body_Data = { "ACCOUNT", "SESSION_KEY" };

            Body_Data[0] = Account;
            Body_Data[1] = Session_Key;

            Header_Data[0] = "get_account_info";
            Header_Data[1] = Class_Func.Serialize_To_String(Body_Data);

            // TIẾN HÀNH GỬI DỮ LIỆU VÀ LẤY KẾT QUẢ TRẢ VỀ TỪ SERVER

            Socket_Client SClient = new Socket_Client();
            string[] SClient_Return = SClient.Send_Data(IP_Server, Port_Server, Class_Func.Serialize_To_String(Header_Data));
            if (SClient_Return[0] == "ERROR") { MessageBox.Show(SClient_Return[1], "THÔNG BÁO"); return; }

            // XỬ LÝ DỮ LIỆU NHẬN ĐƯỢC TỪ SERVER

            string[] Data_Return_From_Server = (string[])Class_Func.Deserialize_From_String(SClient_Return[1]);

            // NẾU NHẬN ĐƯỢC THÔNG BÁO LỖI VÀ LỖI ĐÓ LÀ SESSION_TIME_OUT
            // THÌ THÔNG BÁO LỖI VÀ THOÁT CHƯƠNG TRÌNH VỀ FORM LOGIN

            if (Data_Return_From_Server[0] == "ERROR" && Data_Return_From_Server[1] == "SESSION_TIME_OUT")
            {
                MessageBox.Show("PHIÊN LÀM VIỆC ĐÃ HẾT HẠN. XIN MỜI BẠN ĐĂNG NHẬP LẠI", "THÔNG BÁO");
                this.Close();
                return;
            }

            // NẾU THÔNG BÁO LỖI KHÁC THÌ CHỈ HIỂN THỊ CHO NGƯỜI DÙNG BIẾT

            if (Data_Return_From_Server[0] == "ERROR") { MessageBox.Show(Data_Return_From_Server[1], "THÔNG BÁO"); return; }

            // NẾU KẾT QUẢ NHẬN ĐƯỢC KHÔNG BỊ LỖI
            // THÌ CẬP NHẬT CÁC THÔNG TIN CỦA ACCOUNT TRÊN FORM

            if (Data_Return_From_Server[0] == "OK")
            {
                DataTable[] Account_Info = (DataTable[])Class_Func.Deserialize_From_String(Data_Return_From_Server[1]);

                label_ho_ten.Text = Account_Info[0].Rows[0][0].ToString().Trim();
                string avatar_base64 = Account_Info[0].Rows[0][1].ToString().Trim();

                if (Class_Func.Base64_To_Image(avatar_base64) != null)
                {
                    avatar.Image = Class_Func.Base64_To_Image(avatar_base64);
                }
            }
        }

        private void btn_change_info_Click(object sender, EventArgs e)
        {
            // TRUYỀN THÔNG TIN VÀ MỞ FORM CẬP NHẬT THÔNG TIN ACCOUNT

            Frm_Update_Account_Info FUpdate = new Frm_Update_Account_Info();
            FUpdate.FMain = this;
            FUpdate.IP_Server = IP_Server;
            FUpdate.Port_Server = Port_Server;
            FUpdate.Account = Account;
            FUpdate.Session_Key = Session_Key;
            FUpdate.ShowDialog();
        }

        // PHẦN LỆNH LẤY DANH SÁCH BẠN BÈ, THÊM VÀ XÓA BẠN BÈ

        private void btn_get_list_friend_Click(object sender, EventArgs e)
        {
            Get_List_Friend();
        }

        private void Get_List_Friend()
        {
            // TẠO DỮ LIỆU GỬI ĐẾN SERVER

            string[] Header_Data = { "COMMAND", "OBJECT" };
            string[] Body_Data = { "ACCOUNT", "SESSION_KEY" };

            Body_Data[0] = Account;
            Body_Data[1] = Session_Key;

            Header_Data[0] = "get_list_friend";
            Header_Data[1] = Class_Func.Serialize_To_String(Body_Data);

            // TIẾN HÀNH GỬI DỮ LIỆU VÀ LẤY KẾT QUẢ TRẢ VỀ TỪ SERVER

            Socket_Client SClient = new Socket_Client();
            string[] SClient_Return = SClient.Send_Data(IP_Server, Port_Server, Class_Func.Serialize_To_String(Header_Data));
            if (SClient_Return[0] == "ERROR") { MessageBox.Show(SClient_Return[1], "THÔNG BÁO"); return; }

            // XỬ LÝ DỮ LIỆU NHẬN ĐƯỢC TỪ SERVER

            string[] Data_Return_From_Server = (string[])Class_Func.Deserialize_From_String(SClient_Return[1]);

            // NẾU NHẬN ĐƯỢC THÔNG BÁO LỖI VÀ LỖI ĐÓ LÀ SESSION_TIME_OUT
            // THÌ THÔNG BÁO LỖI VÀ THOÁT CHƯƠNG TRÌNH VỀ FORM LOGIN

            if (Data_Return_From_Server[0] == "ERROR" && Data_Return_From_Server[1] == "SESSION_TIME_OUT")
            {
                MessageBox.Show("PHIÊN LÀM VIỆC ĐÃ HẾT HẠN. XIN MỜI BẠN ĐĂNG NHẬP LẠI", "THÔNG BÁO");
                this.Close();
                return;
            }

            // NẾU THÔNG BÁO LỖI KHÁC THÌ CHỈ HIỂN THỊ CHO NGƯỜI DÙNG BIẾT

            if (Data_Return_From_Server[0] == "ERROR") { MessageBox.Show(Data_Return_From_Server[1], "THÔNG BÁO"); return; }

            // NẾU KẾT QUẢ NHẬN ĐƯỢC KHÔNG BỊ LỖI
            // THÌ CẬP NHẬT LẠI DANH SÁCH BẠN BÈ TỪ SERVER

            if (Data_Return_From_Server[0] == "OK")
            {
                DataTable[] DT_List_Friends = (DataTable[])Class_Func.Deserialize_From_String(Data_Return_From_Server[1]);
                list_friends.DataSource = DT_List_Friends[0];
                if (list_friends.Rows.Count != 0)
                {
                    Acc2 = list_friends.Rows[0].Cells[0].Value.ToString().Trim();
                    list_msg_private.DataSource = null;
                    txt_msg_private.Text = "";

                    if (Acc2 != "")
                    {
                        timer2.Enabled = true;
                        Get_List_Msg_Private(Acc2);
                    }
                    else
                    {
                        timer2.Enabled = false;
                    }
                }
            }
        }

        private void btn_add_friend_Click(object sender, EventArgs e)
        {
            // LẤY VÀ KIỂM TRA THÔNG TIN NGƯỜI DÙNG NHẬP

            string acc2 = txt_acc_friend.Text.Trim();
            if (acc2 == "")
            {
                MessageBox.Show("BẠN CHƯA NHẬP ACCOUNT BẠN BÈ CẦN THÊM", "THÔNG BÁO");
                return;
            }

            // TẠO DỮ LIỆU GỬI ĐẾN SERVER

            string[] Header_Data = { "COMMAND", "OBJECT" };
            string[] Body_Data = { "ACCOUNT", "SESSION_KEY", "ACC2" };

            Body_Data[0] = Account;
            Body_Data[1] = Session_Key;
            Body_Data[2] = acc2;

            Header_Data[0] = "add_friend";
            Header_Data[1] = Class_Func.Serialize_To_String(Body_Data);

            // TIẾN HÀNH GỬI DỮ LIỆU VÀ XỬ LÝ DỮ LIỆU SERVER TRẢ VỀ

            Socket_Client SClient = new Socket_Client();
            string[] SClient_Return = SClient.Send_Data(IP_Server, Port_Server, Class_Func.Serialize_To_String(Header_Data));
            if (SClient_Return[0] == "ERROR") { MessageBox.Show(SClient_Return[1], "THÔNG BÁO"); return; }

            // XỬ LÝ DỮ LIỆU NHẬN ĐƯỢC TỪ SERVER

            string[] Data_Return_From_Server = (string[])Class_Func.Deserialize_From_String(SClient_Return[1]);

            // NẾU NHẬN ĐƯỢC THÔNG BÁO LỖI VÀ LỖI ĐÓ LÀ SESSION_TIME_OUT
            // THÌ THÔNG BÁO LỖI VÀ THOÁT CHƯƠNG TRÌNH VỀ FORM LOGIN

            if (Data_Return_From_Server[0] == "ERROR" && Data_Return_From_Server[1] == "SESSION_TIME_OUT")
            {
                MessageBox.Show("PHIÊN LÀM VIỆC ĐÃ HẾT HẠN. XIN MỜI BẠN ĐĂNG NHẬP LẠI", "THÔNG BÁO");
                this.Close();
                return;
            }

            // NẾU THÔNG BÁO LỖI KHÁC THÌ CHỈ HIỂN THỊ CHO NGƯỜI DÙNG BIẾT

            if (Data_Return_From_Server[0] == "ERROR") { MessageBox.Show(Data_Return_From_Server[1], "THÔNG BÁO"); return; }

            // NẾU KẾT QUẢ NHẬN ĐƯỢC KHÔNG BỊ LỖI
            // THÌ THÔNG BÁO THÀNH CÔNG VÀ CẬP NHẬT LẠI DANH SÁCH BẠN BÈ TỪ SERVER

            if (Data_Return_From_Server[0] == "OK")
            {
                txt_acc_friend.Text = "";
                MessageBox.Show("THÊM BẠN THÀNH CÔNG", "THÔNG BÁO");
                Get_List_Friend();
            }
        }

        private void btn_remove_friend_Click(object sender, EventArgs e)
        {
            // LẤY ACCOUNT ĐANG CHỌN TRONG DANH SÁCH BẠN BÈ

            string acc2 = "";

            if (list_friends.SelectedRows.Count != 0)
            {
                acc2 = list_friends.SelectedRows[0].Cells[0].Value.ToString();
            }

            if (acc2 == "")
            {
                MessageBox.Show("BẠN CHƯA CHỌN ACCOUNT CẦN XÓA RA KHỎI DANH SÁCH BẠN BÈ", "THÔNG BÁO");
                return;
            }

            // TẠO DỮ LIỆU GỬI ĐẾN SERVER

            string[] Header_Data = { "COMMAND", "OBJECT" };
            string[] Body_Data = { "ACCOUNT", "SESSION_KEY", "ACC2" };

            Body_Data[0] = Account;
            Body_Data[1] = Session_Key;
            Body_Data[2] = acc2;

            Header_Data[0] = "remove_friend";
            Header_Data[1] = Class_Func.Serialize_To_String(Body_Data);

            // TIẾN HÀNH GỬI DỮ LIỆU VÀ XỬ LÝ DỮ LIỆU SERVER TRẢ VỀ

            Socket_Client SClient = new Socket_Client();
            string[] SClient_Return = SClient.Send_Data(IP_Server, Port_Server, Class_Func.Serialize_To_String(Header_Data));
            if (SClient_Return[0] == "ERROR") { MessageBox.Show(SClient_Return[1], "THÔNG BÁO"); return; }

            // XỬ LÝ DỮ LIỆU NHẬN ĐƯỢC TỪ SERVER

            string[] Data_Return_From_Server = (string[])Class_Func.Deserialize_From_String(SClient_Return[1]);

            // NẾU NHẬN ĐƯỢC THÔNG BÁO LỖI VÀ LỖI ĐÓ LÀ SESSION_TIME_OUT
            // THÌ THÔNG BÁO LỖI VÀ THOÁT CHƯƠNG TRÌNH VỀ FORM LOGIN

            if (Data_Return_From_Server[0] == "ERROR" && Data_Return_From_Server[1] == "SESSION_TIME_OUT")
            {
                MessageBox.Show("PHIÊN LÀM VIỆC ĐÃ HẾT HẠN. XIN MỜI BẠN ĐĂNG NHẬP LẠI", "THÔNG BÁO");
                this.Close();
                return;
            }

            // NẾU THÔNG BÁO LỖI KHÁC THÌ CHỈ HIỂN THỊ CHO NGƯỜI DÙNG BIẾT

            if (Data_Return_From_Server[0] == "ERROR") { MessageBox.Show(Data_Return_From_Server[1], "THÔNG BÁO"); return; }

            // NẾU KẾT QUẢ NHẬN ĐƯỢC KHÔNG BỊ LỖI
            // THÌ THÔNG BÁO THÀNH CÔNG VÀ CẬP NHẬT LẠI DANH SÁCH BẠN BÈ TỪ SERVER

            if (Data_Return_From_Server[0] == "OK")
            {
                MessageBox.Show("XÓA BẠN THÀNH CÔNG", "THÔNG BÁO");
                Get_List_Friend();
            }
        }

        // PHẦN LỆNH LẤY TIN NHẮN CÁ NHÂN, GỬI VÀ XÓA TIN NHẮN CÁ NHÂN

        private void Get_List_Msg_Private(string Acc2_Local)
        {
            // TẠO DỮ LIỆU GỬI ĐẾN SERVER

            string[] Header_Data = { "COMMAND", "OBJECT" };
            string[] Body_Data = { "ACCOUNT", "SESSION_KEY", "ACC2" };

            Body_Data[0] = Account;
            Body_Data[1] = Session_Key;
            Body_Data[2] = Acc2_Local;

            Header_Data[0] = "get_msg";
            Header_Data[1] = Class_Func.Serialize_To_String(Body_Data);

            // TIẾN HÀNH GỬI DỮ LIỆU VÀ LẤY KẾT QUẢ TRẢ VỀ TỪ SERVER

            Socket_Client SClient = new Socket_Client();
            string[] SClient_Return = SClient.Send_Data(IP_Server, Port_Server, Class_Func.Serialize_To_String(Header_Data));
            if (SClient_Return[0] == "ERROR")
            {
                timer2.Enabled = false;
                MessageBox.Show(SClient_Return[1], "THÔNG BÁO");
                this.Close();
                return;
            }

            // XỬ LÝ DỮ LIỆU NHẬN ĐƯỢC TỪ SERVER

            string[] Data_Return_From_Server = (string[])Class_Func.Deserialize_From_String(SClient_Return[1]);

            // NẾU NHẬN ĐƯỢC THÔNG BÁO LỖI VÀ LỖI ĐÓ LÀ SESSION_TIME_OUT
            // THÌ THÔNG BÁO LỖI VÀ THOÁT CHƯƠNG TRÌNH VỀ FORM LOGIN

            if (Data_Return_From_Server[0] == "ERROR" && Data_Return_From_Server[1] == "SESSION_TIME_OUT")
            {
                timer2.Enabled = false;
                MessageBox.Show("PHIÊN LÀM VIỆC ĐÃ HẾT HẠN. XIN MỜI BẠN ĐĂNG NHẬP LẠI", "THÔNG BÁO");
                this.Close();
                return;
            }

            // NẾU THÔNG BÁO LỖI KHÁC THÌ CHỈ HIỂN THỊ CHO NGƯỜI DÙNG BIẾT

            if (Data_Return_From_Server[0] == "ERROR")
            {
                timer2.Enabled = false;
                MessageBox.Show(Data_Return_From_Server[1], "THÔNG BÁO");
                this.Close();
                return;
            }

            // NẾU KẾT QUẢ NHẬN ĐƯỢC KHÔNG BỊ LỖI
            // THÌ CẬP NHẬT LẠI DANH SÁCH BẠN BÈ TỪ SERVER

            if (Data_Return_From_Server[0] == "OK")
            {
                DataTable[] DT_List_Msg_Private = (DataTable[])Class_Func.Deserialize_From_String(Data_Return_From_Server[1]);
                if (DT_List_Msg_Private[0].Rows.Count != list_msg_private.Rows.Count)
                {
                    int i = 0;

                    List_ID_Msg_Private.Clear();
                    List_ID_Attachment_Msg_Private.Clear();
                    list_msg_private.DataSource = null;

                    DataTable DT_List_Msg_Private_For_ListBox_Style = new DataTable();
                    DT_List_Msg_Private_For_ListBox_Style.Columns.Add("DATA");

                    foreach (DataRow dr in DT_List_Msg_Private[0].Rows)
                    {
                        DataRow dr_new = DT_List_Msg_Private_For_ListBox_Style.NewRow();

                        dr_new["DATA"] = "Người gửi: " + dr[2].ToString() + "\r\n" + "Nội dung: " + dr[1].ToString() + "\r\n" + "Thời gian gửi: " + dr[4].ToString();
                        
                        DT_List_Msg_Private_For_ListBox_Style.Rows.Add(dr_new);

                        List_ID_Msg_Private.Add(i, dr[0].ToString());
                        List_ID_Attachment_Msg_Private.Add(i, dr[5].ToString());

                        i = i + 1;
                    }

                    list_msg_private.DataSource = DT_List_Msg_Private_For_ListBox_Style;
                    list_msg_private.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                    list_msg_private.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                    list_msg_private.CellBorderStyle = DataGridViewCellBorderStyle.None;
                }
            }
        }

        private void btn_send_msg_private_Click(object sender, EventArgs e)
        {
            // LẤY VÀ KIỂM TRA THÔNG TIN NGƯỜI DÙNG NHẬP

            if (Acc2 == "")
            {
                MessageBox.Show("BẠN CHƯA CHỌN BẠN BÈ CẦN GỬI TIN NHẮN", "THÔNG BÁO");
                return;
            }

            string Msg = txt_msg_private.Text.Trim();

            if (Msg == "")
            {
                MessageBox.Show("BẠN CHƯA NHẬP TIN NHẮN", "THÔNG BÁO");
                return;
            }

            // TẠO DỮ LIỆU GỬI ĐẾN SERVER

            string[] Header_Data = { "COMMAND", "OBJECT" };
            string[] Body_Data = { "ACCOUNT", "SESSION_KEY", "ACC2", "MSG", "FILE_NAME_ATTACHMENT", "DATA_ATTACHMENT" };

            Body_Data[0] = Account;
            Body_Data[1] = Session_Key;
            Body_Data[2] = Acc2;
            Body_Data[3] = Msg;
            Body_Data[4] = File_Name_Attachment;
            Body_Data[5] = Data_Attachment;

            Header_Data[0] = "send_msg";
            Header_Data[1] = Class_Func.Serialize_To_String(Body_Data);

            // TIẾN HÀNH GỬI DỮ LIỆU VÀ LẤY KẾT QUẢ TRẢ VỀ TỪ SERVER

            Socket_Client SClient = new Socket_Client();
            string[] SClient_Return = SClient.Send_Data(IP_Server, Port_Server, Class_Func.Serialize_To_String(Header_Data));
            if (SClient_Return[0] == "ERROR") { MessageBox.Show(SClient_Return[1], "THÔNG BÁO"); return; }

            // XỬ LÝ DỮ LIỆU NHẬN ĐƯỢC TỪ SERVER

            string[] Data_Return_From_Server = (string[])Class_Func.Deserialize_From_String(SClient_Return[1]);

            // NẾU NHẬN ĐƯỢC THÔNG BÁO LỖI VÀ LỖI ĐÓ LÀ SESSION_TIME_OUT
            // THÌ THÔNG BÁO LỖI VÀ THOÁT CHƯƠNG TRÌNH VỀ FORM LOGIN

            if (Data_Return_From_Server[0] == "ERROR" && Data_Return_From_Server[1] == "SESSION_TIME_OUT")
            {
                MessageBox.Show("PHIÊN LÀM VIỆC ĐÃ HẾT HẠN. XIN MỜI BẠN ĐĂNG NHẬP LẠI", "THÔNG BÁO");
                this.Close();
                return;
            }

            // NẾU THÔNG BÁO LỖI KHÁC THÌ CHỈ HIỂN THỊ CHO NGƯỜI DÙNG BIẾT

            if (Data_Return_From_Server[0] == "ERROR") { MessageBox.Show(Data_Return_From_Server[1], "THÔNG BÁO"); return; }

            // NẾU KẾT QUẢ NHẬN ĐƯỢC KHÔNG BỊ LỖI
            // THÌ TIẾN HÀNH CHẠY HÀM LẤY DANH SÁCH TIN NHẮN MỚI

            if (Data_Return_From_Server[0] == "OK")
            {
                txt_msg_private.Text = "";
                check_choose_file_attachment_private.Checked = false;
                Get_List_Msg_Private(Acc2);
            }
        }

        private void btn_remove_msg_private_Click(object sender, EventArgs e)
        {
            // LẤY ID TIN NHẮN ĐANG ĐƯỢC TRONG TRONG DANH SÁCH TIN NHẮN

            string ID_Msg = "";

            foreach (DataGridViewRow Dgvr in list_msg_private.SelectedRows)
            {
                ID_Msg = List_ID_Msg_Private[Dgvr.Index].ToString();
                if (ID_Msg != "")
                {
                    break;
                }
            }

            if (ID_Msg == "")
            {
                MessageBox.Show("BẠN CHƯA CHỌN TIN NHẮN CẦN XÓA", "THÔNG BÁO");
                return;
            }

            // TẠO DỮ LIỆU GỬI ĐẾN SERVER

            string[] Header_Data = { "COMMAND", "OBJECT" };
            string[] Body_Data = { "ACCOUNT", "SESSION_KEY", "ID_MSG" };

            Body_Data[0] = Account;
            Body_Data[1] = Session_Key;
            Body_Data[2] = ID_Msg;

            Header_Data[0] = "delete_msg";
            Header_Data[1] = Class_Func.Serialize_To_String(Body_Data);

            // TIẾN HÀNH GỬI DỮ LIỆU VÀ LẤY KẾT QUẢ TRẢ VỀ TỪ SERVER

            Socket_Client SClient = new Socket_Client();
            string[] SClient_Return = SClient.Send_Data(IP_Server, Port_Server, Class_Func.Serialize_To_String(Header_Data));
            if (SClient_Return[0] == "ERROR") { MessageBox.Show(SClient_Return[1], "THÔNG BÁO"); return; }

            // XỬ LÝ DỮ LIỆU NHẬN ĐƯỢC TỪ SERVER

            string[] Data_Return_From_Server = (string[])Class_Func.Deserialize_From_String(SClient_Return[1]);

            // NẾU NHẬN ĐƯỢC THÔNG BÁO LỖI VÀ LỖI ĐÓ LÀ SESSION_TIME_OUT
            // THÌ THÔNG BÁO LỖI VÀ THOÁT CHƯƠNG TRÌNH VỀ FORM LOGIN

            if (Data_Return_From_Server[0] == "ERROR" && Data_Return_From_Server[1] == "SESSION_TIME_OUT")
            {
                MessageBox.Show("PHIÊN LÀM VIỆC ĐÃ HẾT HẠN. XIN MỜI BẠN ĐĂNG NHẬP LẠI", "THÔNG BÁO");
                this.Close();
                return;
            }

            // NẾU THÔNG BÁO LỖI KHÁC THÌ CHỈ HIỂN THỊ CHO NGƯỜI DÙNG BIẾT

            if (Data_Return_From_Server[0] == "ERROR") { MessageBox.Show(Data_Return_From_Server[1], "THÔNG BÁO"); return; }

            // NẾU KẾT QUẢ NHẬN ĐƯỢC KHÔNG BỊ LỖI
            // THÌ TIẾN HÀNH CHẠY HÀM LẤY DANH SÁCH TIN NHẮN MỚI

            if (Data_Return_From_Server[0] == "OK")
            {
                Get_List_Msg_Private(Acc2);
            }
        }

        // PHẦN LỆNH ĐÍNH KÈM VÀ TẢI ĐÍNH KÈM

        private void check_choose_file_attachment_private_CheckedChanged(object sender, EventArgs e)
        {
            if (check_choose_file_attachment_private.Checked == true)
            {
                // MỞ CỬA SỔ CHỌN FILE

                openFileDialog1.Filter = "ALL FILES (*.*)|*.*";
                openFileDialog1.InitialDirectory = "C:\\";
                openFileDialog1.FileName = "";
                openFileDialog1.ShowDialog();

                // LẤY ĐƯỜNG DẪN FILE ĐƯỢC CHỌN

                string File_Name = openFileDialog1.FileName.Trim();

                // KIỂM TRA SỰ TỒN TẠI CỦA FILE ĐƯỢC CHỌN

                if (File_Name == "" || !System.IO.File.Exists(File_Name))
                {
                    check_choose_file_attachment_private.Checked = false;
                    return;
                }

                // KIỂM TRA SIZE CỦA FILE

                System.IO.FileInfo F = new System.IO.FileInfo(File_Name);
                if (F.Length > 5242880)
                {
                    check_choose_file_attachment_private.Checked = false;
                    MessageBox.Show("GIỚI HẠN KÍCH THƯỚC FILE ĐÍNH KÈM LÀ 5 MB", "THÔNG BÁO");
                    return;
                }

                // TIẾN HÀNH MỞ VÀ CHUYỂN ĐỔI FILE SANG BASE64

                btn_send_msg_private.Enabled = false;

                File_Name_Attachment = Class_Func.Get_Name_Of_File_Name(File_Name);

                byte[] Bytes = Class_Func.Read_All_Bytes(File_Name);
                Data_Attachment = Convert.ToBase64String(Bytes);

                btn_send_msg_private.Enabled = true;

                MessageBox.Show("ĐÍNH KÈM FILE THÀNH CÔNG", "THÔNG BÁO");
            }
            else
            {
                File_Name_Attachment = "";
                Data_Attachment = "";
            }
        }

        private void btn_download_attachment_private_Click(object sender, EventArgs e)
        {
            // LẤY ID TIN NHẮN ĐANG ĐƯỢC TRONG TRONG DANH SÁCH TIN NHẮN

            string ID_Attachment = "";

            foreach (DataGridViewRow Dgvr in list_msg_private.SelectedRows)
            {
                ID_Attachment = List_ID_Attachment_Msg_Private[Dgvr.Index].ToString();
                if (ID_Attachment != "")
                {
                    break;
                }
            }

            if (ID_Attachment == "")
            {
                MessageBox.Show("BẠN CHƯA CHỌN TIN NHẮN CÓ DỮ LIỆU ĐÍNH KÈM", "THÔNG BÁO");
                return;
            }

            // TẠO DỮ LIỆU GỬI ĐẾN SERVER

            string[] Header_Data = { "COMMAND", "OBJECT" };
            string[] Body_Data = { "ACCOUNT", "SESSION_KEY", "ACC2", "ID_ATTACHMENT" };

            Body_Data[0] = Account;
            Body_Data[1] = Session_Key;
            Body_Data[2] = Acc2;
            Body_Data[3] = ID_Attachment;

            Header_Data[0] = "download_attachment";
            Header_Data[1] = Class_Func.Serialize_To_String(Body_Data);

            // TIẾN HÀNH GỬI DỮ LIỆU VÀ LẤY KẾT QUẢ TRẢ VỀ TỪ SERVER

            Socket_Client SClient = new Socket_Client();
            string[] SClient_Return = SClient.Send_Data(IP_Server, Port_Server, Class_Func.Serialize_To_String(Header_Data));
            if (SClient_Return[0] == "ERROR")
            {
                MessageBox.Show(SClient_Return[1], "THÔNG BÁO");
                return;
            }

            // XỬ LÝ DỮ LIỆU NHẬN ĐƯỢC TỪ SERVER

            string[] Data_Return_From_Server = (string[])Class_Func.Deserialize_From_String(SClient_Return[1]);

            // NẾU NHẬN ĐƯỢC THÔNG BÁO LỖI VÀ LỖI ĐÓ LÀ SESSION_TIME_OUT
            // THÌ THÔNG BÁO LỖI VÀ THOÁT CHƯƠNG TRÌNH VỀ FORM LOGIN

            if (Data_Return_From_Server[0] == "ERROR" && Data_Return_From_Server[1] == "SESSION_TIME_OUT")
            {
                MessageBox.Show("PHIÊN LÀM VIỆC ĐÃ HẾT HẠN. XIN MỜI BẠN ĐĂNG NHẬP LẠI", "THÔNG BÁO");
                this.Close();
                return;
            }

            // NẾU THÔNG BÁO LỖI KHÁC THÌ CHỈ HIỂN THỊ CHO NGƯỜI DÙNG BIẾT

            if (Data_Return_From_Server[0] == "ERROR")
            {
                MessageBox.Show(Data_Return_From_Server[1], "THÔNG BÁO");
                return;
            }

            // NẾU KẾT QUẢ NHẬN ĐƯỢC KHÔNG BỊ LỖI
            // THÌ XỬ LÝ DỮ LIỆU NHẬN ĐƯỢC TỪ SERVER ĐỂ LẤY FILE NAME VÀ FILE CONTENT

            if (Data_Return_From_Server[0] == "OK")
            {
                string File_Name, File_Content;
                Byte[] Bytes = null;

                DataTable[] DT_Data_Attachment = (DataTable[])Class_Func.Deserialize_From_String(Data_Return_From_Server[1]);
                if (DT_Data_Attachment[0].Rows.Count != 0)
                {
                    // LẤY FILE NAME, FILE CONTENT

                    File_Name = DT_Data_Attachment[0].Rows[0][0].ToString().Trim();
                    File_Content = DT_Data_Attachment[0].Rows[0][1].ToString().Trim();
                    try
                    {
                        Bytes = Convert.FromBase64String(File_Content);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("DỮ LIỆU ĐÍNH KÈM BỊ LỖI. KHÔNG THỂ TẢI VỀ", "THÔNG BÁO");
                        Console.WriteLine(ex.ToString());
                    }

                    // MỞ HỘP THOẠI LƯU FILE

                    if (File_Name == "")
                    {
                        Random r = new Random();
                        File_Name = r.Next(111111111, 999999999).ToString();
                    }

                    saveFileDialog1.Filter = "ALL FILES (*.*)|*.*";
                    saveFileDialog1.FileName = File_Name;
                    saveFileDialog1.InitialDirectory = "C:\\";
  
                    // TIẾN HÀNH LƯU DỮ LIỆU

                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        File_Name = saveFileDialog1.FileName.Trim();
                        if (File_Name != "")
                        {
                            System.IO.File.WriteAllBytes(File_Name, Bytes);
                            MessageBox.Show("LƯU FILE ĐÍNH KÈM THÀNH CÔNG", "THÔNG BÁO");
                        }
                    }
                }
            }
        }

        // PHẦN LỆNH LẤY DANH SÁCH NGƯỜI LẠ GỬI TIN NHẮN

        private void btn_get_list_not_friend_Click(object sender, EventArgs e)
        {
            // TẠO DỮ LIỆU GỬI ĐẾN SERVER

            string[] Header_Data = { "COMMAND", "OBJECT" };
            string[] Body_Data = { "ACCOUNT", "SESSION_KEY" };

            Body_Data[0] = Account;
            Body_Data[1] = Session_Key;

            Header_Data[0] = "get_list_not_friend";
            Header_Data[1] = Class_Func.Serialize_To_String(Body_Data);

            // TIẾN HÀNH GỬI DỮ LIỆU VÀ LẤY KẾT QUẢ TRẢ VỀ TỪ SERVER

            Socket_Client SClient = new Socket_Client();
            string[] SClient_Return = SClient.Send_Data(IP_Server, Port_Server, Class_Func.Serialize_To_String(Header_Data));
            if (SClient_Return[0] == "ERROR") { MessageBox.Show(SClient_Return[1], "THÔNG BÁO"); return; }

            // XỬ LÝ DỮ LIỆU NHẬN ĐƯỢC TỪ SERVER

            string[] Data_Return_From_Server = (string[])Class_Func.Deserialize_From_String(SClient_Return[1]);

            // NẾU NHẬN ĐƯỢC THÔNG BÁO LỖI VÀ LỖI ĐÓ LÀ SESSION_TIME_OUT
            // THÌ THÔNG BÁO LỖI VÀ THOÁT CHƯƠNG TRÌNH VỀ FORM LOGIN

            if (Data_Return_From_Server[0] == "ERROR" && Data_Return_From_Server[1] == "SESSION_TIME_OUT")
            {
                MessageBox.Show("PHIÊN LÀM VIỆC ĐÃ HẾT HẠN. XIN MỜI BẠN ĐĂNG NHẬP LẠI", "THÔNG BÁO");
                this.Close();
                return;
            }

            // NẾU THÔNG BÁO LỖI KHÁC THÌ CHỈ HIỂN THỊ CHO NGƯỜI DÙNG BIẾT

            if (Data_Return_From_Server[0] == "ERROR") { MessageBox.Show(Data_Return_From_Server[1], "THÔNG BÁO"); return; }

            // NẾU KẾT QUẢ NHẬN ĐƯỢC KHÔNG BỊ LỖI
            // THÌ CẬP NHẬT LẠI DANH SÁCH BẠN BÈ TỪ SERVER

            if (Data_Return_From_Server[0] == "OK")
            {
                DataTable[] DT_List_Not_Friends_Send_Msg = (DataTable[])Class_Func.Deserialize_From_String(Data_Return_From_Server[1]);
                list_not_friends_send_msg.DataSource = DT_List_Not_Friends_Send_Msg[0];

                if (list_not_friends_send_msg.Rows.Count != 0)
                {
                    list_msg_private.DataSource = null;
                    txt_msg_private.Text = "";

                    Acc2 = list_not_friends_send_msg.Rows[0].Cells[0].Value.ToString().Trim();
                    if (Acc2 != "")
                    {
                        timer2.Enabled = true;
                        Get_List_Msg_Private(Acc2);
                    }
                    else
                    {
                        timer2.Enabled = false;
                    }
                }
            }
        }

        // PHẦN LỆNH LẤY TIN NHẮN CÔNG CỘNG, GỬI VÀ XÓA TIN NHẮN CÔNG CỘNG

        private void Get_List_Msg_Public()
        {
            // TẠO DỮ LIỆU GỬI ĐẾN SERVER

            string[] Header_Data = { "COMMAND", "OBJECT" };
            string[] Body_Data = { "ACCOUNT", "SESSION_KEY" };

            Body_Data[0] = Account;
            Body_Data[1] = Session_Key;

            Header_Data[0] = "get_msg_public";
            Header_Data[1] = Class_Func.Serialize_To_String(Body_Data);

            // TIẾN HÀNH GỬI DỮ LIỆU VÀ LẤY KẾT QUẢ TRẢ VỀ TỪ SERVER

            Socket_Client SClient = new Socket_Client();
            string[] SClient_Return = SClient.Send_Data(IP_Server, Port_Server, Class_Func.Serialize_To_String(Header_Data));
            if (SClient_Return[0] == "ERROR")
            {
                timer1.Enabled = false;
                MessageBox.Show(SClient_Return[1], "THÔNG BÁO");
                this.Close();
                return;
            }

            // XỬ LÝ DỮ LIỆU NHẬN ĐƯỢC TỪ SERVER

            string[] Data_Return_From_Server = (string[])Class_Func.Deserialize_From_String(SClient_Return[1]);

            // NẾU NHẬN ĐƯỢC THÔNG BÁO LỖI VÀ LỖI ĐÓ LÀ SESSION_TIME_OUT
            // THÌ THÔNG BÁO LỖI VÀ THOÁT CHƯƠNG TRÌNH VỀ FORM LOGIN

            if (Data_Return_From_Server[0] == "ERROR" && Data_Return_From_Server[1] == "SESSION_TIME_OUT")
            {
                timer1.Enabled = false;
                MessageBox.Show("PHIÊN LÀM VIỆC ĐÃ HẾT HẠN. XIN MỜI BẠN ĐĂNG NHẬP LẠI", "THÔNG BÁO");
                this.Close();
                return;
            }

            // NẾU THÔNG BÁO LỖI KHÁC THÌ CHỈ HIỂN THỊ CHO NGƯỜI DÙNG BIẾT

            if (Data_Return_From_Server[0] == "ERROR")
            {
                timer1.Enabled = false;
                MessageBox.Show(Data_Return_From_Server[1], "THÔNG BÁO");
                this.Close();
                return;
            }

            // NẾU KẾT QUẢ NHẬN ĐƯỢC KHÔNG BỊ LỖI
            // THÌ CẬP NHẬT LẠI DANH SÁCH BẠN BÈ TỪ SERVER

            if (Data_Return_From_Server[0] == "OK")
            {
                DataTable[] DT_List_Msg_Public = (DataTable[])Class_Func.Deserialize_From_String(Data_Return_From_Server[1]);
                if (DT_List_Msg_Public[0].Rows.Count != list_msg_public.Rows.Count)
                {
                    int i = 0;

                    List_ID_Msg_Public.Clear();
                    list_msg_public.DataSource = null;

                    DataTable DT_List_Msg_Public_For_ListBox_Style = new DataTable();
                    DT_List_Msg_Public_For_ListBox_Style.Columns.Add("DATA");

                    foreach (DataRow dr in DT_List_Msg_Public[0].Rows)
                    {
                        DataRow dr_new = DT_List_Msg_Public_For_ListBox_Style.NewRow();
                        
                        dr_new["DATA"] = "Người gửi: " + dr[2].ToString() + "\r\n" + "Nội dung: " + dr[1].ToString() + "\r\n" + "Thời gian gửi: " + dr[3].ToString();

                        DT_List_Msg_Public_For_ListBox_Style.Rows.Add(dr_new);

                        List_ID_Msg_Public.Add(i, dr[0].ToString());

                        i = i + 1;
                    }

                    list_msg_public.DataSource = DT_List_Msg_Public_For_ListBox_Style;
                    list_msg_public.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                    list_msg_public.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                    list_msg_public.CellBorderStyle = DataGridViewCellBorderStyle.None;
                }
            }
        }

        private void btn_send_msg_public_Click(object sender, EventArgs e)
        {
            // LẤY VÀ KIỂM TRA THÔNG TIN NGƯỜI DÙNG NHẬP

            string Msg = txt_msg_public.Text.Trim();

            if (Msg == "")
            {
                MessageBox.Show("BẠN CHƯA NHẬP TIN NHẮN", "THÔNG BÁO");
                return;
            }

            // TẠO DỮ LIỆU GỬI ĐẾN SERVER

            string[] Header_Data = { "COMMAND", "OBJECT" };
            string[] Body_Data = { "ACCOUNT", "SESSION_KEY", "MSG" };

            Body_Data[0] = Account;
            Body_Data[1] = Session_Key;
            Body_Data[2] = Msg;

            Header_Data[0] = "send_msg_public";
            Header_Data[1] = Class_Func.Serialize_To_String(Body_Data);

            // TIẾN HÀNH GỬI DỮ LIỆU VÀ LẤY KẾT QUẢ TRẢ VỀ TỪ SERVER

            Socket_Client SClient = new Socket_Client();
            string[] SClient_Return = SClient.Send_Data(IP_Server, Port_Server, Class_Func.Serialize_To_String(Header_Data));
            if (SClient_Return[0] == "ERROR") { MessageBox.Show(SClient_Return[1], "THÔNG BÁO"); return; }

            // XỬ LÝ DỮ LIỆU NHẬN ĐƯỢC TỪ SERVER

            string[] Data_Return_From_Server = (string[])Class_Func.Deserialize_From_String(SClient_Return[1]);

            // NẾU NHẬN ĐƯỢC THÔNG BÁO LỖI VÀ LỖI ĐÓ LÀ SESSION_TIME_OUT
            // THÌ THÔNG BÁO LỖI VÀ THOÁT CHƯƠNG TRÌNH VỀ FORM LOGIN

            if (Data_Return_From_Server[0] == "ERROR" && Data_Return_From_Server[1] == "SESSION_TIME_OUT")
            {
                MessageBox.Show("PHIÊN LÀM VIỆC ĐÃ HẾT HẠN. XIN MỜI BẠN ĐĂNG NHẬP LẠI", "THÔNG BÁO");
                this.Close();
                return;
            }

            // NẾU THÔNG BÁO LỖI KHÁC THÌ CHỈ HIỂN THỊ CHO NGƯỜI DÙNG BIẾT

            if (Data_Return_From_Server[0] == "ERROR") { MessageBox.Show(Data_Return_From_Server[1], "THÔNG BÁO"); return; }

            // NẾU KẾT QUẢ NHẬN ĐƯỢC KHÔNG BỊ LỖI
            // THÌ TIẾN HÀNH CHẠY HÀM LẤY DANH SÁCH TIN NHẮN MỚI

            if (Data_Return_From_Server[0] == "OK")
            {
                txt_msg_public.Text = "";
                Get_List_Msg_Public();
            }
        }

        private void btn_remove_msg_public_Click(object sender, EventArgs e)
        {
            // LẤY ID TIN NHẮN ĐANG ĐƯỢC TRONG TRONG DANH SÁCH TIN NHẮN

            string ID_Msg = "";

            foreach (DataGridViewRow Dgvr in list_msg_public.SelectedRows)
            {
                ID_Msg = List_ID_Msg_Public[Dgvr.Index].ToString();
                if (ID_Msg != "")
                {
                    break;
                }
            }

            if (ID_Msg == "")
            {
                MessageBox.Show("BẠN CHƯA CHỌN TIN NHẮN CẦN XÓA", "THÔNG BÁO");
                return;
            }

            // TẠO DỮ LIỆU GỬI ĐẾN SERVER

            string[] Header_Data = { "COMMAND", "OBJECT" };
            string[] Body_Data = { "ACCOUNT", "SESSION_KEY", "ID_MSG" };

            Body_Data[0] = Account;
            Body_Data[1] = Session_Key;
            Body_Data[2] = ID_Msg;

            Header_Data[0] = "delete_msg_public";
            Header_Data[1] = Class_Func.Serialize_To_String(Body_Data);

            // TIẾN HÀNH GỬI DỮ LIỆU VÀ LẤY KẾT QUẢ TRẢ VỀ TỪ SERVER

            Socket_Client SClient = new Socket_Client();
            string[] SClient_Return = SClient.Send_Data(IP_Server, Port_Server, Class_Func.Serialize_To_String(Header_Data));
            if (SClient_Return[0] == "ERROR") { MessageBox.Show(SClient_Return[1], "THÔNG BÁO"); return; }

            // XỬ LÝ DỮ LIỆU NHẬN ĐƯỢC TỪ SERVER

            string[] Data_Return_From_Server = (string[])Class_Func.Deserialize_From_String(SClient_Return[1]);

            // NẾU NHẬN ĐƯỢC THÔNG BÁO LỖI VÀ LỖI ĐÓ LÀ SESSION_TIME_OUT
            // THÌ THÔNG BÁO LỖI VÀ THOÁT CHƯƠNG TRÌNH VỀ FORM LOGIN

            if (Data_Return_From_Server[0] == "ERROR" && Data_Return_From_Server[1] == "SESSION_TIME_OUT")
            {
                MessageBox.Show("PHIÊN LÀM VIỆC ĐÃ HẾT HẠN. XIN MỜI BẠN ĐĂNG NHẬP LẠI", "THÔNG BÁO");
                this.Close();
                return;
            }

            // NẾU THÔNG BÁO LỖI KHÁC THÌ CHỈ HIỂN THỊ CHO NGƯỜI DÙNG BIẾT

            if (Data_Return_From_Server[0] == "ERROR") { MessageBox.Show(Data_Return_From_Server[1], "THÔNG BÁO"); return; }

            // NẾU KẾT QUẢ NHẬN ĐƯỢC KHÔNG BỊ LỖI
            // THÌ TIẾN HÀNH CHẠY HÀM LẤY DANH SÁCH TIN NHẮN MỚI

            if (Data_Return_From_Server[0] == "OK")
            {
                Get_List_Msg_Public();
            }
        }

        // PHẦN LỆNH THOÁT TÀI KHOẢN, ĐÓNG CÁC FORM CON, TẮT CÁC TIMER TỰ ĐỘNG CẬP NHẬT MỖI KHI ĐÓNG FORM MAIN

        private void Frm_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Enabled = false;
            timer2.Enabled = false;
            Logout(Account, Session_Key);
            Close_All_Form();
        }

        public void Logout(string account, string session_key)
        {
            // TẠO DỮ LIỆU GỬI ĐẾN SERVER

            string[] Header_Data = { "COMMAND", "OBJECT" };
            string[] Body_Data = { "ACCOUNT", "SESSION_KEY" };

            Body_Data[0] = Account;
            Body_Data[1] = Session_Key;

            Header_Data[0] = "logout";
            Header_Data[1] = Class_Func.Serialize_To_String(Body_Data);

            // TIẾN HÀNH GỬI DỮ LIỆU VÀ XỬ LÝ DỮ LIỆU SERVER TRẢ VỀ

            Socket_Client SClient = new Socket_Client();
            string[] SClient_Return = SClient.Send_Data(IP_Server, Port_Server, Class_Func.Serialize_To_String(Header_Data));
            if (SClient_Return[0] == "ERROR") { MessageBox.Show(SClient_Return[1], "THÔNG BÁO"); return; }
        }

        private void Close_All_Form()
        {
            FormCollection Fc = Application.OpenForms;
            if (Fc.Count > 0)
            {
                for (int i = Fc.Count; i > 0; i--)
                {
                    Form Selected_Form = Application.OpenForms[i - 1];
                    if (Selected_Form.Name != "Frm_Main" && Selected_Form.Name != "Frm_Login")
                    {
                        Selected_Form.Close();
                    }
                }
            }
        }

        // PHẦN LỆNH CỦA CÁC TIMER TỰ ĐỘNG

        private void timer1_Tick(object sender, EventArgs e)
        {
            Get_List_Msg_Public();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (Acc2.Trim() != "" && Account.Trim() != "")
            {
                Get_List_Msg_Private(Acc2.Trim());
            }
        }
    }
}
