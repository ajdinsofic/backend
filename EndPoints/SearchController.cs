using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VolanGo.DbConnection;

namespace VolanGo.EndPoints
{
    [ApiController]
    [Route("search")]
    public class SearchController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SearchController(AppDbContext context)
        {
            _context = context;
        }
        
        [HttpGet("AdminOrUser")]
         public async Task<IActionResult> GetUserOeAdminById(object id)
         {
            if(id is string){
               return Ok(_context.UserReviews.FindAsync(id));
            }
            else{
               return Ok(_context.Admins.FindAsync(id));
            }
         }

         [HttpGet("proba")]
         public async Task<IActionResult> TestingConnection()
         {
                if (_context.Database.CanConnect())
                {
                    return Ok("Successfully connected to the database.");
                }
                return BadRequest("Failed to connect to the database.");
    
        }
        
    }
}