using Microsoft.AspNetCore.Identity;

namespace Library.Infrastructure.Entities;

public class RoleEntity : IdentityRole<Guid>
{
    public List<PermissionEntity> Permissions { get; set; }
}