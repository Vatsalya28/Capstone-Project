using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayExpert.Exceptions
{
    public class DatabaseConnectionException : ApplicationException
    {

        public DatabaseConnectionException(string message) : base(message)
        {
        }
        public DatabaseConnectionException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
