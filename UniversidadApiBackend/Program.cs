// 1.Usings para trabajar con entity framework

using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using UniversidadApiBackend;
using UniversidadApiBackend.DataAccess;
using UniversidadApiBackend.Services;

// 10. Use Serilog to log events
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// 11. Configurar Serilog
builder.Host.UseSerilog((hostBuilderCtx,loggerConf) => // hostBuilderCtx es el contexto, loggerConf on las configuraciones
{
    loggerConf
        .WriteTo.Console()
        .WriteTo.Debug()
        .ReadFrom.Configuration(hostBuilderCtx.Configuration);
});

// 2.Conexion con SQL Server Express

const string CONNECTIONNAME = "UniversityDB";

var conectionString = builder.Configuration.GetConnectionString(CONNECTIONNAME);

// 3.Agregar contexto a servicios del constructor
builder.Services.AddDbContext<UniversityDBContext>(options => options.UseSqlServer(conectionString));

// 7. Agregar servicios JWT
builder.Services.AddJwtTokenServices(builder.Configuration);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// 9. Configurar Swagger para que tenga en cuenta la autenticación
builder.Services.AddSwaggerGen(options =>
{
    // Definimos la seguridad
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization using Bearer Scheme"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
}); 

// 4. Añadir servicios creados
builder.Services.AddScoped<IStudentsService, StudentsService>();
builder.Services.AddScoped<ICategoriesService, CategoriesService>();
builder.Services.AddScoped<IChaptersService, ChaptersService>();
builder.Services.AddScoped<ICoursesService, CoursesService>();
// TO DO: Agregar el resto de los servicios

// 8. Agregar política de autorizacion
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("UserOnlyPolicy", policy => policy.RequireClaim("UserOnly","User1"));
});

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

// 12. Decimos a la app que utilice serilog
app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// 6. Decirle a nuestra aplicación que haga uso de CORS
app.UseCors("CorsPolicy");

app.Run();
