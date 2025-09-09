using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StreamLinerDataLayer.Migrations
{
    /// <inheritdoc />
    public partial class ModifyPermissionsModels2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FolderUserPermissions_FolderDocumentPermission_FolderDocumentPermissionId",
                table: "FolderUserPermissions");

            migrationBuilder.DropTable(
                name: "FolderDocumentPermission");

            migrationBuilder.DropIndex(
                name: "IX_FolderUserPermissions_FolderDocumentPermissionId",
                table: "FolderUserPermissions");

            migrationBuilder.DropColumn(
                name: "FolderDocumentPermissionId",
                table: "FolderUserPermissions");

            migrationBuilder.RenameColumn(
                name: "FolderPermissionId",
                table: "FolderUserPermissions",
                newName: "PermissionTypeId");

            migrationBuilder.CreateTable(
                name: "PermissionType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ForDocument = table.Column<bool>(type: "bit", nullable: false),
                    ForFolder = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_PermissionType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FolderUserPermissions_PermissionTypeId",
                table: "FolderUserPermissions",
                column: "PermissionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_FolderUserPermissions_PermissionType_PermissionTypeId",
                table: "FolderUserPermissions",
                column: "PermissionTypeId",
                principalTable: "PermissionType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FolderUserPermissions_PermissionType_PermissionTypeId",
                table: "FolderUserPermissions");

            migrationBuilder.DropTable(
                name: "PermissionType");

            migrationBuilder.DropIndex(
                name: "IX_FolderUserPermissions_PermissionTypeId",
                table: "FolderUserPermissions");

            migrationBuilder.RenameColumn(
                name: "PermissionTypeId",
                table: "FolderUserPermissions",
                newName: "FolderPermissionId");

            migrationBuilder.AddColumn<int>(
                name: "FolderDocumentPermissionId",
                table: "FolderUserPermissions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "FolderDocumentPermission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    CreateId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteId = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ForDocument = table.Column<bool>(type: "bit", nullable: false),
                    ForFolder = table.Column<bool>(type: "bit", nullable: false),
                    PermissionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateId = table.Column<int>(type: "int", nullable: false),
                    Updated = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FolderDocumentPermission", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FolderUserPermissions_FolderDocumentPermissionId",
                table: "FolderUserPermissions",
                column: "FolderDocumentPermissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_FolderUserPermissions_FolderDocumentPermission_FolderDocumentPermissionId",
                table: "FolderUserPermissions",
                column: "FolderDocumentPermissionId",
                principalTable: "FolderDocumentPermission",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
