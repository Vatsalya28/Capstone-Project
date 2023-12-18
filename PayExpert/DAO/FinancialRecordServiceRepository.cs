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
    public class FinancialRecordServiceRepository:IFinancialRecordServiceRepository
    {
        public string connectionString;
        SqlCommand cmd = null;
        public FinancialRecordServiceRepository()
        {
            connectionString = DbConnUtil.GetConnectionString();
            cmd = new SqlCommand();
        }
        public FinancialRecord GetFinancialRecordById(int recordId)
        {
            FinancialRecord financialRecord = null;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "SELECT * FROM FinancialRecord WHERE RecordID=@RecordId";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@RecordId", recordId);
                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        financialRecord = new FinancialRecord
                        {
                            RecordID = (int)reader["RecordID"],
                            EmployeeID = (int)reader["EmployeeID"],
                            RecordDate = (DateTime)reader["RecordDate"],
                            Description = reader["Description"].ToString(),
                            Amount = (double)reader["Amount"],
                            RecordType = reader["RecordType"].ToString()
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return financialRecord;
        }




        public List<FinancialRecord> GetFinancialRecordsForEmployee(int employeeId)
        {
            List<FinancialRecord> financialRecords = new List<FinancialRecord>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "SELECT * FROM FinancialRecord WHERE EmployeeID=@EmployeeId";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        FinancialRecord financialRecord = new FinancialRecord
                        {
                            RecordID = (int)reader["RecordID"],
                            EmployeeID = (int)reader["EmployeeID"],
                            RecordDate = (DateTime)reader["RecordDate"],
                            Description = reader["Description"].ToString(),
                            Amount = (double)reader["Amount"],
                            RecordType = reader["RecordType"].ToString()
                        };
                        financialRecords.Add(financialRecord);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return financialRecords;
        }

        public List<FinancialRecord> GetFinancialRecordsForDate(DateTime recordDate)
        {
            List<FinancialRecord> financialRecords = new List<FinancialRecord>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "SELECT * FROM FinancialRecord WHERE RecordDate=@RecordDate";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@RecordDate", recordDate);
                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        FinancialRecord financialRecord = new FinancialRecord
                        {
                            RecordID = (int)reader["RecordID"],
                            EmployeeID = (int)reader["EmployeeID"],
                            RecordDate = (DateTime)reader["RecordDate"],
                            Description = reader["Description"].ToString(),
                            Amount = (double)reader["Amount"],
                            RecordType = reader["RecordType"].ToString()
                        };
                        financialRecords.Add(financialRecord);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return financialRecords;
        }
        public void AddFinancialRecord(int employeeId, string description, double amount, string recordType, DateTime recordDate)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "INSERT INTO FinancialRecord (EmployeeID, Description, Amount, RecordType, RecordDate) VALUES (@EmployeeId, @Description, @Amount, @RecordType, @RecordDate)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                    cmd.Parameters.AddWithValue("@Description", description);
                    cmd.Parameters.AddWithValue("@Amount", amount);
                    cmd.Parameters.AddWithValue("@RecordType", recordType);
                    cmd.Parameters.AddWithValue("@RecordDate", recordDate);
                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new FinancialRecordException($"Custom error message: Unable to add financial record - {ex.Message}");
            }
        }


    }
}
