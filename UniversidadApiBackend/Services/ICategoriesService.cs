using UniversidadApiBackend.Models.DataModels;

namespace UniversidadApiBackend.Services
{
    public interface ICategoriesService
    {
        IEnumerable<Category> GetCategoriesWithCourses();
    }
}
