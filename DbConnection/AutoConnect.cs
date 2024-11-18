using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace VolanGo.DbConnection
{
    public class AutoConnect
    {
        private readonly AppDbContext _context;

        public AutoConnect( AppDbContext context)
        {
            _context = context;
        }
    }
}