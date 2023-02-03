using MessagePack;
using System.Drawing;
using UniversidadApiBackend.Models.DataModels;

namespace UniversidadApiBackend.Services
{
    public class Services
    {
                //Buscar usuarios por email
        public static void SearchUserById(string email)
        {
            var users = new List<User>();

            var searchUserByEmail = users.FirstOrDefault(searched => searched.EmailAddress.Equals(email));
        }

        //Buscar alumnos mayores de edad
        public static void SearchStudentOver18 ()
        {
            var students = new List<Student>();

            var studentsOver18 = students.FindAll(student => student.Dob.Year <= (DateTime.Today.Year-18));
        }

        //Buscar alumnos que tengan al menos un curso
        public static void SearchStudentWithAtLeastOneCourse()
        {
            var students = new List<Student>();

            var studentsOver18 = students.Where(student => student.Courses.Any());
        }

        //Buscar cursos de un nivel determinado que al menos tengan un alumno inscrito
        public static void SearchCourseByLevel(string level)
        {
            var courses = new List<Course>();

            var searchUserByEmail = courses.FindAll(course => course.Students.Count > 0 && course.Level.Equals(level));
        }

        //Buscar cursos de un nivel determinado que sean de una categoría determinada
        public static void SearchCourseByLevelAndCategory(string level, string category)
        {
            var courses = new List<Course>();

            var searchUserByEmail = courses.Where(course => course.Level.Equals(level) && course.Categories.Equals(category));
        }

        //Buscar cursos sin alumnos
        public static void SearchCourseWithOutStudents()
        {
            var courses = new List<Course>();

            var searchUserByEmail = courses.FindAll(course => course.Students.Count == 0);
        }

    };
}
