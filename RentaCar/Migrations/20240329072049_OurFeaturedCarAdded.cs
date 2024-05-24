using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentaCar.Migrations
{
    public partial class OurFeaturedCarAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ourFeaturedCars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PriceDaily = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PriceWeekly = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<int>(type: "int", nullable: false),
                    Seats = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fuel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ourFeaturedCars", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ourFeaturedCars");
        }
    }
}
