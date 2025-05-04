using Microsoft.EntityFrameworkCore;

namespace CrudOperationsIn.NetCore.Models
{
    public class BrandContext : DbContext
    {
        public BrandContext(DbContextOptions<BrandContext> options) : base(options)
        {
            
        }
        public DbSet<Brands> Brands { get; set; }
    }
}
