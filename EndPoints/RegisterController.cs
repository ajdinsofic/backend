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

namespace VolanGo.EndPoints
{

  [ApiController]
  [Route("register")]
  public class RegisterController : ControllerBase
  {
        private readonly AppDbContext _context;

        public RegisterController(AppDbContext context)
        {
            _context = context;
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
     
             return Ok("Uspjesna verifikacija");
         }
         
    }
  }
  