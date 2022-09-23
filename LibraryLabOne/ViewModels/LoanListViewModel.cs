using LibraryLabOne.Models;

namespace LibraryLabOne.ViewModels
{
    public class LoanListViewModel
    {
        public IEnumerable<Loan> Loans { get; set; }
    }
}
