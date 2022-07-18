using MassTransit;
using SPLAT.Messaging.Contracts;

namespace SPLAT.Invoice
{
    public class EventConsumer : IConsumer<InvoiceToCreate>
    {
        public async Task Consume(ConsumeContext<InvoiceToCreate> context)
        {
            using (InvoiceContext db = new InvoiceContext())
            {
                var existingInvoice = db.Invoices.Where(_
                    => _.CustomerId == context.Message.CustomerNumber
                    && _.InvoiceItems == context.Message.InvoiceItems
                    && _.Created.Date == DateTime.Now.Date);

                if (!existingInvoice.Any())
                {
                    db.Invoices.Add(new Invoice
                    {
                        CustomerId = context.Message.CustomerNumber,
                        InvoiceItems = context.Message.InvoiceItems,
                        Created = DateTime.Now
                    });

                    db.SaveChanges();
                }

                var createdInvoice = db.Invoices.Where(_
                    => _.CustomerId == context.Message.CustomerNumber
                    && _.InvoiceItems == context.Message.InvoiceItems
                    && _.Created.Date == DateTime.Now.Date)
                    .SingleOrDefault();

                if (createdInvoice != null)
                {
                    await context.Publish<InvoiceCreated>(new
                    {
                        InvoiceNumber = createdInvoice.Id,
                        InvoiceData = new
                        {
                            CustomerNumber = context.Message.CustomerNumber,
                            InvoiceItems = context.Message.InvoiceItems
                        }
                    });
                }
            }
        }
    }
}
