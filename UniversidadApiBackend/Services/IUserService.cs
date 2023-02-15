using UniversidadApiBackend.Models.DataModels;

namespace UniversidadApiBackend.Services
{
    public interface IUserService
    {
        IEnumerable<User> SearchUserByEmail();
    }
}
