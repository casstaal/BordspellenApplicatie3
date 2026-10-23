using BordspellenApplicatie.Domain.Interfaces;
using BordspellenApplicatie.Infrastructure.Data;
using BordspellenApplicatie.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var dbConnectionString =
    builder.Configuration.GetConnectionString("ConnectionStringAzureSQL")
    ?? builder.Configuration.GetValue<string>("ConnectionStringAzureSQL")
    ?? throw new InvalidOperationException("Database connection string ontbreekt.");

builder.Services.AddDbContext<BordspellenavondDBContext>(options =>
{
    options
        .UseSqlServer(dbConnectionString, sqlOptions =>
        {
            sqlOptions.MigrationsHistoryTable("EFMigrationHistory", BordspellenavondDBContext.SqlSchema);
        });
});

// Identity dbcontext
builder.Services.AddDbContext<BordspellenavondIdentityDBContext>(
    options => {
        options
                    .UseSqlServer(dbConnectionString, sqlOptions =>
                    {
                        sqlOptions.MigrationsHistoryTable("EFMigrationHistory", BordspellenavondIdentityDBContext.SqlSchema);
                    });
    });

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => { // Opties voor IF biedt, kies zelf wat relevant is -->
    options.User.RequireUniqueEmail = true; 
    options.SignIn.RequireConfirmedPhoneNumber = false; 
    options.Password.RequireDigit = false; 
    options.Password.RequireLowercase = false; 
    options.Password.RequireUppercase = false; 
    options.Password.RequireNonAlphanumeric = false; 
    options.Password.RequiredLength = 3; 
}) 
    
.AddRoles<IdentityRole>()// <-- Support voor role based authz 
// Welke context voor persistentie -->
.AddEntityFrameworkStores<BordspellenavondIdentityDBContext>() 
// Tokens voor wawo reset, tfa, etc -->
.AddDefaultTokenProviders();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services.AddScoped<AuthSeedData>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBoardGameNightRepository, BoardGameNightRepository>();
builder.Services.AddScoped<IUserBoardGameNightRepository, UserBoardGameNightRepository>();
builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();

builder.Services.AddHttpClient();

var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//    var seedData = scope.ServiceProvider.GetRequiredService<AuthSeedData>();

//    await seedData.SeedDatabase();
//}

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
