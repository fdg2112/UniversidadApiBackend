using UniversidadApiBackend.Models.DataModels;

namespace UniversidadApiBackend.Services
{
    public interface ICoursesService
    {
        IEnumerable<Category> GetCourseByCategory();

        IEnumerable<Chapter> GetCourseWithOutChapters();

        IEnumerable<Student> GetCoursesByStudent();
    }
}
