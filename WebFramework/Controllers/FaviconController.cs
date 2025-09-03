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
    public class FaviconController : Controller
    {
        [Get("/favicon.ico")]
        public override HttpResponse View()
        {
            string body = $"not implemented yet";
            string headers =
                $"Content-Length: {Encoding.UTF8.GetByteCount(body)}" +
                "Content-Type: text/plain";

            return new HttpResponse
            (
                "HTTP/1.1 404 NOT FOUND",
                headers,
                body
            );
        }
    }
}
