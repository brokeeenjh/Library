namespace Library.Infrastructure.Entities;

public class PermissionEntity
{
    public string Name { get; set; }

    public PermissionEntity(string permissionName)
    {
        Name = permissionName;
    }
}