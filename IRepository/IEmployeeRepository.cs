using static EmployeeProject.Model.EmployeeModel;

namespace EmployeeProject.IRepository
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllEmployee();

        void AddEmployee(CreateEmployee createEmployee);

        Task<int> UpdateEmployee(UpdateEmployee updateEmployee);

        void DeleteEmployee(int id);
    }
}
