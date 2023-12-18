using PayExpert.Entities;
using PayExpert.DAO;

using System.ComponentModel.DataAnnotations;

namespace PayXpert.Test
{
    public class Tests
    {
        private const string connectionString = "Server=DESKTOP-9L5BOE7;Database=PayXpert;Integrated Security=True;TrustServerCertificate=true";



        #region Test Add Employee
        [Test]
        public void TestToAddEmployee()
        {
            EmployeeServiceRepository employeeServiceRepository = new EmployeeServiceRepository();
            employeeServiceRepository.connectionString = connectionString;
            int addEmployee = employeeServiceRepository.AddEmployee(new Employee
            {
                FirstName = "Vatsalya",
                LastName = "Neymar",
                DateOfBirth = new DateTime(2000, 1, 2),
                Gender = "MAle",
                Email = "bahd",
                PhoneNumber = "sdhu",
                Address = "sdwed",
                Position = "shduhd",
                JoiningDate = new DateTime(2023, 1, 2),
                TerminationDate = null
            });
            Assert.AreEqual(1, addEmployee);
        }
        #endregion

        #region Generate Payroll
        [Test]
        public void TestGeneratePayroll()
        {

            PayrollServiceRepository payrollServiceRepository = new PayrollServiceRepository();
            payrollServiceRepository.connectionString = connectionString;


            int employeeId = 1;


            DateTime startDate = new DateTime(2023, 1, 1);
            DateTime endDate = new DateTime(2023, 1, 15);


            double basicSalary = 5000.00;
            double overtimePay = 1000.00;
            double deductions = 500.00;


            MockUserInput(basicSalary, overtimePay, deductions);


            Payroll generatedPayroll = payrollServiceRepository.GeneratePayroll(employeeId, startDate, endDate);


            Assert.IsNotNull(generatedPayroll);
            Assert.AreEqual(employeeId, generatedPayroll.EmployeeID);
            Assert.AreEqual(startDate, generatedPayroll.PayPeriodStartDate);
            Assert.AreEqual(endDate, generatedPayroll.PayPeriodEndDate);


            Assert.AreEqual(basicSalary, generatedPayroll.BasicSalary);
            Assert.AreEqual(overtimePay, generatedPayroll.OvertimePay);
            Assert.AreEqual(deductions, generatedPayroll.Deductions);

            double expectedNetSalary = basicSalary + overtimePay - deductions;
            Assert.AreEqual(expectedNetSalary, generatedPayroll.NetSalary);


            Assert.IsTrue(generatedPayroll.PayrollID > 0);


        }


        private void MockUserInput(double basicSalary, double overtimePay, double deductions)
        {
           
            Console.SetIn(new StringReader($"{basicSalary}\n{overtimePay}\n{deductions}\n"));
        }



        #endregion


        #region Financial Record

        [Test]
        public void TestGetFinancialRecordsForEmployee()
        {
            FinancialRecordServiceRepository financialRecordServiceRepository = new FinancialRecordServiceRepository();
            financialRecordServiceRepository.connectionString = connectionString;
            int employeeId = 1;


            var financialRecords = financialRecordServiceRepository.GetFinancialRecordsForEmployee(employeeId);


            Assert.IsNotNull(financialRecords);
            Assert.IsInstanceOf<List<FinancialRecord>>(financialRecords);
        }
        #endregion

        [Test]
        public void TestCalculateTax()
        {
            TaxServiceRepository taxServiceRepository = new TaxServiceRepository();
            taxServiceRepository.connectionString = connectionString;
            int employeeId = 1;
            int taxYear = DateTime.Now.Year;
            double calculatedTax = taxServiceRepository.CalculateTax(employeeId, taxYear);
            Assert.GreaterOrEqual(calculatedTax, 0.0, "Tax should not be negative");
        }





    }










}
