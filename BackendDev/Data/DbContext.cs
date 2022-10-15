using BackendDev.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendDev.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<UserRegisterModel> UserRegisterModels { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<UserRegisterModel>().HasKey(x => x.Id);
        }
    }

}
