using Library.Domain.IRepositories;
using Library.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Infrastructure;

public class BookRepository : IBookRepository
{
    private readonly AppDbContext  _dbContext;
    
    public BookRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Book>> GetAllBookAsync()
    {
        var books = await  _dbContext.Books.ToListAsync();
        
        var booksToReturn = books.Select(b => new Book()
        {
            Author = b.Author,
            Title = b.Title,
            PublishDate = b.PublishDate,
            Description = b.Description,
        });
        
        return booksToReturn.ToList();
    }

    public async Task<List<Book>> GetAllBookByAuthorAsync(string author)
    {
        var book = await _dbContext.Books
            .AsNoTracking()
            .Where(b => b.Author == author)
            .ToListAsync();

        var bookToReturn = book.Select(b => new Book()
        {
            Author = b.Author,
            Title = b.Title,
            PublishDate = b.PublishDate,
            Description = b.Description,
        });
        
        return bookToReturn.ToList();
    }

    public async Task<List<Book>> GetAllBookByTitleAsync(string title)
    {
        var book = _dbContext.Books
            .AsNoTracking()
            .Where(b => b.Title == title);

        var bookToReturn = book.Select(b => new Book()
        {
            Author = b.Author,
            Title = b.Title,
            PublishDate = b.PublishDate,
            Description = b.Description,
        });

        return  await bookToReturn.ToListAsync();
    }

    public async Task<Book> GetBookByIdAsync(Guid id)
    {
        var bookEntity = await _dbContext.Books
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        var book = new Book()
        {
            Author = bookEntity.Author,
            Title = bookEntity.Title,
            PublishDate = bookEntity.PublishDate,
            Description = bookEntity.Description

        };
        
        return book;
    }

    public async Task DeleteBookAsync(Guid id)
    {
        var book = await _dbContext.Books
            .FirstOrDefaultAsync(x => x.Id == id);
        
        _dbContext.Books.Remove(book);
        await _dbContext.SaveChangesAsync();
    }

    public async Task AddBookAsync(Book book)
    {
        _dbContext.Add(book);
        await _dbContext.SaveChangesAsync();
    }
}