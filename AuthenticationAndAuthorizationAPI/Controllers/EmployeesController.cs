using AuthenticationAndAuthorizationAPI.Entities;
using AuthenticationAndAuthorizationAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAndAuthorizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("GetAll")]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> GetEmployeeDetails()
        {
            var data = await _employeeService.GetEmployeeDetails();
            if (data is null)
            {
                return NotFound("Employees not found.");
            }
            return Ok(data);
        }

        [HttpGet("GetEmployeeById/{id}")]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var data = await _employeeService.GetEmployeeById(id);
            if (data is null)
            {
                return NotFound("Sorry, but this Employee doesn't exist.");
            }
            return Ok(data);
        }

        [HttpPost("AddEmployee")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddEmployee([FromBody] Employee emp)
        {
            var data = _employeeService.AddEmployee(emp);
            return Ok(data);
        }

        [HttpPut("UpdateEmployee/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateEmployee(Employee emp, int id)
        {
            var data = await _employeeService.UpdateEmployee(emp, id);
            if (data is null)
            {
                return NotFound("Sorry, but this Employee doesn't exist.");
            }
            return Ok(data);
        }

        [HttpDelete("RemoveEmployee/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveEmployee(int id)
        {
            var data = await _employeeService.RemoveEmployee(id);
            if (data is null)
            {
                return NotFound("Sorry, but this Employee doesn't exist.");
            }
            return Ok(data);
        }
    }
}