using Microsoft.EntityFrameworkCore.Migrations;

namespace GamesStoreWebApi.Migrations
{
    public partial class userAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserAddress",
                columns: table => new
                {
                    UserName = table.Column<string>(nullable: false),
                    CountryCode = table.Column<string>(nullable: false),
                    City = table.Column<string>(nullable: false),
                    FirstAddress = table.Column<string>(nullable: false),
                    SecondAddress = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAddress", x => x.UserName);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAddress");
        }
    }
}
