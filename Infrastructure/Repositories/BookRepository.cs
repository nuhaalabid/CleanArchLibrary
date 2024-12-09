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
    public class BookRepository: IBookRepository
    {
        private readonly Realdatabase _realdatabase;

        public BookRepository(Realdatabase realdatabase )
        {
            _realdatabase = realdatabase;
        }

        public async Task<Book> AddBook(Book book)
        {
            await _realdatabase.Books.AddAsync(book);
            await _realdatabase.SaveChangesAsync();
            return book;
        }

        public async Task<Book> UpdateBook(Book book)
        {
            _realdatabase.Books.Update(book);
            await _realdatabase.SaveChangesAsync();
            return book;
        }

        public async Task DeleteBook(int id)
        {
            var book = await _realdatabase.Books.FindAsync(id);
            if (book != null)
            {
                _realdatabase.Books.Remove(book);
                await _realdatabase.SaveChangesAsync();
            }
        }

        public async Task<List<Book>> GetAllBooks()
        {
            return await _realdatabase.Books
                .Include(b => b.Author)
                .ToListAsync(); 
        }

        public async Task<Book> GetBookById(int id)
        {
            return await _realdatabase.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

    }

    
}

