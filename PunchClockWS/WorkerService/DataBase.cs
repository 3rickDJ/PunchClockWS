using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace PunchClockWS.WorkerService
{
    public class DataBase
    {
        private readonly string connectionString;

        public DataBase()
        {
            // Get the connection string from Web.config
            connectionString = "Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True";
        }

        /// <summary>
        /// Inserts a new time record into the TimeRecords table.
        /// </summary>
        /// <param name="employeeId">The ID of the employee.</param>
        /// <param name="recordType">The type of record (Entrada, Inicio comida, etc.).</param>
        /// <param name="timestamp">The timestamp of the record.</param>
        public void InsertTimeRecord(int employeeId, string recordType, DateTime timestamp)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO [WEBAPPS].[dbo].[TimeRecords] (EmployeeId, Type, TimeStamp) VALUES (@EmployeeId, @Type, @Timestamp)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                    cmd.Parameters.AddWithValue("@Type", recordType);
                    cmd.Parameters.AddWithValue("@Timestamp", timestamp);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Gets the last time record for a specific employee.
        /// </summary>
        /// <param name="employeeId">The ID of the employee.</param>
        /// <returns>A dictionary containing the last record type and timestamp.</returns>
        public Record GetLastTimeRecord(int employeeId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT TOP 1 Type, EmployeeId, TimeStamp FROM [WEBAPPS].[dbo].[TimeRecords] WHERE EmployeeId = @EmployeeId ORDER BY Id DESC";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int empId = Convert.ToInt32(reader["EmployeeId"]);
                            string recordType = reader["Type"].ToString();
                            DateTime timestamp = Convert.ToDateTime(reader["TimeStamp"]);
                            return new Record(empId, recordType, timestamp);
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Gets all time records for a specific employee.
        /// </summary>
        /// <param name="employeeId">The ID of the employee.</param>
        /// <returns>A DataTable containing all time records.</returns>
        public DataTable GetAllTimeRecords(int employeeId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT RecordType, Timestamp FROM [WEBAPPS].[dbo].[TimeRecords] WHERE EmployeeId = @EmployeeId ORDER BY Timestamp ASC";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }


        public List<Worker> GetAllEmployees()
        {
            List<Worker> employees = new List<Worker>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Id, Name FROM [WEBAPPS].[dbo].[Employees]";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Worker employee = new Worker(
                                Convert.ToInt32(reader["Id"]),
                                reader["Name"].ToString());
                            employees.Add(employee);
                        }
                    }
                }
            }
            return employees;
        }
    }
}
