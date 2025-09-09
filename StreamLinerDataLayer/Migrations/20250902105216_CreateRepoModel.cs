using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StreamLinerDataLayer.Migrations
{
    /// <inheritdoc />
    public partial class CreateRepoModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RepoPermissionType",
                columns: table => new
                {
                    RepoPermissionTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_RepoPermissionType", x => x.RepoPermissionTypeId);
                });

            migrationBuilder.CreateTable(
                name: "RepositoriesDisk",
                columns: table => new
                {
                    RepositoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RepositoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    License = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SizeInMB = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: false),
                    IsPrivate = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_RepositoriesDisk", x => x.RepositoryId);
                });

            migrationBuilder.CreateTable(
                name: "RepoUserPermissions",
                columns: table => new
                {
                    RepoUserPermissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RepositoryId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RepoPermissionTypeId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_RepoUserPermissions", x => x.RepoUserPermissionId);
                    table.ForeignKey(
                        name: "FK_RepoUserPermissions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RepoUserPermissions_RepoPermissionType_RepoPermissionTypeId",
                        column: x => x.RepoPermissionTypeId,
                        principalTable: "RepoPermissionType",
                        principalColumn: "RepoPermissionTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RepoUserPermissions_RepositoriesDisk_RepositoryId",
                        column: x => x.RepositoryId,
                        principalTable: "RepositoriesDisk",
                        principalColumn: "RepositoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RepoUserPermissions_RepoPermissionTypeId",
                table: "RepoUserPermissions",
                column: "RepoPermissionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RepoUserPermissions_RepositoryId",
                table: "RepoUserPermissions",
                column: "RepositoryId");

            migrationBuilder.CreateIndex(
                name: "IX_RepoUserPermissions_UserId",
                table: "RepoUserPermissions",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RepoUserPermissions");

            migrationBuilder.DropTable(
                name: "RepoPermissionType");

            migrationBuilder.DropTable(
                name: "RepositoriesDisk");
        }
    }
}
