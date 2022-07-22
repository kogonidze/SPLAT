namespace SPLAT.Payment.FakeBank.Models.Db
{
    public class Account
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Patronymic { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Citizenship { get; set; }

        public decimal Balance { get; set; }
    }
}
