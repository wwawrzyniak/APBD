using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication3.Migrations
{
    public partial class volunteer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Volunteer_IdSupervisor",
                table: "Volunteer",
                column: "IdSupervisor");

            migrationBuilder.AddForeignKey(
                name: "FK_Volunteer_Volunteer_IdSupervisor",
                table: "Volunteer",
                column: "IdSupervisor",
                principalTable: "Volunteer",
                principalColumn: "IdVolunteer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Volunteer_Volunteer_IdSupervisor",
                table: "Volunteer");

            migrationBuilder.DropIndex(
                name: "IX_Volunteer_IdSupervisor",
                table: "Volunteer");
        }
    }
}
