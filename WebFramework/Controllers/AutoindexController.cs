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
    public class AutoindexController : Controller
    {
        [Get("/")]
        public override HttpResponse View()
        {
            string[] files = Directory.GetFiles("View/");
            string[] subdirectories = Directory.GetDirectories("View/");

            string[] allItems = files.Concat(subdirectories).ToArray();
            string combinedListString = string.Join(Environment.NewLine, allItems);
            string body = combinedListString;

            byte[] bodyBytes = Encoding.UTF8.GetBytes(body);
            string header = $"Content-Length: {bodyBytes.Length}";

            return new HttpResponse
            (
                "HTTP/1.1 200 OK",
                header,
                body
            );
        }
    }
}
