using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebFramework.Server   
{
    public class HttpParser
    {
        public HttpRequest Parse(ReadOnlySpan<byte> data)
        {
            HttpRequest request = new HttpRequest();

            int lineEnd = data.IndexOf((byte)'\n');
            if (lineEnd < 0)
            {
                throw new Exception("Invalid request");
            }

            var requestLine = data.Slice(0, lineEnd).TrimEnd((byte)'\r');

            int firstSpace = requestLine.IndexOf((byte)' ');
            int secondSpace = requestLine.Slice(firstSpace + 1).IndexOf((byte)' ') + firstSpace + 1;

            request.Method = Encoding.ASCII.GetString(requestLine.Slice(0, firstSpace));
            request.Path = Encoding.ASCII.GetString(requestLine.Slice(firstSpace + 1, secondSpace - firstSpace - 1));
            request.Version = Encoding.ASCII.GetString(requestLine.Slice(secondSpace + 1));

            return request;
        }

        public byte[] Parse(HttpResponse response, out int numBytes)
        {
            string responsePlain =
            $"{response.StatusLine}\r\n" +
            $"{response.Headers}\r\n" +
            "\r\n" +
            (response.Body ?? "");

            responsePlain.Replace("^\uFEFF", "");

            byte[] bytes = Encoding.UTF8.GetBytes(responsePlain);
            numBytes = bytes.Length;
            return bytes;
        }
    }
}
