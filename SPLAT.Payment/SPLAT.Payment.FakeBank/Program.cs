using SPLAT.Payment.FakeBank;
using SPLAT.Payment.FakeBank.Models.Db;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost(
    pattern: "/api/payment",
    handler: async (ProcessPaymentRequest request) =>
    {
        using (FakeBankContext db = new FakeBankContext())
        {
            var account = db.Accounts.Find(request.ClientId);

            if (account == null) return Results.NotFound();

            if (account.Balance < Convert.ToDecimal(request.Price)) return Results.BadRequest();

            db.Transactions.Add(new Transaction { AccountId = account.Id, Price = request.Price, DateTime = DateTime.Now });
            account.Balance -= Convert.ToDecimal(request.Price);

            db.SaveChanges();
        }

        return Results.Ok();
    });

app.Run();
