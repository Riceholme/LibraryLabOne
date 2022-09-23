using Microsoft.EntityFrameworkCore;

namespace LibraryLabOne.Models
{
    public class AppDbContext : DbContext
    {
        //private readonly AppDbContext _appDbContxt;

        //public AppDbContext(AppDbContext appDbContxt)
        //{
        //    _appDbContxt = appDbContxt;
        //}
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        

        public DbSet<Book> Books { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Loan> Loans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //BOOKS
            modelBuilder.Entity<Book>().HasData(new Book
            { Id = 1, Title = "Harry Potter 1", inStock = false });
            modelBuilder.Entity<Book>().HasData(new Book
            { Id = 2, Title = "Harry Potter 2", inStock = false });
            modelBuilder.Entity<Book>().HasData(new Book
            { Id = 3, Title = "Harry Potter 3", inStock = true });
            modelBuilder.Entity<Book>().HasData(new Book
            { Id = 4, Title = "Harry Potter 4", inStock = true });
            modelBuilder.Entity<Book>().HasData(new Book
            { Id = 5, Title = "Harry Potter 5", inStock = true });
            modelBuilder.Entity<Book>().HasData(new Book
            { Id = 6, Title = "Harry Potter 6", inStock = true });
            modelBuilder.Entity<Book>().HasData(new Book
            { Id = 7, Title = "Harry Potter 7", inStock = true });
            modelBuilder.Entity<Book>().HasData(new Book
            { Id = 8, Title = "Harry Potter 1", inStock = true });
            modelBuilder.Entity<Book>().HasData(new Book
            { Id = 9, Title = "Harry Potter 2", inStock = false });
            modelBuilder.Entity<Book>().HasData(new Book
            { Id = 10, Title = "Harry Potter 3", inStock = true });
            modelBuilder.Entity<Book>().HasData(new Book
            { Id = 11, Title = "Harry Potter 4", inStock = true });
            modelBuilder.Entity<Book>().HasData(new Book
            { Id = 12, Title = "Harry Potter 5", inStock = true });
            modelBuilder.Entity<Book>().HasData(new Book
            { Id = 13, Title = "Harry Potter 6", inStock = true });
            modelBuilder.Entity<Book>().HasData(new Book
            { Id = 14, Title = "Harry Potter 7", inStock = true });
            modelBuilder.Entity<Book>().HasData(new Book
            { Id = 15, Title = "Harry Potter 1", inStock = true });
            modelBuilder.Entity<Book>().HasData(new Book
            { Id = 16, Title = "Harry Potter 2", inStock = true });
            modelBuilder.Entity<Book>().HasData(new Book
            { Id = 17, Title = "Harry Potter 3", inStock = true });
            modelBuilder.Entity<Book>().HasData(new Book
            { Id = 18, Title = "Harry Potter 4", inStock = true });
            modelBuilder.Entity<Book>().HasData(new Book
            { Id = 19, Title = "Harry Potter 5", inStock = true });
            modelBuilder.Entity<Book>().HasData(new Book
            { Id = 20, Title = "Harry Potter 6", inStock = true });
            modelBuilder.Entity<Book>().HasData(new Book
            { Id = 21, Title = "Harry Potter 7", inStock = true });

            //CUSTOMERS
            modelBuilder.Entity<Customer>().HasData(new Customer
            {
                Id = 1,
                FirstName = "Knatte",
                LastName = "Anka",
                Email = "knatte@ankbusiness.ank"
            });
            modelBuilder.Entity<Customer>().HasData(new Customer
            {
                Id = 2,
                FirstName = "Fnatte",
                LastName = "Anka",
                Email = "fnatte@ankaab.ank"
            });
            modelBuilder.Entity<Customer>().HasData(new Customer
            {
                Id = 3,
                FirstName = "Tjatte",
                LastName = "Anka",
                Email = "tjatte@ankmail.ank"
            });

            //LOANS
            modelBuilder.Entity<Loan>().HasData(new Loan
            {
                Id = 1,
                BookId = 1,
                CustomerId = 1,
                StartDate = new DateTime(2022, 08, 05),
                EndDate = new DateTime(2022, 08, 20)

            });
            modelBuilder.Entity<Loan>().HasData(new Loan
            {
                Id = 2,
                BookId = 2,
                CustomerId = 2,
                StartDate = new DateTime(2022, 08, 07),
                EndDate= new DateTime(2022, 08, 22)
            });
            modelBuilder.Entity<Loan>().HasData(new Loan
            {
                Id = 3,
                BookId = 9,
                CustomerId = 3,
                StartDate = new DateTime(2022, 08, 15),
                EndDate = new DateTime(2022, 05, 30)
            });
        }
    }
}
