using ExerciceWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ExerciceWebAPI.DAL
{
    public class FollowerDbContext : DbContext
    {
        public DbSet<Follower> Followers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=FollowersDB.db", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map table names
            modelBuilder.Entity<Follower>().ToTable("FollowersDb", "Followers");
            modelBuilder.Entity<Follower>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.HasIndex(e => e.FirstName);
                entity.Property(e => e.LastName);
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
