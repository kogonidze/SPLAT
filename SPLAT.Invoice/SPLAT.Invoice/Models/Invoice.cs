namespace SPLAT.Invoice.Models
{
    public class Invoice
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public DateTime Created { get; set; }

        public List<InvoiceItem> InvoiceItems { get; set; }
    }
}
