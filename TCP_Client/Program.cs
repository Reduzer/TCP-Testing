using System;
using System.Net.Sockets;
using System.Text;

namespace TCPCLient
{
    public class TCPClient
    {
        private static TestClient client;
        
        public static void Main()
        {
            String ip = "192.168.178.37";
            int port = 5000;
            
            client = new TestClient(ip, port);
        }
        
    }

    public class TestClient
    {
        private TcpClient _client;
        private StreamReader _sReader;
        private StreamWriter _sWriter;

        private string message;
        
        private bool bFirstConnection = true;
        private bool _isConnected;

        public TestClient(String ipaddress, int portnumber)
        {
            _client = new TcpClient();
            _client.Connect(ipaddress, portnumber);
            
            HandleCommunication();
        }

        public void HandleCommunication()
        {
            _sReader = new StreamReader(_client.GetStream(), Encoding.ASCII);
            _sWriter = new StreamWriter(_client.GetStream(), Encoding.ASCII);

            _isConnected = true;
            String sData = String.Empty;
            
            while (_isConnected)
            {
                if (bFirstConnection)
                {
                    message = "Username: ";
                    bFirstConnection = false;
                }
                else
                {
                    message = "Message: ";
                }
                
                Console.Write(message);
                sData = Console.ReadLine();
                
                _sWriter.WriteLine(sData);
                _sWriter.Flush();
            }
        }
    }
}