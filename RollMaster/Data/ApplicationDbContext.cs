using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RollMaster.Models;

namespace RollMaster.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {
        public DbSet<User> User { get; set; }
        public DbSet<Character> Character { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Game> Game { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Skill>()
                .HasOne(s => s.Character)
                .WithMany(c => c.skills)
                .HasForeignKey(s => s.CharacterId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
