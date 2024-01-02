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
    public class EmployeeServiceRepository:IEmployeeServiceRepository
    {
        public string connectionString;
        public string ConnectionString
        { 
        get { return connectionString; }
            set { connectionString = value; }

        }
        SqlCommand cmd = null;
        public EmployeeServiceRepository()
        {
            connectionString = DbConnUtil.GetConnectionString();
            cmd = new SqlCommand();
        }
        public int AddEmployee(Employee employeeData)
        {
            int addStatus = 0;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "INSERT INTO Employee (FirstName, LastName,DateOfBirth, Gender, Email, PhoneNumber, Address, Position,JoiningDate) " +
                                      "VALUES (@FirstName, @LastName,@DateOfBirth, @Gender, @Email, @PhoneNumber, @Address, @Position,@JoiningDate)";
                    cmd.Parameters.AddWithValue("@FirstName", employeeData.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", employeeData.LastName);
                    cmd.Parameters.AddWithValue("@DateOfBirth", employeeData.DateOfBirth);
                    cmd.Parameters.AddWithValue("@Gender", employeeData.Gender);
                    cmd.Parameters.AddWithValue("@Email", employeeData.Email);
                    cmd.Parameters.AddWithValue("@PhoneNumber", employeeData.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Address", employeeData.Address);
                    cmd.Parameters.AddWithValue("@Position", employeeData.Position);
                    cmd.Parameters.AddWithValue("@JoiningDate", employeeData.JoiningDate);

                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();
                    addStatus= cmd.ExecuteNonQuery();
                    
                }

            }
            catch (SqlException ex)
            {
                throw new DatabaseConnectionException($"An error occurred while processing the database operation.{ex.Message}");
            }
            return addStatus;
        }
        public void UpdateEmployee(Employee updatedEmployeeData)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "UPDATE Employee SET FirstName = @FirstName, LastName = @LastName, " +
                                      "Gender = @Gender, Email = @Email, PhoneNumber = @PhoneNumber, " +
                                      "Address = @Address, Position = @Position WHERE EmployeeID = @EmployeeID";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@FirstName", updatedEmployeeData.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", updatedEmployeeData.LastName);
                    cmd.Parameters.AddWithValue("@Gender", updatedEmployeeData.Gender);
                    cmd.Parameters.AddWithValue("@Email", updatedEmployeeData.Email);
                    cmd.Parameters.AddWithValue("@PhoneNumber", updatedEmployeeData.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Address", updatedEmployeeData.Address);
                    cmd.Parameters.AddWithValue("@Position", updatedEmployeeData.Position);
                    cmd.Parameters.AddWithValue("@EmployeeID", updatedEmployeeData.EmployeeID);

                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            catch (SqlException ex)
            {
                throw new DatabaseConnectionException($"An error occurred while processing the database operation.{ex.Message}");
            }

        }
        public void RemoveEmployee(int employeeId)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                   
                        cmd.CommandText = "DELETE FROM Employee WHERE EmployeeID = @EmployeeID";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@EmployeeID", employeeId);

                        cmd.Connection = sqlConnection;
                        sqlConnection.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            throw new EmployeeNotFoundException(employeeId);
                        }
                    
                }
            }
            catch (SqlException ex)
            {
                throw new DatabaseConnectionException($"An error occurred while processing the database operation.{ex.Message}");
            }
        }
        public Employee GetEmployeeById(int employeeId)
        {
            Employee employee = null;

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "SELECT * FROM Employee WHERE EmployeeID = @EmployeeID";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@EmployeeID", employeeId);

                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        employee = new Employee
                        {
                            EmployeeID = (int)reader["EmployeeID"],
                            FirstName = (string)reader["FirstName"],
                            LastName = (string)reader["LastName"],
                            Gender = (string)reader["Gender"],
                            Email = (string)reader["Email"],
                            PhoneNumber = (string)reader["PhoneNumber"],
                            Address = (string)reader["Address"],
                            Position = (string)reader["Position"]
                        };
                    }
                    else
                    {

                        throw new EmployeeNotFoundException(employeeId);
                    }
                }
            }
            catch (SqlException ex)
            {

                throw new DatabaseConnectionException($"An error occurred while processing the database operation.{ex.Message}");
            }

            return employee;
        }
        public List<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "SELECT * FROM Employee";
                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Employee employee = new Employee();
                        employee.EmployeeID = (int)reader["EmployeeID"];
                        employee.FirstName = (string)reader["FirstName"];
                        employee.LastName = (string)reader["LastName"];
                        if (reader["DateOfBirth"] != DBNull.Value)
                        {
                            employee.DateOfBirth = (DateTime?)reader["DateOfBirth"];
                        }

                        employee.Gender = (string)reader["Gender"];
                        employee.Email = (string)reader["Email"];
                        employee.PhoneNumber = (string)reader["PhoneNumber"];
                        employee.Address = (string)reader["Address"];
                        employee.Position = (string)reader["Position"];


                        if (reader["JoiningDate"] != DBNull.Value)
                        {
                            employee.JoiningDate = (DateTime?)reader["JoiningDate"];
                        }

                        if (reader["TerminationDate"] != DBNull.Value)
                        {
                            employee.TerminationDate = (DateTime?)reader["TerminationDate"];
                        }
                        employees.Add(employee);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return employees;
        }


        public int CalculateAgeForEmployee(int employeeId)
        {
            int age = 0;

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "SELECT DateOfBirth FROM Employee WHERE EmployeeID = @EmployeeID";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@EmployeeID", employeeId);

                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();

                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        DateTime dateOfBirth = (DateTime)result;

                       
                        DateTime currentDate = DateTime.Now;
                        age = currentDate.Year - dateOfBirth.Year;

                    }
                    else
                    {
                        throw new EmployeeNotFoundException($"Employee with ID {employeeId} not found.");
                    }
                }
            }
            catch (EmployeeNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return age;
        }


    }
}
