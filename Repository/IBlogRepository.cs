namespace Blogs.Repository
{
    public interface IBlogRepository
    {
        Task<List<Blogs.Models.Blog>> Search(string? filterQuery = null);
        Task<List<Blogs.Models.Blog>> GetBlogs();
        Task<Blogs.Models.Blog> GetBlogById(int id);
        Task<Blogs.Models.Blog> PutBlog(int id, Blogs.Models.Blog blog);
        Task<Blogs.Models.Blog> PostBlog(Blogs.Models.Blog blog);
        Task<Blogs.Models.Blog> DeleteBlog(int id);
        Task<List<Blogs.Models.Blog>> getMypost(int id);
    }
}
