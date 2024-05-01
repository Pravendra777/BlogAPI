using Blogs.Exception;
using Blogs.Repository;

namespace Blogs.Services
{
    public class BlogsService : IBlogsService
    {
        private readonly IBlogRepository _repo;
        public BlogsService(IBlogRepository repo)
        {
            _repo = repo;
        }
        public async Task<List<Blogs.Models.Blog>> GetBlogs()
        {
            return await _repo.GetBlogs();
        }
        public async Task<Blogs.Models.Blog> GetBlogById(int id)
        {
            if (await _repo.GetBlogById(id) == null)
            {
                throw new BlogsNotFoundException($"Blog with Blog id {id} does not exists");
            }
            return await _repo.GetBlogById(id);
        }
        public async Task<Blogs.Models.Blog> PutBlog(int id, Blogs.Models.Blog blog)
        {
            if (await _repo.GetBlogById(id) == null)
            {
                throw new BlogsNotFoundException($"Blog with Blog id {id} does not exists");
            }
            return await _repo.PutBlog(id, blog);
        }
        public async Task<Blogs.Models.Blog> PostBlog(Blogs.Models.Blog blog)
        {
            return await _repo.PostBlog(blog);
        }
        public async Task<Blogs.Models.Blog> DeleteBlog(int id)
        {
            if (await _repo.GetBlogById(id) == null)
            {
                throw new BlogsNotFoundException($"Blog with Blog id {id} does not exists");
            }
            return await _repo.DeleteBlog(id);
        }
        public async Task<List<Blogs.Models.Blog>> getMypost(int id)
        {
            return await _repo.getMypost(id);
        }

        public async Task<List<Blogs.Models.Blog>> Search(string? filterQuery = null)
        {
            return await _repo.Search(filterQuery);
        }
    }
}

