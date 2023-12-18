using PayExpert.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayExpert.DAO
{
    public interface IFinancialRecordServiceRepository
    {
        public FinancialRecord GetFinancialRecordById(int recordId);
        public List<FinancialRecord> GetFinancialRecordsForDate(DateTime recordDate);
        public List<FinancialRecord> GetFinancialRecordsForEmployee(int employeeId);
        public void AddFinancialRecord(int employeeId, string description, double amount, string recordType, DateTime recordDate);


    }
}
