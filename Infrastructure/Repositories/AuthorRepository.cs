using Applikation.Interfaces.RepositoryInterfaces;
using Infrastructure.Database;
using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly Realdatabase _realdatabase;

        public AuthorRepository(Realdatabase realdatabase)
        {
            _realdatabase = realdatabase;
        }

        public async Task<Author> AddAuthor(Author author)
        {
            _realdatabase.Authors.Add(author);
            await _realdatabase.SaveChangesAsync();
            return author;
        }

        public async Task<Author> UpdateAuthor(Author author)
        {
            _realdatabase.Authors.Update(author);
            await _realdatabase.SaveChangesAsync();
            return author;
        }

        public async Task DeleteAuthor(int id)
        {
            var author = await _realdatabase.Authors.FindAsync(id);
            if (author != null)
            {
                _realdatabase.Authors.Remove(author);
                await _realdatabase.SaveChangesAsync();
            }
        }

        public async Task<Author> GetAuthorById(int id)
        {
            return await _realdatabase.Authors.FindAsync(id);
        }

        public async Task<List<Author>> GetAllAuthors()
        {
            return await _realdatabase.Authors.ToListAsync();
        }
    }
}
        

    
    

