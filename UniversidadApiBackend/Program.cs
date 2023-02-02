// 1.Usings para trabajar con entity framework

using Microsoft.EntityFrameworkCore;
using UniversidadApiBackend.DataAccess;
using UniversidadApiBackend.Services;

var builder = WebApplication.CreateBuilder(args);

// 2.Conexion con SQL Server Express

const string CONNECTIONNAME = "UniversityDB";

var conectionString = builder.Configuration.GetConnectionString(CONNECTIONNAME);

// 3.Agregar contexto a servicios del constructor
builder.Services.AddDbContext<UniversityDBContext>(options => options.UseSqlServer(conectionString));

// 7. Agregar servicios JWT
// TO DO
// builder.Services.AddJwtTokenServices(builder.Configuration);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // 8. TO DO: Configurar Swagger para que tenga en cuenta la autenticación

// 4. Añadir servicios creados
builder.Services.AddScoped<IStudentsService, StudentsService>();
// TO DO: Agregar el resto de los servicios




// 5. Configurar CORS
builder.Services.AddCors( options =>
{
    options.AddPolicy(name: "CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
    });
});

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

// 6. Decirle a nuestra aplicación que haga uso de CORS
app.UseCors("CorsPolicy");

app.Run();
