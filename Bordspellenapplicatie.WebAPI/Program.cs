using Bordspellenapplicatie.WebAPI.GraphQL;
using Bordspellenapplicatie.WebAPI.Mappings;
using BordspellenApplicatie.Domain.Interfaces;
using BordspellenApplicatie.Infrastructure.Data;
using BordspellenApplicatie.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var dbConnectionString = builder.Configuration.GetValue<string>("ConnectionStringAzureSQL");

builder.Services.AddDbContext<BordspellenavondDBContext>(options =>
{
    options
        .UseSqlServer(dbConnectionString, sqlOptions =>
        {
            sqlOptions.MigrationsHistoryTable("EFMigrationHistory", BordspellenavondDBContext.SqlSchema);
        });
});

builder.Services.AddDbContext<BordspellenavondIdentityDBContext>(options =>
{
    options
        .UseSqlServer(dbConnectionString, sqlOptions =>
        {
            sqlOptions.MigrationsHistoryTable("EFMigrationHistory", BordspellenavondIdentityDBContext.SqlSchema);
        });
});

builder.Services.AddAutoMapper(cfg => { }, typeof(AutoMapperProfiles));

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>();

builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<IdentityUser>(options => {
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedPhoneNumber = false;

    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 3;

})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<BordspellenavondIdentityDBContext>();


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBoardGameNightRepository, BoardGameNightRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IGameRepository, GameRepository>();

var app = builder.Build();
// app.UseHttpLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.MapGraphQL("/graphql");


app.Run();