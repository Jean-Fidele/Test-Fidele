using Domain.Identity;
using Microsoft.EntityFrameworkCore;

namespace Data.Context.DbInitializer
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>().HasNoKey().HasData(
                new ApplicationUser
                {
                    Id = "0001",
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    Firstname = "admin@gmail.com",
                    EmailConfirmed = true,
                    Lastname = "Admin Spark",
                }
            );

        }
    }
}