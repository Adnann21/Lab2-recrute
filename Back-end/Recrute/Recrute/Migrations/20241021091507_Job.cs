using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recrute.Migrations
{
    public partial class Job : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                  
                    CompName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CompLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Jobs");
        }
    }
}
