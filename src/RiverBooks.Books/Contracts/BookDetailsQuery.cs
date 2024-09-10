﻿using Ardalis.Result;
using MediatR;

namespace RiverBooks.Books.Contracts
{
    public record BookDetailsQuery(Guid Id) : IRequest<Result<BookDetailsResponse>>;

    internal class BookDetailsQueryHandler : IRequestHandler<BookDetailsQuery, Result<BookDetailsResponse>>
    {
        private readonly IBookService _bookService;

        public BookDetailsQueryHandler(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<Result<BookDetailsResponse>> Handle(BookDetailsQuery request, CancellationToken cancellationToken)
        {
            var book = await _bookService.GetBookAsync(request.Id);

            if (book is null)
            {
                return Result<BookDetailsResponse>.NotFound();
            }

            var response = Result<BookDetailsResponse>.Success(new BookDetailsResponse(book.Id, book.Title, book.Author, book.Price));

            return response;
        }

    }
}
