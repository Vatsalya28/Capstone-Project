using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayExpert.Entities
{
    public class Tax
    {
        public int TaxID { get; set; }
        public int EmployeeID { get; set; }
        public int TaxYear { get; set; }

       
      public double TaxableIncome { get; set; }
   
        public double TaxAmount { get; set; }

        public override string ToString()
        {
            return $"TaxID: {TaxID}\tEmployeeID: {EmployeeID}\t" +
                   $"TaxYear: {TaxYear}\tTaxableIncome: {TaxableIncome}\tTaxAmount:{TaxAmount}";
        }

    }
    
}
