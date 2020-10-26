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
    public partial class Frm_Register : Form
    {
        // KHAI BÁO CÁC BIẾN CẦN SỬ DỤNG

        public string IP_Server;
        public Int32 Port_Server;

        // KHAI BÁO BIẾN SỬ DỤNG CLASS FUNCTION

        Functions Class_Func = new Functions();

        public Frm_Register()
        {
            InitializeComponent();
        }

        private void Frm_Register_Load(object sender, EventArgs e)
        {
            txt_ip_server.Text = IP_Server;
            txt_port_server.Text = Port_Server.ToString();
        }

        private void btn_register_Click(object sender, EventArgs e)
        {
            // LẤY VÀ KIỂM TRA CÁC THÔNG TIN NGƯỜI DÙNG NHẬP

            string account = txt_account.Text.Trim();
            string password = txt_password.Text.Trim();
            string full_name = txt_ho_ten.Text.Trim();

            if (account == "" || password == "" || full_name == "")
            {
                MessageBox.Show("BẠN CHƯA NHẬP ĐẦY ĐỦ THÔNG TIN", "THÔNG BÁO");
                return;
            }

            IP_Server = txt_ip_server.Text.Trim();
            if (IP_Server == "")
            {
                MessageBox.Show("BẠN CHƯA NHẬP IP MÁY CHỦ", "THÔNG BÁO");
                return;
            }

            Int32.TryParse(txt_port_server.Text.Trim(), out Port_Server);
            if (Port_Server <= 0)
            {
                MessageBox.Show("CỔNG KẾT NỐI PHẢI LỚN HƠN 0", "THÔNG BÁO");
                return;
            }

            // TẠO DỮ LIỆU GỬI ĐẾN SERVER

            string[] Header_Data = { "COMMAND", "OBJECT" };
            string[] Body_Data = { "ACCOUNT", "PASSWORD", "FULL_NAME" };

            Body_Data[0] = account;
            Body_Data[1] = password;
            Body_Data[2] = full_name;

            Header_Data[0] = "register";
            Header_Data[1] = Class_Func.Serialize_To_String(Body_Data);

            // TIẾN HÀNH GỬI DỮ LIỆU VÀ LẤY KẾT QUẢ TRẢ VỀ TỪ SERVER

            Socket_Client SClient = new Socket_Client();
            string[] SClient_Return = SClient.Send_Data(IP_Server, Port_Server, Class_Func.Serialize_To_String(Header_Data));
            if (SClient_Return[0] == "ERROR") { MessageBox.Show(SClient_Return[1], "THÔNG BÁO"); return; }

            // XỬ LÝ DỮ LIỆU NHẬN ĐƯỢC TỪ SERVER
            // NẾU KẾT QUẢ NHẬN ĐƯỢC LÀ LỖI THÌ THÔNG BÁO CHO NGƯỜI DÙNG

            string[] Data_Return_From_Server = (string[])Class_Func.Deserialize_From_String(SClient_Return[1]);
            if (Data_Return_From_Server[0] == "ERROR") { MessageBox.Show(Data_Return_From_Server[1], "THÔNG BÁO"); return; }

            // NẾU KẾT QUẢ NHẬN ĐƯỢC KHÔNG BỊ LỖI THÌ
            // ĐÓNG FORM ĐĂNG KÝ VÀ THÔNG BÁO THÀNH CÔNG

            if (Data_Return_From_Server[0] == "OK")
            {
                this.Close();
                MessageBox.Show("ĐĂNG KÝ THÀNH CÔNG", "THÔNG BÁO");
            }
        }
    }
}
