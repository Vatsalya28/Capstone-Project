using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PayExpert.Entities
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Position { get; set; }
        public DateTime? JoiningDate { get; set; }
        public DateTime? TerminationDate { get; set; }

        public override string ToString()
        {
            
            return $"Id:{EmployeeID}\t FirstName:{FirstName}\t LastName:{LastName}\tDate Of Birth:{DateOfBirth}  \tGender:{Gender}\tEmail:{Email}\tPhoneNumber:{PhoneNumber}\tAddress:{Address}\tPosition:{Position}\tJoiningDate:{JoiningDate}\tTerminantion Date:{TerminationDate}";
        
        }
      
    }
}
