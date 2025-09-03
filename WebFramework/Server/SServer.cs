using System;
using System.Diagnostics.Metrics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WebFramework.Modules.CGI;
using WebFramework.Routing;

namespace WebFramework.Server
{
    public class SServer
    {
        private readonly TcpListener listener;

        private Router router;
        private RouteTable routeTable;
        private Cgi cgiHandler;

        public SServer(string addr, int port)
        {
            listener = new TcpListener(IPAddress.Parse(addr), port);
        }

        public void Start()
        {
            listener.Start();

            router = new Router();
            routeTable = AttributeRouteScanner.BuildRouteTable(Assembly.GetExecutingAssembly());
            cgiHandler = new Cgi();
            Console.WriteLine($"[Server] Started at {listener.LocalEndpoint}");
        }

        public async Task Update()
        {
            while (true)
            {
                var client = await listener.AcceptTcpClientAsync();
                Console.WriteLine($"[Server] Connection: {client.Client.RemoteEndPoint}");
                _ = HandleClient(client);
            }
        }

        private async Task HandleClient(TcpClient client)
        {
            using var stream = client.GetStream();

            HttpParser parser = new HttpParser();
            byte[] buffer = new byte[4096];
            int bytesRead;

            bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);

            ReadOnlyMemory<byte> mem = new ReadOnlyMemory<byte>(buffer, 0, bytesRead);

            HttpRequest request = parser.Parse(mem.Span);

            await cgiHandler.Exec(request);

            HttpContext httpContext = router.ResolveRequest(routeTable, request);

            Console.WriteLine($"[Server] Method: {request.Method}, Path: {request.Path}");

            byte[] respBytes = parser.Parse(httpContext.Response, out int nb);

            await stream.WriteAsync(respBytes, 0, nb);
        }
    }
}
