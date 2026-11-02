namespace Library.Domain.IRepositories;

public interface IUserRepository
{
    public Task Register(string username, string password, string email);
    public Task<string> Login(string username, string password);
}