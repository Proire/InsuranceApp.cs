using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CustomExceptions
{
    public class EmployeeSchemeException : ApplicationException
    {
        public EmployeeSchemeException(string message)
            : base(message)
        {
        }

        public EmployeeSchemeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
