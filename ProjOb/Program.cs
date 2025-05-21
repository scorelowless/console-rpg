namespace ProjOb;

public static class Program
{
    private static async Task Main(string[] args)
    {
        if (args.Length == 0)
        {
            Usage();
            return;
        }

        int parsedPort;
        int port;
        switch (args[0])
        {
            case "--server":
                port = args.Length == 2 && int.TryParse(args[1], out parsedPort) ? parsedPort : 5555;
                Server server = new Server();
                await server.Start(port);
                break;

            case "--client":
                string address;

                if (args.Length > 1)
                {
                    string[] parts = args[1].Split(':');
                    if (parts.Length != 2 || !int.TryParse(parts[1], out parsedPort))
                    {
                        Usage();
                        return;
                    }
                    address = parts[0];
                    port = parsedPort;
                }
                else
                {
                    address = "127.0.0.1";
                    port = 5555;
                }

                Client client = new Client();
                await client.Start(address, port);
                break;

            default:
                Usage();
                break;
        }
    }

    private static void Usage()
    {
        Console.WriteLine("Usage:");
        Console.WriteLine("  --server [port]");
        Console.WriteLine("  --client [address:port]");
    }
}