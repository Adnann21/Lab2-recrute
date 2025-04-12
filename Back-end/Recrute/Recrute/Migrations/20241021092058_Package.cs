using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recrute.Migrations
{
    public partial class Package : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "Package",
               columns: table => new
               {
                   Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                   Lloji = table.Column<string>(type: "nvarchar(450)", nullable: false),
                   Qmimi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                   Nr_Employ = table.Column<string>(type: "nvarchar(max)", nullable: false),
                   
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_Package", x => x.Id);
               });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
               name: "Package");
        }
    }
}
