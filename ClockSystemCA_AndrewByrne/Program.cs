
using DAL.Interfaces;
using DAL.Repos;
using ClockSystem.Services.Services;
using ClockSystem.Services.Interfaces;
using ClockSystemCA_AndrewByrne;
using Microsoft.EntityFrameworkCore;
using DAL;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

// Add AppDbContext to Services, AppDbContext is in DAL
builder.Services.AddDbContext<AppDbContext>(options =>
{
    // options is the param for AppDbContext, passing in the connection string
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add UserDbContext to Services, UserDbContext is in DAL
builder.Services.AddDbContext<UserDbContext>(options =>
{
    // options is the param for AppDbContext, passing in the connection string
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Adding my connection between my interface and repo/services
// to allow using Interface reference for dependancy injection.
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleRepo, RoleRepo>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IClockRecordRepo, ClockRecordRepo>();
builder.Services.AddScoped<IClockRecordService, ClockRecordService>();
builder.Services.AddScoped<IClockTypeRepo, ClockTypeRepo>();
builder.Services.AddScoped<IClockTypeService, ClockTypeService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IAbsenceRequestRepo, AbsenceRequestRepo>();
builder.Services.AddScoped<IAbsenceRequestService, AbsenceRequestService>();
builder.Services.AddScoped<IAbsenceTypeRepo, AbsenceTypeRepo>();
builder.Services.AddScoped<IAbsenceTypeService, AbsenceTypeService>();
builder.Services.AddScoped<IAbsenceRecordRepo, AbsenceRecordRepo>();
builder.Services.AddScoped<IAbsenceRecordService, AbsenceRecordService>();
builder.Services.AddScoped<IFlexiRepo, FlexiRepo>();
builder.Services.AddScoped<IFlexiService, FlexiService>();

// Adding Authentication to force login for access
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        // Redirect for failed auth
        options.LoginPath = "/Account/Index";
        // Redirect for logout 
        options.LogoutPath = "/Account/Index";
        // Time cookie lasts
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        // Allow cookie time to extend every time action is taken and user re-authenticated
        options.SlidingExpiration = true;
    });

// Adding Authorisation to use role based policy, User Role stored in Auth cookie
builder.Services.AddAuthorization(options =>
{
    // Policy for each user. Allows for enforcement like this in controllers [Authorize(Roles = "Employee")] 
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
    options.AddPolicy("EmployeePolicy", policy => policy.RequireRole("Employee"));
});

// Add HttpContextAccessor so I can pass to Account Service
builder.Services.AddHttpContextAccessor();

// Add helper scoped
builder.Services.AddScoped<UserHelper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Index}/{id?}");

app.Run();
