using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayExpert.Exceptions
{
    public class FinancialRecordException : ApplicationException
    {
        public FinancialRecordException(string message)
            : base(message)
        {
        }
    }
}
