using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Net;
using System.Net.Sockets;
using System.Data.SqlClient;

namespace HUYNH_SKYPE_SERVER
{
    public partial class Frm_Main : Form
    {
        // KHAI BÁO BIẾN CHỨA THÔNG SỐ KẾT NỐI SQL SERVER

        string Sql_Connection_String = @"Data Source=.\SqlExpress;Initial Catalog=HUYNH_SKYPE;Integrated Security=True";

        // KHAI BÁO BIẾN CHỨA THÔNG SỐ IP MÁY CHỦ, SỐ CỔNG SẼ MỞ

        string IP_Server = "127.0.0.1";
        Int32 Port_Server = 20000;
        
        // KHAI BÁO BIẾN CHỨA THÔNG SỐ GIÂY SẼ XÓA TRẠNG THÁI ONLINE CỦA ACCOUNT NẾU VƯỢT QUÁ QUY ĐỊNH

        int Second_For_Reset_Session_Online = 600;

        // KHAI BÁO CÁC BIẾN DÙNG TRUY XUẤT SQL

        SqlConnection Sql_Conn;
        SqlCommand Sql_Cmd;
        String Sql_Query;
        SqlDataReader Sql_Data_Reader;
        DataTable DT;

        // KHAI BÁO BIẾN DÙNG TẠO MÁY CHỦ

        Socket Listener = null;

        // KHAI BÁO BIẾN SỬ DỤNG CLASS FUNCTION

        Functions Class_Func = new Functions();

        // HÀM XỬ LÝ DỮ LIỆU NHẬN ĐƯỢC TỪ CLIENT

