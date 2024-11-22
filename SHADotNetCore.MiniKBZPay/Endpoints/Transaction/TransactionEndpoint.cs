using Microsoft.AspNetCore.Mvc;
using SHADotNetCore.MiniKBZPay.Models;

namespace SHADotNetCore.MiniKBZPay.Endpoints.Transaction;

public static class TransactionEndpoint
{
    public static IEndpointRouteBuilder UseTransactionsEndPoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/transactions", async (TransactionModel transactionDetails, AppDbContext _db, [FromServices]
TransactionService service) =>
        {
            try
            {
                var transactionReciept = await service.TransferBalance(transactionDetails, _db);
                return Results.Ok(transactionReciept);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });
        return app;
    }
}
