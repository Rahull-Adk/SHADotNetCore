using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SHADotNetCore.MiniKBZPay.Models;
using static SHADotNetCore.MiniKBZPay.Endpoints.Bank.BankService;

namespace SHADotNetCore.MiniKBZPay.Endpoints.Bank;

public static class BankEndpoint
{

    public static IEndpointRouteBuilder UseBankEndPoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/bank/deposit", async (BankDetails details, AppDbContext _db, [FromServices] BankService service) =>
        {
            try
            {
                var deposit = await service.Deposit(details, _db);
                return Results.Ok($"Deposit {details.Balance} successfully");
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        app.MapPost("/bank/withdraw", async (BankDetails details, AppDbContext _db, [FromServices] BankService service) =>
        {
            try
            {
                var withdrawal = await service.WithDrawal(details, _db);
                return Results.Ok($"${details.Balance} withdrew successfully");
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        return app;
    }
}
