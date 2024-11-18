using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VolanGo.DbConnection;
using VolanGo.Entities;
using VolanGo.Requests;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Cors;

namespace VolanGo.EndPoints
{

  [ApiController]
  [Route("api/[controller]")]
  public class PersonsController : ControllerBase
  {
         private readonly AppDbContext _context;
     
         public PersonsController(AppDbContext context)
         {
             _context = context;
         }
  
         [HttpPost("register")]
         [ProducesResponseType(StatusCodes.Status201Created)]
         public async Task<IActionResult> RegisterPerson([FromBody] RegisterRequestPerson person)
         {
            //  Validacija - proveri da li je JMBG već u bazi
             if (await _context.Person.FindAsync(person.JMBG) != null)
             {
                 return BadRequest("Person with the given JMBG already exists.");
             }
     
             var newPerson = new Person(){
                 JMBG = person.JMBG,
                 Name = person.Name,
                 Surname = person.Surname,
                 Gender = person.Gender,
                 DateOfBirth = person.DateOfBirth,
                 email = person.email,
             };
     
             // Dodaj novu osobu u bazu
             await _context.Person.AddAsync(newPerson);
             await _context.SaveChangesAsync();
     
             return Ok("Uspjesna verifikacija");
         }
     
         // Primer GET metode za dohvaćanje osobe po JMBG-u
         [HttpGet("{id}")]
         public async Task<IActionResult> GetPersonById(string id)
         {
             var person = await _context.Person.FindAsync(id);
             if (person == null)
             {
                return NotFound();
             }
     
             return Ok(person);
         }

        [HttpGet("proba")]
        public IActionResult GetStrings()
        {
            var response = new string[]
            {
                "Hello, world!",
                "Welcome to ASP.NET Core",
                "This is a sample string array"
            };

            return Ok(response); // Returns an HTTP 200 with the string array
        }

  }
  
}