using PayExpert.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayExpert.Service
{
    public interface IPayExpertService
    {

        void GetAllEmployees(IEmployeeServiceRepository repository);
        void AddEmployee(IEmployeeServiceRepository repository);
        void UpdateEmployee(IEmployeeServiceRepository repository);
        void RemoveEmployee(IEmployeeServiceRepository repository);
        void GetEmployeeById(IEmployeeServiceRepository repository);

        void GeneratePayroll(IPayrollServiceRepository payrollRepository, IEmployeeServiceRepository employeeRepository);
        void GetPayrollsForEmployee(IPayrollServiceRepository repository);
        void GetPayrollsForPeriod(IPayrollServiceRepository repository);

        void GetTaxById(ITaxServiceRepository repository);
        void GetTaxesForEmployee(ITaxServiceRepository repository);
        void GetTaxForYear(ITaxServiceRepository repository);
        void CalculateTax(ITaxServiceRepository repository);

        void GetFinancialRecordById(IFinancialRecordServiceRepository repository);
        void GetFinancialRecordsForEmployee(IFinancialRecordServiceRepository repository);
        void GetFinancialRecordsForDate(IFinancialRecordServiceRepository repository);
        void AddFinancialRecord(IFinancialRecordServiceRepository repository);

        void CalculateAge(IEmployeeServiceRepository repository);

    }
}
