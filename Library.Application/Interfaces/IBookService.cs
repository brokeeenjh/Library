using Library.Application.DTO;
using Library.Domain.IRepositories;

namespace Library.Application.Interfaces;

public interface IBookService
{
    public Task<IEnumerable<BookDTO>> GetAllBooksAsync();
    public Task<List<BookDTO>> GetAllBookByTitleAsync(string title);    
    public Task<List<BookDTO>> GetAllBookByAuthorAsync(string author);
    public Task<BookDTO> GetBookByIdAsync(Guid id);
    public  Task DeleteBookAsync(Guid id);
    public Task AddBookAsync(string Title, string Author, string Description, DateTime PublishDate);
}