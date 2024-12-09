using MediatR;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applikation.Books.Commands.UpdateBook
{
    public class UpdateBookByIdCommand : IRequest<OperationResult<Book>>
    {
        public int Id { get; }
        public Book UpdatedBook { get; }

        public UpdateBookByIdCommand(Book updatedBook, int id)
        {
            UpdatedBook = updatedBook;
            Id = id;
        }

       
    }
}
