using Microsoft.EntityFrameworkCore.Migrations;

namespace Doctorla.Data.EF.Migrations
{
    public partial class speacialtydoctoriddelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Specialties");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DoctorId",
                table: "Specialties",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
