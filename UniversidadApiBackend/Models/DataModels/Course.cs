using System.ComponentModel.DataAnnotations;

namespace UniversidadApiBackend.Models.DataModels
{
    public enum Levels
    {
        Básico,
        Intermedio,
        Avanzado
    }
    public class Course : BaseEntity
    {
        [Required, StringLength(100)] 
        public string Name { get; set; } = string.Empty;
        [StringLength(280)]
        public string LongDescription { get; set; } = string.Empty;
        [StringLength(100)]
        public string ShortDescription { get; set; } = string.Empty;
        [StringLength(100)]
        public string TargetAudiences { get; set; } = string.Empty;
        [StringLength(100)]
        public string Objetives { get; set; } = string.Empty;
        [StringLength(100)]
        public string Requirements { get; set; } = string.Empty;
        public Levels Level { get; set; } = Levels.Básico;
        [Required]
        public ICollection<Category> Categories { get; set; } = new List<Category>();
        [Required]
        public Chapter Chapters { get; set; } = new Chapter();
        [Required]
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }

    
}
