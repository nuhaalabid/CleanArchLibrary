using MediatR;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applikation.Books.Commands.AddBook
{
    public class AddBookCommad
    {

        public class AddBookCommand : IRequest<Book>
        {
            public AddBookCommand(Book newBook)
            {
                NewBook = newBook;
            }
            public Book NewBook { get; }
        }
    }
}
