using System.Net.Sockets;

namespace Chat_Threads;

internal class Chat
    {
        private UdpClient? client;

        public void UDP_Chat_Threads(string ip, int port)
        {
            client = new UdpClient();
            client.Connect(ip, port);
        }
    }
