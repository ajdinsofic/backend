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

//         [HttpPost("admin")]        
//          public async Task<IActionResult> LoginAdmin([FromBody] LoginAdmin Ladmin){

//            var admin = _context.Admins
//                 .FirstOrDefault(a => a.AdminId == Ladmin.AdminId && a.Password == Ladmin.Password);
//            if (admin != null)
//             {
//             return Ok(new
//             {
//                 token = _jwtToken.GenerateJWToken(admin.AdminId.ToString(), "admin"),
//                 myAuthInfo = new
//                 {
//                     userId = admin.AdminId,
//                     username = admin.Username,
//                     firstName = ' ',
//                     lastName = ' ',
//                     isAdmin = true,
//                     isUser = false,
//                     isLoggedIn = true
//                 }
//                 });
//             }
//            else if(admin == null){
//                 return Ok(new {succes = false});
//             }
//             return BadRequest();

//          }

//          [HttpPost("user")]
// public async Task<IActionResult> LoginUser([FromBody] LoginUser Luser)
// {
//     var user = _context.Users
//         .FirstOrDefault(u => u.Username == Luser.Username && u.Password == Luser.Password);

//     if (user != null)
//     {
//         return Ok(new
//         {
//             success = true,
//             token = _jwtToken.GenerateJWToken(user.UserId.ToString(), "user"),
//             myAuthInfo = new
//             {
//                 userId = user.UserId,
//                 username = user.Username,
//                 firstName = user.FirstName,
//                 lastName = user.LastName,
//                 isAdmin = false,
//                 isUser = true,
//                 isLoggedIn = true
//             }
//         });
//     }

//     return Ok(new { success = false, message = "Invalid username or password." });
// }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginUser loginData)
        {

            // validacija podataka (da li je posalo email, passvord )
            // prvo provjeriti da li user postoji u bazi ako ne postaji odmah return 401
            // pasvord => hash + provjera od usera iz baze hash password da li su jednaki = success fail = 401
            // Check if it's an admin login
            var admin = _context.Admins
                .FirstOrDefault(a => a.Username == loginData.Username && a.Password == loginData.Password);
            if (admin != null)
            {
              return Ok(new
            {
                token = _jwtToken.GenerateJWToken(admin.AdminId.ToString(), "admin"),
                // save u db personal access token (user_id, token, expires_at)
                myAuthInfo = new
                {
                    userId = admin.AdminId,
                    username = admin.Username,
                    firstName = string.Empty,
                    lastName = string.Empty,
                    isAdmin = true,
                    isUser = false,
                    isLoggedIn = true
                }
            });
            }

        // Check if it's a user login
            var user = _context.Users
                .FirstOrDefault(u => u.Username == loginData.Username && u.Password == loginData.Password);
            if (user != null)
            {
                return Ok(new
            {
                token = _jwtToken.GenerateJWToken(user.UserId.ToString(), "user"),
                myAuthInfo = new
                {
                    userId = user.UserId,
                    username = user.Username,
                    firstName = user.FirstName,
                    lastName = user.LastName,
                    isAdmin = false,
                    isUser = true,
                    isLoggedIn = true
                }
            });
        }

        // If no match found
        return BadRequest(new { success = false, message = "Invalid username or password." });
    }
    }
}