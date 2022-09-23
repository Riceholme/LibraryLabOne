using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryLabOne.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(20)")]
        [DisplayName("First Name")]
        [MaxLength(20, ErrorMessage = "Maximum is 20 letters")]
        public string FirstName { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(20)")]
        [DisplayName("Last Name")]
        [MaxLength(20, ErrorMessage = "Maximum is 20 letters")]
        public string LastName { get; set; }

        //[Required]
        [Column(TypeName = "nvarchar(25)")]
        [DisplayName("Email")]
        [MaxLength(25, ErrorMessage = "Maximum is 25 characters")]
        public string? Email { get; set; }

        public List<Loan>? Books { get; set; }
    }
}
