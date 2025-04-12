using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recrute.Migrations
{
    public partial class UsingPack : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "UsingPack",
               columns: table => new
               {
                   Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                   Lloji = table.Column<string>(type: "nvarchar(450)", nullable: false),
                   Qmimi = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                   Nr_Employ = table.Column<string>(type: "nvarchar(max)", nullable: false),
                   RecrComp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                   Exp_Day = table.Column<DateOnly>(type: "Date", nullable: false),


               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_UsingPack", x => x.Id);
               });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
               name: "UsingPack");
        }
    }
}
