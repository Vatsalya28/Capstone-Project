using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayExpert.Entities;
using PayExpert.Utility;

using Microsoft.Data.SqlClient;
using PayExpert.Exceptions;

namespace PayExpert.DAO
{
    public class PayrollServiceRepository : IPayrollServiceRepository
    {
        public string connectionString;
        SqlCommand cmd = null;
        public PayrollServiceRepository()
        {
            connectionString = DbConnUtil.GetConnectionString();
            cmd = new SqlCommand();
        }
        public Payroll GetPayrollById(int payrollId)
        {
            Payroll payroll = null;

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "SELECT * FROM Payroll WHERE PayrollID = @PayrollID";
                    cmd.Parameters.AddWithValue("@PayrollID", payrollId);

                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        payroll = new Payroll
                        {
                            PayrollID = (int)reader["PayrollID"],
                            EmployeeID = (int)reader["EmployeeID"],
                            PayPeriodStartDate = (DateTime)reader["PayPeriodStartDate"],
                            PayPeriodEndDate = (DateTime)reader["PayPeriodEndDate"],
                            BasicSalary = (double)reader["BasicSalary"],
                            OvertimePay = (double)reader["OvertimePay"],
                            Deductions = (double)reader["Deductions"],
                            NetSalary = (double)reader["NetSalary"]
                        };
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DatabaseConnectionException($"An error occurred while processing the database operation.{ex.Message}");
            }

            return payroll;
        }

        public List<Payroll> GetPayrollsForEmployee(int employeeId)
        {
            List<Payroll> payrolls = new List<Payroll>();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "SELECT * FROM Payroll WHERE EmployeeID = @EmployeeID";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@EmployeeID", employeeId);

                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Payroll payroll = new Payroll
                        {
                            PayrollID = (int)reader["PayrollID"],
                            EmployeeID = (int)reader["EmployeeID"],
                            PayPeriodStartDate = (DateTime)reader["PayPeriodStartDate"],
                            PayPeriodEndDate = (DateTime)reader["PayPeriodEndDate"],
                            BasicSalary = (double)reader["BasicSalary"],
                            OvertimePay = (double)reader["OvertimePay"],
                            Deductions = (double)reader["Deductions"],
                            NetSalary = (double)reader["NetSalary"]
                        };

                        payrolls.Add(payroll);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DatabaseConnectionException($"An error occurred while processing the database operation.{ex.Message}");
            }

            return payrolls;
        }
        public List<Payroll> GetPayrollsForPeriod(DateTime startDate, DateTime endDate)
        {
            List<Payroll> payrolls = new List<Payroll>();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    
                   cmd.CommandText = "SELECT * FROM Payroll WHERE PayPeriodStartDate >= @StartDate AND PayPeriodEndDate <= @EndDate";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@StartDate", startDate);
                    cmd.Parameters.AddWithValue("@EndDate", endDate);

                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Payroll payroll = new Payroll
                        {
                            PayrollID = (int)reader["PayrollID"],
                            EmployeeID = (int)reader["EmployeeID"],
                            PayPeriodStartDate = (DateTime)reader["PayPeriodStartDate"],
                            PayPeriodEndDate = (DateTime)reader["PayPeriodEndDate"],
                            BasicSalary = (double)reader["BasicSalary"],
                            OvertimePay = (double)reader["OvertimePay"],
                            Deductions = (double)reader["Deductions"],
                            NetSalary = (double)reader["NetSalary"]
                        };

                        payrolls.Add(payroll);
                    }
                }
            }
            catch (SqlException ex)
            {

                throw new DatabaseConnectionException($"An error occurred while processing the database operation.{ex.Message}");
            }
            return payrolls;
        }


        public Payroll GeneratePayroll(int employeeId, DateTime startDate, DateTime endDate)
        {
            Payroll payroll = new Payroll();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "INSERT INTO Payroll (EmployeeID, PayPeriodStartDate, PayPeriodEndDate, BasicSalary, OvertimePay, Deductions, NetSalary) " +
                                      "VALUES (@EmployeeID, @StartDate, @EndDate, @BasicSalary, @OvertimePay, @Deductions, @NetSalary); " +
                                      "SELECT SCOPE_IDENTITY();";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                    cmd.Parameters.AddWithValue("@StartDate", startDate);
                    cmd.Parameters.AddWithValue("@EndDate", endDate);

                    double basicSalary = FetchBasicSalaryFromUserInput();
                    double overtimePay = FetchOvertimePayFromUserInput();
                    double deductions = FetchDeductionsFromUserInput();
                    double netSalary = basicSalary + overtimePay - deductions;

                    cmd.Parameters.AddWithValue("@BasicSalary", basicSalary);
                    cmd.Parameters.AddWithValue("@OvertimePay", overtimePay);
                    cmd.Parameters.AddWithValue("@Deductions", deductions);
                    cmd.Parameters.AddWithValue("@NetSalary", netSalary);

                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();

                    object payrollId = cmd.ExecuteScalar();

                    if (payrollId != null)
                    {
                        payroll = GetPayrollById(Convert.ToInt32(payrollId));
                    }
                    else
                    {
                        Console.WriteLine("Failed to generate payroll. Payroll ID not returned.");
                    }
                }
            }
            catch (SqlException ex)
            {

                throw new DatabaseConnectionException($"An error occurred while processing the database operation.{ex.Message}");
            }

            return payroll;
        }

       

        private double FetchBasicSalaryFromUserInput()
        {
            double basicSalary;

            Console.Write("Enter Basic Salary: ");
            string input = Console.ReadLine();

            if (double.TryParse(input, out basicSalary))
            {
                return basicSalary;
            }
            else
            {
           
                throw new InvalidInputException("Invalid input for Basic Salary.");
            }
        }

        private double FetchOvertimePayFromUserInput()
        {
            double overtimePay;

            Console.Write("Enter Overtime Pay: ");
            string input = Console.ReadLine();

            if (double.TryParse(input, out overtimePay))
            {
                return overtimePay;
            }
            else
            {
       
                throw new InvalidInputException("Invalid input for Overtime Pay.");
            }
        }

        private double FetchDeductionsFromUserInput()
        {
            double deductions;

            Console.Write("Enter Deductions: ");
            string input = Console.ReadLine();

            if (double.TryParse(input, out deductions))
            {
                return deductions;
            }
            else
            {
                throw new InvalidInputException("Invalid input for Deductions.");
            }

        }

    }
}

