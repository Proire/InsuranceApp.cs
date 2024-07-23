using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CustomExceptions
{
    public class PolicyException : ApplicationException
    {
        public PolicyException(string message)
            : base(message)
        {
        }

        public PolicyException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
