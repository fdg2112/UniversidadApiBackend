using UniversidadApiBackend.Models.DataModels;

namespace UniversidadApiBackend.Services
{
    public interface IChaptersService
    {
        IEnumerable<Chapter> GetChapterByeCourse();
    }
}
