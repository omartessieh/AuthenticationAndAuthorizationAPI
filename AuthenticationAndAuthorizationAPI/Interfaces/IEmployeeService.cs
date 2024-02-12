using AuthenticationAndAuthorizationAPI.Entities;

namespace AuthenticationAndAuthorizationAPI.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetEmployeeDetails();
        Task<Employee> AddEmployee(Employee employee);
        Task<Employee> GetEmployeeById(int id);
        Task<Employee> UpdateEmployee(Employee data, int id);
        Task<Employee> RemoveEmployee(int id);
    }
}