using AuthenticationAndAuthorizationAPI.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAndAuthorizationAPI.Interfaces
{
    public interface IAuthService
    {
        string Login(LoginRequest loginRequest);
        Role AddRole(Role role);
        bool AssignRoleToUser(AddUserRole obj);

        //Without StoreProcedure
        User AddUser(User user);

        //Using StoreProcedure
        //Task<IEnumerable<User>> AddUser([FromBody] User user);
    }
}