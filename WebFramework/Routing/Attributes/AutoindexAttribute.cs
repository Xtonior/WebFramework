using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFramework.Routing.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AutoindexAttribute : Attribute
    {
        public string Path { get; }

        public AutoindexAttribute(string path)
        {
            Path = path;
        }
    }
}
