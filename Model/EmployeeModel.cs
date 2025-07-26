namespace EmployeeProject.Model
{
    public class EmployeeModel
    {
        public class Employee : CreateEmployee
        {
            public int EmployeeID { get; set; }

        }

        public class CreateEmployee
        {
            public string Name { get; set; } = null!;

            public string Email { get; set; } = null!;

            public string? Department { get; set; }

            public decimal? Salary { get; set; }

            public DateTime? JoiningDate { get; set; }
        }

        public class UpdateEmployee
        {
            public string Name { get; set; } = null!;
            public string Email { get; set; }
            public decimal? Salary { get; set; }
            public int EmployeeID { get; set; }

        }

    }
}
