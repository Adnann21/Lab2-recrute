using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recrute.Migrations
{
    public partial class Employ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                 name: "Employ",
                 columns: table => new
                 {
                     Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                     Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                     CompName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                     Region = table.Column<string>(type: "nvarchar(max)", nullable: false),
                     
                 },
                 constraints: table =>
                 {
                     table.PrimaryKey("PK_Employ", x => x.Username);
                 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
             name: "Employ");
        }
    }
}
