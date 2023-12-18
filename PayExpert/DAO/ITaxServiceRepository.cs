using PayExpert.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayExpert.DAO
{
    public interface ITaxServiceRepository
    {
        public Tax GetTaxForId(int taxID);
        public List<Tax> GetTaxesForEmployee(int employeeId);
        public List<Tax> GetTaxForYear(int taxYear);
        public double CalculateTax(int employeeId, int taxYear);
    }
}
