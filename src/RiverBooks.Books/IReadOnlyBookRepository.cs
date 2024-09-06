namespace RiverBooks.Books;

internal interface IReadOnlyBookRepository
{
    Task<List<Book>> ListAsync();
    Task<Book?> GetByIdAsync(Guid id);
}
