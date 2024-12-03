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

            Books = new List<Book>
            {
                new Book(1, "Sky", "Beskrivning1", Authors[0]),
                new Book(2, "Fire", "Beskrivning2", Authors[1]),
                new Book(3, "Ocean", "Beskrivning3", Authors[2]),
                new Book(4, "Dream", "Beskrivning4", Authors[1]),
                new Book(5, "Star", "Beskrivning5", Authors[0])
            };

            Users = new List<User>
            {
                new User(1, "admin", "password123"),
                new User(2, "user1", "securepass")
            };
        }
    }

}

