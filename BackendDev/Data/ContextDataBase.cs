using BackendDev.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendDev.Data
{
    public class ContextDataBase : DbContext
    {
        public DbSet<ProfileModel> ProfileModels { get; set; }

        public ContextDataBase(DbContextOptions<ContextDataBase> options): base(options)
        {
            Database.EnsureCreated();
        }

      /*  protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          //  modelBuilder.Entity<ProfileModel>().HasKey(x => x.Id);
        }*/
    }

}
