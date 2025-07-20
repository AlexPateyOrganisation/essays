using Essays.Core.Data.EntityMappings;
using Essays.Core.Data.Interceptors;
using Essays.Core.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace Essays.Core.Data.Data;

public class EssaysContext(DbContextOptions<EssaysContext> options) : DbContext(options)
{
    public DbSet<Essay> Essays => Set<Essay>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new EssayMapping());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new EssaysContextSaveChangesInterceptor());
    }
}