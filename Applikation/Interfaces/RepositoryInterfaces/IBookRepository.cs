using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applikation.Interfaces.RepositoryInterfaces
{
    public interface IBookRepository
    { 
        Task<Book> AddBook(Book book);
        Task<Book> UpdateBook(Book book);
        Task DeleteBook (int id);
        Task<Book> GetBookById (int id);
        Task<List<Book>> GetAllBooks();
    }
}
