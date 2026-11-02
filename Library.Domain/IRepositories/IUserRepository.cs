namespace Library.Domain.IRepositories;

public interface IUserRepository
{
    public Task Register(string username, string password, string email);
    public Task<string> Login(string email, string password);
    public Task<IEnumerable<string>> GetAllPermissions(Guid id);
}