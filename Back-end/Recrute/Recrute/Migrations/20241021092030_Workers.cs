using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recrute.Migrations
{
    public partial class Workers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Workers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
            .Annotation("SqlServer:Identity", "1, 1"),
                    CompName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pozition = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    App_Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Payment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Emp_Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                  
                   
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
               name: "Workers");
        }
    }
}
