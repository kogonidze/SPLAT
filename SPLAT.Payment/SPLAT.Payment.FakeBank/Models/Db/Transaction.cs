namespace SPLAT.Payment.FakeBank.Models.Db
{
    public class Transaction
    {
        public int Id { get; set; }

        public int AccountId { get; set; }

        public double Price { get; set; }

        public DateTime DateTime { get; set; }
    }
}
