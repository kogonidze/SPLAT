using MassTransit;
using SPLAT.Messaging.Contracts;

namespace SPLAT.Monolith.Services
{
    public class InvoiceSvc : IInvoiceSvc
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public InvoiceSvc(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task CreateInvoice(int customerNumber, List<InvoiceItems> invoiceItems)
        {
            await _publishEndpoint.Publish<InvoiceToCreate>(new { CustomerNumber = customerNumber, InvoiceItems  = invoiceItems});
        }
    }
}
