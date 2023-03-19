using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CategorieMap());
            builder.ApplyConfiguration(new ProduitMap());
            builder.ApplyConfiguration(new UserInfoMap());
            base.OnModelCreating(builder);
        }
    }
}