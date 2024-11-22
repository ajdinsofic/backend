using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VolanGo.DbConnection;
using VolanGo.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Cors;
using RS1_2024_25.API.Data.Models;
using Microsoft.EntityFrameworkCore;
using VolanGo.RequestBodies;
using VolanGo.Generate;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace VolanGo.EndPoints
{

  [ApiController]
  [Route("register")]
  public class RegisterController : ControllerBase
  {
        private readonly AppDbContext _context;
        private readonly GenerateToken _jwtToken;

        public RegisterController(AppDbContext context)
        {
            _context = context;
            _jwtToken = new GenerateToken();
        }
 
        /// REGISTRACIJA ZA ADMINA
         [HttpPost("user")]
         [ProducesResponseType(StatusCodes.Status201Created)]
         public async Task<IActionResult> RegisterUser([FromBody] User user)
         {
            if (await _context.Users.Where(u => u.Username == user.Email && u.Password == user.Password).FirstOrDefaultAsync() != null)
             {
                 return BadRequest("User vec postoji u sistemu, logiraj te se!!!");
             }
             // Dodaj novu osobu u bazu
             await _context.Users.AddAsync(user);
             await _context.SaveChangesAsync();

             var token = _jwtToken.GenerateJWToken(user.UserId.ToString(), "user");
             return Ok(new { token });
        }

    }
  }
  