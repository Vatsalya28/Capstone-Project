using System;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayExpert.Entities;

namespace PayExpert.DAO
{
    public interface IEmployeeServiceRepository
    {
        List<Employee> GetAllEmployees();
        public Employee GetEmployeeById(int employeeId);
        public void RemoveEmployee(int employeeId);
        public void UpdateEmployee(Employee updatedEmployeeData);
        public int AddEmployee(Employee employeeData);
        public int CalculateAgeForEmployee(int employeeId);
    }
}
