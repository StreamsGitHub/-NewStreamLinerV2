using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StreamLinerEntitiesLayer.Entities;
using StreamLinerEntitiesLayer.HREntities;

namespace StreamLinerDataLayer.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
       : base(options)
        {
        }

        // General
        public DbSet<UsersGroup> UsersGroup { get; set; }
        public DbSet<Groups> Groups { get; set; }

        public DbSet<Projects> Projects { get; set; }
        public DbSet<Strlcs> Strlcs { get; set; }


        // Resourc
        public DbSet<Partner> Partner { get; set; }
        public DbSet<ResourceCity> ResourceCity { get; set; }
        public DbSet<ResourceGender> ResourceGender { get; set; }
        public DbSet<ResourceMaritalStatus> ResourceMaritalStatus { get; set; }
        public DbSet<ResourceNationality> ResourceNationality { get; set; }
        public DbSet<FingerPrint> FingerPrint { get; set; }

        // Repositories
        public DbSet<RepositoriesDisk> RepositoriesDisk { get; set; }
        public DbSet<RepoUserPermission> RepoUserPermissions { get; set; }
        public DbSet<RepoPermissionType> RepoPermissionType { get; set; }


        // Foldrs
        public DbSet<Folder> Folders { get; set; }
        public DbSet<PermissionType> PermissionType { get; set; }
        public DbSet<PermissionScope> PermissionScope { get; set; }
        public DbSet<FolderUserPermission> FolderUserPermissions { get; set; }
        public DbSet<FolderUserPermissionScope> FolderUserPermissionScope { get; set; }
     


        // Documents
        public DbSet<Document> Documents { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<FileFormat> FileFormats { get; set; }
        public DbSet<UploadFile> UploadFile { get; set; }

        public DbSet<MetaDataTemplate> MetaDataTemplates { get; set; }
        public DbSet<MetaDataTemplateField> MetaDataTemplateFields { get; set; }
        public DbSet<DocTemplate> DocTemplates { get; set; }



        // Meetings
        public DbSet<MeetingRoom> MeetingRooms { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<MeetingUser> MeetingUsers { get; set; }

        // TaskList
        public DbSet<TaskList> TaskLists { get; set; }
        public DbSet<TasksAttachment> TasksAttachments { get; set; }
        public DbSet<TasksUser> TasksUsers { get; set; }

      


        // HR Entities

        //public DbSet<FingerPrint> FingerPrint { get; set; }
        public DbSet<HRschema> HRschema { get; set; }
        public DbSet<HRRequestType> HRRequestType { get; set; }
        public DbSet<HRRequest> HRRequest { get; set; }
        public DbSet<HRAttend> HRAttend { get; set; }
        //public DbSet<UploadFile> UploadFile { get; set; }
        //public DbSet<NewFingerPrint> NewFingerPrint { get; set; }
        public DbSet<FinancialYear> FinancialYear { get; set; }
        public DbSet<HREmployeeRule> HREmployeeRule { get; set; }
        public DbSet<HRApplication> HRApplication { get; set; }
        public DbSet<HRCompany> HRCompany { get; set; }
        public DbSet<HRContract> HRContract { get; set; }
        public DbSet<HRContractType> HRContractType { get; set; }
        public DbSet<HRDegree> HRDegree { get; set; }
        public DbSet<HRDepartment> HRDepartment { get; set; }
        public DbSet<HRNotes> HRNotes { get; set; }
        public DbSet<HRDocuments> HRDocuments { get; set; }

        public DbSet<HRJobPositions> HRJobPositions { get; set; }
        public DbSet<HRSource> HRSource { get; set; }
        public DbSet<HRSpecialistLevel> HRSpecialistLevel { get; set; }
        public DbSet<HRStage> HRStage { get; set; }
        //public DbSet<Users> Users { get; set; }
        //public DbSet<ResourceCity> ResourceCity { get; set; }
        //public DbSet<ResourceGender> ResourceGender { get; set; }
        //public DbSet<ResourceMaritalStatus> ResourceMaritalStatus { get; set; }
        //public DbSet<ResourceNationality> ResourceNationality { get; set; }
        public DbSet<HRAttendances> HRAttendance { get; set; }
        public DbSet<HRShiftAttend> HRShiftAttend { get; set; }

        public DbSet<HRPermission> HRPermission { get; set; }
        public DbSet<HRMission> HRMission { get; set; }
        public DbSet<HRMissionType> HRMissionType { get; set; }
        public DbSet<HRDayType> HRDayType { get; set; }
        public DbSet<HRBenefitsType> HRBenefitsType { get; set; }
        public DbSet<HRPenaltyTypes> HRPenaltyTypes { get; set; }
        public DbSet<HRTeamWork> HRTeamWork { get; set; }
        public DbSet<HRTeamWorkDetails> HRTeamWorkDetails { get; set; }
        public DbSet<HRPenalty> HRPenalty { get; set; }
        public DbSet<HRBenefits> HRBenefits { get; set; }
        public DbSet<HRAttendRole> HRAttendRole { get; set; }

        public DbSet<HRVacationType> HRVacationType { get; set; }
        public DbSet<HRVacationRule> HRVacationRule { get; set; }
        public DbSet<HRVacationRuleLine> HRVacationRuleLine { get; set; }
        public DbSet<HRVacations> HRVacations { get; set; }
        public DbSet<HRAdvancePayment> HRAdvancePayment { get; set; }



        public DbSet<HRAllowanceType> HRAllowanceType { get; set; }
        public DbSet<HROverTimes> HROverTimes { get; set; }
        public DbSet<HRExpenses> HRExpenses { get; set; }
        public DbSet<HRAllowance> HRAllowance { get; set; }
    }
}
