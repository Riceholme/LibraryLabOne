using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryLabOne.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(30)")]
        [DisplayName("Title")]
        [MaxLength(30, ErrorMessage = "Maximum is 30 letters")]
        public string Title { get; set; }

        [Required]
        public bool inStock { get; set; }
    }
}
