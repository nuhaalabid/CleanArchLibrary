using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Infrastructure.Database
{
        public class FakeDatabase
        {
            public List<Book> Books { get; set; } = new List<Book>();
            public List<Author> Authors { get; set; } = new List<Author>();
            public List<User> Users { get; set; } = new List<User>();
        public FakeDatabase()
        {
            Authors = new List<Author>
            {
                new Author(1, "Anna"),
                new Author(2, "Bob"),
                new Author(3, "Carl")
            };

            var Books = new List<Book>
{
             new Book { Id = 1, Title = "Sky", Description = "Beskrivning1", AuthorId = 1, Author = Authors[0] },
             new Book { Id = 2, Title = "Fire", Description = "Beskrivning2", AuthorId = 2, Author = Authors[1] },
             new Book { Id = 3, Title = "Ocean", Description = "Beskrivning3", AuthorId = 3, Author = Authors[2] },
             new Book { Id = 4, Title = "Dream", Description = "Beskrivning4", AuthorId = 2, Author = Authors[1] },
             new Book { Id = 5, Title = "Star", Description = "Beskrivning5", AuthorId = 1, Author = Authors[0] }
};

            Users = new List<User>
            {
                new User(1, "admin", "password123"),
                new User(2, "user1", "securepass")
            };
        }
    }

}

