using System;
using System.IO;
using System.Threading;
using System.Net.Sockets;
using System.Text;

namespace TCPTesting
{
    public class TCPTesting
    {
        private static TcpListener server;
        private static bool isRunning = false;
        private static int port = 5000;
        
        public static void Main()
        {
            start();
        }

        private static void start()
        {
            Thread t = new Thread(HandleConnection);
            t.Start();
        }

        public static void HandleConnection()
        {
            server = new TcpListener(port);
            server.Start();


            while (!isRunning)
            {
                TcpClient newClient = server.AcceptTcpClient();
                Thread t = new Thread(new ParameterizedThreadStart(HandleClient));
                t.Start(newClient);
            }
        }

        public static void HandleClient(object obj)
        {
            String name;

            TcpClient client = (TcpClient) obj;
            StreamWriter writer = new StreamWriter(client.GetStream(), Encoding.ASCII);
            StreamReader reader = new StreamReader(client.GetStream(), Encoding.ASCII);

            bool clientIsConnected = true;
            string data = String.Empty;

            name = reader.ReadLine();

            while (clientIsConnected)
            {
                data = reader.ReadLine();
                if (data != null)
                {
                    Console.WriteLine("Client " + name + " " + data);
                }
            }
        }
        
        
    }
}