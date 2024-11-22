using System.ComponentModel.DataAnnotations;

namespace SHADotNetCore.MiniKBZPay.Endpoints.Transaction;


public class Receipt
{
    public int Id { get; set; }
    public required string FromMobileNumber { get; set; }
    public required string ToMobileNumber { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; } = DateTime.Now;
    public required string Note { get; set; }
    public TransactionType TransactionType { get; set; }
}

public class TransactionModel
{

    [Key]
    public int TransactionId { get; set; }
    [Phone]
    public required string FromMobileNumber { get; set; }
    [Phone]
    public required string ToMobileNumber { get; set; }
    public required string PIN { get; set; }
    public required int Amount { get; set; }
    public required string Note { get; set; }

}
public enum TransactionType
{
    None,
    Transfer,
    Recieved
}