        private string Process_Data_From_Client(string Base64_Data)
        {
            // KHAI BÁO CẤU TRÚC DỮ LIỆU SẼ TRẢ VỀ CHO CLIENT

            string[] Data_Return = { "STATUS", "DATA" };

            // LẤY VÀ KIỂM TRA DỮ LIỆU NHẬN ĐƯỢC TỪ CLIENT

            string[] Header_Data = { "COMMAND", "OBJECT" };
            Header_Data = (string[])Class_Func.Deserialize_From_String(Base64_Data);

            if (Header_Data.Count() != 2)
            {
                Data_Return[0] = "ERROR";
                Data_Return[1] = "DỮ LIỆU NHẬN ĐƯỢC KHÔNG ĐẦY ĐỦ";
                return Class_Func.Serialize_To_String(Data_Return);
            }

            // THỰC HIỆN LỆNH THEO TỪNG COMMAND KHÁC NHAU

            string Command = Header_Data[0];

            // LỆNH XỬ LÝ ĐĂNG NHẬP

            #region --- KHỐI LỆNH XỬ LÝ ĐĂNG NHẬP ---

            if (Command == "login")
            {
                string[] Body_Data = (string[])Class_Func.Deserialize_From_String(Header_Data[1]);
                if (Body_Data.Count() != 2)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = "DỮ LIỆU NHẬN ĐƯỢC KHÔNG ĐẦY ĐỦ";
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // LẤY VÀ KIỂM TRA THÔNG TIN TỪ CLIENT

                string account = Body_Data[0].Trim().ToLower();
                string password = Body_Data[1].Trim();

                if (account == "" || password == "")
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = "DỮ LIỆU NHẬN ĐƯỢC KHÔNG ĐẦY ĐỦ";
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // KIỂM TRA THÔNG TIN ĐĂNG NHẬP

                Sql_Query = "SELECT ACCOUNT FROM ACCOUNTS WHERE (ACCOUNT = @ACCOUNT) AND (PASSWORD = @PASSWORD)";

                DT = new DataTable();
                Sql_Conn = new SqlConnection(Sql_Connection_String);
                try
                {
                    Sql_Conn.Open();
                    Sql_Cmd = new SqlCommand(Sql_Query, Sql_Conn);
                    Sql_Cmd.Parameters.AddWithValue("ACCOUNT", account);
                    Sql_Cmd.Parameters.AddWithValue("PASSWORD", password);
                    Sql_Data_Reader = Sql_Cmd.ExecuteReader();
                    DT.Load(Sql_Data_Reader);
                    Sql_Cmd.Dispose();
                    Sql_Conn.Close();
                }
                catch (Exception ex)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = ex.ToString();
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // NẾU THÔNG TIN ĐĂNG NHẬP KHÔNG ĐÚNG THÌ BÁO LỖI VỀ CHO CLIENT

                if (DT.Rows.Count == 0)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = "ACCOUNT HOẶC MẬT KHẨU KHÔNG ĐÚNG";
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // NẾU THÔNG TIN ĐĂNG NHẬP ĐÚNG THÌ TIẾN HÀNH TẠO VÀ LƯU SESSION_KEY VÀO BẢNG SESSIONS

                string session_key = Class_Func.CREATE_MD5_HASH(account + DateTime.Now.ToString());

                Sql_Query = "";
                Sql_Query = Sql_Query + "DELETE FROM SESSIONS WHERE (ACCOUNT = @ACCOUNT);";
                Sql_Query = Sql_Query + "\r\n";
                Sql_Query = Sql_Query + "INSERT INTO SESSIONS (ACCOUNT, SESSION_KEY, LAST_ACTIVE) VALUES (@ACCOUNT, @SESSION_KEY, GETDATE());";

                Sql_Conn = new SqlConnection(Sql_Connection_String);
                try
                {
                    Sql_Conn.Open();
                    Sql_Cmd = new SqlCommand(Sql_Query, Sql_Conn);
                    Sql_Cmd.Parameters.AddWithValue("ACCOUNT", account);
                    Sql_Cmd.Parameters.AddWithValue("SESSION_KEY", session_key);
                    Sql_Cmd.ExecuteNonQuery();
                    Sql_Cmd.Dispose();
                    Sql_Conn.Close();
                }
                catch (Exception ex)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = ex.ToString();
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // TRẢ KẾT QUẢ SESSION VỀ CHO CLIENT

                Data_Return[0] = "OK";
                Data_Return[1] = session_key;
                return Class_Func.Serialize_To_String(Data_Return);
            }

            #endregion

            // LỆNH XỬ LÝ ĐĂNG XUẤT

            #region --- KHỐI LỆNH XỬ LÝ ĐĂNG XUẤT ---

            if (Command == "logout")
            {
                string[] Body_Data = (string[])Class_Func.Deserialize_From_String(Header_Data[1]);
                if (Body_Data.Count() != 2)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = "DỮ LIỆU NHẬN ĐƯỢC KHÔNG ĐẦY ĐỦ";
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // LẤY VÀ KIỂM TRA THÔNG TIN TỪ CLIENT

                string account = Body_Data[0].Trim().ToLower();
                string session_key = Body_Data[1].Trim();

                if (account == "" || session_key == "")
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = "DỮ LIỆU NHẬN ĐƯỢC KHÔNG ĐẦY ĐỦ";
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // KIỂM TRA TRẠNG THÁI ONLINE

                if (Check_Session_Online_Active(account, session_key) == false)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = "SESSION_TIME_OUT";
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // XÓA SESSION_KEY RA KHỎI BẢNG SESSION

                Sql_Query = "DELETE FROM SESSIONS WHERE (ACCOUNT = @ACCOUNT) AND (SESSION_KEY = @SESSION_KEY)";

                Sql_Conn = new SqlConnection(Sql_Connection_String);
                try
                {
                    Sql_Conn.Open();
                    Sql_Cmd = new SqlCommand(Sql_Query, Sql_Conn);
                    Sql_Cmd.Parameters.AddWithValue("ACCOUNT", account);
                    Sql_Cmd.Parameters.AddWithValue("SESSION_KEY", session_key);
                    Sql_Cmd.ExecuteNonQuery();
                    Sql_Cmd.Dispose();
                    Sql_Conn.Close();
                }
                catch (Exception ex)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = ex.ToString();
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // TRẢ KẾT QUẢ VỀ CHO CLIENT

                Data_Return[0] = "OK";
                Data_Return[1] = "ĐĂNG XUẤT THÀNH CÔNG";
                return Class_Func.Serialize_To_String(Data_Return);
            }

            #endregion

            // LỆNH XỬ LÝ ĐĂNG KÝ

            #region --- KHỐI LỆNH XỬ LÝ ĐĂNG KÝ ---

            if (Command == "register")
            {
                string[] Body_Data = (string[])Class_Func.Deserialize_From_String(Header_Data[1]);
                if (Body_Data.Count() != 3)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = "DỮ LIỆU NHẬN ĐƯỢC KHÔNG ĐẦY ĐỦ";
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // LẤY VÀ KIỂM TRA THÔNG TIN TỪ CLIENT

                string account = Body_Data[0].Trim().ToLower();
                string password = Body_Data[1].Trim();
                string full_name = Body_Data[2].Trim();

                if (account == "" || password == "" || full_name == "")
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = "DỮ LIỆU NHẬN ĐƯỢC KHÔNG ĐẦY ĐỦ";
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // KIỂM TRA ACCOUNT ĐÃ CÓ ĐĂNG KÝ CHƯA

                Sql_Query = "SELECT ACCOUNT FROM ACCOUNTS WHERE (ACCOUNT = @ACCOUNT)";

                DT = new DataTable();
                Sql_Conn = new SqlConnection(Sql_Connection_String);
                try
                {
                    Sql_Conn.Open();
                    Sql_Cmd = new SqlCommand(Sql_Query, Sql_Conn);
                    Sql_Cmd.Parameters.AddWithValue("ACCOUNT", account);
                    Sql_Data_Reader = Sql_Cmd.ExecuteReader();
                    DT.Load(Sql_Data_Reader);
                    Sql_Cmd.Dispose();
                    Sql_Conn.Close();
                }
                catch (Exception ex)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = ex.ToString();
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // NẾU ĐÃ TỒN TẠI ACCOUNT MUỐN ĐĂNG KÝ THÌ BÁO LỖI VỀ CHO CLIENT

                if (DT.Rows.Count != 0)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = "ACCOUNT NÀY ĐÃ ĐĂNG KÝ RỒI";
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // NẾU CHƯA THÌ TIẾN HÀNH THÊM ACCOUNT VÀO CSDL

                Sql_Query = "INSERT INTO ACCOUNTS (ACCOUNT, PASSWORD, FULL_NAME, AVATAR, DATE_TIME_CREATE) VALUES (@ACCOUNT, @PASSWORD, @FULL_NAME, '', GETDATE())";

                Sql_Conn = new SqlConnection(Sql_Connection_String);
                try
                {
                    Sql_Conn.Open();
                    Sql_Cmd = new SqlCommand(Sql_Query, Sql_Conn);
                    Sql_Cmd.Parameters.AddWithValue("ACCOUNT", account);
                    Sql_Cmd.Parameters.AddWithValue("PASSWORD", password);
                    Sql_Cmd.Parameters.AddWithValue("FULL_NAME", full_name);
                    Sql_Cmd.ExecuteNonQuery();
                    Sql_Cmd.Dispose();
                    Sql_Conn.Close();
                }
                catch (Exception ex)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = ex.ToString();
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // TRẢ KẾT QUẢ VỀ CHO CLIENT

                Data_Return[0] = "OK";
                Data_Return[1] = "ĐĂNG KÝ THÀNH CÔNG";
                return Class_Func.Serialize_To_String(Data_Return);
            }

            #endregion

            // LỆNH XỬ LÝ LẤY THÔNG TIN ACCOUNT

            #region --- KHỐI LỆNH XỬ LÝ LẤY THÔNG TIN ACCOUNT ---

            if (Command == "get_account_info")
            {
                string[] Body_Data = (string[])Class_Func.Deserialize_From_String(Header_Data[1]);
                if (Body_Data.Count() != 2)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = "DỮ LIỆU NHẬN ĐƯỢC KHÔNG ĐẦY ĐỦ";
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // LẤY VÀ KIỂM TRA THÔNG TIN TỪ CLIENT

                string account = Body_Data[0].Trim().ToLower();
                string session_key = Body_Data[1].Trim();

                if (account == "" || session_key == "")
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = "DỮ LIỆU NHẬN ĐƯỢC KHÔNG ĐẦY ĐỦ";
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // KIỂM TRA TRẠNG THÁI ONLINE

                if (Check_Session_Online_Active(account, session_key) == false)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = "SESSION_TIME_OUT";
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // TIẾN HÀNH LẤY THÔNG TIN TỪ CSDL

                Sql_Query = "SELECT FULL_NAME, AVATAR FROM ACCOUNTS WHERE (ACCOUNT = @ACCOUNT)";

                DataTable[] DTZ = { new DataTable() };
                Sql_Conn = new SqlConnection(Sql_Connection_String);
                try
                {
                    Sql_Conn.Open();
                    Sql_Cmd = new SqlCommand(Sql_Query, Sql_Conn);
                    Sql_Cmd.Parameters.AddWithValue("ACCOUNT", account);
                    Sql_Data_Reader = Sql_Cmd.ExecuteReader();
                    DTZ[0].Load(Sql_Data_Reader);
                    Sql_Cmd.Dispose();
                    Sql_Conn.Close();
                }
                catch (Exception ex)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = ex.ToString();
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // NẾU KHÔNG CÓ THÔNG TIN NÀO VỀ ACCOUNT CẦN LẤY THÌ BÁO LỖI VỀ CHO CLIENT

                if (DT.Rows.Count == 0)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = "KHÔNG TỒN TẠI THÔNG TIN CỦA ACCOUNT";
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // TRẢ KẾT QUẢ VỀ CHO CLIENT

                Data_Return[0] = "OK";
                Data_Return[1] = Class_Func.Serialize_To_String(DTZ);
                return Class_Func.Serialize_To_String(Data_Return);
            }

            #endregion

            // LỆNH XỬ LÝ CẬP NHẬT THÔNG TIN ACCOUNT

            #region --- KHỐI LỆNH XỬ LÝ CẬP NHẬT THÔNG TIN ACCOUNT ---

            if (Command == "update_account_info")
            {
                string[] Body_Data = (string[])Class_Func.Deserialize_From_String(Header_Data[1]);
                if (Body_Data.Count() != 6)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = "DỮ LIỆU NHẬN ĐƯỢC KHÔNG ĐẦY ĐỦ";
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // LẤY VÀ KIỂM TRA THÔNG TIN TỪ CLIENT

                string account = Body_Data[0].Trim().ToLower();
                string session_key = Body_Data[1].Trim();
                string current_password = Body_Data[2].Trim();
                string new_password = Body_Data[3].Trim();
                string new_full_name = Body_Data[4].Trim();
                string new_avatar = Body_Data[5].Trim();

                if (account == "" || session_key == "" || current_password == "")
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = "DỮ LIỆU NHẬN ĐƯỢC KHÔNG ĐẦY ĐỦ";
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // KIỂM TRA TRẠNG THÁI ONLINE

                if (Check_Session_Online_Active(account, session_key) == false)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = "SESSION_TIME_OUT";
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // KIỂM TRA XÁC NHẬN MẬT KHẨU

                Sql_Query = "SELECT ACCOUNT FROM ACCOUNTS WHERE (ACCOUNT = @ACCOUNT) AND (PASSWORD = @CURRENT_PASSWORD)";

                DT = new DataTable();
                Sql_Conn = new SqlConnection(Sql_Connection_String);
                try
                {
                    Sql_Conn.Open();
                    Sql_Cmd = new SqlCommand(Sql_Query, Sql_Conn);
                    Sql_Cmd.Parameters.AddWithValue("ACCOUNT", account);
                    Sql_Cmd.Parameters.AddWithValue("CURRENT_PASSWORD", current_password);
                    Sql_Data_Reader = Sql_Cmd.ExecuteReader();
                    DT.Load(Sql_Data_Reader);
                    Sql_Cmd.Dispose();
                    Sql_Conn.Close();
                }
                catch (Exception ex)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = ex.ToString();
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // NẾU MẬT KHẨU XÁC NHẬN KHÔNG ĐÚNG THÌ KHÔNG CẬP NHẬT VÀ BÁO LỖI VỀ CHO CLIENT

                if (DT.Rows.Count == 0)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = "MẬT KHẨU XÁC NHẬN KHÔNG ĐÚNG";
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // TIẾN HÀNH CẬP NHẬT THÔNG TIN VÀO CSDL

                Sql_Query = "";

                if (new_full_name != "")
                {
                    Sql_Query = Sql_Query + "UPDATE ACCOUNTS SET FULL_NAME = @NEW_FULL_NAME";
                    Sql_Query = Sql_Query + "\r\n";
                    Sql_Query = Sql_Query + "WHERE (ACCOUNT = @ACCOUNT) AND (PASSWORD = @CURRENT_PASSWORD);";
                    Sql_Query = Sql_Query + "\r\n\r\n";
                }

                if (new_avatar != "")
                {
                    Sql_Query = Sql_Query + "UPDATE ACCOUNTS SET AVATAR = @NEW_AVATAR";
                    Sql_Query = Sql_Query + "\r\n";
                    Sql_Query = Sql_Query + "WHERE (ACCOUNT = @ACCOUNT) AND (PASSWORD = @CURRENT_PASSWORD);";
                    Sql_Query = Sql_Query + "\r\n\r\n";
                }

                if (new_password != "")
                {
                    Sql_Query = Sql_Query + "UPDATE ACCOUNTS SET PASSWORD = @NEW_PASSWORD";
                    Sql_Query = Sql_Query + "\r\n";
                    Sql_Query = Sql_Query + "WHERE (ACCOUNT = @ACCOUNT) AND (PASSWORD = @CURRENT_PASSWORD);";
                    Sql_Query = Sql_Query + "\r\n\r\n";
                }

                Sql_Conn = new SqlConnection(Sql_Connection_String);
                try
                {
                    Sql_Conn.Open();
                    Sql_Cmd = new SqlCommand(Sql_Query, Sql_Conn);
                    Sql_Cmd.Parameters.AddWithValue("ACCOUNT", account);
                    Sql_Cmd.Parameters.AddWithValue("CURRENT_PASSWORD", current_password);
                    Sql_Cmd.Parameters.AddWithValue("NEW_PASSWORD", new_password);
                    Sql_Cmd.Parameters.AddWithValue("NEW_FULL_NAME", new_full_name);
                    Sql_Cmd.Parameters.AddWithValue("NEW_AVATAR", new_avatar);
                    Sql_Cmd.ExecuteNonQuery();
                    Sql_Cmd.Dispose();
                    Sql_Conn.Close();
                }
                catch (Exception ex)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = ex.ToString();
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // TRẢ KẾT QUẢ VỀ CHO CLIENT

                Data_Return[0] = "OK";
                Data_Return[1] = "CẬP NHẬT THÔNG TIN THÀNH CÔNG";
                return Class_Func.Serialize_To_String(Data_Return);
            }

            #endregion

            // LỆNH XỬ LÝ LẤY DANH SÁCH BẠN BÈ

            #region --- KHỐI LỆNH XỬ LÝ LẤY DANH SÁCH BẠN BÈ ---

            if (Command == "get_list_friend")
            {
                string[] Body_Data = (string[])Class_Func.Deserialize_From_String(Header_Data[1]);
                if (Body_Data.Count() != 2)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = "DỮ LIỆU NHẬN ĐƯỢC KHÔNG ĐẦY ĐỦ";
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // LẤY VÀ KIỂM TRA THÔNG TIN TỪ CLIENT

                string account = Body_Data[0].Trim().ToLower();
                string session_key = Body_Data[1].Trim();

                if (account == "" || session_key == "")
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = "DỮ LIỆU NHẬN ĐƯỢC KHÔNG ĐẦY ĐỦ";
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // KIỂM TRA TRẠNG THÁI ONLINE

                if (Check_Session_Online_Active(account, session_key) == false)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = "SESSION_TIME_OUT";
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // LẤY DANH SÁCH ACCOUNT BẠN BÈ VÀ TRẠNG THÁI ONLINE TRONG CSDL

                Sql_Query = "";
                Sql_Query = Sql_Query + "SELECT ACC2 AS 'ACCOUNT',";
                Sql_Query = Sql_Query + "\r\n";
                Sql_Query = Sql_Query + "(CASE";
                Sql_Query = Sql_Query + "\r\n";
                Sql_Query = Sql_Query + "WHEN EXISTS (SELECT ACCOUNT FROM SESSIONS WHERE ACCOUNT = [FRIENDS].ACC2)";
                Sql_Query = Sql_Query + "\r\n";
                Sql_Query = Sql_Query + "THEN 'Online' ELSE ''";
                Sql_Query = Sql_Query + "\r\n";
                Sql_Query = Sql_Query + "END) AS 'STATUS'";
                Sql_Query = Sql_Query + "\r\n";
                Sql_Query = Sql_Query + "FROM FRIENDS";
                Sql_Query = Sql_Query + "\r\n";
                Sql_Query = Sql_Query + "WHERE ACC1 = @ACCOUNT";

                DataTable[] DTZ = { new DataTable() };
                Sql_Conn = new SqlConnection(Sql_Connection_String);
                try
                {
                    Sql_Conn.Open();
                    Sql_Cmd = new SqlCommand(Sql_Query, Sql_Conn);
                    Sql_Cmd.Parameters.AddWithValue("ACCOUNT", account);
                    Sql_Data_Reader = Sql_Cmd.ExecuteReader();
                    DTZ[0].Load(Sql_Data_Reader);
                    Sql_Cmd.Dispose();
                    Sql_Conn.Close();
                }
                catch (Exception ex)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = ex.ToString();
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // TRẢ KẾT QUẢ VỀ CHO CLIENT

                Data_Return[0] = "OK";
                Data_Return[1] = Class_Func.Serialize_To_String(DTZ);
                return Class_Func.Serialize_To_String(Data_Return);
            }

            #endregion

            // LỆNH XỬ LÝ THÊM BẠN BÈ

            #region --- KHỐI LỆNH XỬ LÝ THÊM BẠN BÈ ---

            if (Command == "add_friend")
            {
                string[] Body_Data = (string[])Class_Func.Deserialize_From_String(Header_Data[1]);
                if (Body_Data.Count() != 3)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = "DỮ LIỆU NHẬN ĐƯỢC KHÔNG ĐẦY ĐỦ";
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // LẤY VÀ KIỂM TRA THÔNG TIN TỪ CLIENT

                string account = Body_Data[0].Trim().ToLower();
                string session_key = Body_Data[1].Trim();
                string acc2 = Body_Data[2].Trim().ToLower();

                if (account == "" || session_key == "" || acc2 == "")
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = "DỮ LIỆU NHẬN ĐƯỢC KHÔNG ĐẦY ĐỦ";
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // KIỂM TRA TRẠNG THÁI ONLINE

                if (Check_Session_Online_Active(account, session_key) == false)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = "SESSION_TIME_OUT";
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // KIỂM TRA KHÔNG CHO PHÉP THÊM CHÍNH MÌNH LÀM BẠN BÈ

                if (acc2 == account)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = "BẠN KHÔNG THỂ THÊM CHÍNH MÌNH LÀM BẠN BÈ";
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // KIỂM TRA ACCOUNT BẠN BÈ CÓ TỒN TẠI HAY KHÔNG

                Sql_Query = "SELECT ACCOUNT FROM ACCOUNTS WHERE (ACCOUNT = @ACC2)";

                DT = new DataTable();
                Sql_Conn = new SqlConnection(Sql_Connection_String);
                try
                {
                    Sql_Conn.Open();
                    Sql_Cmd = new SqlCommand(Sql_Query, Sql_Conn);
                    Sql_Cmd.Parameters.AddWithValue("ACC2", acc2);
                    Sql_Data_Reader = Sql_Cmd.ExecuteReader();
                    DT.Load(Sql_Data_Reader);
                    Sql_Cmd.Dispose();
                    Sql_Conn.Close();
                }
                catch (Exception ex)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = ex.ToString();
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // NẾU ACCOUNT BẠN BÈ CẦN THÊM KHÔNG TỒN TẠI THÌ BÁO LỖI VỀ CHO CLIENT

                if (DT.Rows.Count == 0)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = "ACCOUNT BẠN BÈ KHÔNG TỒN TẠI TRONG CSDL";
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // KIỂM TRA ACCOUNT CẦN THÊM CÓ TỒN TẠI TRONG DANH SÁCH BẠN BÈ HAY CHƯA

                Sql_Query = "SELECT * FROM FRIENDS WHERE (ACC1 = @ACCOUNT) AND (ACC2 = @ACC2);";

                DT = new DataTable();
                Sql_Conn = new SqlConnection(Sql_Connection_String);
                try
                {
                    Sql_Conn.Open();
                    Sql_Cmd = new SqlCommand(Sql_Query, Sql_Conn);
                    Sql_Cmd.Parameters.AddWithValue("ACCOUNT", account);
                    Sql_Cmd.Parameters.AddWithValue("ACC2", acc2);
                    Sql_Data_Reader = Sql_Cmd.ExecuteReader();
                    DT.Load(Sql_Data_Reader);
                    Sql_Cmd.Dispose();
                    Sql_Conn.Close();
                }
                catch (Exception ex)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = ex.ToString();
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // NẾU ACCOUNT CẦN THÊM ĐÃ TỒN TẠI TRONG DANH SÁCH BẠN BÈ THÌ BÁO LỖI VỀ CHO CLIENT

                if (DT.Rows.Count != 0)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = "ACCOUNT CẦN THÊM ĐÃ TỒN TẠI TRONG DANH SÁCH BẠN BÈ";
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // THÊM BẠN BÈ VÀO CSDL

                Sql_Query = "";
                Sql_Query = Sql_Query + "IF EXISTS (SELECT ACCOUNT FROM ACCOUNTS WHERE (ACCOUNT = @ACC2))";
                Sql_Query = Sql_Query + "\r\n";
                Sql_Query = Sql_Query + "BEGIN";
                Sql_Query = Sql_Query + "\r\n";
                Sql_Query = Sql_Query + "   DELETE FROM FRIENDS WHERE (ACC1 = @ACCOUNT) AND (ACC2 = @ACC2);";
                Sql_Query = Sql_Query + "\r\n";
                Sql_Query = Sql_Query + "   INSERT INTO FRIENDS (ACC1, ACC2) VALUES (@ACCOUNT, @ACC2)";
                Sql_Query = Sql_Query + "\r\n";
                Sql_Query = Sql_Query + "END";

                Sql_Conn = new SqlConnection(Sql_Connection_String);
                try
                {
                    Sql_Conn.Open();
                    Sql_Cmd = new SqlCommand(Sql_Query, Sql_Conn);
                    Sql_Cmd.Parameters.AddWithValue("ACCOUNT", account);
                    Sql_Cmd.Parameters.AddWithValue("ACC2", acc2);
                    Sql_Cmd.ExecuteNonQuery();
                    Sql_Cmd.Dispose();
                    Sql_Conn.Close();
                }
                catch (Exception ex)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = ex.ToString();
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // TRẢ KẾT QUẢ VỀ CHO CLIENT

                Data_Return[0] = "OK";
                Data_Return[1] = "THÊM BẠN THÀNH CÔNG";
                return Class_Func.Serialize_To_String(Data_Return);
            }

            #endregion

            // LỆNH XỬ LÝ XÓA BẠN BÈ

            #region --- KHỐI LỆNH XỬ LÝ XÓA BẠN BÈ ---

            if (Command == "remove_friend")
            {
                string[] Body_Data = (string[])Class_Func.Deserialize_From_String(Header_Data[1]);
                if (Body_Data.Count() != 3)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = "DỮ LIỆU NHẬN ĐƯỢC KHÔNG ĐẦY ĐỦ";
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // LẤY VÀ KIỂM TRA THÔNG TIN TỪ CLIENT

                string account = Body_Data[0].Trim().ToLower();
                string session_key = Body_Data[1].Trim();
                string acc2 = Body_Data[2].Trim().ToLower();

                if (account == "" || session_key == "" || acc2 == "")
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = "DỮ LIỆU NHẬN ĐƯỢC KHÔNG ĐẦY ĐỦ";
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // KIỂM TRA TRẠNG THÁI ONLINE

                if (Check_Session_Online_Active(account, session_key) == false)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = "SESSION_TIME_OUT";
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // XÓA BẠN BÈ TRONG CSDL

                Sql_Query = "DELETE FROM FRIENDS WHERE (ACC1 = @ACCOUNT) AND (ACC2 = @ACC2)";

                Sql_Conn = new SqlConnection(Sql_Connection_String);
                try
                {
                    Sql_Conn.Open();
                    Sql_Cmd = new SqlCommand(Sql_Query, Sql_Conn);
                    Sql_Cmd.Parameters.AddWithValue("ACCOUNT", account);
                    Sql_Cmd.Parameters.AddWithValue("ACC2", acc2);
                    Sql_Cmd.ExecuteNonQuery();
                    Sql_Cmd.Dispose();
                    Sql_Conn.Close();
                }
                catch (Exception ex)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = ex.ToString();
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // TRẢ KẾT QUẢ VỀ CHO CLIENT

                Data_Return[0] = "OK";
                Data_Return[1] = "XÓA BẠN THÀNH CÔNG";
                return Class_Func.Serialize_To_String(Data_Return);
            }

            #endregion

            // LỆNH XỬ LẤY TIN NHẮN RIÊNG TƯ

            #region --- KHỐI LỆNH XỬ LÝ LẤY TIN NHẮN RIÊNG TƯ ---

            if (Command == "get_msg")
            {
                string[] Body_Data = (string[])Class_Func.Deserialize_From_String(Header_Data[1]);
                if (Body_Data.Count() != 3)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = "DỮ LIỆU NHẬN ĐƯỢC KHÔNG ĐẦY ĐỦ";
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // LẤY VÀ KIỂM TRA THÔNG TIN TỪ CLIENT

                string account = Body_Data[0].Trim().ToLower();
                string session_key = Body_Data[1].Trim();
                string acc2 = Body_Data[2].Trim().ToLower();

                if (account == "" || session_key == "" || acc2 == "")
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = "DỮ LIỆU NHẬN ĐƯỢC KHÔNG ĐẦY ĐỦ";
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // KIỂM TRA TRẠNG THÁI ONLINE

                if (Check_Session_Online_Active(account, session_key) == false)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = "SESSION_TIME_OUT";
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // LẤY TIN NHẮN TỪ CSDL

                Sql_Query = "";
                Sql_Query = Sql_Query + "DELETE FROM ATTACHMENTS WHERE ID IN (SELECT ID_ATTACHMENT FROM CHATS WHERE (MASK_DELETE_BY_ACC1 = 1) AND (MASK_DELETE_BY_ACC2 = 1));";
                Sql_Query = Sql_Query + "\r\n";
                Sql_Query = Sql_Query + "DELETE FROM CHATS WHERE (MASK_DELETE_BY_ACC1 = 1) AND (MASK_DELETE_BY_ACC2 = 1);";
                Sql_Query = Sql_Query + "\r\n";
                Sql_Query = Sql_Query + "SELECT TOP 50 ID, MSG AS 'NỘI DUNG', ACC1 AS 'NGƯỜI GỬI', ACC2 AS 'NGƯỜI NHẬN', DATE_TIME_CREATE AS 'GỬI LÚC', ID_ATTACHMENT AS 'MÃ ĐÍNH KÈM' FROM CHATS ";
                Sql_Query = Sql_Query + "\r\n";
                Sql_Query = Sql_Query + "WHERE ((ACC1 = @ACCOUNT) AND (ACC2 = @ACC2) AND (MASK_DELETE_BY_ACC1 = 0)) OR ((ACC1 = @ACC2) AND (ACC2 = @ACCOUNT) AND (MASK_DELETE_BY_ACC2 = 0)) ";
                Sql_Query = Sql_Query + "\r\n";
                Sql_Query = Sql_Query + "ORDER BY DATE_TIME_CREATE DESC";

                DataTable[] DTZ = { new DataTable() };
                Sql_Conn = new SqlConnection(Sql_Connection_String);
                try
                {
                    Sql_Conn.Open();
                    Sql_Cmd = new SqlCommand(Sql_Query, Sql_Conn);
                    Sql_Cmd.Parameters.AddWithValue("ACCOUNT", account);
                    Sql_Cmd.Parameters.AddWithValue("ACC2", acc2);
                    Sql_Data_Reader = Sql_Cmd.ExecuteReader();
                    DTZ[0].Load(Sql_Data_Reader);
                    Sql_Cmd.Dispose();
                    Sql_Conn.Close();
                }
                catch (Exception ex)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = ex.ToString();
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // TRẢ KẾT QUẢ VỀ CHO CLIENT

                Data_Return[0] = "OK";
                Data_Return[1] = Class_Func.Serialize_To_String(DTZ);
                return Class_Func.Serialize_To_String(Data_Return);
            }

            #endregion

            // LỆNH XỬ LÝ GỬI TIN NHẮN RIÊNG TƯ

            #region --- KHỐI LỆNH XỬ LÝ GỬI TIN NHẮN RIÊNG TƯ ---

            if (Command == "send_msg")
            {
                string[] Body_Data = (string[])Class_Func.Deserialize_From_String(Header_Data[1]);
                if (Body_Data.Count() != 6)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = "DỮ LIỆU NHẬN ĐƯỢC KHÔNG ĐẦY ĐỦ";
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // LẤY VÀ KIỂM TRA THÔNG TIN TỪ CLIENT

                string account = Body_Data[0].Trim().ToLower();
                string session_key = Body_Data[1].Trim();
                string acc2 = Body_Data[2].Trim().ToLower();
                string msg = Body_Data[3].Trim();
                string file_name_attachment = Body_Data[4].Trim();
                string data_attachment = Body_Data[5].Trim();

                if (account == "" || session_key == "" || acc2 == "" || msg == "")
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = "DỮ LIỆU NHẬN ĐƯỢC KHÔNG ĐẦY ĐỦ";
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // KIỂM TRA TRẠNG THÁI ONLINE

                if (Check_Session_Online_Active(account, session_key) == false)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = "SESSION_TIME_OUT";
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // NẾU CÓ ĐÌNH KÈM DỮ LIỆU THÌ TIẾN HÀNH LƯU DỮ LIỆU ĐÍNH KÈM TRƯỚC
                // SAU ĐÓ LẤY ID DỮ LIỆU ĐÍNH KÈM VÀ LƯU VÀO TIN NHẮN

                string id_attachment = "";

                if (data_attachment != "")
                {
                    Sql_Query = "INSERT INTO ATTACHMENTS (ACC1, ACC2, FILE_NAME, DATA) OUTPUT INSERTED.ID VALUES (@ACC1, @ACC2, @FILE_NAME, @DATA)";

                    Sql_Conn = new SqlConnection(Sql_Connection_String);
                    try
                    {
                        Sql_Conn.Open();
                        Sql_Cmd = new SqlCommand(Sql_Query, Sql_Conn);
                        Sql_Cmd.Parameters.AddWithValue("ACC1", account);
                        Sql_Cmd.Parameters.AddWithValue("ACC2", acc2);
                        Sql_Cmd.Parameters.AddWithValue("FILE_NAME", file_name_attachment);
                        Sql_Cmd.Parameters.AddWithValue("DATA", data_attachment);
                        id_attachment = Sql_Cmd.ExecuteScalar().ToString();
                        Sql_Cmd.Dispose();
                        Sql_Conn.Close();
                    }
                    catch (Exception ex)
                    {
                        Data_Return[0] = "ERROR";
                        Data_Return[1] = ex.ToString();
                        return Class_Func.Serialize_To_String(Data_Return);
                    }
                }

                // THÊM TIN NHẮN VÀO CSDL

                Sql_Query = "INSERT INTO CHATS (MSG, ACC1, ACC2, ID_ATTACHMENT) VALUES (@MSG, @ACC1, @ACC2, @ID_ATTACHMENT)";

                Sql_Conn = new SqlConnection(Sql_Connection_String);
                try
                {
                    Sql_Conn.Open();
                    Sql_Cmd = new SqlCommand(Sql_Query, Sql_Conn);
                    Sql_Cmd.Parameters.AddWithValue("MSG", msg);
                    Sql_Cmd.Parameters.AddWithValue("ACC1", account);
                    Sql_Cmd.Parameters.AddWithValue("ACC2", acc2);
                    Sql_Cmd.Parameters.AddWithValue("ID_ATTACHMENT", id_attachment);
                    Sql_Cmd.ExecuteNonQuery();
                    Sql_Cmd.Dispose();
                    Sql_Conn.Close();
                }
                catch (Exception ex)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = ex.ToString();
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // TRẢ KẾT QUẢ VỀ CHO CLIENT

                Data_Return[0] = "OK";
                Data_Return[1] = "GỬI TIN NHẮN THÀNH CÔNG";
                return Class_Func.Serialize_To_String(Data_Return);
            }

            #endregion

            // LỆNH XỬ LÝ XÓA TIN NHẮN RIÊNG TƯ

            #region --- KHỐI LỆNH XỬ LÝ XÓA TIN NHẮN CÁ NHÂN ---

            if (Command == "delete_msg")
            {
                string[] Body_Data = (string[])Class_Func.Deserialize_From_String(Header_Data[1]);
                if (Body_Data.Count() != 3)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = "DỮ LIỆU NHẬN ĐƯỢC KHÔNG ĐẦY ĐỦ";
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // LẤY VÀ KIỂM TRA THÔNG TIN TỪ CLIENT

                string account = Body_Data[0].Trim().ToLower();
                string session_key = Body_Data[1].Trim();
                string id_msg = Body_Data[2].Trim();

                if (account == "" || session_key == "" || id_msg == "")
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = "DỮ LIỆU NHẬN ĐƯỢC KHÔNG ĐẦY ĐỦ";
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // KIỂM TRA TRẠNG THÁI ONLINE

                if (Check_Session_Online_Active(account, session_key) == false)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = "SESSION_TIME_OUT";
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // CẬP NHẬT TRẠNG THÁI XÓA TIN NHẮN TRONG CSDL

                Sql_Query = "";
                Sql_Query = Sql_Query + "UPDATE CHATS SET MASK_DELETE_BY_ACC1 = 1 WHERE (ACC1 = @ACCOUNT) AND (ID = @ID);";
                Sql_Query = Sql_Query + "\r\n";
                Sql_Query = Sql_Query + "UPDATE CHATS SET MASK_DELETE_BY_ACC2 = 1 WHERE (ACC2 = @ACCOUNT) AND (ID = @ID);";

                Sql_Conn = new SqlConnection(Sql_Connection_String);
                try
                {
                    Sql_Conn.Open();
                    Sql_Cmd = new SqlCommand(Sql_Query, Sql_Conn);
                    Sql_Cmd.Parameters.AddWithValue("ACCOUNT", account);
                    Sql_Cmd.Parameters.AddWithValue("ID", id_msg);
                    Sql_Cmd.ExecuteNonQuery();
                    Sql_Cmd.Dispose();
                    Sql_Conn.Close();
                }
                catch (Exception ex)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = ex.ToString();
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // TRẢ KẾT QUẢ VỀ CHO CLIENT

                Data_Return[0] = "OK";
                Data_Return[1] = "XÓA TIN NHẮN THÀNH CÔNG";
                return Class_Func.Serialize_To_String(Data_Return);
            }

            #endregion

            // LỆNH XỬ LÝ TẢI FILE ĐÍNH KÈM

            #region --- KHỐI LỆNH XỬ LÝ TẢI FILE ĐÍNH KÈM ---

            if (Command == "download_attachment")
            {
                string[] Body_Data = (string[])Class_Func.Deserialize_From_String(Header_Data[1]);
                if (Body_Data.Count() != 4)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = "DỮ LIỆU NHẬN ĐƯỢC KHÔNG ĐẦY ĐỦ";
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // LẤY VÀ KIỂM TRA THÔNG TIN TỪ CLIENT

                string account = Body_Data[0].Trim().ToLower();
                string session_key = Body_Data[1].Trim();
                string acc2 = Body_Data[2].Trim().ToLower();
                string id_attachment = Body_Data[3].Trim();

                if (account == "" || session_key == "" || id_attachment == "" || acc2 == "")
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = "DỮ LIỆU NHẬN ĐƯỢC KHÔNG ĐẦY ĐỦ";
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // KIỂM TRA TRẠNG THÁI ONLINE

                if (Check_Session_Online_Active(account, session_key) == false)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = "SESSION_TIME_OUT";
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // LẤY DỮ LIỆU ĐÍNH KÈM TỪ CSDL

                Sql_Query = "SELECT FILE_NAME, DATA FROM ATTACHMENTS WHERE (ID = @ID_ATTACHMENT) AND (((ACC1 = @ACCOUNT) AND (ACC2 = @ACC2)) OR ((ACC1 = @ACC2) AND (ACC2 = @ACCOUNT)))";

                DataTable[] DTZ = { new DataTable() };
                Sql_Conn = new SqlConnection(Sql_Connection_String);
                try
                {
                    Sql_Conn.Open();
                    Sql_Cmd = new SqlCommand(Sql_Query, Sql_Conn);
                    Sql_Cmd.Parameters.AddWithValue("ACCOUNT", account);
                    Sql_Cmd.Parameters.AddWithValue("ACC2", acc2);
                    Sql_Cmd.Parameters.AddWithValue("ID_ATTACHMENT", id_attachment);
                    Sql_Data_Reader = Sql_Cmd.ExecuteReader();
                    DTZ[0].Load(Sql_Data_Reader);
                    Sql_Cmd.Dispose();
                    Sql_Conn.Close();
                }
                catch (Exception ex)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = ex.ToString();
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // NẾU KHÔNG TÌM THẤY DỮ LIỆU ĐÍNH KÈM THÌ BÁO LỖI VỀ CHO CLIENT

                if (DTZ[0].Rows.Count == 0)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = "DỮ LIỆU ĐÍNH KÈM KHÔNG TỒN TẠI";
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // NẾU DỮ LIỆU ĐÍNH KÈM LÀ RỖNG THÌ BÁO LỖI VỀ CHO CLIENT

                string Data_Attachment = DTZ[0].Rows[0][0].ToString().Trim();
                if (Data_Attachment == "")
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = "DỮ LIỆU ĐÍNH KÈM BỊ LỖI";
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // TRẢ KẾT QUẢ VỀ CHO CLIENT

                Data_Return[0] = "OK";
                Data_Return[1] = Class_Func.Serialize_To_String(DTZ);
                return Class_Func.Serialize_To_String(Data_Return);
            }

            #endregion

            // LỆNH XỬ LẤY TIN NHẮN CÔNG CỘNG

            #region --- KHỐI LỆNH XỬ LÝ LẤY TIN NHẮN CÔNG CỘNG ---

            if (Command == "get_msg_public")
            {
                string[] Body_Data = (string[])Class_Func.Deserialize_From_String(Header_Data[1]);
                if (Body_Data.Count() != 2)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = "DỮ LIỆU NHẬN ĐƯỢC KHÔNG ĐẦY ĐỦ";
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // LẤY VÀ KIỂM TRA THÔNG TIN TỪ CLIENT

                string account = Body_Data[0].Trim().ToLower();
                string session_key = Body_Data[1].Trim();

                if (account == "" || session_key == "")
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = "DỮ LIỆU NHẬN ĐƯỢC KHÔNG ĐẦY ĐỦ";
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // KIỂM TRA TRẠNG THÁI ONLINE

                if (Check_Session_Online_Active(account, session_key) == false)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = "SESSION_TIME_OUT";
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // LẤY TIN NHẮN TỪ CSDL

                Sql_Query = "SELECT TOP 50 ID, MSG AS 'NỘI DUNG', ACC1 AS 'NGƯỜI GỬI', DATE_TIME_CREATE AS 'GỬI LÚC' FROM CHATS_PUBLIC ";
                Sql_Query = Sql_Query + "\r\n";
                Sql_Query = Sql_Query + "ORDER BY DATE_TIME_CREATE DESC";

                DataTable[] DTZ = { new DataTable() };
                Sql_Conn = new SqlConnection(Sql_Connection_String);
                try
                {
                    Sql_Conn.Open();
                    Sql_Cmd = new SqlCommand(Sql_Query, Sql_Conn);
                    Sql_Data_Reader = Sql_Cmd.ExecuteReader();
                    DTZ[0].Load(Sql_Data_Reader);
                    Sql_Cmd.Dispose();
                    Sql_Conn.Close();
                }
                catch (Exception ex)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = ex.ToString();
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // TRẢ KẾT QUẢ VỀ CHO CLIENT

                Data_Return[0] = "OK";
                Data_Return[1] = Class_Func.Serialize_To_String(DTZ);
                return Class_Func.Serialize_To_String(Data_Return);
            }

            #endregion

            // LỆNH XỬ LÝ GỬI TIN NHẮN CÔNG CỘNG

            #region --- KHỐI LỆNH XỬ LÝ GỬI TIN NHẮN CÔNG CỘNG ---

            if (Command == "send_msg_public")
            {
                string[] Body_Data = (string[])Class_Func.Deserialize_From_String(Header_Data[1]);
                if (Body_Data.Count() != 3)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = "DỮ LIỆU NHẬN ĐƯỢC KHÔNG ĐẦY ĐỦ";
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // LẤY VÀ KIỂM TRA THÔNG TIN TỪ CLIENT

                string account = Body_Data[0].Trim().ToLower();
                string session_key = Body_Data[1].Trim();
                string msg = Body_Data[2].Trim();

                if (account == "" || session_key == "" || msg == "")
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = "DỮ LIỆU NHẬN ĐƯỢC KHÔNG ĐẦY ĐỦ";
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // KIỂM TRA TRẠNG THÁI ONLINE

                if (Check_Session_Online_Active(account, session_key) == false)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = "SESSION_TIME_OUT";
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // THÊM TIN NHẮN VÀO BẢNG CHATS_PUBLIC TRONG CSDL

                Sql_Query = "INSERT INTO CHATS_PUBLIC (MSG, ACC1) VALUES (@MSG, @ACC1)";

                Sql_Conn = new SqlConnection(Sql_Connection_String);
                try
                {
                    Sql_Conn.Open();
                    Sql_Cmd = new SqlCommand(Sql_Query, Sql_Conn);
                    Sql_Cmd.Parameters.AddWithValue("MSG", msg);
                    Sql_Cmd.Parameters.AddWithValue("ACC1", account);
                    Sql_Cmd.ExecuteNonQuery();
                    Sql_Cmd.Dispose();
                    Sql_Conn.Close();
                }
                catch (Exception ex)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = ex.ToString();
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // TRẢ KẾT QUẢ VỀ CHO CLIENT

                Data_Return[0] = "OK";
                Data_Return[1] = "GỬI TIN NHẮN CÔNG CỘNG THÀNH CÔNG";
                return Class_Func.Serialize_To_String(Data_Return);
            }

            #endregion

            // LỆNH XỬ LÝ XÓA TIN NHẮN CÔNG CỘNG

            #region --- KHỐI LỆNH XỬ LÝ XÓA TIN NHẮN CÔNG CỘNG ---

            if (Command == "delete_msg_public")
            {
                string[] Body_Data = (string[])Class_Func.Deserialize_From_String(Header_Data[1]);
                if (Body_Data.Count() != 3)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = "DỮ LIỆU NHẬN ĐƯỢC KHÔNG ĐẦY ĐỦ";
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // LẤY VÀ KIỂM TRA THÔNG TIN TỪ CLIENT

                string account = Body_Data[0].Trim().ToLower();
                string session_key = Body_Data[1].Trim();
                string id_msg = Body_Data[2].Trim();

                if (account == "" || session_key == "" || id_msg == "")
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = "DỮ LIỆU NHẬN ĐƯỢC KHÔNG ĐẦY ĐỦ";
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // KIỂM TRA TRẠNG THÁI ONLINE

                if (Check_Session_Online_Active(account, session_key) == false)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = "SESSION_TIME_OUT";
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // CẬP NHẬT TRẠNG THÁI XÓA TIN NHẮN TRONG CSDL

                Sql_Query = "DELETE FROM CHATS_PUBLIC WHERE (ACC1 = @ACCOUNT) AND (ID = @ID);";

                Sql_Conn = new SqlConnection(Sql_Connection_String);
                try
                {
                    Sql_Conn.Open();
                    Sql_Cmd = new SqlCommand(Sql_Query, Sql_Conn);
                    Sql_Cmd.Parameters.AddWithValue("ACCOUNT", account);
                    Sql_Cmd.Parameters.AddWithValue("ID", id_msg);
                    Sql_Cmd.ExecuteNonQuery();
                    Sql_Cmd.Dispose();
                    Sql_Conn.Close();
                }
                catch (Exception ex)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = ex.ToString();
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // TRẢ KẾT QUẢ VỀ CHO CLIENT

                Data_Return[0] = "OK";
                Data_Return[1] = "XÓA TIN NHẮN CÔNG CỘNG THÀNH CÔNG";
                return Class_Func.Serialize_To_String(Data_Return);
            }

            #endregion

            // LỆNH XỬ LÝ LẤY DANH SÁCH NGƯỜI LẠ

            #region --- KHỐI LỆNH XỬ LÝ LẤY DANH SÁCH NGƯỜI LẠ ---

            if (Command == "get_list_not_friend")
            {
                string[] Body_Data = (string[])Class_Func.Deserialize_From_String(Header_Data[1]);
                if (Body_Data.Count() != 2)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = "DỮ LIỆU NHẬN ĐƯỢC KHÔNG ĐẦY ĐỦ";
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // LẤY VÀ KIỂM TRA THÔNG TIN TỪ CLIENT

                string account = Body_Data[0].Trim().ToLower();
                string session_key = Body_Data[1].Trim();

                if (account == "" || session_key == "")
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = "DỮ LIỆU NHẬN ĐƯỢC KHÔNG ĐẦY ĐỦ";
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // KIỂM TRA TRẠNG THÁI ONLINE

                if (Check_Session_Online_Active(account, session_key) == false)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = "SESSION_TIME_OUT";
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // LẤY DANH SÁCH ACCOUNT BẠN BÈ VÀ TRẠNG THÁI ONLINE TRONG CSDL

                Sql_Query = "";
                Sql_Query = Sql_Query + "SELECT ACCOUNT,";
                Sql_Query = Sql_Query + "\r\n";
                Sql_Query = Sql_Query + "(CASE";
                Sql_Query = Sql_Query + "\r\n";
                Sql_Query = Sql_Query + "WHEN EXISTS (SELECT ACCOUNT FROM SESSIONS WHERE ACCOUNT = [ACCOUNTS].ACCOUNT)";
                Sql_Query = Sql_Query + "\r\n";
                Sql_Query = Sql_Query + "THEN 'Online' ELSE ''";
                Sql_Query = Sql_Query + "\r\n";
                Sql_Query = Sql_Query + "END) AS 'STATUS'";
                Sql_Query = Sql_Query + "\r\n";
                Sql_Query = Sql_Query + "FROM ACCOUNTS";
                Sql_Query = Sql_Query + "\r\n";
                Sql_Query = Sql_Query + "WHERE";
                Sql_Query = Sql_Query + "\r\n";
                Sql_Query = Sql_Query + "([ACCOUNTS].ACCOUNT NOT IN (SELECT ACC2 FROM FRIENDS WHERE (ACC1 = @ACCOUNT)))";
                Sql_Query = Sql_Query + "\r\n";
                Sql_Query = Sql_Query + "AND";
                Sql_Query = Sql_Query + "\r\n";
                Sql_Query = Sql_Query + "([ACCOUNTS].ACCOUNT IN (SELECT ACC1 FROM CHATS WHERE ACC2 = @ACCOUNT))";

                DataTable[] DTZ = { new DataTable() };
                Sql_Conn = new SqlConnection(Sql_Connection_String);
                try
                {
                    Sql_Conn.Open();
                    Sql_Cmd = new SqlCommand(Sql_Query, Sql_Conn);
                    Sql_Cmd.Parameters.AddWithValue("ACCOUNT", account);
                    Sql_Data_Reader = Sql_Cmd.ExecuteReader();
                    DTZ[0].Load(Sql_Data_Reader);
                    Sql_Cmd.Dispose();
                    Sql_Conn.Close();
                }
                catch (Exception ex)
                {
                    Data_Return[0] = "ERROR";
                    Data_Return[1] = ex.ToString();
                    return Class_Func.Serialize_To_String(Data_Return);
                }

                // TRẢ KẾT QUẢ VỀ CHO CLIENT

                Data_Return[0] = "OK";
                Data_Return[1] = Class_Func.Serialize_To_String(DTZ);
                return Class_Func.Serialize_To_String(Data_Return);
            }

            #endregion

            // NẾU KHÔNG CÓ LỆNH NÀO HỢP LỆ THÌ BÁO LỖI VỀ CHO CLIENT

            Data_Return[0] = "ERROR";
            Data_Return[1] = "LỆNH NHẬN ĐƯỢC KHÔNG HỢP LỆ";
            return Class_Func.Serialize_To_String(Data_Return);
        }

