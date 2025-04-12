using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recrute.Migrations
{
    public partial class RecrComp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
    name: "RecruteComp",
    columns: table => new
    {
        Id = table.Column<int>(nullable: false).Annotation("SqlServer:Identity", "1, 1"),
        CompName = table.Column<string>(type: "nvarchar(255)", nullable: false), // Specify max length here
        Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
        Nr_Employ = table.Column<int>(nullable: false)
    },
    constraints: table =>
    {
        table.PrimaryKey("PK_RecruteComp", x => x.Id);
        table.UniqueConstraint("AK_RecruteComp_CompName", x => x.CompName); // Unique constraint on CompName
    });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
               name: "RecrurteComp");
        }
    }
}
