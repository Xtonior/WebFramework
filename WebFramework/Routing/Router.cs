using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WebFramework.Controllers;
using WebFramework.Server;

namespace WebFramework.Routing
{
    public class Router
    {
        public HttpContext ResolveRequest(RouteTable routeTable, HttpRequest request)
        {
            HttpResponse response;
            HttpContext httpContext;
            httpContext = new HttpContext();

            var handler = routeTable.Match(request.Path);

            if (handler != null)
            {
                var controllerType = handler.Value.ControllerType;
                var controllerInstance = Activator.CreateInstance(controllerType);

                try
                {
                    response = (HttpResponse)handler.Value.Method.Invoke(controllerInstance, null);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Controller threw: " + ex.InnerException?.Message);
                    throw;
                }
            }
            else
            {
                Console.WriteLine($"[Router] Couldn't find suitable handler for request {httpContext.Request.Method}:{httpContext.Request.Path}");

                response = new HttpResponse();
                response.StatusLine = "HTTP/1.1 404 Not Found";
                response.Body = File.ReadAllText("View/404.html");
            }

            httpContext.Request = request;
            httpContext.Response = response;
            return httpContext;
        }
    }
}
