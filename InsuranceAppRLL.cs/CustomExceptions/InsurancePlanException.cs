using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CustomExceptions
{
    public class InsurancePlanException: ApplicationException
    {

        public InsurancePlanException(string message)
            : base(message)
        {
        }

        public InsurancePlanException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
