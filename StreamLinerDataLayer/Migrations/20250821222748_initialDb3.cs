using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StreamLinerDataLayer.Migrations
{
    /// <inheritdoc />
    public partial class initialDb3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_HRSpecialistLevel_HRSpecialistLevelSpecialistLevelId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_HRSpecialistLevelSpecialistLevelId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "HRSpecialistLevelSpecialistLevelId",
                table: "AspNetUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HRSpecialistLevelSpecialistLevelId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_HRSpecialistLevelSpecialistLevelId",
                table: "AspNetUsers",
                column: "HRSpecialistLevelSpecialistLevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_HRSpecialistLevel_HRSpecialistLevelSpecialistLevelId",
                table: "AspNetUsers",
                column: "HRSpecialistLevelSpecialistLevelId",
                principalTable: "HRSpecialistLevel",
                principalColumn: "SpecialistLevelId");
        }
    }
}
