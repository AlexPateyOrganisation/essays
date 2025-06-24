using Essays.Core.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace Essays.Core.Data.Data;

public class EssaysContext(DbContextOptions<EssaysContext> options) : DbContext(options)
{
    public DbSet<Essay> Essays => Set<Essay>();
}