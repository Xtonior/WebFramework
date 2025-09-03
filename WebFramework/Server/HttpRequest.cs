using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFramework.Server
{
    public struct HttpRequest
    {
        public string Method;
        public string Path;
        public string Version;
        public string Headers;
        public string Body;
    }
}
