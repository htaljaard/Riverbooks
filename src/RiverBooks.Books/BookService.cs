namespace RiverBooks.Books;

internal class BookService : IBookService
{
    public List<BookDto> GetBooks()
    {
        return [
            new BookDto(Guid.NewGuid(), "The Fellowship of the Ring", "J.R.R. Tolkien", 1954),
            new BookDto(Guid.NewGuid(), "The Two Towers", "J.R.R. Tolkien", 1954),
            new BookDto(Guid.NewGuid(), "The Return of the King", "J.R.R. Tolkien", 1955)
        ];
    }
}
