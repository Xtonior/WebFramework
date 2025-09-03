using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WebFramework.Client
{
    public class SClient
    {
        private TcpClient client;

        public SClient()
        {
            client = new TcpClient();
        }

        public async Task Connect(string address, int port)
        {
            await client.ConnectAsync(address, port);
            Console.WriteLine($"[Client] Connected to {client.Client.RemoteEndPoint}");
        }

        public async Task Send(string message)
        {
            var stream = client.GetStream();
            using var writer = new StreamWriter(stream, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false), leaveOpen: true) { AutoFlush = true };
            await writer.WriteLineAsync(message);
        }

        public async Task Receive()
        {
            var stream = client.GetStream();
            using var reader = new StreamReader(stream, Encoding.UTF8, leaveOpen: true);
            string? line = await reader.ReadLineAsync();
            if (line != null)
            {
                Console.WriteLine($"[Client] Server response: {line}");
            }
        }

        public async Task Disconnect()
        {
            await Send("END");
            client.Close();
            Console.WriteLine("[Client] Disconnected");
        }
    }
}
