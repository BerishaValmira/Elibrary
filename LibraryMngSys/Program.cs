using LibraryMngSys.Areas.Identity.Data;
using LibraryMngSys.Data;
using LibraryMngSys.Models.Author;
using LibraryMngSys.Models.Book;
using LibraryMngSys.Models.Category;
using LibraryMngSys.Models.Role;
using LibraryMngSys.Models.Shop;
using LibraryMngSys.Models.Supplier;
using LibraryMngSys.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("LibraryMngSysContextConnection") ?? throw new InvalidOperationException("Connection string 'LibraryMngSysContextConnection' not found.");

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddDbContext<LibraryMngSysContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddDefaultIdentity<LibraryMngSysUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<LibraryMngSysContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

AddAuthorizationPolicies(builder.Services);

AddServices();

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
app.UseAuthentication(); ;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();

void AddAuthorizationPolicies(IServiceCollection services)
{
    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("EmployeeOnly", policy => policy.RequireClaim("EMLOYEE"));
        options.AddPolicy("AdminOnly", policy => policy.RequireClaim("ADMIN"));
        options.AddPolicy("UserOnly", policy => policy.RequireClaim("USER"));
    });

    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("RequireAdmin", policy => policy.RequireRole("admin"));
        options.AddPolicy("RequireUser", policy => policy.RequireRole("user"));
        options.AddPolicy("RequireEmployee", policy => policy.RequireRole("employee"));
    });
}

void AddServices()
{
    builder.Services.AddTransient<UserServices>();
    builder.Services.AddTransient<UserUtility>();
    builder.Services.AddTransient<RoleServices>();
    builder.Services.AddTransient<ShopServices>();
    builder.Services.AddTransient<ShopUtility>();
    builder.Services.AddTransient<CategoryServices>();
    builder.Services.AddTransient<CategoryUtility>();
    builder.Services.AddTransient<BookUtility>();
    builder.Services.AddTransient<BookServices>();
    builder.Services.AddTransient<AuthorUtility>();
    builder.Services.AddTransient<AuthorServices>();
    builder.Services.AddTransient<SupplierUtility>();
    builder.Services.AddTransient<SupplierServices>();
}