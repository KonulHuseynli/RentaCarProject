using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentaCar.Migrations
{
    public partial class CategoryAndCategoryComponentAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryComponents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DailyPrice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WeeklyPrice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<int>(type: "int", nullable: false),
                    Seat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FuelType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Auto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryComponents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryComponents_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryComponents_CategoryId",
                table: "CategoryComponents",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryComponents");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
