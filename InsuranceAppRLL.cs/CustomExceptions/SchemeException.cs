using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CustomExceptions
{
    public class SchemeException : ApplicationException
    {
        public SchemeException(string message)
            : base(message)
        {
        }

        public SchemeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
