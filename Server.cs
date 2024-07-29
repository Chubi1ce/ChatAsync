using System.IO.Compression;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Chat_Threads;

internal class Server
{
    private static bool exitReqquested = false;
    public static async Task StartServer()
    {
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);
        UdpClient client = new UdpClient(12345);
        Console.WriteLine("Waiting incomming messages. Press 'Esc' for exit.");

        CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
        CancellationToken token = cancelTokenSource.Token;

        while (!exitReqquested)
        {
            // ConsoleKeyInfo keyInfo = Console.ReadKey();
            // if (keyInfo.Key == ConsoleKey.Escape)
            // {
            //     break;
            // }

            // Task task = new Task(() =>
            // {
            //     Console.WriteLine(msg.ToString());
            //     Message msgToClient = new Message("server", "message delivered", DateTime.Now);
            //     if (token.IsCancellationRequested)
            //     {
            //         Console.WriteLine("Операция прервана");
            //         return;
            //     }
            //     string js = msgToClient.ConvertToJSON();
            //     byte[] bytes = Encoding.UTF8.GetBytes(js);
            //     await client.SendAsync(bytes, endPoint);
            // }, token);

            try
            {
                var data = client.Receive(ref endPoint);
                string str = Encoding.UTF8.GetString(data);
                Message? msg = Message.ConvertFromJSON(str);

                if (msg != null)
                {
                    if (msg.Text.ToLower() == Client.stopWord.ToLower())
                    {
                        cancelTokenSource.Cancel();
                    }

                    await Task.Run(async () =>
                    {
                        Console.WriteLine(msg.ToString());
                        Message msgToClient = new Message("server", "message delivered", DateTime.Now);
                        if (token.IsCancellationRequested)
                        {
                            if (token.IsCancellationRequested)
                                token.ThrowIfCancellationRequested();
                        }
                        string js = msgToClient.ConvertToJSON();
                        byte[] bytes = Encoding.UTF8.GetBytes(js);
                        await client.SendAsync(bytes, endPoint);
                    }, token);
                }
                else
                {
                    Console.WriteLine("Huston, we have a problem!!!");
                }

            }
            catch (Exception ex)
            {
                if (ex is TaskCanceledException)
                {
                    Console.WriteLine("Server stopped...");
                    break;
                }
                else
                    Console.WriteLine(ex.Message);
            }
        }
    }
}
