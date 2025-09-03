using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFramework.Server;

namespace WebFramework.Routing
{
    public abstract class Controller
    {
        public abstract HttpResponse View();
    }
}
