using SPLAT.Messaging.Contracts;

namespace SPLAT.Monolith.Services
{
    public interface IInvoiceSvc
    {
        Task CreateInvoice(int customerNumber, List<InvoiceItems> invoiceItems);
    }
}
