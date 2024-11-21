using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RS1_2024_25.API.Data.Models;
using RS1_2024_25.API.Data.Models.Auth;
using VolanGo.Entities;

namespace VolanGo.DbConnection
{
    
   public class AppDbContext : DbContext
   {
       public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
       {
       }

       public DbSet<Person> Person { get; set; }

       public DbSet<Admin> Admins { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CustomerSupportTickets> CustomerSupportTickets { get; set; }
        public DbSet<Discount> Discount { get; set; }
        public DbSet<Locations> Locations { get; set; }
        public DbSet<LoyaltyPoints> LoyaltyPoints { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<VehicleTypes> VehicleTypes { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleImages> VehicleImages { get; set; }
        public DbSet<VehicleMaintenance> VehicleMaintenance { get; set; }
        public DbSet<DamageReports> DamageReports { get; set; }
        public DbSet<UserReviews> UserReviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Set delete behavior for all relationships
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.NoAction;
            }

            // Uncomment and configure if you are using inheritance
            // modelBuilder.Entity<BaseClass>().UseTpcMappingStrategy();
        }


    }
}