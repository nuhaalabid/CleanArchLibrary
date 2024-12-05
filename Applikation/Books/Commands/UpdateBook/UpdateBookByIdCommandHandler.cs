using Applikation.Interfaces.RepositoryInterfaces;
using MediatR;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applikation.Books.Commands.UpdateBook
{
    public class UpdateBookByIdCommandHandler : IRequestHandler<UpdateBookByIdCommand, Book>
    {
        private readonly IBookRepository _bookRepository;

        public UpdateBookByIdCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Book> Handle(UpdateBookByIdCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetBookById(request.Id);

            if (book == null)
            {
                return null;
            }

            // Uppdatera bokens egenskaper
            book.Title = request.UpdatedBook.Title;
            book.Description = request.UpdatedBook.Description;
            book.Author = request.UpdatedBook.Author;

            return await _bookRepository.UpdateBook(book);
        }
    }

}
