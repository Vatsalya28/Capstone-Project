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
    public class TaxServiceRepository:ITaxServiceRepository
    {
        public string connectionString;
        SqlCommand cmd =null;
        public TaxServiceRepository()
        {
            connectionString = DbConnUtil.GetConnectionString();
            cmd=new SqlCommand();
        }
        public Tax GetTaxForId(int taxID)
        {
            Tax tax = null;
            try
            {
                using(SqlConnection sqlConnection=new SqlConnection(connectionString))
                {
                    cmd.CommandText = "SELECT * FROM Tax WHERE TaxID=@TaxId";
                    cmd.Parameters.AddWithValue("@TaxId", taxID);
                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        tax = new Tax
                        {
                            TaxID = (int)reader["TaxID"],
                        EmployeeID = (int)reader["EmployeeID"],
                            TaxYear = (int)reader["TaxYear"],
                            TaxableIncome = (double)reader["TaxableIncome"],
                            TaxAmount = (double)reader["TaxAmount"]
                        };
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return tax;
        }
        public List<Tax>GetTaxesForEmployee(int employeeId)
        {
            List<Tax> taxes = new List<Tax>();
            try
            {
                using(SqlConnection sqlConnection=new SqlConnection(connectionString))
                {
                    cmd.CommandText = "SELECT * FROM Tax WHERE EmployeeID=@EmployeeID";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Tax tax = new Tax
                        {
                            TaxID = (int)reader["TaxID"],
                            EmployeeID = (int)reader["EmployeeID"],
                            TaxYear = (int)reader["TaxYear"],
                            TaxableIncome = (double)reader["TaxableIncome"],
                            TaxAmount = (double)reader["TaxAmount"]
                        };
                        taxes.Add(tax);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine( ex.Message);
            }
            return taxes;
        }

        public List<Tax> GetTaxForYear(int taxYear)
        {
            List<Tax> taxes = new List<Tax>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "SELECT * FROM Tax WHERE TaxYear=@TaxYear";
                    cmd.Parameters.AddWithValue("@TaxYear", taxYear);
                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                       Tax tax = new Tax
                        {
                            TaxID = (int)reader["TaxID"],
                            EmployeeID = (int)reader["EmployeeID"],
                            TaxYear = (int)reader["TaxYear"],
                            TaxableIncome = (double)reader["TaxableIncome"],
                            TaxAmount = (double)reader["TaxAmount"]
                        };
                        taxes.Add(tax);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return taxes;
        }

         private const double FixedTaxRate = 0.1;
        public double CalculateTax(int employeeId, int taxYear)
        {
            double totalTax = 0.0;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "SELECT SUM(NetSalary) AS TotalNetSalary FROM Payroll WHERE EmployeeID=@EmployeeId AND YEAR(PayPeriodStartDate) = @TaxYear";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                    cmd.Parameters.AddWithValue("@TaxYear", taxYear);
                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();
                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        double totalNetSalary = Convert.ToDouble(result);
                        totalTax = totalNetSalary * FixedTaxRate;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new TaxCalculationException($"Error calculating tax: {ex.Message}");
            }
            return totalTax;
        }


    }
}
