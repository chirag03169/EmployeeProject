using EmployeeProject.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static EmployeeProject.Model.EmployeeModel;

namespace EmployeeProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _repo;
        private object updateEmployee;

        public EmployeeController(IEmployeeRepository repo) 
        {
            _repo = repo;

        }

        [HttpGet("GetAllEmployee")]
        public async Task<IActionResult> GetAllEmployee()
        {
            var data = await _repo.GetAllEmployee();
            return Ok(data);
        }

        [HttpPost("AddEmployee")]
        public async Task<IActionResult> AddEmployee(CreateEmployee createEmployee)
        {
            _repo.AddEmployee(createEmployee);
            return Ok("Success");
        }


        [HttpPost("UpdateEmployee")]
        public IActionResult UpdateEmployee(UpdateEmployee updateEmployee)
        {
            var data = _repo.UpdateEmployee(updateEmployee);
            return Ok(data);    


        }

        [HttpPost("DeleteEmployee")]
        public IActionResult DeleteEmployee(int id)
        {
            _repo.DeleteEmployee(id);
            return Ok("succes");


        }


    }
}
