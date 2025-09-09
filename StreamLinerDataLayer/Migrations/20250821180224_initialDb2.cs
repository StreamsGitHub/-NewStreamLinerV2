using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StreamLinerDataLayer.Migrations
{
    /// <inheritdoc />
    public partial class initialDb2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskLists",
                columns: table => new
                {
                    TaskListId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    priorty = table.Column<int>(type: "int", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreateId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<bool>(type: "bit", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    UpdateId = table.Column<int>(type: "int", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteId = table.Column<int>(type: "int", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskLists", x => x.TaskListId);
                });

            migrationBuilder.CreateTable(
                name: "TasksAttachments",
                columns: table => new
                {
                    TasksAttachmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileTypeId = table.Column<int>(type: "int", nullable: true),
                    FileSize = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaskId = table.Column<int>(type: "int", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreateId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<bool>(type: "bit", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    UpdateId = table.Column<int>(type: "int", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteId = table.Column<int>(type: "int", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TasksAttachments", x => x.TasksAttachmentId);
                    table.ForeignKey(
                        name: "FK_TasksAttachments_FileFormats_FileTypeId",
                        column: x => x.FileTypeId,
                        principalTable: "FileFormats",
                        principalColumn: "FormatId");
                    table.ForeignKey(
                        name: "FK_TasksAttachments_TaskLists_TaskId",
                        column: x => x.TaskId,
                        principalTable: "TaskLists",
                        principalColumn: "TaskListId");
                });

            migrationBuilder.CreateTable(
                name: "TasksUsers",
                columns: table => new
                {
                    TaskUserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskId = table.Column<int>(type: "int", nullable: true),
                    userIdFrom = table.Column<int>(type: "int", nullable: true),
                    userIdTo = table.Column<int>(type: "int", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreateId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<bool>(type: "bit", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    UpdateId = table.Column<int>(type: "int", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteId = table.Column<int>(type: "int", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TasksUsers", x => x.TaskUserId);
                    table.ForeignKey(
                        name: "FK_TasksUsers_AspNetUsers_userIdFrom",
                        column: x => x.userIdFrom,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TasksUsers_AspNetUsers_userIdTo",
                        column: x => x.userIdTo,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TasksUsers_TaskLists_TaskId",
                        column: x => x.TaskId,
                        principalTable: "TaskLists",
                        principalColumn: "TaskListId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TasksAttachments_FileTypeId",
                table: "TasksAttachments",
                column: "FileTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TasksAttachments_TaskId",
                table: "TasksAttachments",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TasksUsers_TaskId",
                table: "TasksUsers",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TasksUsers_userIdFrom",
                table: "TasksUsers",
                column: "userIdFrom");

            migrationBuilder.CreateIndex(
                name: "IX_TasksUsers_userIdTo",
                table: "TasksUsers",
                column: "userIdTo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TasksAttachments");

            migrationBuilder.DropTable(
                name: "TasksUsers");

            migrationBuilder.DropTable(
                name: "TaskLists");
        }
    }
}
