namespace SPLAT.Messaging.Contracts
{
    public interface InvoiceToCreate
    {
        int CustomerNumber { get; set; }

        List<InvoiceItems> InvoiceItems { get; set; }
    }
}
