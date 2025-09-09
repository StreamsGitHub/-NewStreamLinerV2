using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StreamLinerDataLayer.Data;
using StreamLinerEntitiesLayer.Entities;
using StreamLinerLogicLayer.Helper.EmailService;
using StreamLinerLogicLayer.Helper.LicenseServices;
using StreamLinerLogicLayer.Mappings;
using StreamLinerLogicLayer.Services;
using StreamLinerLogicLayer.Services.AdvancePaymentServices;
using StreamLinerLogicLayer.Services.AllowanceServices;
using StreamLinerLogicLayer.Services.Auth;
using StreamLinerLogicLayer.Services.BenefitServices;
using StreamLinerLogicLayer.Services.ExpensesServices;
using StreamLinerLogicLayer.Services.FieldServices;
using StreamLinerLogicLayer.Services.FieldTypeServices;
using StreamLinerLogicLayer.Services.MeetingRoomServices;
using StreamLinerLogicLayer.Services.MetaDataServices;
using StreamLinerLogicLayer.Services.MetaDataTemplateServices;
using StreamLinerLogicLayer.Services.MissionServices;
using StreamLinerLogicLayer.Services.OvertimeServices;
using StreamLinerLogicLayer.Services.PenaltyServices;
using StreamLinerLogicLayer.Services.PermissionServices;
using StreamLinerLogicLayer.Services.RepoDocument;
using StreamLinerLogicLayer.Services.RepoFolder;
using StreamLinerLogicLayer.Services.RepositoryServices;
using StreamLinerLogicLayer.Services.TemplateServices;
using StreamLinerLogicLayer.Services.VacationsServices;
using StreamLinerRepositoryLayer.IRepositories;
using StreamLinerRepositoryLayer.Repositories;
using System.Security;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// --- MVC + Controllers ---
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

// --- AutoMapper ---
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);


// --- DB Context ---
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// --- Identity ---
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// --- Authorization Policies ---
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("StreamsRights", policy =>
          policy.RequireRole("Administrator", "Admin", "NamedUser" , "Participant"));
});

// --- password rules, lockout ---
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
});

// --- Configure cookie auth ---
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
});

// --- DI Services ---
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<ILicenseService, LicenseService>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IRepoService, RepoService>();
builder.Services.AddScoped<IFolderService, FolderService>();
builder.Services.AddScoped<IDocumentService, DocumentService>();
builder.Services.AddScoped<IProject, ProjectService>();
builder.Services.AddScoped<IField, FieldService>();
builder.Services.AddScoped<IFiedlType, FieldTypeService>();
builder.Services.AddScoped<IMeetingRoom, MeetingRoomService>();


builder.Services.AddScoped<IMetaDataService, MetaDataService>();
builder.Services.AddScoped<IMetaDataTemplateService, MetaDataTemplateService>();
builder.Services.AddScoped<IDocTemplateService, DocTemplateService>();
builder.Services.AddScoped<IAdvancePaymentService, AdvancePaymentService>();
builder.Services.AddScoped<IAllowanceService, AllowanceService>();
builder.Services.AddScoped<IBenefitService, BenefitService>();

builder.Services.AddScoped<IExpensesService, ExpensesService>();
builder.Services.AddScoped<IMissionService, MissionService>();
builder.Services.AddScoped<IOvertimeService, OvertimeService>();
builder.Services.AddScoped<IPenaltyService, PenaltyService>();
builder.Services.AddScoped<IPermissionService, PermissionService>();
builder.Services.AddScoped<IVacationsService, VacationsService>();

// --- JWT ---
var jwtSettings = builder.Configuration.GetSection("Jwt");
var secretKey = jwtSettings["Secret"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie();
//.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
//{
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        ValidIssuer = jwtSettings["Issuer"],
//        ValidAudience = jwtSettings["Audience"],
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
//    };
//});

// --- Build the app ---
var app = builder.Build();

// ---  Error handling for production ---
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

//app.MapStaticAssets();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
