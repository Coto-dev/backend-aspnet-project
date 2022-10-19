using Microsoft.EntityFrameworkCore;

namespace BackendDev.Data.Models
{
    public class ContextDataBase : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<GenreModel> Genres { get; set; }
        public DbSet<MovieModel> MovieModels { get; set; }
        public DbSet<ReviewModelBd> ReviewModels { get; set; }

        public ContextDataBase(DbContextOptions<ContextDataBase> options) : base(options)
        {
            //Database.EnsureCreated();
        }

        /*  protected override void OnModelCreating(ModelBuilder modelBuilder)
          {
            //  modelBuilder.Entity<ProfileModel>().HasKey(x => x.Id);
          }*/
    }

}
