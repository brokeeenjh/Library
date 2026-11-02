namespace Library.Domain.Permissions;

public class Role
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    
    
    public List<Permission> Permission { get; set; }
}