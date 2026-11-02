using Library.Domain.Permissions;

namespace Library.Application.Interfaces;

public interface IJwtProvider
{
    public string GenerateToken(User user);
}