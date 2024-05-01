using Blogs.Exception;
using Blogs.Models;
using Blogs.Repository;

namespace Blogs.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUserRepository _repo;
        public UsersService(IUserRepository repo)
        {
            _repo = repo;
        }
        public async Task<List<User>> GetUsers()
        {
            return await _repo.GetUsers();
        }
        public async Task<User> GetUserById(int id)
        {
            if (await _repo.GetUserById(id) == null)
            {
                throw new UsersNotFoundException($"User with User id {id} does not exists");
            }
            return await _repo.GetUserById(id);
        }
        public async Task<User> PostUser(User user)
        {
            return await _repo.PostUser(user);
        }
        public async Task<User> PutUser(int id, User user)
        {

            if (await _repo.GetUserById(id) == null)
            {
                throw new UsersNotFoundException($"User with User id {id} does not exists");
            }
            return await _repo.PutUser(id, user);
        }
        public async Task<User> DeleteUser(int id)
        {
            if (await _repo.GetUserById(id) == null)
            {
                throw new UsersNotFoundException($"User with User id {id} does not exists");
            }
            return await _repo.DeleteUser(id);
        }
        public async Task<string> Login(string email, string password)
        {
            return await _repo.Login(email, password);
        }
    }
}
