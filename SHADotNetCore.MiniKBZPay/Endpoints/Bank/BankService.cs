using Microsoft.EntityFrameworkCore;
using SHADotNetCore.MiniKBZPay.Endpoints.User;
using SHADotNetCore.MiniKBZPay.Models;
using SHADotNetCore.MiniKBZPay.Services;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace SHADotNetCore.MiniKBZPay.Endpoints.Bank;

public class BankService
{
    public class BankDetails
    {
        [Phone]

        public required string MobileNumber { get; set; }
        public required int Balance { get; set; }
    }

    public async Task<UserModel> Deposit(BankDetails details, AppDbContext _db)
    {
        var bankValidation = await ValidationService.BankDetailsValidation(details, _db);
        if (!bankValidation.IsValid)
        {
            throw new Exception(bankValidation.ErrorMessage);
        }

        var user = await _db.Users.FirstOrDefaultAsync(x => x.MobileNumber == details.MobileNumber);
        var userValidation = await ValidationService.UserValidation(user, _db);
        if (!userValidation.IsValid)
        {
            throw new Exception(userValidation.ErrorMessage);
        }

        user.Balance += details.Balance;
        _db.Entry(user).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return user;
    }
    public async Task<UserModel> WithDrawal(BankDetails details, AppDbContext _db)
    {
        var bankValidation = await ValidationService.BankDetailsValidation(details, _db);
        if (!bankValidation.IsValid)
        {
            throw new Exception(bankValidation.ErrorMessage);
        }

        var user = await _db.Users.FirstOrDefaultAsync(x => x.MobileNumber == details.MobileNumber);

        var userValidation = await ValidationService.UserValidation(user, _db);
        if (!userValidation.IsValid)
        {
            throw new Exception(userValidation.ErrorMessage);
        }

        if (user.Balance < details.Balance)
        {
            throw new Exception("Insufficient Balance");
        }

        user.Balance -= details.Balance;
        _db.Entry(user).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return user;
    }
}
