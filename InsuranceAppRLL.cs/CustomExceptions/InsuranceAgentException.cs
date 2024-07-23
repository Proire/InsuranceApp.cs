using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CustomExceptions
{
    public class InsuranceAgentException : Exception
    {
        public InsuranceAgentException()
        {
        }

        public InsuranceAgentException(string message)
            : base(message)
        {
        }

        public InsuranceAgentException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
