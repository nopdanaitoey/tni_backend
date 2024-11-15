using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using tni_back.Database;
using tni_back.Middlewares;
using tni_back.Repositories;
using tni_back.Repositories.IRepositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
#region Repositories
builder.Services.AddScoped<IMasterProductRepository, MasterProductRepository>();
builder.Services.AddScoped<IUserCartRepository, UserCartRepositroy>();
builder.Services.AddScoped<IStockProductRepository, StockProductRepository>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
#endregion Repositories

builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(Program).Assembly));

IConfigurationRoot appsettings = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

var connectionStrings = appsettings.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<TniContext>(options =>
    options.UseMySql(connectionStrings, ServerVersion.AutoDetect(connectionStrings)));




var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(options => options
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
using var scope = app.Services.CreateScope();
var dataContext = scope.ServiceProvider.GetRequiredService<TniContext>();
await dataContext.SeedDatabase();
app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();

app.Run();

