using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayExpert.Exceptions
{
    public class TaxCalculationException : ApplicationException
    {
        public TaxCalculationException(string message)
            : base(message)
        {
        }
    }
}
