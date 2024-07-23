using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CustomExceptions
{
    public class AdminException : Exception
    {
        public AdminException()
        {
        }

        public AdminException(string message)
            : base(message)
        {
        }

        public AdminException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
