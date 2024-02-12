using AuthenticationAndAuthorizationAPI.Entities;
using AuthenticationAndAuthorizationAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AuthenticationAndAuthorizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;

        public AuthController(IAuthService auth)
        {
            _auth = auth;
        }

        [HttpPost("login")]
        public string Login([FromBody] LoginRequest obj)
        {
            var token = _auth.Login(obj);
            return token;
        }

        [HttpPost("assignRole")]
        public bool AssignRoleToUser([FromBody] AddUserRole userRole)
        {
            var addedUserRole = _auth.AssignRoleToUser(userRole);
            return addedUserRole;
        }

        [HttpPost("addRole")]
        public Role AddRole([FromBody] Role role)
        {
            var addedRole = _auth.AddRole(role);
            return addedRole;
        }

        //Without StoreProcedure
        [HttpPost("addUser")]
        public User AddUser([FromBody] User user)
        {
            var addeduser = _auth.AddUser(user);
            return addeduser;
        }

        //Using StoreProcedure
        //[HttpPost("AddUser")]
        //public async Task<ActionResult<IEnumerable<User>>> AddUser(User user)
        //{
        //    try
        //    {
        //        var addedUsers = await _auth.AddUser(user);

        //        if (addedUsers == null)
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            return Ok(addedUsers);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        //    }
        //}
    }
}