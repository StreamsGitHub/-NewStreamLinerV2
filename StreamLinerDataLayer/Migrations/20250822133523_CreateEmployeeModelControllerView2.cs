using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StreamLinerDataLayer.Migrations
{
    /// <inheritdoc />
    public partial class CreateEmployeeModelControllerView2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HRAdvancePayment_AspNetUsers_userId",
                table: "HRAdvancePayment");

            migrationBuilder.DropForeignKey(
                name: "FK_HRAllowance_AspNetUsers_userId",
                table: "HRAllowance");

            migrationBuilder.DropForeignKey(
                name: "FK_HRAttend_AspNetUsers_userId",
                table: "HRAttend");

            migrationBuilder.DropForeignKey(
                name: "FK_HRExpenses_AspNetUsers_userId",
                table: "HRExpenses");

            migrationBuilder.DropForeignKey(
                name: "FK_HROverTimes_AspNetUsers_userId",
                table: "HROverTimes");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "HROverTimes",
                newName: "PartnerId");

            migrationBuilder.RenameIndex(
                name: "IX_HROverTimes_userId",
                table: "HROverTimes",
                newName: "IX_HROverTimes_PartnerId");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "HRExpenses",
                newName: "PartnerId");

            migrationBuilder.RenameIndex(
                name: "IX_HRExpenses_userId",
                table: "HRExpenses",
                newName: "IX_HRExpenses_PartnerId");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "HRAttend",
                newName: "PartnerId");

            migrationBuilder.RenameIndex(
                name: "IX_HRAttend_userId",
                table: "HRAttend",
                newName: "IX_HRAttend_PartnerId");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "HRAllowance",
                newName: "PartnerId");

            migrationBuilder.RenameIndex(
                name: "IX_HRAllowance_userId",
                table: "HRAllowance",
                newName: "IX_HRAllowance_PartnerId");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "HRAdvancePayment",
                newName: "PartnerId");

            migrationBuilder.RenameIndex(
                name: "IX_HRAdvancePayment_userId",
                table: "HRAdvancePayment",
                newName: "IX_HRAdvancePayment_PartnerId");

            migrationBuilder.CreateTable(
                name: "FingerPrint",
                columns: table => new
                {
                    FingerPrintId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UploadFileId = table.Column<int>(type: "int", nullable: false),
                    ACNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateAndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Exception = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Operation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IconUpload = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_FingerPrint", x => x.FingerPrintId);
                });

            migrationBuilder.CreateTable(
                name: "UploadFile",
                columns: table => new
                {
                    UploadFileId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_UploadFile", x => x.UploadFileId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_HRAdvancePayment_Partner_PartnerId",
                table: "HRAdvancePayment",
                column: "PartnerId",
                principalTable: "Partner",
                principalColumn: "PartnerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HRAllowance_Partner_PartnerId",
                table: "HRAllowance",
                column: "PartnerId",
                principalTable: "Partner",
                principalColumn: "PartnerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HRAttend_Partner_PartnerId",
                table: "HRAttend",
                column: "PartnerId",
                principalTable: "Partner",
                principalColumn: "PartnerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HRExpenses_Partner_PartnerId",
                table: "HRExpenses",
                column: "PartnerId",
                principalTable: "Partner",
                principalColumn: "PartnerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HROverTimes_Partner_PartnerId",
                table: "HROverTimes",
                column: "PartnerId",
                principalTable: "Partner",
                principalColumn: "PartnerId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HRAdvancePayment_Partner_PartnerId",
                table: "HRAdvancePayment");

            migrationBuilder.DropForeignKey(
                name: "FK_HRAllowance_Partner_PartnerId",
                table: "HRAllowance");

            migrationBuilder.DropForeignKey(
                name: "FK_HRAttend_Partner_PartnerId",
                table: "HRAttend");

            migrationBuilder.DropForeignKey(
                name: "FK_HRExpenses_Partner_PartnerId",
                table: "HRExpenses");

            migrationBuilder.DropForeignKey(
                name: "FK_HROverTimes_Partner_PartnerId",
                table: "HROverTimes");

            migrationBuilder.DropTable(
                name: "FingerPrint");

            migrationBuilder.DropTable(
                name: "UploadFile");

            migrationBuilder.RenameColumn(
                name: "PartnerId",
                table: "HROverTimes",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_HROverTimes_PartnerId",
                table: "HROverTimes",
                newName: "IX_HROverTimes_userId");

            migrationBuilder.RenameColumn(
                name: "PartnerId",
                table: "HRExpenses",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_HRExpenses_PartnerId",
                table: "HRExpenses",
                newName: "IX_HRExpenses_userId");

            migrationBuilder.RenameColumn(
                name: "PartnerId",
                table: "HRAttend",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_HRAttend_PartnerId",
                table: "HRAttend",
                newName: "IX_HRAttend_userId");

            migrationBuilder.RenameColumn(
                name: "PartnerId",
                table: "HRAllowance",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_HRAllowance_PartnerId",
                table: "HRAllowance",
                newName: "IX_HRAllowance_userId");

            migrationBuilder.RenameColumn(
                name: "PartnerId",
                table: "HRAdvancePayment",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_HRAdvancePayment_PartnerId",
                table: "HRAdvancePayment",
                newName: "IX_HRAdvancePayment_userId");

            migrationBuilder.AddForeignKey(
                name: "FK_HRAdvancePayment_AspNetUsers_userId",
                table: "HRAdvancePayment",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HRAllowance_AspNetUsers_userId",
                table: "HRAllowance",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HRAttend_AspNetUsers_userId",
                table: "HRAttend",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HRExpenses_AspNetUsers_userId",
                table: "HRExpenses",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HROverTimes_AspNetUsers_userId",
                table: "HROverTimes",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
