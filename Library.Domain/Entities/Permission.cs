using Microsoft.AspNetCore.Authorization;

namespace Library.Domain.Permissions;

public class Permission : IAuthorizationRequirement
{
    public string Name { get; set; }

    public Permission(string permission)
    {
        Name =  permission;
    }
}