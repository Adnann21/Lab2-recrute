//using Gym.Models;
using Microsoft.EntityFrameworkCore;
using Recrute.Models;


namespace Recrute.Data
{
    public class RecruteDbContext : DbContext
    {

        public RecruteDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Workers>()
                .HasData(new List<Workers> {
                
                });

        }

        public DbSet<Users> user { get; set; }
        public DbSet<Signup> signup { get; set; }
        public DbSet<Employ> employ { get; set; }
        public DbSet<RecruteComp> recruteComp { get; set; }
        public DbSet<UsingPack> usingpack { get; set; }
        public DbSet<Workers> workers { get; set; }
          public DbSet<Company> Comp {  get; set; }
          public DbSet<Password> Pass {  get; set; }
        public DbSet<Models.Applicants> applicants { get; set; }
        public DbSet<Jobs> jobs { get; set; }
       
    }
  
}
