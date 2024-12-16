using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Titeln är obligatorisk.")]
        [StringLength(200, ErrorMessage = "Titeln får inte överstiga 200 tecken.")]
        public string Title { get; set; }

        [StringLength(500, ErrorMessage = "Beskrivningen får inte överstiga 500 tecken.")]
        public string Description { get; set; }

        [ForeignKey("Author")]
        [Required(ErrorMessage = "Författar-ID är obligatoriskt.")]
        public int AuthorId { get; set; }
 
        public Author Author { get; set; }
       
        public Book() { }


        public Book(int id, string title, string description, int authorId ,Author author)
        {
            Id = id;
            Title = title;
            Description = description;
            AuthorId = authorId;
            Author = author;
        }

        
    }
}
