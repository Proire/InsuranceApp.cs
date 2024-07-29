using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CustomExceptions
{
    public class CommissionException : ApplicationException
    {
        public CommissionException(string message)
            : base(message)
        {
        }

        public CommissionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
