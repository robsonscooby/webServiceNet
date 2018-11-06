using Microsoft.EntityFrameworkCore;

namespace webServiceNet.Models
{
    public class CervejaDbContext : DbContext
    {
        public CervejaDbContext(DbContextOptions<CervejaDbContext> options): base (options)
        {}

        public DbSet<Cerveja> Cervejas { get; set; }
    }
}