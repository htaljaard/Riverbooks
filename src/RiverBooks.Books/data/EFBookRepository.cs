using Microsoft.EntityFrameworkCore;

namespace RiverBooks.Books;

internal class EFBookRepository : IBookRepository
{
    private readonly BooksDBContext _context;

    public EFBookRepository(BooksDBContext context)
    {
        _context = context;
    }

    public Task AddAsync(Book book)
    {
        _context.Books.Add(book);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Book book)
    {
        _context.Remove(book);
        return Task.CompletedTask;
    }

    public async Task<Book?> GetByIdAsync(Guid id)
    {
        return await _context.Books.FindAsync(id);
    }

    public Task<List<Book>> ListAsync()
    {
        return _context.Books.ToListAsync();
    }

    public Task SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }
}