using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models
{
    public class Author
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Namnet är obligatoriskt.")]
        [StringLength(100, ErrorMessage = "Namnet får inte vara längre än 100 tecken.")]
        public string Name { get; set; }

        public Author() 
        {
        }

        public Author(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
