using Microsoft.EntityFrameworkCore;
using SPLAT.Messaging.Contracts;

namespace SPLAT.Invoice
{
    public class InvoiceContext : DbContext
    {
        public DbSet<Invoice>  Invoices { get; set; }

        public InvoiceContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=invoicedb;Trusted_Connection=True;");
        }
    }
}
