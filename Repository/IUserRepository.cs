namespace Blogs.Repository
{
    public interface IUserRepository
    {
        Task<List<Blogs.Models.User>> GetUsers();
        Task<Blogs.Models.User> GetUserById(int id);
        Task<Blogs.Models.User> PostUser(Blogs.Models.User user);
        Task<Blogs.Models.User> PutUser(int id, Blogs.Models.User user);
        Task<Blogs.Models.User> DeleteUser(int id);
        Task<string> Login(string email, string password);
    }
}
