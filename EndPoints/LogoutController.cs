using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VolanGo.DbConnection;

namespace VolanGo.EndPoints
{
    [ApiController]
    [Route("logout")]
    public class LogoutController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LogoutController(AppDbContext context)
        {
            _context = context;
        }
    }
}