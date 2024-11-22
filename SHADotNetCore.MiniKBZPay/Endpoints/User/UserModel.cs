using SHADotNetCore.MiniKBZPay.Endpoints.Transaction;
using System.ComponentModel.DataAnnotations;

namespace SHADotNetCore.MiniKBZPay.Endpoints.User;

public class UserModel
{
    [Key]
    public int UserId { get; set; }
    public required string FullName { get; set; }
    [Phone]
    public required string MobileNumber { get; set; }
    public required string PIN { get; set; }
    public required int Balance { get; set; } = 0;
    public required List<Receipt> receipts { get; set; } = new List<Receipt>();
}
