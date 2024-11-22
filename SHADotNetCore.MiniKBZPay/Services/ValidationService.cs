using Microsoft.EntityFrameworkCore;
using SHADotNetCore.MiniKBZPay.Endpoints.Transaction;
using SHADotNetCore.MiniKBZPay.Endpoints.User;
using SHADotNetCore.MiniKBZPay.Models;
using static SHADotNetCore.MiniKBZPay.Endpoints.Bank.BankService;

namespace SHADotNetCore.MiniKBZPay.Services;

public static class ValidationService
{
    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public string? ErrorMessage { get; set; }
    }

    public static async Task<ValidationResult> UserValidation(UserModel user, AppDbContext _db)
    {
        if (user is null)
        {
            return new ValidationResult { IsValid = false, ErrorMessage = "No user received." };
        }

        if (string.IsNullOrWhiteSpace(user.MobileNumber))
        {
            return new ValidationResult { IsValid = false, ErrorMessage = "Mobile number is required." };
        }

        if (user.MobileNumber.Length is not 10)
        {
            return new ValidationResult { IsValid = false, ErrorMessage = "Mobile number must be 10 characters long." };
        }
            
        bool isPhoneNumberAlreadyRegistered = await _db.Users.AnyAsync(x => x.MobileNumber == user.MobileNumber);
        if (!isPhoneNumberAlreadyRegistered)
        {
            return new ValidationResult { IsValid = false, ErrorMessage = "Phone number is not registered." };
        }

        if (string.IsNullOrWhiteSpace(user.FullName))
        {
            return new ValidationResult { IsValid = false, ErrorMessage = "Full name is required." };
        }

        if (string.IsNullOrWhiteSpace(user.PIN))
        {
            return new ValidationResult { IsValid = false, ErrorMessage = "PIN is required." };
        }

        if (user.PIN.Length is not 6)
        {
            return new ValidationResult { IsValid = false, ErrorMessage = "PIN must be exactly 6 characters long." };
        }

        return new ValidationResult { IsValid = true };
    }

    public static async Task<ValidationResult> BankDetailsValidation(BankDetails details, AppDbContext _db)
    {
        if (details is null)
        {
            return new ValidationResult { IsValid = false, ErrorMessage = "No bank details received." };
        }

        if (string.IsNullOrEmpty(details.MobileNumber))
        {
            return new ValidationResult { IsValid = false, ErrorMessage = "Mobile number is required for banking." };
        }
        bool isPhoneNumberAlreadyRegistered = await _db.Users.AnyAsync(x => x.MobileNumber == details.MobileNumber);
        if (!isPhoneNumberAlreadyRegistered)
        {
            return new ValidationResult { IsValid = false, ErrorMessage = "Invalid phone number." };
        }
        if (details.MobileNumber.Length is not 10)
        {
            return new ValidationResult { IsValid = false, ErrorMessage = "Mobile number must be 10 characters long." };
        }

        if (details.Balance <= 1000)
        {
            return new ValidationResult { IsValid = false, ErrorMessage = "Balance must be greater than 1000." };
        }

        return new ValidationResult { IsValid = true, ErrorMessage = "Passed the test" };
    }


    public static async Task<ValidationResult> TransferValidation(TransactionModel transactionDetails, AppDbContext _db)
    {
        if (transactionDetails is null)
        {
            return new ValidationResult { IsValid = false, ErrorMessage = "No Transaction Details Recieved" };
        }
        if (transactionDetails.FromMobileNumber.Length is not 10)
        {
            return new ValidationResult { IsValid = false, ErrorMessage = "Mobile number must be 10 characters long." };
        }
        bool isFromPhoneNumberAlreadyRegistered = await _db.Users.AnyAsync(x => x.MobileNumber == transactionDetails.FromMobileNumber);
        if (!isFromPhoneNumberAlreadyRegistered)
        {
            return new ValidationResult { IsValid = false, ErrorMessage = "Invalid phone number." };
        }
        if (transactionDetails.ToMobileNumber.Length is not 10)
        {
            return new ValidationResult { IsValid = false, ErrorMessage = "Mobile number must be 10 characters long." };
        }
        bool isToPhoneNumberAlreadyRegistered = await _db.Users.AnyAsync(x => x.MobileNumber == transactionDetails.ToMobileNumber);
        if (!isToPhoneNumberAlreadyRegistered)
        {
            return new ValidationResult { IsValid = false, ErrorMessage = "Invalid phone number." };
        }
        if (transactionDetails.FromMobileNumber == transactionDetails.ToMobileNumber)
        {
            return new ValidationResult { IsValid = false, ErrorMessage = "Phone number cannot be the same" };
        }
        var user = await _db.Users.FirstOrDefaultAsync(x => x.MobileNumber == transactionDetails.FromMobileNumber);
        if (user is null)
        {
            return new ValidationResult { IsValid = false, ErrorMessage = "User not recieved" };
        }
        if(transactionDetails.PIN.Length is not 6)
        {
            return new ValidationResult { IsValid = false, ErrorMessage = "Invalid Pin " };
        }
        if (user.PIN != transactionDetails.PIN)
        {
            return new ValidationResult { IsValid = false, ErrorMessage = "Invalid Credentials" };
        }
        if (transactionDetails.Amount <= 0)
        {
            return new ValidationResult { IsValid = false, ErrorMessage = "Transaction amount must be greater than zero." };
        }

        if (transactionDetails.Amount > user.Balance)
        {
            return new ValidationResult { IsValid = false, ErrorMessage = "Insufficient Amount" };
        }
        if (string.IsNullOrEmpty(transactionDetails.Note))
        {
            return new ValidationResult
            {
                IsValid = false,
                ErrorMessage = "Note is required"

            };

        }
        return new ValidationResult { IsValid = true, ErrorMessage = "Passed the test" };
    }

}