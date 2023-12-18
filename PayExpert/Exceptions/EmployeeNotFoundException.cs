using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayExpert.Entities;
namespace PayExpert.Exceptions
{
    
        
        public class EmployeeNotFoundException : ApplicationException
        {
            public int EmployeeId { get; }

            public EmployeeNotFoundException(int employeeId)
                : base($"Employee with ID {employeeId} not found.")
            {
                EmployeeId = employeeId;
            }
        }
    

}
