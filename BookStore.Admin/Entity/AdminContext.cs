using Microsoft.EntityFrameworkCore;

namespace BookStore.Admin.Entity;

public class AdminContext : DbContext
{
    public AdminContext(DbContextOptions<AdminContext> options) : base(options)
    {
    }

    public DbSet<AdminEntity> AdminTable { get; set; }
}
