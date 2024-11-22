using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SHADotNetCore.MiniKBZPay.Migrations;

/// <inheritdoc />
public partial class AddedBalance : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<int>(
            name: "Balance",
            table: "Users",
            type: "int",
            nullable: false,
            defaultValue: 0);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Balance",
            table: "Users");
    }
}
