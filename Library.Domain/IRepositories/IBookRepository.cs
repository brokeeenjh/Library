namespace Library.Domain.IRepositories;

public interface IBookRepository
{
    public Task<List<Book>> GetAllBookAsync();
    public Task<List<Book>> GetAllBookByAuthorAsync(string author);
    public Task<List<Book>> GetAllBookByTitleAsync(string title);
    public Task<Book>  GetBookByIdAsync(Guid id);
    public Task DeleteBookAsync(Guid id);
    public Task AddBookAsync(Book book);
}