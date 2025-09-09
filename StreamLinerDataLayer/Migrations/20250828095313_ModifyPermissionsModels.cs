using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StreamLinerDataLayer.Migrations
{
    /// <inheritdoc />
    public partial class ModifyPermissionsModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FolderUserPermissions_FolderPermissions_FolderPermissionId",
                table: "FolderUserPermissions");

            migrationBuilder.DropTable(
                name: "FolderPermissions");

            migrationBuilder.DropIndex(
                name: "IX_FolderUserPermissions_FolderPermissionId",
                table: "FolderUserPermissions");

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
                    table.PrimaryKey("PK_FolderDocumentPermission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FolderPermissionScope",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_FolderPermissionScope", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FolderUserPermissionScope",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FolderId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PermissionScopeId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_FolderUserPermissionScope", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    GroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_Groups", x => x.GroupId);
                });

            migrationBuilder.CreateTable(
                name: "UsersGroup",
                columns: table => new
                {
                    UsersGroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_UsersGroup", x => x.UsersGroupId);
                    table.ForeignKey(
                        name: "FK_UsersGroup_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersGroup_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FolderUserPermissions_FolderDocumentPermissionId",
                table: "FolderUserPermissions",
                column: "FolderDocumentPermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersGroup_GroupId",
                table: "UsersGroup",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersGroup_UserId",
                table: "UsersGroup",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FolderUserPermissions_FolderDocumentPermission_FolderDocumentPermissionId",
                table: "FolderUserPermissions",
                column: "FolderDocumentPermissionId",
                principalTable: "FolderDocumentPermission",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FolderUserPermissions_FolderDocumentPermission_FolderDocumentPermissionId",
                table: "FolderUserPermissions");

            migrationBuilder.DropTable(
                name: "FolderDocumentPermission");

            migrationBuilder.DropTable(
                name: "FolderPermissionScope");

            migrationBuilder.DropTable(
                name: "FolderUserPermissionScope");

            migrationBuilder.DropTable(
                name: "UsersGroup");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_FolderUserPermissions_FolderDocumentPermissionId",
                table: "FolderUserPermissions");

            migrationBuilder.DropColumn(
                name: "FolderDocumentPermissionId",
                table: "FolderUserPermissions");

            migrationBuilder.CreateTable(
                name: "FolderPermissions",
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
                    PermissionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateId = table.Column<int>(type: "int", nullable: false),
                    Updated = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FolderPermissions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FolderUserPermissions_FolderPermissionId",
                table: "FolderUserPermissions",
                column: "FolderPermissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_FolderUserPermissions_FolderPermissions_FolderPermissionId",
                table: "FolderUserPermissions",
                column: "FolderPermissionId",
                principalTable: "FolderPermissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
