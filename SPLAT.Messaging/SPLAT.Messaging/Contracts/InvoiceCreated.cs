namespace SPLAT.Messaging.Contracts
{
    public interface InvoiceCreated
    {
        int InvoiceNumber { get; }

        InvoiceToCreate InvoiceData { get; }
    }
}
