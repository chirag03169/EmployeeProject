using EmployeeProject.IRepository;
using EmployeeProject.Model;
using static EmployeeProject.Model.EmployeeModel;
using System.Data;
using System.Data.SqlClient;

namespace EmployeeProject.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string _conn;

        public EmployeeRepository(IConfiguration config)
        {
            _conn = config.GetConnectionString("DefaultConnection");
        }

        public void AddEmployee(CreateEmployee createEmployee)
        {
            using var conn = new SqlConnection(_conn);
            using var cmd = new SqlCommand("spAddEmployee", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@Name", createEmployee.Name);
            cmd.Parameters.AddWithValue("@Email", createEmployee.Email);
            cmd.Parameters.AddWithValue("@Department", createEmployee.Department);
            cmd.Parameters.AddWithValue("@Salary", createEmployee.Salary);
            cmd.Parameters.AddWithValue("@JoiningDate",createEmployee.JoiningDate);

            conn.Open();
            int affected = cmd.ExecuteNonQuery();
        }

        public async Task<IEnumerable<EmployeeModel.Employee>> GetAllEmployee()
        {
            var list = new List<Employee>();
            using var conn = new SqlConnection(_conn);
            using var cmd = new SqlCommand("spGetAllEmployees", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
                list.Add(new Employee
                {
                    EmployeeID = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Email = reader.GetString(2),
                    Department = reader.IsDBNull(3) ? null : reader.GetString(3),
                    Salary = reader.IsDBNull(4) ? null : reader.GetDecimal(4),
                    JoiningDate = reader.IsDBNull(5) ? null : reader.GetDateTime(5)
                });
            return list;
        }

        public async Task<int> UpdateEmployee(UpdateEmployee updateEmployee)
        {
            using var conn = new SqlConnection(_conn);
            using var cmd = new SqlCommand("usp_updateEmployee", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@Name", updateEmployee.Name);
            cmd.Parameters.AddWithValue("@Email", updateEmployee.Email);
            cmd.Parameters.AddWithValue("@Salary", updateEmployee.Salary);
            cmd.Parameters.AddWithValue("@id", updateEmployee.EmployeeID);

            conn.Open();
            int affected = cmd.ExecuteNonQuery();
            return affected;

        }

        public void DeleteEmployee(int id)
        {
            using var conn = new SqlConnection(_conn);
            using var cmd = new SqlCommand("usp_DeleteEmployee", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@id", id);

            conn.Open();
            int affected = cmd.ExecuteNonQuery();
        }
    }
}
