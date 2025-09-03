using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFramework.Routing;
using WebFramework.Routing.Attributes;
using WebFramework.Server;

namespace WebFramework.Controllers
{
    public class IndexController : Controller
    {
        [Get("/index")]
        public override HttpResponse View()
        {
            string body = File.ReadAllText("View/index/index.html");

            byte[] bodyBytes = Encoding.UTF8.GetBytes(body);
            string header = $"Content-Length: {bodyBytes.Length}";

            return new HttpResponse
            (
                "HTTP/1.1 200 OK",
                header,
                body
            );
        }

        [Post("/get-file")]
        public void RouteTo(HttpRequest request)
        {
            using var reader = new StreamReader(request.Body);
            string body = reader.ReadToEnd();
            Console.WriteLine($"Получены данные: {body}");
        }
    }
}
