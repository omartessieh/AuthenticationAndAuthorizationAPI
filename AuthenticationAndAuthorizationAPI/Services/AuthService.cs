using AuthenticationAndAuthorizationAPI.Data;
using AuthenticationAndAuthorizationAPI.Entities;
using AuthenticationAndAuthorizationAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationAndAuthorizationAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public Role AddRole(Role role)
        {
            var addedRole = _context.Roles.Add(role);
            _context.SaveChanges();
            return addedRole.Entity;
        }

        public bool AssignRoleToUser(AddUserRole obj)
        {
            try
            {
                var addRoles = new List<UserRole>();
                var user = _context.Users.SingleOrDefault(s => s.Id == obj.UserId);
                if (user == null)
                    throw new Exception("user is not valid");
                foreach (int role in obj.RoleIds)
                {
                    var userRole = new UserRole();
                    userRole.RoleId = role;
                    userRole.UserId = user.Id;
                    addRoles.Add(userRole);
                }
                _context.UserRoles.AddRange(addRoles);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public string Login(LoginRequest loginRequest)
        {
            if (loginRequest.Username != null && loginRequest.Password != null)
            {
                var user = _context.Users.SingleOrDefault(s => s.Username == loginRequest.Username && s.Password == loginRequest.Password);
                if (user != null)
                {
                    var claims = new List<Claim> {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim("Id", user.Id.ToString()),
                        new Claim("UserName", user.Name)
                    };
                    var userRoles = _context.UserRoles.Where(u => u.UserId == user.Id).ToList();
                    var roleIds = userRoles.Select(s => s.RoleId).ToList();
                    var roles = _context.Roles.Where(r => roleIds.Contains(r.Id)).ToList();
                    foreach (var role in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role.Name));
                    }
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn);

                    var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                    return jwtToken;
                }
                else
                {
                    throw new Exception("user is not valid");
                }
            }
            else
            {
                throw new Exception("credentials are not valid");
            }
        }

        //Without StoreProcedure
        public User AddUser(User user)
        {
            var addedUser = _context.Users.Add(user);
            _context.SaveChanges();
            return addedUser.Entity;
        }

        //Using StoreProcedure
        //public async Task<IEnumerable<User>> AddUser([FromBody] User user)
        //{
        //    try
        //    {
        //        var item = new User()
        //        {
        //            Name = user.Name,
        //            Username = user.Username,
        //            Password = user.Password,
        //        };

        //        var result = await _context.Users.FromSqlRaw($"exec SP_CreateUser @Name='{item.Name}', @Username='{item.Username}', @Password='{item.Password}'").ToListAsync();

        //        return result;
        //    }
        //    catch (DbUpdateException ex)
        //    {
        //        var sqlException = ex.GetBaseException() as SqlException;

        //        if (sqlException != null && sqlException.Number == 50001) // Username already exists error
        //        {
        //            return null;
        //        }

        //        throw; // Rethrow other exceptions
        //    }
        //}
    }
}