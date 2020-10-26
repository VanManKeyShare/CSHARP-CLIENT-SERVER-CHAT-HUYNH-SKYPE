using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;

namespace HUYNH_SKYPE_CLIENT
{
    public class Socket_Client
    {
        public string[] Send_Data(string IP_Server_String, Int32 Port_Server, string Data_Send_To_Server)
        {
            string[] Data_Return = { "STATUS", "MSG" };

            string Detect_End_Of_Msg = "<EOF>";

            // DATA BUFFER FOR INCOMING DATA

            byte[] Bytes = new byte[1048576];
            string Data = null;

            // KIỂM TRA IP SERVER

            IPAddress IP_Server;
            if (IPAddress.TryParse(IP_Server_String, out IP_Server) == false)
            {
                Data_Return[0] = "ERROR";
                Data_Return[1] = "IP KHÔNG HỢP LỆ";
                return Data_Return;
            }

            // CONNECT TO A REMOTE DEVICE

            try
            {
                IPEndPoint remoteEP = new IPEndPoint(IP_Server, Port_Server);

                // CREATE A TCP/IP  SOCKET

                Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                // CONNECT THE SOCKET TO THE REMOTE ENDPOINT. CATCH ANY ERRORS
                try
                {
                    sender.Connect(remoteEP);

                    // ENCODE THE DATA STRING INTO A BYTE ARRAY

                    byte[] msg = Encoding.UTF8.GetBytes(Data_Send_To_Server + Detect_End_Of_Msg);

                    // SEND THE DATA THROUGH THE SOCKET

                    int bytesSent = sender.Send(msg);

                    // RECEIVE THE RESPONSE FROM THE REMOTE DEVICE

                    while (true)
                    {
                        Bytes = new byte[1048576];
                        int bytesRec = sender.Receive(Bytes);
                        Data += Encoding.UTF8.GetString(Bytes, 0, bytesRec);
                        if (Data.IndexOf(Detect_End_Of_Msg) > -1)
                        {
                            break;
                        }
                    }

                    // RELEASE THE SOCKET

                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();

                    // TRẢ KẾT QUẢ

                    Data = Data.Substring(0, Data.Length - Detect_End_Of_Msg.Length);

                    Data_Return[0] = "OK";
                    Data_Return[1] = Data;
                    return Data_Return;
                }
                catch (ArgumentNullException anex)
                {
                    Console.WriteLine(anex.ToString());

                    Data_Return[0] = "ERROR";
                    Data_Return[1] = "KẾT NỐI MÁY CHỦ THẤT BẠI";
                    return Data_Return;
                }
                catch (SocketException sex)
                {
                    Console.WriteLine(sex.ToString());

                    Data_Return[0] = "ERROR";
                    Data_Return[1] = "KẾT NỐI MÁY CHỦ THẤT BẠI";
                    return Data_Return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());

                    Data_Return[0] = "ERROR";
                    Data_Return[1] = "KẾT NỐI MÁY CHỦ THẤT BẠI";
                    return Data_Return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                Data_Return[0] = "ERROR";
                Data_Return[1] = "KẾT NỐI MÁY CHỦ THẤT BẠI";
                return Data_Return;
            }
        }
    }
}
