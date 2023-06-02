using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspnetApp.Service.Exceptions
{
    public class AspnetAppException : Exception
    {
        public int Code { get; set; }
        public AspnetAppException(int code,string message) : base(message)
        {
                Code = code;
        }
    }
}
