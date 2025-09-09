using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StreamLinerDataLayer.Migrations
{
    /// <inheritdoc />
    public partial class CreateFolderUserPermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Folders_AspNetUsers_OwnerId",
                table: "Folders");

            migrationBuilder.DropIndex(
                name: "IX_Folders_OwnerId",
                table: "Folders");

            migrationBuilder.AddColumn<int>(
                name: "ApplicationUserId",
                table: "Folders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FolderPermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreateId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<bool>(type: "bit", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    UpdateId = table.Column<int>(type: "int", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteId = table.Column<int>(type: "int", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FolderPermissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FolderUserPermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FolderId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FolderPermissionId = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreateId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<bool>(type: "bit", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    UpdateId = table.Column<int>(type: "int", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteId = table.Column<int>(type: "int", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FolderUserPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FolderUserPermissions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FolderUserPermissions_FolderPermissions_FolderPermissionId",
                        column: x => x.FolderPermissionId,
                        principalTable: "FolderPermissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FolderUserPermissions_Folders_FolderId",
                        column: x => x.FolderId,
                        principalTable: "Folders",
                        principalColumn: "FolderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Folders_ApplicationUserId",
                table: "Folders",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FolderUserPermissions_FolderId",
                table: "FolderUserPermissions",
                column: "FolderId");

            migrationBuilder.CreateIndex(
                name: "IX_FolderUserPermissions_FolderPermissionId",
                table: "FolderUserPermissions",
                column: "FolderPermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_FolderUserPermissions_UserId",
                table: "FolderUserPermissions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Folders_AspNetUsers_ApplicationUserId",
                table: "Folders",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Folders_AspNetUsers_ApplicationUserId",
                table: "Folders");

            migrationBuilder.DropTable(
                name: "FolderUserPermissions");

            migrationBuilder.DropTable(
                name: "FolderPermissions");

            migrationBuilder.DropIndex(
                name: "IX_Folders_ApplicationUserId",
                table: "Folders");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Folders");

            migrationBuilder.CreateIndex(
                name: "IX_Folders_OwnerId",
                table: "Folders",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Folders_AspNetUsers_OwnerId",
                table: "Folders",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
