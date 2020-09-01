using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GamesStoreWebApi.Migrations
{
    public partial class AddOrderDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    Subtotal = table.Column<decimal>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    OrderStatus = table.Column<string>(nullable: true),
                    IdProduct = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetails");
        }
    }
}
