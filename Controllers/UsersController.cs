using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Blogs.Data;
using Blogs.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Blogs.Mapper;
using Blogs.Dto.User;
using Blogs.Services;

namespace Blogs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly BlogContext _context;
        private readonly IUsersService _serv;
        public UsersController(BlogContext context, IUsersService serv)
        {
            _context = context;
            _serv = serv;
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _serv.GetUsers();
        }
        [HttpGet]
        [Authorize]
        [Route("user")]
        public async Task<ActionResult<User>> GetUser()
        {
            int id = GetidFromToken();
            return await _serv.GetUserById(id);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserProfileDto user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }
            var Dbuser = _context.Users.Find(id);
            Dbuser.FirstName = user.FirstName;
            Dbuser.LastName = user.LastName;
            Dbuser.Phone = user.Phone;
            Dbuser.Email = user.Email;
            await _serv.PutUser(id, Dbuser);
            return NoContent();
        }
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            var result = await _serv.PostUser(user);
            return CreatedAtAction("GetUser", new { id = result.UserId }, result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            return Ok(await _serv.DeleteUser(id));
        }
        
        [HttpPost("login/{email}/{password}")]
        public async Task<string> Login(string email, string password)
        {
            return await _serv.Login(email, password);
        }
        private int GetidFromToken()
        {
            var id = HttpContext.User.FindFirst("UserId").Value;
            return int.Parse(id);
        }
        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
    /* public class UsersController : ControllerBase
     {
         private readonly BlogContext _context;
         private readonly IConfiguration _configuration;

         public UsersController(BlogContext context, IConfiguration configuration)
         {
             _context = context;
             _configuration = configuration;
         }

         // GET: api/Users
         [HttpGet]
         [Authorize]
         public async Task<ActionResult<IEnumerable<User>>> GetUsers()
         {
             return await _context.Users.ToListAsync();
         }

         // GET: api/Users/5
         [HttpGet]
         [Authorize]
         [Route("user")]
         public async Task<ActionResult> GetUser()
         {
             int id = GetidFromToken();
             var user = await _context.Users.FindAsync(id);

             if (user == null)
             {
                 return NotFound();
             }

             return Ok(user.ToProfile());
         }

         // PUT: api/Users/5
         // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
         [HttpPut("{id}")]
         public async Task<IActionResult> PutUser(int id, UserProfileDto user)
         {
             if (id != user.UserId)
             {
                 return BadRequest();
             }
             var Dbuser = _context.Users.Find(id);
             Dbuser.FirstName=user.FirstName;
             Dbuser.LastName=user.LastName;
             Dbuser.Phone=user.Phone;
             Dbuser.Email = user.Email; 



             try
             {
                 await _context.SaveChangesAsync();
             }
             catch (DbUpdateConcurrencyException)
             {
                 if (!UserExists(id))
                 {
                     return NotFound();
                 }
                 else
                 {
                     throw;
                 }
             }

             return NoContent();
         }

         // POST: api/Users
         // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
         [HttpPost]
         public async Task<ActionResult<User>> PostUser(User user)
         {
             _context.Users.Add(user);
             await _context.SaveChangesAsync();

             return CreatedAtAction("GetUser", new { id = user.UserId }, user);
         }

         // DELETE: api/Users/5

         [HttpDelete("{id}")]
         public async Task<IActionResult> DeleteUser(int id)
         {
             var user = await _context.Users.FindAsync(id);
             if (user == null)
             {
                 return NotFound();
             }

             _context.Users.Remove(user);
             await _context.SaveChangesAsync();

             return NoContent();
         }
         [HttpPost("login/{email}/{password}")]
         public string Login(string email, string password)
         {

             var userExist = _context.Users.FirstOrDefault(t => t.Email == email && EF.Functions.Collate(t.Password, "SQL_Latin1_General_CP1_CS_AS") == password);
             if (userExist != null)
             {
                 var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                 var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                 var claims = new[]
                 {
                      new Claim(ClaimTypes.Email,userExist.Email),
                      new Claim("UserId",userExist.UserId.ToString()),
                      new Claim(ClaimTypes.Role,userExist.UserType)
      };
                 var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.Now.AddMinutes(30), signingCredentials: credentials);
                  return new JwtSecurityTokenHandler().WriteToken(token);

             }
             return null;
         }
         private int GetidFromToken()
         {
             var id = HttpContext.User.FindFirst("UserId").Value;
             return int.Parse(id);
         }

         private bool UserExists(int id)
         {
             return _context.Users.Any(e => e.UserId == id);
         }
     }*/
}
