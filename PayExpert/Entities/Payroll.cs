using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayExpert.Entities
{
    public class Payroll
    {
        public int PayrollID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime PayPeriodStartDate { get; set; }
        public DateTime PayPeriodEndDate { get; set; }
        public double BasicSalary { get; set; }
        public double OvertimePay { get; set; }
        public double Deductions { get; set; }
        public double NetSalary { get; set; }

        public override string ToString()
        {
            return $"PayrollID: {PayrollID}\tEmployeeID: {EmployeeID}\t" +
                   $"PayPeriodStartDate: {PayPeriodStartDate}\tPayPeriodEndDate: {PayPeriodEndDate}\t" +
                   $"BasicSalary: {BasicSalary}\tOvertimePay: {OvertimePay}\t" +
                   $"Deductions: {Deductions}\tNetSalary: {NetSalary}";
        }
    }
}

