using PayExpert.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayExpert.DAO
{
    public interface IPayrollServiceRepository
    {
        
        public Payroll GetPayrollById(int payrollId);
        public List<Payroll> GetPayrollsForEmployee(int employeeId);
        List<Payroll> GetPayrollsForPeriod(DateTime startDate, DateTime endDate);
        public Payroll GeneratePayroll(int employeeId, DateTime startDate, DateTime endDate);
    }
}
