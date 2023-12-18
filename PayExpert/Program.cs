using PayExpert.Service;
using PayExpert.DAO;
using PayExpert.Entities;
using PayExpert.Exceptions;
using System;

class Program
{
    static void Main(string[] args)
    {
        IPayExpertService payExpertService = new PayExpertService();
        bool exit = false;

        do
        {
            Console.WriteLine("======== PayExpert System ========");
            Console.WriteLine("1. Employee Service");
            Console.WriteLine("2. Payroll Service");
            Console.WriteLine("3. Tax Service");
            Console.WriteLine("4. Financial Record Service");
            Console.WriteLine("0. Exit");
           

            Console.Write("Enter your choice: ");
            string serviceChoice = Console.ReadLine();

            switch (serviceChoice)
            {
                case "1":
                    EmployeeServiceMenu(payExpertService);
                    break;
                case "2":
                    PayrollServiceMenu(payExpertService);
                    break;
                case "3":
                    TaxServiceMenu(payExpertService);
                    break;
                case "4":
                    FinancialRecordServiceMenu(payExpertService);
                    break;
                case "0":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
            Console.Clear();
        } while (!exit);
    }

    static void EmployeeServiceMenu(IPayExpertService payExpertService)
    {
        bool exitEmployeeService = false;

        do
        {
            Console.WriteLine("======== Employee Service ========");
            Console.WriteLine("1. Get All Employees");
            Console.WriteLine("2. Add Employee");
            Console.WriteLine("3. Update Employee");
            Console.WriteLine("4. Remove Employee");
            Console.WriteLine("5. Get Employee by ID");
            Console.WriteLine("6. Calculate Age");
            Console.WriteLine("0. Back to Main Menu");
            Console.WriteLine("===============================");

            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    payExpertService.GetAllEmployees(new EmployeeServiceRepository());
                    break;
                case "2":
                    payExpertService.AddEmployee(new EmployeeServiceRepository());
                    break;
                case "3":
                    payExpertService.UpdateEmployee(new EmployeeServiceRepository());
                    break;
                case "4":
                    payExpertService.RemoveEmployee(new EmployeeServiceRepository());
                    break;
                case "5":
                    payExpertService.GetEmployeeById(new EmployeeServiceRepository());
                    break;
                case "6":
                    payExpertService.CalculateAge(new EmployeeServiceRepository());
                    break;
                case "0":
                    exitEmployeeService = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
            Console.Clear();
        } while (!exitEmployeeService);
    }

    static void PayrollServiceMenu(IPayExpertService payExpertService)
    {
        bool exitPayrollService = false;

        do
        {
            Console.WriteLine("======== Payroll Service ========");
            Console.WriteLine("1. Generate Payroll");
            Console.WriteLine("2. Get Payrolls for Employee");
            Console.WriteLine("3. Get Payrolls for Period");
            Console.WriteLine("0. Back to Main Menu");
            Console.WriteLine("===============================");

            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    payExpertService.GeneratePayroll(new PayrollServiceRepository(), new EmployeeServiceRepository());
                    break;
                case "2":
                    payExpertService.GetPayrollsForEmployee(new PayrollServiceRepository());
                    break;
                case "3":
                    payExpertService.GetPayrollsForPeriod(new PayrollServiceRepository());
                    break;
                case "0":
                    exitPayrollService = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
            Console.Clear();
        } while (!exitPayrollService);
    }

    static void TaxServiceMenu(IPayExpertService payExpertService)
    {
        bool exitTaxService = false;

        do
        {
            Console.WriteLine("======== Tax Service ========");
            Console.WriteLine("1. Get Tax by ID");
            Console.WriteLine("2. Get Taxes for Employee");
            Console.WriteLine("3. Get Tax for Year");
            Console.WriteLine("4. Calculate Tax");
            Console.WriteLine("0. Back to Main Menu");
            Console.WriteLine("===============================");

            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    payExpertService.GetTaxById(new TaxServiceRepository());
                    break;
                case "2":
                    payExpertService.GetTaxesForEmployee(new TaxServiceRepository());
                    break;
                case "3":
                    payExpertService.GetTaxForYear(new TaxServiceRepository());
                    break;
                case "4":
                    payExpertService.CalculateTax(new TaxServiceRepository());
                    break;
                case "0":
                    exitTaxService = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
            Console.Clear();
        } while (!exitTaxService);
    }

    static void FinancialRecordServiceMenu(IPayExpertService payExpertService)
    {
        bool exitFinancialRecordService = false;

        do
        {
            Console.WriteLine("======== Financial Record Service ========");
            Console.WriteLine("1. Get Financial Record by ID");
            Console.WriteLine("2. Get Financial Records for Employee");
            Console.WriteLine("3. Get Financial Records for Date");
            Console.WriteLine("4. Add Financial Record");
          
            Console.WriteLine("0. Back to Main Menu");
            Console.WriteLine("===============================");

            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    payExpertService.GetFinancialRecordById(new FinancialRecordServiceRepository());
                    break;
                case "2":
                    payExpertService.GetFinancialRecordsForEmployee(new FinancialRecordServiceRepository());
                    break;
                case "3":
                    payExpertService.GetFinancialRecordsForDate(new FinancialRecordServiceRepository());
                    break;
                case "4":
                    payExpertService.AddFinancialRecord(new FinancialRecordServiceRepository());
                    break;
                
                case "0":
                    exitFinancialRecordService = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
            Console.Clear();
        } while (!exitFinancialRecordService);
    }

}
