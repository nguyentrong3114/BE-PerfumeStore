using Microsoft.EntityFrameworkCore;
using BE_AMPerfume.Core.Models;

namespace BE_AMPerfume.DAL.Data;

public class AMPerfumeDbContext : DbContext
{
    public AMPerfumeDbContext(DbContextOptions<AMPerfumeDbContext> options)
        : base(options) { }

    public DbSet<User> Users => Set<User>();
}
