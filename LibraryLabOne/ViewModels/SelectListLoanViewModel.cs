using LibraryLabOne.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryLabOne.ViewModels
{
    public class SelectListLoanViewModel
    {
        public List<SelectListItem> selectListItems { get; set; }
        public IEnumerable<Customer> customers { get; set; }
        public IEnumerable<Book> books { get; set; }
    }
}
