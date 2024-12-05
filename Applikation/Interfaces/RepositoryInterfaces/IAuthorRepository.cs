using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applikation.Interfaces.RepositoryInterfaces
{
    public interface IAuthorRepository
    {
        Task<Author> AddAuthor(Author author);
        Task<Author> UpdateAuthor(Author author);
        Task DeleteAuthor(int id);
        Task<Author> GetAuthorById(int id);
        Task<List<Author>> GetAllAuthors();
    }
}
