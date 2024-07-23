using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModelLayer
{
    public class ResponseModel<T>
    {
        public bool Status { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public T Data { get; set; }
    }
}
