using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayExpert.Entities
{
    public class FinancialRecord
    {
        public int RecordID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime RecordDate { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public string RecordType { get; set; }

        public override string ToString()
        {
            return $"RecordID: {RecordID}\t EmployeeID: {EmployeeID}\t RecordDate: {RecordDate}\t Description: {Description}\t Amount: {Amount}\t RecordType: {RecordType}";
        }
    }
}
