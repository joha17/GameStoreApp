using Microsoft.EntityFrameworkCore.Migrations;

namespace GamesStoreWebApi.Migrations
{
    public partial class AddOrderDetailsKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "OrderDetails",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PrimaryKey_Id",
                table: "OrderDetails",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PrimaryKey_Id",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "OrderDetails");
        }
    }
}
