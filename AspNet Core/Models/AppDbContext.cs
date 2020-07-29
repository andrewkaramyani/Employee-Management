using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNet_Core.Models
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Employee> employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();

            foreach (var forignkey in modelBuilder.Model.GetEntityTypes().
                SelectMany(c => c.GetForeignKeys()))
            {
                forignkey.DeleteBehavior = DeleteBehavior.Restrict;
            }
            //modelBuilder.Entity<Employee>().HasData(
            //    new Employee { Id = 1, Name = "Andrew", Department = Dept.IT, Email = "andrew@gmail.com" },
            //    new Employee { Id = 2, Name = "Ali", Department = Dept.Manger, Email = "Ali@gmail.com" },
            //    new Employee { Id = 3, Name = "Tony", Department = Dept.None, Email = "Tony@gmail.com" }
            //    );
        }
    }
}
