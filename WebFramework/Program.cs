using WebFramework.Client;
using WebFramework.Server;

namespace WebFramework
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var server = new SServer("127.0.0.1", 8888);
            var client = new SClient();

            server.Start();

            var serverTask = server.Update(); // works on background

            await serverTask;
        }
    }
}
