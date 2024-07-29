using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CustomExceptions
{
    public class PaymentException : ApplicationException
    {
        public PaymentException(string message)
            : base(message)
        {
        }

        public PaymentException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
