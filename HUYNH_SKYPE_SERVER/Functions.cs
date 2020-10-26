using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;

namespace HUYNH_SKYPE_SERVER
{
    public class Functions
    {
        // HÀM CHUYỂN OBJECT THÀNH CHUỖI BASE64

        public string Serialize_To_String(object DATA)
        {
            if (DATA == null)
            {
                return string.Empty;
            }
            else
            {
                System.IO.MemoryStream MEMORY_STREAM = new System.IO.MemoryStream();
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter BINARY_FORMATTER = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                BINARY_FORMATTER.Serialize(MEMORY_STREAM, DATA);
                return System.Convert.ToBase64String(MEMORY_STREAM.GetBuffer());
            }
        }

        // HÀM CHUYỂN CHUỖI BASE64 THÀNH OBJECT

        public object Deserialize_From_String(string BIN_STRING)
        {
            if (BIN_STRING == null)
            {
                return null;
            }
            else
            {
                if (BIN_STRING.Length == 0)
                {
                    return null;
                }
                else
                {
                    try
                    {
                        byte[] BIN_DATA = System.Convert.FromBase64String(BIN_STRING);
                        System.Runtime.Serialization.Formatters.Binary.BinaryFormatter BINARY_FORMATTER = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                        System.IO.MemoryStream MEMORY_STREAM = new System.IO.MemoryStream(BIN_DATA);
                        return BINARY_FORMATTER.Deserialize(MEMORY_STREAM);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        return null;
                    }
                }
            }
        }

        // HÀM DÙNG ĐỂ KIỂM TRA CỔNG ĐƯỢC CHỌN ĐÃ ĐƯỢC SỬ DỤNG CHƯA

        public bool Check_Port_Is_Used(string IP_Server, Int32 Port_Server)
        {
            bool isOpen = false;
            try
            {
                TcpClient TcpClient = new TcpClient();
                TcpClient.Connect(IP_Server, Port_Server);
                isOpen = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                isOpen = false;
            }
            return isOpen;
        }

        // HÀM TẠO MÃ MD5

        public string CREATE_MD5_HASH(string input)
        {
            MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}
