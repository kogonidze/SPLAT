using Microsoft.EntityFrameworkCore;
using SPLAT.Invoice.Models;

namespace SPLAT.Invoice.Context
{
    public class InvoiceContext : DbContext
    {
        public DbSet<Models.Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }

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
