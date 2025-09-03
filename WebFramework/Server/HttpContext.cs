using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFramework.Server
{
    public class HttpContext
    {
        public HttpRequest Request;
        public HttpResponse Response;

        public HttpContext()
        {

        }

        public HttpContext(HttpRequest request, HttpResponse response)
        {
            Request = request;
            Response = response;
        }   
    }
}
