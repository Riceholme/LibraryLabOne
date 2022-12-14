using LibraryLabOne.Models;
using LibraryLabOne.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            var customer = _context.Customers
                .FirstOrDefault(x => x.Id == id);
            var customerLoans = _context.Loans
                .Where(x => x.CustomerId == id)
                .Include(y => y.Book)
                .Where(b => b.BookId == b.Book.Id);


            var customerLoanedBooks = _context.Loans
                .Include(b => b.Book)
                //.Include(p => p.Customer)
                .Where(c => c.Id == id)
                .ToList();



            var cLoansVModel = new CustomerLoansViewModel()
            {
                Loans = customerLoans,
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
            var anyLoans = _context.Loans.Any(x => x.CustomerId == id);
            if (customer != null)
            {
                if (anyLoans == true)
                {
                    return RedirectToAction(nameof(DeleteError));
                }
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(CustomerList));
            }
            return NotFound();
        }
        public IActionResult DeleteError()
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

                }
                //return NotFound("Loan not found");
            }
            //return NotFound("Id was not found");
            return RedirectToAction(nameof(CustomerList));
        }

        public IActionResult SelectListLoan()
        {
            List<SelectListItem> sLICustomers = new List<SelectListItem>() { };
            foreach (var cust in _context.Customers)
            {
                sLICustomers.Add(new SelectListItem { Text = cust.FirstName + cust.LastName, Value = cust.Id.ToString() });
            }
            List<SelectListItem> sLIBooks = new List<SelectListItem>() { };
            foreach (var book in _context.Books)
            {
                if (book.inStock == true)
                {
                    sLIBooks.Add(new SelectListItem { Text = book.Title, Value = book.Id.ToString() });
                }
            }
            //var dictionaryOne = new Dictionary<int, string>();
            //foreach (var cust in _context.Customers)
            //    {
            //    dictionaryOne.Add(cust.Id, cust.FirstName);
            //    }
            //ViewBag.SelectList = new SelectList(dictionaryOne,"Id", "Name");
            //Loan tempLoan = new Loan();
            //SelectList sLCusts = new SelectList(_context.Customers, "CustomerId", "Name", loan.CustomerId);
            //SelectList sLBooks = new SelectList(_context.Books, "BookId", "Title", loan.BookId);

            //ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "FirstName"/*, loan.CustomerId*/);
            //ViewData["BookId"] = new SelectList(_context.Books, "BookId", "Title"/*, loan.BookId*/);
            //var dbCusts = from custies in _context.Customers
            //              select custies;
            //var dbBooks = from bookies in _context.Books
            //              select bookies;
            //var selectListLoanVM = new SelectListLoanViewModel()
            //{
            //    customers = dbCusts,
            //    books = dbBooks
            //};
            //ViewBag.selCusts = new SelectList(_context.Customers, "CustomerId", "Name", tempLoan.CustomerId);
            //ViewBag.selCusts = sLCusts;
            //ViewBag.selBooks = sLBooks;
            //ViewBag.selBooks = new SelectList(_context.Books, "BookId", "Title", tempLoan.BookId);
            ViewBag.MyCustss = sLICustomers;
            ViewBag.MyBookss = sLIBooks;
            //TempLoanViewModel tlvm = new TempLoanViewModel();
            return View();
        }

        [HttpPost]
        public IActionResult StringDetourForFun(TempLoanViewModel tempLoanVM)
        {

            var customerId = Int32.Parse(tempLoanVM.StringCustId);
            var bookId = Int32.Parse(tempLoanVM.StringBookId);
            //Loan tempLoan = new Loan()
            //{
            //    CustomerId = customerId,
            //    BookId = bookId,
            //};

            var StartDateStamp = DateTime.Now;
            var endDate = StartDateStamp.AddDays(15);

            _context.Loans.Add(new Loan() { BookId = bookId, CustomerId = customerId, StartDate = StartDateStamp, EndDate = endDate });
            var bookGotLoaned = _context.Books.Find(bookId);
            bookGotLoaned.inStock = false;
            _context.Books.Update(bookGotLoaned);
            _context.SaveChanges();
            return RedirectToAction(nameof(CustomerList));

            //return RedirectToAction("Index", "Home");
            //return RedirectToAction(AddCustomerAndBookToLoan(tempLoan));
        }
        //public IActionResult CreateLoanView()
        //{
        //    var allBooks = _context.Books.ToList();
        //    var allCust = _context.Customers.ToList();

        //    var createLoanV = new CreateLoanViewModel()
        //    {
        //        books = allBooks,
        //        customers = allCust
        //    };
        //    return View(createLoanV);
        //}
        //public IActionResult SelectedCustomerForLoan(int id)
        //{
        //    var newLoan = new Loan() { };

        //}
        //public async Task<IActionResult> LoanBookPage(int id)
        //{
        //    var newLoan = new Loan() { };
        //    if (newLoan.CustomerId == 0 && newLoan.BookId == 0)
        //    {
        //        newLoan.CustomerId = id;
        //        return View(await _context.Books.ToListAsync());
        //    }
        //    else if(newLoan.CustomerId != 0 && newLoan.BookId == 0)
        //    {
        //        newLoan.BookId = id;
        //        return RedirectToAction(nameof(AddCustomerAndBookToLoan));
        //    }
        //    return RedirectToAction(nameof(CustomerList));
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCustomerAndBookToLoan(/*[Bind("Id", "BookId", "CustomerId", "StartDate","ReturnedDate","EndDate")] Loan loan)*/Loan loan)
        {
            if (ModelState.IsValid)
            {
                var StartDateStamp = DateTime.Now;
                var endDate = StartDateStamp.AddDays(15);

                _context.Loans.Add(new Loan() { BookId = loan.BookId, CustomerId = loan.CustomerId, StartDate = StartDateStamp, EndDate = endDate });
                _context.SaveChanges();
                return RedirectToAction(nameof(CustomerList));
            }
            return RedirectToAction("Index", "Home");
        }
        //public IActionResult AddBookToLoan(int id)
        //{
        //    var book = _context.Books.FirstOrDefault(x => x.Id == id);
        //    var newLoan = new Loan()
        //    {

        //    };
        //    return RedirectToAction(nameof(LoanCustomerPage));
        //}
        //public async Task<IActionResult> LoanCustomerPage()
        //{
        //    return View(await _context.Customers.ToListAsync());
        //}
        //public IActionResult AddCustomerToLoan()
        //{
        //    return RedirectToAction(nameof(CustomerList));
        //}
        //public IActionResult LoanConfirmationView()
        //{
        //    return View();
        //}

    }
}
