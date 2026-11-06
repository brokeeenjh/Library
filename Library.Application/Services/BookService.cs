using Library.Application.DTO;
using Library.Application.Interfaces;
using Library.Domain.IRepositories;

namespace Library.Application.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    
    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<IEnumerable<BookDTO>> GetAllBooksAsync()
    {
        var book = await _bookRepository.GetAllBookAsync();

        var bookDTO = book.Select(x => new BookDTO(x.Title, x.Author, x.Description, x.PublishDate)
        {
        });
        
        return bookDTO;
    }

    public async Task<List<BookDTO>> GetAllBookByTitleAsync(string title)
    {
        var books = await _bookRepository.GetAllBookByTitleAsync(title);

        var booksDto = books.Select(x => new BookDTO(x.Title, x.Author, x.Description, x.PublishDate));
        
        return booksDto.ToList();
    }

    public async Task<List<BookDTO>> GetAllBookByAuthorAsync(string author)
    {
        var books = await _bookRepository.GetAllBookByAuthorAsync(author);
        
        var booksDto =  books.Select(x => new BookDTO(x.Title, x.Author, x.Description, x.PublishDate));
        
        return booksDto.ToList();
    }

    public async Task<BookDTO> GetBookByIdAsync(Guid id)
    {
        var book = await _bookRepository.GetBookByIdAsync(id);
        
        var bookDTO = new BookDTO(book.Title, book.Author, book.Description, book.PublishDate);
        
        return bookDTO;
    }

    public async Task DeleteBookAsync(Guid id)
    {
        await _bookRepository.DeleteBookAsync(id);
    }

    public async Task AddBookAsync(string Title, string Author, string Description, DateTime PublishDate)
    {
        await _bookRepository.AddBookAsync(Title, Author, Description, PublishDate);
    }
}