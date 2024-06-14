using System;
using System.Net.Sockets;
using System.Text;

namespace TCPCLient
{
    public class TCPClient
    {
        public static void Main()
        {

            String ip = "127.0.0.1";
            int port = 5000;
            
            TestClient client = new TestClient(ip, port);
        }
    }

    public class TestClient
    {
        private TcpClient _client;
        private StreamReader _sReader;
        private StreamWriter _sWriter;

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
            String sData = null;
            while (_isConnected)
            {
                Console.Write("Eingabe ");
                sData = Console.ReadLine();

                _sWriter.WriteLine(sData);
                _sWriter.Flush();
            }
        }
    }
}