// 1.Usings para trabajar con entity framework

using Microsoft.EntityFrameworkCore;
using UniversidadApiBackend.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// 2.Conexion con SQL Server Express

const string CONNECTIONNAME = "UniversityDB";

var conectionString = builder.Configuration.GetConnectionString(CONNECTIONNAME);

// 3.Agregar contexto a servicios del constructor
builder.Services.AddDbContext<UniversityDBContext>(options => options.UseSqlServer(conectionString));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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
