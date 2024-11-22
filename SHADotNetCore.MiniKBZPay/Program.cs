using Microsoft.EntityFrameworkCore;
using SHADotNetCore.MiniKBZPay.Endpoints.Bank;
using SHADotNetCore.MiniKBZPay.Endpoints.Transaction;
using SHADotNetCore.MiniKBZPay.Endpoints.User;
using SHADotNetCore.MiniKBZPay.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<BankService>();
builder.Services.AddScoped<TransactionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseUserEndpoint();
app.UseBankEndPoint();
app.UseTransactionsEndPoint();

app.Run();
