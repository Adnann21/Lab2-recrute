using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recrute.Migrations
{
    public partial class JobStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "JobStatus",
               columns: table => new
               {
                   Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                   Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                   Pozition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                   Status_Job = table.Column<string>(type: "nvarchar(max)", nullable: false),
                  
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_JobStatus", x => x.Id);
               });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
               name: "JobStatus");
        }
    }
}
