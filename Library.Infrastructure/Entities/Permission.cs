namespace Library.Infrastructure.Entities;

public class Permission
{
    public string Name { get; set; }

    public Permission(string permissionName)
    {
        Name = permissionName;
    }
}