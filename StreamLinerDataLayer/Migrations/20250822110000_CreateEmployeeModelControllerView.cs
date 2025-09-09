using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StreamLinerDataLayer.Migrations
{
    /// <inheritdoc />
    public partial class CreateEmployeeModelControllerView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HRAttendance_AspNetUsers_userId",
                table: "HRAttendance");

            migrationBuilder.DropForeignKey(
                name: "FK_HRBenefits_AspNetUsers_userId",
                table: "HRBenefits");

            migrationBuilder.DropForeignKey(
                name: "FK_HRContract_AspNetUsers_userId",
                table: "HRContract");

            migrationBuilder.DropForeignKey(
                name: "FK_HREmployeeRule_AspNetUsers_userId",
                table: "HREmployeeRule");

            migrationBuilder.DropForeignKey(
                name: "FK_HRMission_AspNetUsers_userId",
                table: "HRMission");

            migrationBuilder.DropForeignKey(
                name: "FK_HRPenalty_AspNetUsers_userId",
                table: "HRPenalty");

            migrationBuilder.DropForeignKey(
                name: "FK_HRPermission_AspNetUsers_userId",
                table: "HRPermission");

            migrationBuilder.DropForeignKey(
                name: "FK_HRRequest_AspNetUsers_userId",
                table: "HRRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_HRVacations_AspNetUsers_userId",
                table: "HRVacations");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "HRVacations",
                newName: "PartnerId");

            migrationBuilder.RenameIndex(
                name: "IX_HRVacations_userId",
                table: "HRVacations",
                newName: "IX_HRVacations_PartnerId");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "HRRequest",
                newName: "PartnerId");

            migrationBuilder.RenameIndex(
                name: "IX_HRRequest_userId",
                table: "HRRequest",
                newName: "IX_HRRequest_PartnerId");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "HRPermission",
                newName: "PartnerId");

            migrationBuilder.RenameIndex(
                name: "IX_HRPermission_userId",
                table: "HRPermission",
                newName: "IX_HRPermission_PartnerId");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "HRPenalty",
                newName: "PartnerId");

            migrationBuilder.RenameIndex(
                name: "IX_HRPenalty_userId",
                table: "HRPenalty",
                newName: "IX_HRPenalty_PartnerId");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "HRMission",
                newName: "PartnerId");

            migrationBuilder.RenameIndex(
                name: "IX_HRMission_userId",
                table: "HRMission",
                newName: "IX_HRMission_PartnerId");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "HREmployeeRule",
                newName: "PartnerId");

            migrationBuilder.RenameIndex(
                name: "IX_HREmployeeRule_userId",
                table: "HREmployeeRule",
                newName: "IX_HREmployeeRule_PartnerId");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "HRContract",
                newName: "PartnerId");

            migrationBuilder.RenameIndex(
                name: "IX_HRContract_userId",
                table: "HRContract",
                newName: "IX_HRContract_PartnerId");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "HRBenefits",
                newName: "PartnerId");

            migrationBuilder.RenameIndex(
                name: "IX_HRBenefits_userId",
                table: "HRBenefits",
                newName: "IX_HRBenefits_PartnerId");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "HRAttendance",
                newName: "PartnerId");

            migrationBuilder.RenameIndex(
                name: "IX_HRAttendance_userId",
                table: "HRAttendance",
                newName: "IX_HRAttendance_PartnerId");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "TasksUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "TasksAttachments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "TaskLists",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "MetaDataTemplates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "MetaDataTemplateFields",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "MeetingUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Meetings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "MeetingRooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "HRVacationType",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "HRVacations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "HRVacationRuleLine",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "HRVacationRule",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "HRTeamWorkDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PartnerId",
                table: "HRTeamWorkDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "HRTeamWork",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "HRStage",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "HRSpecialistLevel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "HRSource",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "HRShiftAttend",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "HRschema",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "HRRequestType",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "HRRequest",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "HRPermission",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "HRPenaltyTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "HRPenalty",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "HROverTimes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "HRNotes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "HRMissionType",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "HRMission",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "HRJobPositions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "HRExpenses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "HREmployeeRule",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "HRDocuments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "HRDepartment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "HRDegree",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "HRDayType",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "HRContractType",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "HRContract",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "HRBenefitsType",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "HRBenefits",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "HRAttendRole",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "HRAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "HRAttend",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "HRApplication",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "HRAllowanceType",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "HRAllowance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "HRAdvancePayment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Folders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "FinancialYear",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "FileFormats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "FieldType",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Fields",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Documents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "DocTemplates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Mobile",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PartnerId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StateId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ResourceCity",
                columns: table => new
                {
                    CityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceCity", x => x.CityId);
                });

            migrationBuilder.CreateTable(
                name: "ResourceGender",
                columns: table => new
                {
                    GenderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenderName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceGender", x => x.GenderId);
                });

            migrationBuilder.CreateTable(
                name: "ResourceMaritalStatus",
                columns: table => new
                {
                    MaritalStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaritalStatusName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceMaritalStatus", x => x.MaritalStatusId);
                });

            migrationBuilder.CreateTable(
                name: "ResourceNationality",
                columns: table => new
                {
                    NationalityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NationalityName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceNationality", x => x.NationalityId);
                });

            migrationBuilder.CreateTable(
                name: "Partner",
                columns: table => new
                {
                    PartnerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FingerPrintId = table.Column<int>(type: "int", nullable: false),
                    BirthData = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HireData = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EmployeeImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    PartnerType = table.Column<int>(type: "int", nullable: false),
                    Employee = table.Column<bool>(type: "bit", nullable: false),
                    Customer = table.Column<bool>(type: "bit", nullable: false),
                    Vendor = table.Column<bool>(type: "bit", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassportNo = table.Column<int>(type: "int", nullable: false),
                    NationalityId = table.Column<int>(type: "int", nullable: false),
                    GenderId = table.Column<int>(type: "int", nullable: false),
                    MaritalStatusId = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    ManagerId = table.Column<int>(type: "int", nullable: false),
                    CoachId = table.Column<int>(type: "int", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    BankAccountNumber = table.Column<int>(type: "int", nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Insurance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Tax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ContractId = table.Column<int>(type: "int", nullable: false),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: true),
                    End = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HRShiftAttendId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_Partner", x => x.PartnerId);
                    table.ForeignKey(
                        name: "FK_Partner_HRCompany_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "HRCompany",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Partner_HRDepartment_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "HRDepartment",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Partner_ResourceCity_CityId",
                        column: x => x.CityId,
                        principalTable: "ResourceCity",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Partner_ResourceGender_GenderId",
                        column: x => x.GenderId,
                        principalTable: "ResourceGender",
                        principalColumn: "GenderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Partner_ResourceMaritalStatus_MaritalStatusId",
                        column: x => x.MaritalStatusId,
                        principalTable: "ResourceMaritalStatus",
                        principalColumn: "MaritalStatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Partner_ResourceNationality_NationalityId",
                        column: x => x.NationalityId,
                        principalTable: "ResourceNationality",
                        principalColumn: "NationalityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HRTeamWorkDetails_PartnerId",
                table: "HRTeamWorkDetails",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Partner_CityId",
                table: "Partner",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Partner_CompanyId",
                table: "Partner",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Partner_DepartmentId",
                table: "Partner",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Partner_GenderId",
                table: "Partner",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Partner_MaritalStatusId",
                table: "Partner",
                column: "MaritalStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Partner_NationalityId",
                table: "Partner",
                column: "NationalityId");

            migrationBuilder.AddForeignKey(
                name: "FK_HRAttendance_Partner_PartnerId",
                table: "HRAttendance",
                column: "PartnerId",
                principalTable: "Partner",
                principalColumn: "PartnerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HRBenefits_Partner_PartnerId",
                table: "HRBenefits",
                column: "PartnerId",
                principalTable: "Partner",
                principalColumn: "PartnerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HRContract_Partner_PartnerId",
                table: "HRContract",
                column: "PartnerId",
                principalTable: "Partner",
                principalColumn: "PartnerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HREmployeeRule_Partner_PartnerId",
                table: "HREmployeeRule",
                column: "PartnerId",
                principalTable: "Partner",
                principalColumn: "PartnerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HRMission_Partner_PartnerId",
                table: "HRMission",
                column: "PartnerId",
                principalTable: "Partner",
                principalColumn: "PartnerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HRPenalty_Partner_PartnerId",
                table: "HRPenalty",
                column: "PartnerId",
                principalTable: "Partner",
                principalColumn: "PartnerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HRPermission_Partner_PartnerId",
                table: "HRPermission",
                column: "PartnerId",
                principalTable: "Partner",
                principalColumn: "PartnerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HRRequest_Partner_PartnerId",
                table: "HRRequest",
                column: "PartnerId",
                principalTable: "Partner",
                principalColumn: "PartnerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HRTeamWorkDetails_Partner_PartnerId",
                table: "HRTeamWorkDetails",
                column: "PartnerId",
                principalTable: "Partner",
                principalColumn: "PartnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_HRVacations_Partner_PartnerId",
                table: "HRVacations",
                column: "PartnerId",
                principalTable: "Partner",
                principalColumn: "PartnerId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HRAttendance_Partner_PartnerId",
                table: "HRAttendance");

            migrationBuilder.DropForeignKey(
                name: "FK_HRBenefits_Partner_PartnerId",
                table: "HRBenefits");

            migrationBuilder.DropForeignKey(
                name: "FK_HRContract_Partner_PartnerId",
                table: "HRContract");

            migrationBuilder.DropForeignKey(
                name: "FK_HREmployeeRule_Partner_PartnerId",
                table: "HREmployeeRule");

            migrationBuilder.DropForeignKey(
                name: "FK_HRMission_Partner_PartnerId",
                table: "HRMission");

            migrationBuilder.DropForeignKey(
                name: "FK_HRPenalty_Partner_PartnerId",
                table: "HRPenalty");

            migrationBuilder.DropForeignKey(
                name: "FK_HRPermission_Partner_PartnerId",
                table: "HRPermission");

            migrationBuilder.DropForeignKey(
                name: "FK_HRRequest_Partner_PartnerId",
                table: "HRRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_HRTeamWorkDetails_Partner_PartnerId",
                table: "HRTeamWorkDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_HRVacations_Partner_PartnerId",
                table: "HRVacations");

            migrationBuilder.DropTable(
                name: "Partner");

            migrationBuilder.DropTable(
                name: "ResourceCity");

            migrationBuilder.DropTable(
                name: "ResourceGender");

            migrationBuilder.DropTable(
                name: "ResourceMaritalStatus");

            migrationBuilder.DropTable(
                name: "ResourceNationality");

            migrationBuilder.DropIndex(
                name: "IX_HRTeamWorkDetails_PartnerId",
                table: "HRTeamWorkDetails");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "TasksUsers");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "TasksAttachments");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "TaskLists");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "MetaDataTemplates");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "MetaDataTemplateFields");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "MeetingUsers");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "MeetingRooms");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "HRVacationType");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "HRVacations");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "HRVacationRuleLine");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "HRVacationRule");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "HRTeamWorkDetails");

            migrationBuilder.DropColumn(
                name: "PartnerId",
                table: "HRTeamWorkDetails");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "HRTeamWork");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "HRStage");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "HRSpecialistLevel");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "HRSource");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "HRShiftAttend");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "HRschema");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "HRRequestType");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "HRRequest");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "HRPermission");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "HRPenaltyTypes");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "HRPenalty");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "HROverTimes");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "HRNotes");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "HRMissionType");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "HRMission");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "HRJobPositions");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "HRExpenses");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "HREmployeeRule");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "HRDocuments");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "HRDepartment");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "HRDegree");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "HRDayType");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "HRContractType");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "HRContract");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "HRBenefitsType");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "HRBenefits");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "HRAttendRole");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "HRAttendance");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "HRAttend");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "HRApplication");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "HRAllowanceType");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "HRAllowance");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "HRAdvancePayment");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Folders");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "FinancialYear");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "FileFormats");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "FieldType");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "DocTemplates");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Company",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Mobile",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PartnerId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "PartnerId",
                table: "HRVacations",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_HRVacations_PartnerId",
                table: "HRVacations",
                newName: "IX_HRVacations_userId");

            migrationBuilder.RenameColumn(
                name: "PartnerId",
                table: "HRRequest",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_HRRequest_PartnerId",
                table: "HRRequest",
                newName: "IX_HRRequest_userId");

            migrationBuilder.RenameColumn(
                name: "PartnerId",
                table: "HRPermission",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_HRPermission_PartnerId",
                table: "HRPermission",
                newName: "IX_HRPermission_userId");

            migrationBuilder.RenameColumn(
                name: "PartnerId",
                table: "HRPenalty",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_HRPenalty_PartnerId",
                table: "HRPenalty",
                newName: "IX_HRPenalty_userId");

            migrationBuilder.RenameColumn(
                name: "PartnerId",
                table: "HRMission",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_HRMission_PartnerId",
                table: "HRMission",
                newName: "IX_HRMission_userId");

            migrationBuilder.RenameColumn(
                name: "PartnerId",
                table: "HREmployeeRule",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_HREmployeeRule_PartnerId",
                table: "HREmployeeRule",
                newName: "IX_HREmployeeRule_userId");

            migrationBuilder.RenameColumn(
                name: "PartnerId",
                table: "HRContract",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_HRContract_PartnerId",
                table: "HRContract",
                newName: "IX_HRContract_userId");

            migrationBuilder.RenameColumn(
                name: "PartnerId",
                table: "HRBenefits",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_HRBenefits_PartnerId",
                table: "HRBenefits",
                newName: "IX_HRBenefits_userId");

            migrationBuilder.RenameColumn(
                name: "PartnerId",
                table: "HRAttendance",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_HRAttendance_PartnerId",
                table: "HRAttendance",
                newName: "IX_HRAttendance_userId");

            migrationBuilder.AlterColumn<string>(
                name: "DepartmentId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_HRAttendance_AspNetUsers_userId",
                table: "HRAttendance",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HRBenefits_AspNetUsers_userId",
                table: "HRBenefits",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HRContract_AspNetUsers_userId",
                table: "HRContract",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HREmployeeRule_AspNetUsers_userId",
                table: "HREmployeeRule",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HRMission_AspNetUsers_userId",
                table: "HRMission",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HRPenalty_AspNetUsers_userId",
                table: "HRPenalty",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HRPermission_AspNetUsers_userId",
                table: "HRPermission",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HRRequest_AspNetUsers_userId",
                table: "HRRequest",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HRVacations_AspNetUsers_userId",
                table: "HRVacations",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
