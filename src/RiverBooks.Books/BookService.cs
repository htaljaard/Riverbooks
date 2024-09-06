


namespace RiverBooks.Books;

internal class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task CreateBookAsync(BookDto newBook)
    {
        var book = new Book(newBook.Id, newBook.Title, newBook.Author, newBook.Year, newBook.Price);

        await _bookRepository.AddAsync(book);
        await _bookRepository.SaveChangesAsync();
    }

    public async Task DeleteBookAsync(Guid id)
    {
        var bookToDelete = await _bookRepository.GetByIdAsync(id);

        if (bookToDelete is not null)
        {
            await _bookRepository.DeleteAsync(bookToDelete);
            await _bookRepository.SaveChangesAsync();
        }
    }

    public async Task<BookDto> GetBookAsync(Guid id)
    {
        var book = await _bookRepository.GetByIdAsync(id);

        return new BookDto(book!.Id, book.Title, book.Author, book.Year, book.Price);
    }

    public async Task<List<BookDto>> ListBooksAsync()
    {
        var books = (await _bookRepository.ListAsync()).Select(a => new BookDto(a.Id, a.Title, a.Author, a.Year, a.Price)).ToList();

        return books;
    }

    public async Task UpdatePriceAsync(Guid id, decimal newPrice)
    {
        var book = await _bookRepository.GetByIdAsync(id);

        book!.UpdatePrice(newPrice);

        await _bookRepository.SaveChangesAsync();
    }
}
