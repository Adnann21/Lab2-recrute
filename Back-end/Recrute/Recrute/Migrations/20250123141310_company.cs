using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recrute.Migrations
{
    public partial class company : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "Company",
               columns: table => new
               {
                   CompanyName= table.Column<string>(type: "nvarchar(max)", nullable: false),
                   RecruteComp = table.Column<string>(type: "nvarchar(450)", nullable: false),
                  
                   Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                  

               },
               constraints: table =>
               {
                  
               });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
              name: "Company");
        }
    }
}
