using Chat_Threads;

internal class Program
{
    private static async Task Main (string[] args)
    {
        if (args.Length == 0)
        {
           await Server.StartServer();
        }
        else
        {
            // new Thread(() =>
            // {
            //     Client.StartClient(args[0]);
            // }).Start();

            await Client.StartClient(args[0]);
        }
    }
}
