namespace BMS_Db.EfContext;
using Microsoft.EntityFrameworkCore;
public class BmsV1DbContext : DbContext
{
    public BmsV1DbContext(DbContextOptions<BmsV1DbContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }
}