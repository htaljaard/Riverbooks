namespace RiverBooks.Books;

internal interface IBookService
{
    Task<List<BookDto>> ListBooksAsync();

    Task<BookDto> GetBookAsync(Guid id);

    Task CreateBookAsync(BookDto newBook);

    Task DeleteBookAsync(Guid id);

    Task UpdatePriceAsync(Guid id, decimal newPrice);
}
