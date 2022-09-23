using LibraryLabOne.Models;

namespace LibraryLabOne.ViewModels
{
    public class CustomerLoansViewModel
    { 
        public IEnumerable<Loan> Loans { get; set; }
        public Customer Cust { get; set; }
        public Book Books { get; set; }
    }
}
