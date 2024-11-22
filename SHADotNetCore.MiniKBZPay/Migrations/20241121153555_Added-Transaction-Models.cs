using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SHADotNetCore.MiniKBZPay.Migrations;

/// <inheritdoc />
public partial class AddedTransactionModels : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Receipt",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                FromMobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                ToMobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                TransactionType = table.Column<int>(type: "int", nullable: false),
                UserModelUserId = table.Column<int>(type: "int", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Receipt", x => x.Id);
                table.ForeignKey(
                    name: "FK_Receipt_Users_UserModelUserId",
                    column: x => x.UserModelUserId,
                    principalTable: "Users",
                    principalColumn: "UserId");
            });

        migrationBuilder.CreateIndex(
            name: "IX_Receipt_UserModelUserId",
            table: "Receipt",
            column: "UserModelUserId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Receipt");
    }
}
