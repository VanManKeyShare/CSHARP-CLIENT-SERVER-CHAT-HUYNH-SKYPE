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
    public partial class Frm_Update_Account_Info : Form
    {
        // KHAI BÁO CÁC BIẾN CẦN SỬ DỤNG

        public Frm_Main FMain;

        public string IP_Server;
        public Int32 Port_Server;

        public string Account;
        public string Session_Key;

        // KHAI BÁO BIẾN SỬ DỤNG CLASS FUNCTION

        Functions Class_Func = new Functions();

        public Frm_Update_Account_Info()
        {
            InitializeComponent();
        }

        private void Get_Account_Info()
        {
            txt_account.Text = Account;

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
                FMain.Close();
                return;
            }

            // NẾU THÔNG BÁO LỖI KHÁC THÌ CHỈ HIỂN THỊ CHO NGƯỜI DÙNG BIẾT

            if (Data_Return_From_Server[0] == "ERROR") { MessageBox.Show(Data_Return_From_Server[1], "THÔNG BÁO"); return; }

            // NẾU KẾT QUẢ NHẬN ĐƯỢC KHÔNG BỊ LỖI
            // THÌ LẤY THÔNG TIN VÀ HIỂN THỊ LÊN FORM

            if (Data_Return_From_Server[0] == "OK")
            {
                DataTable[] Account_Info = (DataTable[])Class_Func.Deserialize_From_String(Data_Return_From_Server[1]);

                txt_full_name.Text = Account_Info[0].Rows[0][0].ToString().Trim();
                string avatar_base64 = Account_Info[0].Rows[0][1].ToString().Trim();

                if (Class_Func.Base64_To_Image(avatar_base64) != null)
                {
                    avatar.Image = Class_Func.Base64_To_Image(avatar_base64);
                }
            }
        }

        private void Frm_Update_Account_Info_Load(object sender, EventArgs e)
        {
            Get_Account_Info();
        }

        private void btn_choose_avatar_Click(object sender, EventArgs e)
        {
            // HIỂN THỊ HỘP THOẠI CHỌN FILE ẢNH AVATAR

            openFileDialog1.FileName = "";
            openFileDialog1.InitialDirectory = "C:\\";
            openFileDialog1.Filter = "IMAGE FILES (*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG";
            openFileDialog1.ShowDialog();

            // NẾU ĐÃ CHỌN FILE ẢNH THÌ TIẾN HÀNH THAY ĐỔI KÍCH THƯỚC ẢNH VỀ 80 x 80
            // SAU ĐÓ HIỂN THỊ LÊN CONTROL PICTURE

            if (openFileDialog1.FileName != "")
            {
                avatar.Image = Class_Func.Resize_Image(new Bitmap(openFileDialog1.FileName), 80, 80);
            }
        }

        private void btn_update_account_info_Click(object sender, EventArgs e)
        {
            // LẤY VÀ KIỂM TRA THÔNG TIN NGƯỜI DÙNG NHẬP

            string new_password = txt_new_password.Text.Trim();
            string full_name = txt_full_name.Text.Trim();
            string current_password = txt_current_password.Text.Trim();
            string new_avatar = Class_Func.Image_To_Base64(avatar.Image).Trim();

            if (current_password == "")
            {
                MessageBox.Show("BẠN CHƯA NHẬP MẬT KHẨU XÁC NHẬN", "THÔNG BÁO");
                return;
            }

            // TẠO DỮ LIỆU GỬI ĐẾN SERVER

            string[] Header_Data = { "COMMAND", "OBJECT" };
            string[] Body_Data = { "ACCOUNT", "SESSION_KEY", "CURRENT_PASSWORD", "NEW_PASSWORD", "NEW_FULL_NAME", "NEW_AVATAR" };

            Body_Data[0] = Account;
            Body_Data[1] = Session_Key;
            Body_Data[2] = current_password;
            Body_Data[3] = new_password;
            Body_Data[4] = full_name;
            Body_Data[5] = new_avatar;

            Header_Data[0] = "update_account_info";
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
                FMain.Close();
                return;
            }

            // NẾU THÔNG BÁO LỖI KHÁC THÌ CHỈ HIỂN THỊ CHO NGƯỜI DÙNG BIẾT

            if (Data_Return_From_Server[0] == "ERROR") { MessageBox.Show(Data_Return_From_Server[1], "THÔNG BÁO"); return; }

            // NẾU KẾT QUẢ NHẬN ĐƯỢC KHÔNG BỊ LỖI
            // THÌ THÔNG BÁO THÀNH CÔNG VÀ THOÁT CHƯƠNG TRÌNH VỀ FORM LOGIN

            if (Data_Return_From_Server[0] == "OK")
            {
                MessageBox.Show("CẬP NHẬT THÔNG TIN THÀNH CÔNG. XIN MỜI THOÁT VÀ ĐĂNG NHẬP LẠI", "THÔNG BÁO");
                FMain.Close();
            }
        }
    }
}
