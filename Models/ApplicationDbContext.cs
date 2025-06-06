using Microsoft.EntityFrameworkCore;

namespace YakitTuketimTahmini.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<TahminVerisi> Tahminler { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
