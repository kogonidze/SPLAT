using Microsoft.EntityFrameworkCore;
using SPLAT.Payment.FakeBank.Models.Db;

namespace SPLAT.Payment.FakeBank
{
    public class FakeBankContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public FakeBankContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=fakebankdb;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            SeedAccounts(modelBuilder);
        }

        private void SeedAccounts(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().HasData(new Account
            {
                Id = 1,
                FirstName = "Viktor",
                LastName = "Viktorov",
                Patronymic = "Viktorovich",
                DateOfBirth = new DateTime(1990, 1, 1),
                Citizenship = "US",
                Balance = 1000
            });

            modelBuilder.Entity<Account>().HasData(new Account
            {
                Id = 2,
                FirstName = "Ivan",
                LastName = "Ivanov",
                Patronymic = "Ivanovich",
                DateOfBirth = new DateTime(2000, 4, 3),
                Citizenship = "Israel",
                Balance = 1000000
            });

            modelBuilder.Entity<Account>().HasData(new Account
            {
                Id = 3,
                FirstName = "Petr",
                LastName = "Petrov",
                Patronymic = "Petrovich",
                DateOfBirth = new DateTime(1993, 5, 17),
                Citizenship = "Trinidad Tobago",
                Balance = 0
            });
        }
    }
}
