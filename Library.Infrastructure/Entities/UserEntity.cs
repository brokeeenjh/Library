using Microsoft.AspNetCore.Identity;

namespace Library.Infrastructure.Entities;

public class UserEntity : IdentityUser<Guid>
{
    public Role Role { get; set; }
}