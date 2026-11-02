using Library.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.DbContext;

public class AppDbContext : IdentityDbContext<UserEntity, RoleEntity, Guid>
{
    public DbSet<BookEntity>  Books { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
}