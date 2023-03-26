using Data.Context.DbInitializer;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Seed();
            modelBuilder.Entity<ApplicationUser>().HasKey(m => m.Id);
            modelBuilder.Entity<IdentityRole>().HasKey(m => m.Id);
            base.OnModelCreating(modelBuilder);
        }
    }
}
