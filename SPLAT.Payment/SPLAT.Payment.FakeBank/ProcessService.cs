namespace SPLAT.Payment.FakeBank
{
    public class ProcessService
    {
        public ProcessService()
        {

        }

        public void ProcessPayment(int clientId, double price)
        {
            using (FakeBankContext db = new FakeBankContext())
            {
                var account = db.Accounts.Find(clientId);

                //if (account == null)  
            }
        }
    }
}
