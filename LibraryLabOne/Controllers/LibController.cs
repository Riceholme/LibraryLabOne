using LibraryLabOne.Models;
using LibraryLabOne.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryLabOne.Controllers
{
    public class LibController : Controller
    {
        private readonly AppDbContext _context;

        public LibController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult LoanPage()
        {
            return View();
        }

        public async Task<IActionResult> CustomerList()
        {
            return View(await _context.Customers.ToListAsync());
        }

        public IActionResult SingleCustomer()
        {
            return View();
        }

        public IActionResult CustomerLoans(int id)
        {
            var customerLoanedBooks = _context.Loans
                .Include(b => b.Book)
                //.Include(p => p.Customer)
                .Where(c => c.Id == id)
                .ToList();

            var customer = _context.Customers
                .FirstOrDefault(x => x.Id == id);

            var cLoansVModel = new CustomerLoansViewModel()
            {
                Loans = customerLoanedBooks,
                Cust = customer
                
            };

            return View(cLoansVModel);
        }

        public IActionResult CustomerDetails(int id)
        {
            var customer = _context.Customers
                .FirstOrDefault(x => x.Id == id);

            var cDetailsVModel = new CustomerDetailsViewModel()
            {
                Customer = customer
            };
            return View(cDetailsVModel);
        }
        
        public IActionResult AddCustomer(int id)
        {
            if (id == 0)
            {
                return View(new Customer());
            }
            else
            {
                return View(_context.Customers.Find(id));
            }
        }
        [HttpPost, ActionName("CreateACustomer")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateACustomer([Bind("Id,FirstName,LastName,Email,Books")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
            }
            else
            {
                return RedirectToAction(nameof(CreateACustomer));
            }
            //return View(customer);
            return RedirectToAction(nameof(CustomerList));
        }
        //[ActionName("CreateCustomerForm")]
        public IActionResult CreateACustomer()
        {
            return View();
        }
        public IActionResult EditCustomer(int id)
        {
            var customer = _context.Customers
                .FirstOrDefault(x => x.Id == id);

            var cDetailsVModel = new CustomerDetailsViewModel()
            {
                Customer = customer
            };
            return View(customer);
        }
        public IActionResult UpdateCust(int id)
        {
            if (id == 0)
            {
                return View(new Customer());
            }
            else
            {
                return View(_context.Customers.Find(id));
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateCust([Bind("Id,FirstName,LastName,Email")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(customer).State = EntityState.Modified;
                _context.SaveChanges();
                RedirectToAction(nameof(CustomerList));
            }
            return RedirectToAction(nameof(CustomerList));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(CustomerList));
            }
            return NotFound();
        }
        public async Task<IActionResult> LoanBookPage()
        {
            return View(await _context.Books.ToListAsync());
        }
        public IActionResult AddCustomerAndBookToLoan(Loan loan)
        {
            if (loan.CustomerId != null && loan.BookId != null)
            {
                var StartDateStamp = DateTime.Now;
                var endDate = StartDateStamp.AddDays(15);

                _context.Loans.Add(new Loan() { BookId = loan.BookId, CustomerId = loan.CustomerId, StartDate = StartDateStamp, EndDate = endDate });
                _context.SaveChanges();
                return RedirectToAction(nameof(CustomerList));
            }
            return RedirectToAction("Index", "Home");

        }
        public IActionResult AddBookToLoan(int id)
        {
            var book = _context.Books.FirstOrDefault(x => x.Id == id);
            return RedirectToAction(nameof(LoanCustomerPage));
        }
        public async Task<IActionResult> LoanCustomerPage()
        {
            return View(await _context.Customers.ToListAsync());
        }
        public IActionResult AddCustomerToLoan()
        {
            return RedirectToAction(nameof(CustomerList));
        }
        public IActionResult LoanConfirmationView()
        {
            return View();
        }
        public IActionResult ReturnLoanedBook(int id)
        {
            if (id != 0)
            {
                var loanToRemove = _context.Loans.FirstOrDefault(x => x.Id == id);
                if (loanToRemove != null)
                {
                    var bookOfLoan = _context.Books.FirstOrDefault(x => x.Id == loanToRemove.BookId);
                    bookOfLoan.inStock = true;
                    _context.Loans.Remove(loanToRemove);
                    _context.Entry(bookOfLoan).State = EntityState.Modified;
                    _context.SaveChanges();
                    return View(nameof(LoanBookPage));
                }
                return NotFound("Loan not found");
            }
            return NotFound("Id was not found");
        }
    }
}
