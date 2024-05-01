using Blogs.Models;

namespace Blogs.Services
{
    public interface IUsersService
    {
        Task<List<User>> GetUsers();
        Task<User> GetUserById(int id);
        Task<User> PostUser(User user);
        Task<User> PutUser(int id, User user);
        Task<User> DeleteUser(int id);
        Task<string> Login(string email, string password);
    }
}
