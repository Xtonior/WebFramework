using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WebFramework.Server;

namespace WebFramework.Routing
{
    public class RouteTable
    {
        private readonly Dictionary<string, (Type ControllerType, MethodInfo Method)> routingTable = new();

        public void Add(string path, (Type ControllerType, MethodInfo Method) handler)
        {
            routingTable.Add($"{path}", handler);
        }

        public (Type ControllerType, MethodInfo Method)? Match(string path)
            => routingTable.TryGetValue($"{path}", out var handler) ? handler : null;
    }
}
