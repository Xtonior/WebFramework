using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WebFramework.Routing.Attributes;
using WebFramework.Server;

namespace WebFramework.Routing
{
    public static class AttributeRouteScanner
    {
        public static RouteTable BuildRouteTable (Assembly assembly)
        {
            RouteTable routeTable = new RouteTable();

            foreach (var type in assembly.GetTypes())
            {
                foreach (var method in type.GetMethods())
                {
                    var getAttrib = method.GetCustomAttribute<GetAttribute>();

                    if (getAttrib != null)
                    {
                        Console.WriteLine($"[Route Scanner] Adding route: {method.Name}");

                        routeTable.Add(getAttrib.Path, (type, method));
                    }

                    var postAttrib = method.GetCustomAttribute<PostAttribute>();

                    if (postAttrib != null)
                    {
                        Console.WriteLine($"[Route Scanner] Adding route: {method.Name}");

                        routeTable.Add(postAttrib.Path, (type, method));
                    }

                    var autoIndexAttrib = method.GetCustomAttribute<AutoindexAttribute>();

                    if (autoIndexAttrib != null)
                    {
                        Console.WriteLine($"[Route Scanner] Adding route: {method.Name}");

                        routeTable.Add(autoIndexAttrib.Path, (type, method));
                    }
                }
            }

            return routeTable;
        }
    }
}
