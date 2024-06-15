using System;
using System.IO;
using System.Threading;
using System.Net.Sockets;
using System.Text;

namespace TCPTesting
{
    /// <summary>
    /// 
    /// </summary>
    public class TCPTesting
    {
        /// <summary>
        /// 
        /// </summary>
        private static TcpListener server;
        
        /// <summary>
        /// 
        /// </summary>
        private static bool isRunning = false;
        
        /// <summary>
        /// Defines on which port the Program should run for sending and receiving packages
        /// </summary>
        private static int port = 5000;
        
        public static void Main()
        {
            start();
        }
        
        /// <summary>
        /// 
        /// </summary>
        private static void start()
        {
            Thread t = new Thread(HandleConnection);
            t.Start();
        }

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public static void HandleClient(object obj)
        {
            String name;

            //
            TcpClient client = (TcpClient) obj;
            
            //
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
                    Console.WriteLine(name + ": " + data);
                }
            }
        }
        
        
    }
}