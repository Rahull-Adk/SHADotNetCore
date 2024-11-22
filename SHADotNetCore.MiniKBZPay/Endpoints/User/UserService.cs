using Microsoft.EntityFrameworkCore;
using SHADotNetCore.MiniKBZPay.Endpoints.Transaction;
using SHADotNetCore.MiniKBZPay.Models;
using SHADotNetCore.MiniKBZPay.Services;

namespace SHADotNetCore.MiniKBZPay.Endpoints.User;

public class UserService
{
    public async Task<UserModel> RegisterUser(UserModel user, AppDbContext _db)
    {
        var validatedUser = await ValidationService.UserValidation(user, _db);
        if (!validatedUser.IsValid)
        {
            throw new InvalidOperationException(validatedUser.ErrorMessage);
        }
        await _db.AddAsync(user);
        await _db.SaveChangesAsync();
        return user;
    }

    public async Task<List<UserModel>> GetAllUser(AppDbContext _db)
    {
        var user = await _db.Users.AsNoTracking().ToListAsync();
        return user;
    }

    public async Task<UserModel> GetUser(int id, AppDbContext _db)
    {
        var user = await _db.Users.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == id);
        if (user is null)
        {
            throw new KeyNotFoundException("User not found");
        }
        return user;
    }

    public async Task<UserModel> UpdateUser(int id, UserModel user, AppDbContext _db)
    {

        var userToUpdate = await _db.Users.FirstOrDefaultAsync(x => x.UserId == id);
        if (userToUpdate is null)
        {
            throw new KeyNotFoundException("User not found");
        }
        var validationResult = await ValidationService.UserValidation(user, _db);
        if (!validationResult.IsValid)
        {
            throw new InvalidOperationException(validationResult.ErrorMessage);
        }

        userToUpdate.FullName = user.FullName;
        userToUpdate.MobileNumber = user.MobileNumber;
        userToUpdate.PIN = user.PIN;

        _db.Entry(userToUpdate).State = EntityState.Modified;
        await _db.SaveChangesAsync();

        return userToUpdate;
    }

    public async Task<string> DeleteUser(int id, AppDbContext _db)
    {
        var user = await _db.Users.FirstOrDefaultAsync(x => x.UserId == id);
        if (user is null)
        {
            throw new KeyNotFoundException("User not found");
        }
        _db.Entry(user).State = EntityState.Deleted;
        await _db.SaveChangesAsync();
        return "Account Deleted Successfully";
    }

    public async Task<List<Receipt>> TransactionHistory(int id, AppDbContext _db, int limit = 10) {
        var user = await _db.Users.Include(x => x.receipts).FirstOrDefaultAsync(x => x.UserId == id);
        if (user is null)
        {
            throw new KeyNotFoundException("User not found");
        }
        var reciepts = user.receipts.OrderByDescending(x => x.TransactionDate)
            .Take(limit).ToList();


        return reciepts;

    }

}

 