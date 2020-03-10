using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAnnotations
{
    public class Course
    {
        public Course()
        {
            Tags = new HashSet<Tag>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Description { get; set; }

        public int Level { get; set; }

        public float FullPrice { get; set; }

        [ForeignKey("Author")] // Magic String Issue
        public int AuthorId { get; set; }

        // This or above both pair the two variables to one column
        // [ForeignKey("AuthorId")]
        public virtual Author Author { get; set; }
        
        public virtual ICollection<Tag> Tags { get; set; }
    }
}