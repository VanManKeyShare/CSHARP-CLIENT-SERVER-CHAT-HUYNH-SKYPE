using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;

namespace HUYNH_SKYPE_CLIENT
{
    public class Functions
    {
        // HÀM LẤY NỘI DUNG FILE

        public byte[] Read_All_Bytes(string File_Name)
        {
            byte[] Buffer = null;
            using (System.IO.FileStream Fs = new System.IO.FileStream(File_Name, System.IO.FileMode.Open, System.IO.FileAccess.Read))
            {
                Buffer = new byte[Fs.Length];
                Fs.Read(Buffer, 0, (int)Fs.Length);
            }
            return Buffer;
        }

        // HÀM LẤY TÊN FILE TỪ ĐƯỜNG DẪN

        public string Get_Name_Of_File_Name(string File_Name)
        {
            int Last_Index_Xuyet = File_Name.LastIndexOf("\\");
            if (Last_Index_Xuyet < 0) { Last_Index_Xuyet = 0; }
            return File_Name.Substring(Last_Index_Xuyet + 1);
        }

        // HÀM CHUYỂN ĐỐI TƯỢNG IMAGE THÀNH CHUỐI BASE64

        public string Image_To_Base64(System.Drawing.Image Image)
        {
            using (System.IO.MemoryStream MS = new System.IO.MemoryStream())
            {
                Image.Save(MS, System.Drawing.Imaging.ImageFormat.Png);
                byte[] Image_Bytes = MS.ToArray();
                return Convert.ToBase64String(Image_Bytes);
            }
        }

        // HÀM CHUYỂN CHUỖI BASE64 THÀNH ĐỐI TƯỢNG IMAGE

        public System.Drawing.Image Base64_To_Image(string Base64_String)
        {
            try
            {
                byte[] Image_Bytes = Convert.FromBase64String(Base64_String);
                System.IO.MemoryStream ms = new System.IO.MemoryStream(Image_Bytes, 0, Image_Bytes.Length);
                ms.Write(Image_Bytes, 0, Image_Bytes.Length);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                return image;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        // HÀM THAY ĐỔI SIZE CỦA IMAGE

        public System.Drawing.Image Resize_Image(System.Drawing.Image Img, int Width, int Height)
        {
            System.Drawing.Bitmap Bm = new System.Drawing.Bitmap(Width, Height);
            System.Drawing.Graphics Gp = System.Drawing.Graphics.FromImage((System.Drawing.Image)Bm);
            Gp.DrawImage(Img, 0, 0, Width, Height);
            Gp.Dispose();
            return (System.Drawing.Image)Bm;
        }

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
    }
}
