using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RollMaster.Models;

namespace RollMaster.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {
        public DbSet<User> User { get; set; }
        public DbSet<Character> Character { get; set; }
    }

}
