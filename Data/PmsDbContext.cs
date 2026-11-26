using Microsoft.EntityFrameworkCore;
using PMS.Models;

namespace PMS.Data;

public class PmsDbContext(DbContextOptions<PmsDbContext> options) : DbContext(options)
{
    public DbSet<Watch> Watches => Set<Watch>();
}
