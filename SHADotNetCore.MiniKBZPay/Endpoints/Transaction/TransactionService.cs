using Microsoft.EntityFrameworkCore;
using SHADotNetCore.MiniKBZPay.Models;
using SHADotNetCore.MiniKBZPay.Services;

namespace SHADotNetCore.MiniKBZPay.Endpoints.Transaction;


public class TransactionService
{
    public async Task<List<Receipt>> TransferBalance(TransactionModel transactionDetails, AppDbContext _db)
    {
        var validations = await ValidationService.TransferValidation(transactionDetails, _db);
        if (!validations.IsValid)
        {
            throw new Exception(validations.ErrorMessage);
        }
        var fromUser = await _db.Users.FirstOrDefaultAsync(x => x.MobileNumber == transactionDetails.FromMobileNumber);
        var toUser = await _db.Users.FirstOrDefaultAsync(x => x.MobileNumber == transactionDetails.ToMobileNumber);


        fromUser!.Balance -= transactionDetails.Amount;
        toUser!.Balance += transactionDetails.Amount;

        Receipt transferReceipt = new Receipt
        {
            FromMobileNumber = fromUser.MobileNumber,
            ToMobileNumber = toUser.MobileNumber,
            Amount = transactionDetails.Amount,
            TransactionDate = DateTime.Now,
            Note = transactionDetails.Note,
            TransactionType = TransactionType.Transfer

        };

        Receipt recieveReceipt = new Receipt
        {
            FromMobileNumber = fromUser.MobileNumber,
            ToMobileNumber = toUser.MobileNumber,
            Amount = transactionDetails.Amount,
            TransactionDate = DateTime.Now,
            Note = transactionDetails.Note,
            TransactionType = TransactionType.Recieved

        };
        fromUser.receipts.Add(transferReceipt);
        toUser.receipts.Add(recieveReceipt);

        await _db.SaveChangesAsync();
        return new List<Receipt>
        {
            transferReceipt,
            recieveReceipt,
        };
    }
}
