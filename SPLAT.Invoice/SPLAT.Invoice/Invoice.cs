using SPLAT.Messaging.Contracts;

namespace SPLAT.Invoice
{
    public class Invoice
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public List<InvoiceItems> InvoiceItems { get; set; }

        public DateTime Created { get; set; }
    }
}
