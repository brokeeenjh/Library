using Microsoft.AspNetCore.Identity;

namespace Library.Infrastructure.Entities;

public class Role : IdentityRole<Guid>
{
    public List<Permission> Permissions { get; set; }
}