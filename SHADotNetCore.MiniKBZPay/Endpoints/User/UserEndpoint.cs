using Microsoft.AspNetCore.Mvc;
using SHADotNetCore.MiniKBZPay.Models;
using System.Runtime.CompilerServices;

namespace SHADotNetCore.MiniKBZPay.Endpoints.User;


public static class UserEndpoint
{
    public static IEndpointRouteBuilder UseUserEndpoint(this IEndpointRouteBuilder app)
    {

        app.MapPost("/register", async (UserModel user, AppDbContext _db, [FromServices] UserService service) =>
                    {
                        try
                        {
                            var registerUser = await service.RegisterUser(user, _db);
                            return Results.Created("User created successfully", registerUser);
                        }
                        catch (Exception ex)
                        {
                            return Results.BadRequest(ex.Message);
                        }

                    });
        app.MapGet("/users", async (AppDbContext _db, [FromServices] UserService service) =>
        {
            try
            {
                var users = await service.GetAllUser(_db);
                return Results.Ok(users);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }

        });

        app.MapGet("/users/{id}", async (int id, AppDbContext _db, [FromServices] UserService service) =>
        {
            try
            {
                var users = await service.GetUser(id, _db);
                return Results.Ok(users);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }

        });

        app.MapPut("/users/{id}", async (int id, UserModel user, AppDbContext _db, [FromServices] UserService service) =>
                    {
                        try
                        {
                            var updateUserr = await service.UpdateUser(id, user, _db);
                            return Results.Created("User created successfully", updateUserr);
                        }
                        catch (Exception ex)
                        {
                            return Results.BadRequest(ex.Message);
                        }

                    });
        app.MapDelete("/users/{id}", async (int id, AppDbContext _db, [FromServices] UserService service) =>
        {
            try
            {
                var deleteUser = await service.DeleteUser(id, _db);
                return Results.Ok(deleteUser);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }

        });
        app.MapGet("/users/{id}/transactionHistroy", async (int id,int limit,  AppDbContext _db, [FromServices] UserService service) =>
        {
            try
            {
                var transactionHistory = await service.TransactionHistory(id, _db, limit);
                return Results.Ok(transactionHistory);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });
        return app;
    }
}
