using AuthenticationAndAuthorizationAPI.Data;
using AuthenticationAndAuthorizationAPI.Entities;
using AuthenticationAndAuthorizationAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationAndAuthorizationAPI.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(ApplicationDbContext context, ILogger<EmployeeService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Employee>> GetEmployeeDetails()
        {
            var employees = await _context.Employees.ToListAsync();
            return employees;
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            return employee;
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            _logger.LogInformation("Create Begins");
            var emp = await _context.Employees.AddAsync(employee);
            _context.SaveChanges();
            return emp.Entity;
        }

        public async Task<Employee> UpdateEmployee(Employee data, int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                employee.Name = data.Name;
                employee.Company = data.Company;
                employee.Position = data.Position;
                await _context.SaveChangesAsync();
            }
            return employee;
        }

        public async Task<Employee> RemoveEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
            return employee;
        }
    }
}