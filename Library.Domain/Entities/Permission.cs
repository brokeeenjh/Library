namespace Library.Domain.Permissions;

public class Permission
{
    public string Name { get; set; }

    public Permission(string permission)
    {
        Name =  permission;
    }
}