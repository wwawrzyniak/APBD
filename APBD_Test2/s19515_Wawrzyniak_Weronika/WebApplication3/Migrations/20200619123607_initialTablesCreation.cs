using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication3.Migrations
{
    public partial class initialTablesCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BreedType",
                columns: table => new
                {
                    IdBreedType = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Description = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BreedType", x => x.IdBreedType);
                });

            migrationBuilder.CreateTable(
                name: "Volunteer",
                columns: table => new
                {
                    IdVolunteer = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSupervisor = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 80, nullable: true),
                    Surname = table.Column<string>(maxLength: 80, nullable: true),
                    Phone = table.Column<string>(maxLength: 30, nullable: true),
                    Address = table.Column<string>(maxLength: 100, nullable: true),
                    Email = table.Column<string>(maxLength: 80, nullable: true),
                    StartingDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Volunteer", x => x.IdVolunteer);
                });

            migrationBuilder.CreateTable(
                name: "Pet",
                columns: table => new
                {
                    IdPet = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdBreedType = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 80, nullable: true),
                    isMale = table.Column<bool>(nullable: false),
                    DateRegistered = table.Column<DateTime>(nullable: false),
                    ApprocimateDateOfBirth = table.Column<DateTime>(nullable: false),
                    DateAdopted = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pet", x => x.IdPet);
                    table.ForeignKey(
                        name: "FK_Pet_BreedType_IdBreedType",
                        column: x => x.IdBreedType,
                        principalTable: "BreedType",
                        principalColumn: "IdBreedType",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Volunteer_Pet",
                columns: table => new
                {
                    IdVolunteer = table.Column<int>(nullable: false),
                    IdPet = table.Column<int>(nullable: false),
                    DateAccepted = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Volunteer_Pet", x => new { x.IdVolunteer, x.IdPet });
                    table.ForeignKey(
                        name: "FK_Volunteer_Pet_Pet_IdPet",
                        column: x => x.IdPet,
                        principalTable: "Pet",
                        principalColumn: "IdPet",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Volunteer_Pet_Volunteer_IdVolunteer",
                        column: x => x.IdVolunteer,
                        principalTable: "Volunteer",
                        principalColumn: "IdVolunteer",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pet_IdBreedType",
                table: "Pet",
                column: "IdBreedType");

            migrationBuilder.CreateIndex(
                name: "IX_Volunteer_Pet_IdPet",
                table: "Volunteer_Pet",
                column: "IdPet");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Volunteer_Pet");

            migrationBuilder.DropTable(
                name: "Pet");

            migrationBuilder.DropTable(
                name: "Volunteer");

            migrationBuilder.DropTable(
                name: "BreedType");
        }
    }
}