        // HÀM TẠO MÁY CHỦ

        private string[] Create_Server(string IP_Server_String, Int32 Port_Server)
        {
            string Data = null;
            byte[] Bytes = new Byte[1048576];
            string Detect_End_Of_Msg = "<EOF>";

            // KHAI BÁO CẤU TRÚC DỮ LIỆU SẼ TRẢ VỀ CHO CLIENT

            string[] Data_Return = { "STATUS", "MSG" };

            // KIỂM TRA IP SERVER

            IPAddress IP_Server;
            if (IPAddress.TryParse(IP_Server_String, out IP_Server) == false)
            {
                Data_Return[0] = "ERROR";
                Data_Return[1] = "IP KHÔNG HỢP LỆ";
                return Data_Return;
            }

            // KIỂM TRA CỔNG ĐÃ CHỌN CÓ ĐANG BỊ SỬ DỤNG HAY CHƯA

            if (Class_Func.Check_Port_Is_Used(IP_Server.ToString(), Port_Server) == true)
            {
                Data_Return[0] = "ERROR";
                Data_Return[1] = "CỔNG BẠN CHỌN ĐÃ ĐƯỢC SỬ DỤNG. XIN HÃY CHỌN CỔNG KHÁC";
                return Data_Return;
            }

            IPEndPoint LocalEndPoint = new IPEndPoint(IP_Server, Port_Server);

            // CREATE A TCP/IP SOCKET

            Listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // LISTEN FOR INCOMING CONNECTIONS

            try
            {
                Listener.Bind(LocalEndPoint);
                Listener.Listen(10);

                // START LISTENING FOR CONNECTIONS

                while (true)
                {
                    // PROGRAM IS SUSPENDED WHILE WAITING FOR AN INCOMING CONNECTION.

                    Socket handler = Listener.Accept();
                    Data = null;

                    // AN INCOMING CONNECTION NEEDS TO BE PROCESSED

                    while (true)
                    {
                        Bytes = new byte[1048576];
                        int bytesRec = handler.Receive(Bytes);
                        Data += Encoding.UTF8.GetString(Bytes, 0, bytesRec);
                        if (Data.IndexOf(Detect_End_Of_Msg) > -1)
                        {
                            break;
                        }
                    }

                    // XỬ LÝ DỮ LIỆU NHẬN ĐƯỢC TỪ CLIENT

                    Data = Process_Data_From_Client(Data.Substring(0, Data.Length - Detect_End_Of_Msg.Length));

                    // TRẢ DỮ LIỆU SAU KHI XỬ LÝ VỀ CHO CLIENT

                    byte[] msg = Encoding.UTF8.GetBytes(Data + Detect_End_Of_Msg);

                    handler.Send(msg);
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
            }
            catch (Exception e)
            {
                Data_Return[0] = "ERROR";
                Data_Return[1] = e.ToString();
            }

            Data_Return[0] = "OK";
            Data_Return[1] = "OK";

            return Data_Return;
        }

        // HÀM KIỂM TRA XEM ACCOUNT CÓ ĐANG ONLINE HAY KHÔNG

        bool Check_Session_Online_Active(string account, string session_key)
        {
            // KIỂM TRA TRẠNG THÁI ONLINE

            Sql_Query = "SELECT * FROM SESSIONS WHERE ACCOUNT = @ACCOUNT AND SESSION_KEY = @SESSION_KEY";

            DT = new DataTable();
            Sql_Conn = new SqlConnection(Sql_Connection_String);
            try
            {
                Sql_Conn.Open();
                Sql_Cmd = new SqlCommand(Sql_Query, Sql_Conn);
                Sql_Cmd.Parameters.AddWithValue("ACCOUNT", account);
                Sql_Cmd.Parameters.AddWithValue("SESSION_KEY", session_key);
                Sql_Data_Reader = Sql_Cmd.ExecuteReader();
                DT.Load(Sql_Data_Reader);
                Sql_Cmd.Dispose();
                Sql_Conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

            if (DT.Rows.Count == 0)
            {
                return false;
            }

            // NẾU ĐANG CÒN ONLINE THÌ CẬP NHẬT THỜI GIAN ACTIVE

            Sql_Query = "UPDATE SESSIONS SET LAST_ACTIVE = GETDATE() WHERE ACCOUNT = @ACCOUNT AND SESSION_KEY = @SESSION_KEY";

            Sql_Conn = new SqlConnection(Sql_Connection_String);
            try
            {
                Sql_Conn.Open();
                Sql_Cmd = new SqlCommand(Sql_Query, Sql_Conn);
                Sql_Cmd.Parameters.AddWithValue("ACCOUNT", account);
                Sql_Cmd.Parameters.AddWithValue("SESSION_KEY", session_key);
                Sql_Cmd.ExecuteNonQuery();
                Sql_Cmd.Dispose();
                Sql_Conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            // SAU ĐÓ TRẢ KẾT QUẢ VỀ CHO CLIENT

            return true;
        }

        public Frm_Main()
        {
            InitializeComponent();
        }

        private void Frm_Main_Load(object sender, EventArgs e)
        {
            txt_ip_server.Text = IP_Server;
            txt_port_server.Text = Port_Server.ToString();
            btn_stop_server.Enabled = false;
        }

        private void btn_start_server_Click(object sender, EventArgs e)
        {
            // LẤY VÀ KIỂM TRA IP SERVER

            IP_Server = txt_ip_server.Text.Trim();
            if (IP_Server == "")
            {
                MessageBox.Show("IP SERVER KHÔNG ĐƯỢC RỖNG","THÔNG BÁO");
                return;
            }

            // LẤY VÀ KIỂM TRA SỐ CỔNG

            Int32.TryParse(txt_port_server.Text.Trim(), out Port_Server);
            if (Port_Server <= 0)
            {
                MessageBox.Show("SỐ CỔNG PHẢI LỚN HƠN 0", "THÔNG BÁO");
                return;
            }

            // KIỂM TRA CỔNG ĐÃ CHỌN CÓ ĐANG BỊ SỬ DỤNG HAY CHƯA

            if (Class_Func.Check_Port_Is_Used(IP_Server, Port_Server) == true)
            {
                MessageBox.Show("CỔNG BẠN CHỌN ĐÃ ĐƯỢC SỬ DỤNG. XIN HÃY CHỌN CỔNG KHÁC", "THÔNG BÁO");
                return;
            }

            // BẮT ĐẦU CHẠY MÁY CHỦ

            if (backgroundWorker1.IsBusy == false)
            {
                backgroundWorker1.RunWorkerAsync();
            }

            // KHÓA CÁC CONTROL

            txt_ip_server.Enabled = false;
            txt_port_server.Enabled = false;
            btn_start_server.Enabled = false;
            btn_stop_server.Enabled = true;

            // GỌI LỆNH 2 BUTTON CẬP NHẬT DANH SÁCH ACCOUNT VÀ ACCOUNT ĐANG ONLINE

            btn_update_list_account_Click(sender, e);
            btn_update_list_account_online_Click(sender, e);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] status;

            status = Create_Server(IP_Server, Port_Server);
            
            if (status[0] == "ERROR")
            {
                btn_stop_server_Click(sender, e);
                MessageBox.Show(status[1], "THÔNG BÁO");
            }
        }

        private void btn_stop_server_Click(object sender, EventArgs e)
        {
            if (Listener != null)
            {
                Listener.Close();
            }

            backgroundWorker1.CancelAsync();
            backgroundWorker1.Dispose();

            this.Invoke(new MethodInvoker(delegate
            {
                txt_ip_server.Enabled = true;
                txt_port_server.Enabled = true;
                btn_start_server.Enabled = true;
                btn_stop_server.Enabled = false;

                list_accounts.DataSource = null;
                list_accounts_online.DataSource = null;
            }));
        }

        // BUTTON LẤY DANH SÁCH ACCOUNT TRONG CSDL

        private void btn_update_list_account_Click(object sender, EventArgs e)
        {
            SqlConnection Sql_Conn;
            SqlCommand Sql_Cmd;
            String Sql_Query;
            SqlDataReader Sql_Data_Reader;
            DataTable DT;

            Sql_Query = "SELECT ACCOUNT FROM ACCOUNTS";

            DT = new DataTable();
            Sql_Conn = new SqlConnection(Sql_Connection_String);
            try
            {
                Sql_Conn.Open();
                Sql_Cmd = new SqlCommand(Sql_Query, Sql_Conn);
                Sql_Data_Reader = Sql_Cmd.ExecuteReader();
                DT.Load(Sql_Data_Reader);
                Sql_Cmd.Dispose();
                Sql_Conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }

            // NẾU DANH SÁCH ACCOUNT CÓ SỰ THAY ĐỔI SO VỚI DANH SÁCH ACCOUNT HIỆN CÓ
            // THÌ CẬP NHẬT LẠI DANH SÁCH ACCOUNT HIỆN CÓ

            if (DT.Rows.Count != list_accounts.Items.Count)
            {
                list_accounts.DataSource = DT;
                list_accounts.DisplayMember = "ACCOUNT";
                list_accounts.ValueMember = "ACCOUNT";
            }
        }

        // BUTTON LẤY DANH SÁCH ACCOUNT ĐANG ONLINE TRONG CSDL

        private void btn_update_list_account_online_Click(object sender, EventArgs e)
        {
            SqlConnection Sql_Conn;
            SqlCommand Sql_Cmd;
            String Sql_Query;
            SqlDataReader Sql_Data_Reader;
            DataTable DT;

            Sql_Query = "";
            Sql_Query = Sql_Query + "DELETE FROM SESSIONS WHERE (DATEDIFF(second, LAST_ACTIVE, GETDATE()) > " + Second_For_Reset_Session_Online + ");";
            Sql_Query = Sql_Query + "\r\n";
            Sql_Query = Sql_Query + "SELECT ACCOUNT FROM SESSIONS;";

            DT = new DataTable();
            Sql_Conn = new SqlConnection(Sql_Connection_String);
            try
            {
                Sql_Conn.Open();
                Sql_Cmd = new SqlCommand(Sql_Query, Sql_Conn);
                Sql_Data_Reader = Sql_Cmd.ExecuteReader();
                DT.Load(Sql_Data_Reader);
                Sql_Cmd.Dispose();
                Sql_Conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }

            // NẾU DANH SÁCH ACCOUNT ONLINE CÓ SỰ THAY ĐỔI SO VỚI DANH SÁCH ACCOUNT ONLINE HIỆN CÓ
            // THÌ CẬP NHẬT LẠI DANH SÁCH ACCOUNT ONLINE HIỆN CÓ

            if (DT.Rows.Count != list_accounts_online.Items.Count)
            {
                list_accounts_online.DataSource = DT;
                list_accounts_online.DisplayMember = "ACCOUNT";
                list_accounts_online.ValueMember = "ACCOUNT";
            }
        }
    }
}
