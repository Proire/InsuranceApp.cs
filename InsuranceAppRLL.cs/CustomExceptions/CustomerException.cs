using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CustomExceptions
{
    public class CustomerException : Exception
    {
        public CustomerException()
        {
        }

        public CustomerException(string message)
            : base(message)
        {
        }

        public CustomerException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
