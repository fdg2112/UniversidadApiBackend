using Microsoft.EntityFrameworkCore;
using UniversidadApiBackend.Models.DataModels;

namespace UniversidadApiBackend.DataAccess
{
    public class UniversityDBContext: DbContext
    {
        private readonly ILoggerFactory _loggerFactory;

        public UniversityDBContext(DbContextOptions<UniversityDBContext> options, ILoggerFactory loggerFactory) : base(options) 
        {
            _loggerFactory = loggerFactory;
        }

        public DbSet<User>? Users { get; set; }
        public DbSet<Course>? Courses { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Student>? students { get; set; }
        public DbSet<Chapter>? Chapters { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var logger = _loggerFactory.CreateLogger<UniversityDBContext>();
            optionsBuilder.LogTo(d => logger.Log(LogLevel.Information, d, new[] { DbLoggerCategory.Database.Name }), LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }

    }
}
