using Microsoft.EntityFrameworkCore.Migrations;

namespace Doctorla.Data.EF.Migrations
{
    public partial class AppointmentTableFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Appointments",
                newName: "SessionPrice");

            migrationBuilder.AddColumn<double>(
                name: "SessionTime",
                table: "Appointments",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SessionTime",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "SessionPrice",
                table: "Appointments",
                newName: "Price");
        }
    }
}
