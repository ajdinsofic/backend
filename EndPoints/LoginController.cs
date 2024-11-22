using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VolanGo.DbConnection;
using VolanGo.Generate;
using VolanGo.RequestBodies;

namespace VolanGo.EndPoints
{
    [ApiController]
    [Route("login")]
    public class LoginController : ControllerBase
    {
        private readonly AppDbContext _context;
        public GenerateToken _jwtToken;

        public LoginController(AppDbContext context)
        {
            _context = context;
            _jwtToken = new GenerateToken();
        }

        [HttpPost("admin")]
         public async Task<IActionResult> LoginAdmin([FromBody] LoginAdmin Ladmin){

           var admin = _context.Admins
                .FirstOrDefault(a => a.AdminId == Ladmin.AdminId && a.Password == Ladmin.Password);
           if(admin != null){
             return Ok(new {LoginToken = _jwtToken.GenerateJWToken(Ladmin.AdminId.ToString(), "user")});
           }
           else if(admin == null){
                return Ok(new {succes = false});
            }
            return BadRequest();
         }

         [HttpPost("user")]
         public async Task<IActionResult> LoginUser([FromBody] LoginUser Luser){

           var user = _context.Users
                .FirstOrDefault(u => u.Username == Luser.Username && u.Password == Luser.Password);
            if (user != null)
            {
                string LoginToken = _jwtToken.GenerateJWToken(Luser.Username, "user");
                return Ok(new {LoginToken});
            }
            else if(user == null){
                return Ok(new {succes = false, message = "Neispravan username ili šifra."});
            }
            return BadRequest();
         }
    }
}