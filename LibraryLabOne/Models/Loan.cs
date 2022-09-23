using System.ComponentModel.DataAnnotations;

namespace LibraryLabOne.Models
{
    public class Loan
    {
        [Key]
        public int Id { get; set; }
        public Book Book { get; set; }
        public int BookId { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
