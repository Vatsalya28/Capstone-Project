using PayExpert.DAO;
using PayExpert.Entities;
using PayExpert.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayExpert.Service
{
    public class PayExpertService:IPayExpertService
    { 
        public void GetAllEmployees(IEmployeeServiceRepository repository)
        {
            var employees = repository.GetAllEmployees();
            foreach (var employee in employees)
            {
                Console.WriteLine($"ID: {employee.EmployeeID}");
                Console.WriteLine($"Name: {employee.FirstName} {employee.LastName}");
                Console.WriteLine($"Date Of Birth: {FormatDate(employee.DateOfBirth)}");
                Console.WriteLine($"Gender: {employee.Gender}");
                Console.WriteLine($"Email: {employee.Email}");
                Console.WriteLine($"Phone Number: {employee.PhoneNumber}");
                Console.WriteLine($"Address: {employee.Address}");
                Console.WriteLine($"Position: {employee.Position}");
                Console.WriteLine($"Joining Date: {FormatDate(employee.JoiningDate)}");

                if (employee.TerminationDate.HasValue)
                {
                    Console.WriteLine($"Termination Date: {FormatDate(employee.TerminationDate.Value)}");
                }

                Console.WriteLine(); 
            }
        }

        private string FormatDate(DateTime? date)
        {
            return date.HasValue ? date.Value.ToShortDateString() : "N/A";
        }




        public void AddEmployee(IEmployeeServiceRepository repository)
        {

            Employee newEmployee = new Employee();

            Console.Write("Enter First Name: ");
            newEmployee.FirstName = Console.ReadLine();

            Console.Write("Enter Last Name: ");
            newEmployee.LastName = Console.ReadLine();
            Console.Write("Enter DOB (YYYY-MM-DD): ");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime dob))
            {
                newEmployee.DateOfBirth = dob;
            }
            else
            {
                Console.WriteLine("Invalid date format. Please enter the date in the correct format (YYYY-MM-DD).");

            }

            Console.Write("Enter Gender: ");
            newEmployee.Gender = Console.ReadLine();

            Console.Write("Enter Email: ");
            newEmployee.Email = Console.ReadLine();

            Console.Write("Enter Phone Number: ");
            newEmployee.PhoneNumber = Console.ReadLine();

            Console.Write("Enter Address: ");
            newEmployee.Address = Console.ReadLine();

            Console.Write("Enter Position: ");
            newEmployee.Position = Console.ReadLine();

            Console.Write("Enter Joining Date (YYYY-MM-DD): ");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime jd))
            {
                newEmployee.JoiningDate = jd;
            }
            else
            {
                Console.WriteLine("Invalid date format. Please enter the date in the correct format (YYYY-MM-DD).");

            }
            try
            {
                repository.AddEmployee(newEmployee);
                Console.WriteLine("Employee added successfully.");
            }
            catch (DatabaseConnectionException ex)
            {
                Console.WriteLine($"Error adding employee: {ex.Message}");
                
            }


        }

        public  void UpdateEmployee(IEmployeeServiceRepository repository)
        {
            try
            {
                Console.Write("Enter Employee ID to update: ");
                if (int.TryParse(Console.ReadLine(), out int employeeId))
                {
                    Employee existingEmployee = repository.GetEmployeeById(employeeId);

                    if (existingEmployee != null)
                    {
                        Console.WriteLine($"Current Details for Employee ID {existingEmployee.EmployeeID}:");
                        Console.WriteLine($"1. First Name: {existingEmployee.FirstName}");
                        Console.WriteLine($"2. Last Name: {existingEmployee.LastName}");
                        Console.WriteLine($"3. Gender: {existingEmployee.Gender}");
                        Console.WriteLine($"4. Email: {existingEmployee.Email}");
                        Console.WriteLine($"5. Phone Number: {existingEmployee.PhoneNumber}");
                        Console.WriteLine($"6. Address: {existingEmployee.Address}");
                        Console.WriteLine($"7. Position: {existingEmployee.Position}");


                        Console.WriteLine("\nEnter updated information:");

                        Console.Write("New First Name: ");
                        string newFirstName = Console.ReadLine();
                        if (!string.IsNullOrEmpty(newFirstName))
                        {
                            existingEmployee.FirstName = newFirstName;
                        }


                        Console.Write("New Last Name: ");
                        string newLastName = Console.ReadLine();
                        if (!string.IsNullOrEmpty(newLastName))
                        {
                            existingEmployee.LastName = newLastName;
                        }
                        Console.Write("New Gender: ");
                        string newGender = Console.ReadLine();
                        if (!string.IsNullOrEmpty(newGender))
                        {
                            existingEmployee.Gender = newGender;
                        }

                        Console.Write("New Email: ");
                        string newEmail = Console.ReadLine();
                        if (!string.IsNullOrEmpty(newEmail))
                        {
                            existingEmployee.Email = newEmail;
                        }

                        Console.Write("New Phone Number: ");
                        string newPhoneNumber = Console.ReadLine();
                        if (!string.IsNullOrEmpty(newPhoneNumber))
                        {
                            existingEmployee.PhoneNumber = newPhoneNumber;
                        }

                        Console.Write("New Address: ");
                        string newAddress = Console.ReadLine();
                        if (!string.IsNullOrEmpty(newAddress))
                        {
                            existingEmployee.Address = newAddress;
                        }

                        Console.Write("New Position: ");
                        string newPosition = Console.ReadLine();
                        if (!string.IsNullOrEmpty(newPosition))
                        {
                            existingEmployee.Position = newPosition;
                        }

                        try
                        {

                            repository.UpdateEmployee(existingEmployee);
                            Console.WriteLine("Employee updated successfully.");
                        }
                        catch (DatabaseConnectionException ex)
                        {
                            Console.WriteLine($"Error adding employee: {ex.Message}");

                        }
                    } }
                else
                {
                    throw new InvalidInputException("Invalid Employee ID.");
                }
            }
            catch (EmployeeNotFoundException ex)
            {
                Console.WriteLine($"Employee not found with ID: {ex.EmployeeId}");
            }
            catch(InvalidInputException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public void RemoveEmployee(IEmployeeServiceRepository repository)
        {
            try
            {
                Console.Write("Enter Employee ID to remove: ");
                if (int.TryParse(Console.ReadLine(), out int employeeId))
                {
                    repository.RemoveEmployee(employeeId);
                    Console.WriteLine("Employee removed successfully.");
                }
                else
                {
                    throw new InvalidInputException("Invalid employee ID input.");
                }
            }
            catch(InvalidInputException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (EmployeeNotFoundException ex)
            {
                Console.WriteLine($"Employee not found with ID: {ex.EmployeeId}");
            }
            catch (DatabaseConnectionException ex)
            {
                Console.WriteLine($"Error removing employee: {ex.Message}");
            }

        }

        public void GetEmployeeById(IEmployeeServiceRepository repository)
        {
            try
            {
                Console.Write("Enter Employee ID to view: ");
                if (int.TryParse(Console.ReadLine(), out int employeeId))
                {
                    Employee employee = repository.GetEmployeeById(employeeId);

                    if (employee != null)
                    {
                        Console.WriteLine($"ID: {employee.EmployeeID}, Name: {employee.FirstName} {employee.LastName},Date Of Birth:{employee.DateOfBirth},Gender:{employee.Gender},Phone Number:{employee.PhoneNumber},Address:{employee.Address}, Position: {employee.Position}");
                    }

                }
                else
                {
                    throw new InvalidInputException("Invalid Employee ID.");
                }
            }
            catch (InvalidInputException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (EmployeeNotFoundException ex)
            {
                Console.WriteLine($"Employee not found with ID: {ex.EmployeeId}");
            }
            catch (DatabaseConnectionException ex)
            {
                Console.WriteLine($"Database connection error: {ex.Message}");

            }

        }

        public void GeneratePayroll(IPayrollServiceRepository payrollRepository, IEmployeeServiceRepository employeeRepository)
        {
            try
            {
                Console.Write("Enter Employee ID for payroll generation: ");
                if (int.TryParse(Console.ReadLine(), out int employeeId))
                {
                    Employee employee = employeeRepository.GetEmployeeById(employeeId);

                    if (employee != null)
                    {
                        Console.Write("Enter Pay Period Start Date (yyyy-MM-dd): ");
                        if (DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
                        {
                            Console.Write("Enter Pay Period End Date (yyyy-MM-dd): ");
                            if (DateTime.TryParse(Console.ReadLine(), out DateTime endDate))
                            {
                              
                                    Payroll generatedPayroll = payrollRepository.GeneratePayroll(employeeId, startDate, endDate);
                                    Console.WriteLine("Payroll generated successfully:");
                                    Console.WriteLine($"Employee ID: {generatedPayroll.EmployeeID}");
                                    Console.WriteLine($"Payroll ID: {generatedPayroll.PayrollID}");
                                    Console.WriteLine($"Start Date: {generatedPayroll.PayPeriodStartDate.ToShortDateString()}");
                                    Console.WriteLine($"End Date: {generatedPayroll.PayPeriodEndDate.ToShortDateString()}");
                                    Console.WriteLine($"Basic Salary: {generatedPayroll.BasicSalary}");
                                    Console.WriteLine($"Overtime Pay: {generatedPayroll.OvertimePay}");
                                    Console.WriteLine($"Deductions: {generatedPayroll.Deductions}");
                                    Console.WriteLine($"Net Salary: {generatedPayroll.NetSalary}");
                                
                               
                            }
                            else
                            {
                                Console.WriteLine("Invalid End Date format.");
                          
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Start Date format.");
                          
                        }
                    }

                }
            }
            catch (EmployeeNotFoundException ex)
            {
                Console.WriteLine($"Employee not found: {ex.Message}");
            }
            catch(PayrollGenerationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (DatabaseConnectionException ex)
            {
                Console.WriteLine($"Error adding employee: {ex.Message}");

            }
        }
        public void GetPayrollsForEmployee(IPayrollServiceRepository repository)
        {
            try
            {
                Console.Write("Enter Employee ID to view payrolls: ");
                if (int.TryParse(Console.ReadLine(), out int employeeId))
                {
                    var payrolls = repository.GetPayrollsForEmployee(employeeId);

                    if (payrolls.Count > 0)
                    {
                        foreach (var payroll in payrolls)
                        {

                            Console.WriteLine($"Payroll ID: {payroll.PayrollID},Employee ID:{payroll.EmployeeID},Basic Salary:{payroll.BasicSalary} ,Net Salary: {payroll.NetSalary}");
                        }
                    }
                    else
                    {
                        throw new EmployeeNotFoundException("No employee");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Input");
                }
            }
            catch (EmployeeNotFoundException ex)
            {
               
                Console.WriteLine($"Employee not found: {ex.Message}");
            }
            
        }

        public  void GetPayrollsForPeriod(IPayrollServiceRepository repository)
        {
            try
            {
                Console.Write("Enter Start Date for payrolls (dd/mm/yyyy): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
                {
                    Console.Write("Enter End Date for payrolls (dd/mm/yyyy): ");
                    if (DateTime.TryParse(Console.ReadLine(), out DateTime endDate))
                    {
                        var payrolls = repository.GetPayrollsForPeriod(startDate, endDate);

                        if (payrolls.Count > 0)
                        {
                            foreach (var payroll in payrolls)
                            {

                                Console.WriteLine($"Payroll ID: {payroll.PayrollID}, Net Salary: {payroll.NetSalary}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No payrolls found for the given period.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid End Date format.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Start Date format.");
                }
            }
            catch (DatabaseConnectionException ex)
            {
                Console.WriteLine($"Error adding employee: {ex.Message}");

            }
        }
        public void GetTaxById(ITaxServiceRepository repository)
        {
            try
            {
                Console.WriteLine("Enter Tax ID to view:");
                if (int.TryParse(Console.ReadLine(), out int taxId))
                {
                    Tax tax = repository.GetTaxForId(taxId);
                    if (tax != null)
                    {
                        Console.WriteLine($"Tax Id:{tax.TaxID},Employee ID:{tax.EmployeeID},Tax Year:{tax.TaxYear},Taxable Income:{tax.TaxableIncome}");
                    }
                    else
                    {
                        Console.WriteLine("Tax Id not found");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Tax Id");
                }
            }
            catch (DatabaseConnectionException ex)
            {
                Console.WriteLine($"Error adding : {ex.Message}");

            }
        }
        public  void GetTaxesForEmployee(ITaxServiceRepository repository)
        {
            try
            {
                Console.Write("Enter Employee ID to view Tax: ");
                if (int.TryParse(Console.ReadLine(), out int employeeId))
                {
                    var taxes = repository.GetTaxesForEmployee(employeeId);

                    if (taxes.Count > 0)
                    {
                        foreach (var tax in taxes)
                        {

                            Console.WriteLine($"Tax ID: {tax.TaxID},Employee ID:{tax.EmployeeID},Tax Year:{tax.TaxYear} ,Taxable Income: {tax.TaxableIncome}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Tax Record found for the given employee.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Employee ID.");
                }
            }
            catch (DatabaseConnectionException ex)
            {
                Console.WriteLine($"Error adding : {ex.Message}");

            }
        }
        public void GetTaxForYear(ITaxServiceRepository repository)
        {
            try
            {
                Console.WriteLine("Enter Tax Year to view:");
                if (int.TryParse(Console.ReadLine(), out int taxYear))
                {
                    var taxes = repository.GetTaxForYear(taxYear);
                    Console.WriteLine($"Number of records for the year: {taxes.Count}");

                    if (taxes != null && taxes.Any())
                    {
                        foreach (var tax in taxes)
                        {
                            Console.WriteLine($"Tax Id:{tax.TaxID}, Employee ID:{tax.EmployeeID}, Tax Year:{tax.TaxYear}, Taxable Income:{tax.TaxableIncome}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No records for this year");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Tax Year");
                }
            }
            catch (DatabaseConnectionException ex)
            {
                Console.WriteLine($"Error adding : {ex.Message}");

            }
        }

        public void CalculateTax(ITaxServiceRepository repository)
        {
            try
            {
                Console.WriteLine("Enter Employee ID:");
                if (int.TryParse(Console.ReadLine(), out int employeeId))
                {
                    Console.WriteLine("Enter Tax Year:");
                    if (int.TryParse(Console.ReadLine(), out int taxYear))
                    {
                        double totalTax = repository.CalculateTax(employeeId, taxYear);
                        Console.WriteLine($"Total Tax for Employee ID {employeeId} in Tax Year {taxYear}: {totalTax}");
                    }
                    else
                    {
                        Console.WriteLine("Invalid Tax Year input.");
                        throw new InvalidInputException($"Invalid Tax Year: {taxYear}");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Employee ID input.");
                 
                }
            }
            catch (TaxCalculationException ex)
            {
                Console.WriteLine($"Tax calculation error: {ex.Message}");
            }
            catch (DatabaseConnectionException ex)
            {
                Console.WriteLine($"Error adding : {ex.Message}");

            }


        }

        public void GetFinancialRecordById(IFinancialRecordServiceRepository repository)
        {
            Console.WriteLine("Enter Financial Record ID to view:");
            if (int.TryParse(Console.ReadLine(), out int recordId))
            {
                FinancialRecord financialRecord = repository.GetFinancialRecordById(recordId);
                if (financialRecord != null)
                {
                    Console.WriteLine($"Record Id:{financialRecord.RecordID}, Employee ID:{financialRecord.EmployeeID}, Record Date:{financialRecord.RecordDate}, Description:{financialRecord.Description}, Amount:{financialRecord.Amount}, Record Type:{financialRecord.RecordType}");
                }
                else
                {
                    Console.WriteLine("Financial Record Id not found");
                }
            }
            else
            {
                Console.WriteLine("Invalid Financial Record Id");
            }
        }


        public void GetFinancialRecordsForEmployee(IFinancialRecordServiceRepository repository)
        {
            Console.WriteLine("Enter Employee ID to view financial records:");
            if (int.TryParse(Console.ReadLine(), out int employeeId))
            {
                var recordsForEmployee = repository.GetFinancialRecordsForEmployee(employeeId);

                if (recordsForEmployee.Count > 0)
                {
                    foreach (var record in recordsForEmployee)
                    {
                        Console.WriteLine($"Record Id:{record.RecordID}, Employee ID:{record.EmployeeID}, Record Date:{record.RecordDate}, Description:{record.Description}, Amount:{record.Amount}, Record Type:{record.RecordType}");
                    }
                }
                else
                {
                    Console.WriteLine("No financial records found for the given employee.");
                }
            }
            else
            {
                Console.WriteLine("Invalid Employee ID");
            }
        }

        public void GetFinancialRecordsForDate(IFinancialRecordServiceRepository repository)
        {
            Console.WriteLine("Enter Record Date to view financial records(yy/mm/dd):");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime recordDate))
            {
                var recordsForDate = repository.GetFinancialRecordsForDate(recordDate);

                if (recordsForDate.Count > 0)
                {
                    foreach (var record in recordsForDate)
                    {
                        Console.WriteLine($"Record Id:{record.RecordID}, Employee ID:{record.EmployeeID}, Record Date:{record.RecordDate}, Description:{record.Description}, Amount:{record.Amount}, Record Type:{record.RecordType}");
                    }
                }
                else
                {
                    Console.WriteLine("No financial records found for the given date.");
                }
            }
            else
            {
                Console.WriteLine("Invalid Record Date");
            }
        }

        public void AddFinancialRecord(IFinancialRecordServiceRepository repository)
        {
            try
            {
                Console.WriteLine("Enter Employee ID:");
                if (int.TryParse(Console.ReadLine(), out int employeeId))
                {
                    Console.WriteLine("Enter Description:");
                    string description = Console.ReadLine();

                    Console.WriteLine("Enter Amount:");
                    if (double.TryParse(Console.ReadLine(), out double amount))
                    {
                        Console.WriteLine("Enter Record Type:");
                        string recordType = Console.ReadLine();

                        DateTime recordDate = DateTime.Today;

                        try
                        {
                            repository.AddFinancialRecord(employeeId, description, amount, recordType, recordDate);
                            Console.WriteLine("Financial Record added successfully!");
                        }
                        catch (FinancialRecordException ex)
                        {
                            Console.WriteLine($"Financial record addition error: {ex.Message}");

                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid Amount");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Employee ID");
                }
            }
            catch (FinancialRecordException ex)
            {
                Console.WriteLine($"Financial record addition error: {ex.Message}");

            }
        }

        public void CalculateAge(IEmployeeServiceRepository repository)
        {
         
                Console.Write("Enter Employee ID : ");
                if (int.TryParse(Console.ReadLine(), out int employeeId))
                {
                    int age = repository.CalculateAgeForEmployee(employeeId);

                    if (age > 0)
                    {
                        Console.WriteLine($"Age of Employee with ID {employeeId}: {age} years");
                    }
}

        }








    }
}
