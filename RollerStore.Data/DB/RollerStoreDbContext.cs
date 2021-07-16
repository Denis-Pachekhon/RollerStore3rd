using Microsoft.EntityFrameworkCore;
using RollerStore.Data.Entities;

namespace RollerStore.Data.DB
{
    public class RollerStoreDbContext : DbContext
    {
        public RollerStoreDbContext(DbContextOptions<RollerStoreDbContext> options) : base(options)
        {
        }

        public DbSet<StoreEntity> Stores { get; set; }
        
        public DbSet<RollerEntity> Rollers { get; set; }
    }
}
