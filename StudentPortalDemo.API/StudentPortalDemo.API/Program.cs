using Microsoft.EntityFrameworkCore;
using StudentPortalDemo.API.DataModels;
using StudentPortalDemo.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("StudentAdminPortalDb");
builder.Services.AddDbContext<StudentAdminContext>(
    options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<IStudentsRepo, SqlStudentsRepo>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
