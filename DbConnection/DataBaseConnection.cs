using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VolanGo.Entities;

namespace VolanGo.DbConnection
{
    
   public class AppDbContext : DbContext
   {
       public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
       {
       }

       public DbSet<Person> Person { get; set; }

        internal async Task FindByIdAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}