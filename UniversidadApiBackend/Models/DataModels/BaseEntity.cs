using System.ComponentModel.DataAnnotations;

namespace UniversidadApiBackend.Models.DataModels
{
    public class BaseEntity
    {
        [Required]
        [Key] 
        public int Id { get; set; }
        public string CreatedBye { get; set; } = string.Empty;
        public DateTime CreateAt { get; set; }  = DateTime.Now;
        public string UpdateBy { get; set; } = string.Empty;
        public DateTime UpdateAt { get; set; } = DateTime.Now;
        public string DeleteBy { get; set; } = string.Empty;
        public DateTime DeleteAt { get; set; } = DateTime.Now;

    }
}
