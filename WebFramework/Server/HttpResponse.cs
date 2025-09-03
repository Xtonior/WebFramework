using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFramework.Server
{
    public class HttpResponse
    {
        public string StatusLine;
        public string Headers;
        public string Body;

        public HttpResponse()
        {

        }

        public HttpResponse(string statusLine, string headers, string body)
        {
            StatusLine = statusLine;
            Headers = headers;
            Body = body;
        }
    }
}
