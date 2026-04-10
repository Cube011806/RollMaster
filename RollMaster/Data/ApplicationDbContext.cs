using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RollMaster.Models;

namespace RollMaster.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Armor> Armors { get; set; }
        public DbSet<BodyArmor> BodyArmors { get; set; }
        public DbSet<Helmet> Helmets { get; set; }
        public DbSet<Shield> Shields { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Skill>()
                .HasOne(s => s.Character)
                .WithMany(c => c.Skills)
                .HasForeignKey(s => s.CharacterId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Weapon>()
                .HasOne(w => w.Character)
                .WithMany(c => c.Weapons)
                .HasForeignKey(w => w.CharacterId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Armor>().UseTptMappingStrategy();

            modelBuilder.Entity<Character>()
                .HasOne(c => c.Zbroja)
                .WithOne(a => a.Character)
                .HasForeignKey<BodyArmor>(a => a.CharacterId);

            modelBuilder.Entity<Character>()
                .HasOne(c => c.Helm)
                .WithOne(h => h.Character)
                .HasForeignKey<Helmet>(h => h.CharacterId);

            modelBuilder.Entity<Character>()
                .HasOne(c => c.Tarcza)
                .WithOne(s => s.Character)
                .HasForeignKey<Shield>(s => s.CharacterId);

            modelBuilder.Entity<Character>()
                .HasOne(c => c.Game)
                .WithMany(g => g.Characters)
                .HasForeignKey(c => c.GameId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }

}
