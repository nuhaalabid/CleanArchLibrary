﻿using System.Text.Json.Serialization;

namespace Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public int AuthorId { get; set; }
        [JsonIgnore]
        public Author Author { get; set; }

        public Book() { }
        public Book(int id, string title, string description)
        {
            Id = id;
            Title = title;
            Description = description;
        }

        public Book(int id, string title, string description, Author author)
        {
            Id = id;
            Title = title;
            Description = description;
            Author = author;
        }
    }
}
