using MassTransit;
using SPLAT.Messaging.Contracts;
using SPLAT.Invoice.Context;

namespace SPLAT.Invoice.Consumers
{
    public class EventConsumer : IConsumer<InvoiceToCreate>
    {
        public async Task Consume(ConsumeContext<InvoiceToCreate> context)
        {
            using (InvoiceContext db = new InvoiceContext())
            {
                var existingInvoice = GetExistingInvoice(db, context);

                if (existingInvoice is not null)
                {
                    return;
                }

                CreateInvoice(db, context);

                var createdInvoice = GetExistingInvoice(db, context);

                if (createdInvoice is not null)
                {
                    await context.Publish<InvoiceCreated>(new
                    {
                        InvoiceNumber = createdInvoice.Id,
                        InvoiceData = new
                        {
                            context.Message.CustomerNumber,
                            context.Message.InvoiceItems
                        }
                    });
                }
            }
        }

        private Models.Invoice? GetExistingInvoice(InvoiceContext db, ConsumeContext<InvoiceToCreate> context)
        {
            return db.Invoices.Where(_
                    => _.CustomerId == context.Message.CustomerNumber
                    && _.Created.Date == DateTime.Now.Date)
                    .FirstOrDefault();
        }

        private void CreateInvoice(InvoiceContext db, ConsumeContext<InvoiceToCreate> context)
        {
            var invoice = new Models.Invoice
            {
                CustomerId = context.Message.CustomerNumber,
                Created = DateTime.Now
            };

            db.Invoices.Add(invoice);

            var invoiceItems = new List<Models.InvoiceItem>();

            foreach (var item in context.Message.InvoiceItems)
            {
                invoiceItems.Add(new Models.InvoiceItem
                {
                    Invoice = invoice,
                    ActualMileage = item.ActualMileage,
                    BaseRate = item.BaseRate,
                    Description = item.Description,
                    Price = item.Price,
                    IsHazardousMaterial = item.IsHazardousMaterial,
                    IsOversized = item.IsOversized,
                    IsRefrigerated = item.IsRefrigerated
                });
            }

            db.InvoiceItems.AddRange(invoiceItems);

            db.SaveChanges();
        }
    }
}
