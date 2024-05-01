using Blogs.Data;
using Microsoft.EntityFrameworkCore;

namespace Blogs.Repository
{
    public class BlogsRepository : IBlogRepository
    {
        private readonly BlogContext _context;
        public BlogsRepository(BlogContext context)
        {
            _context = context;
        }
        public async Task<List<Blogs.Models.Blog>> GetBlogs()
        {
            return await _context.Blogs.OrderByDescending(t=>t.CreatedDate).ToListAsync();
        }
        public async Task<Blogs.Models.Blog> GetBlogById(int id)
        {
            return await _context.Blogs.FindAsync(id);
        }
        public async Task<Blogs.Models.Blog> PutBlog(int id, Blogs.Models.Blog blog)
        {
            var result = await _context.Blogs.FindAsync(id);
            result.Title = blog.Title;
            result.Details = blog.Details;
            result.CreatedDate = blog.CreatedDate;
            await _context.SaveChangesAsync();
            return result;
        }
        public async Task<Blogs.Models.Blog> PostBlog(Blogs.Models.Blog blog)
        {
            await _context.Blogs.AddAsync(blog);
            await _context.SaveChangesAsync();
            return blog;
        }
        public async Task<Blogs.Models.Blog> DeleteBlog(int id)
        {
            var blog = await _context.Blogs.FirstOrDefaultAsync(x => x.Idblog == id);
            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
            return blog;
        }
        public async Task<List<Blogs.Models.Blog>> getMypost(int id)
        {
            var myblogs = await _context.Blogs.Where(t => t.UserId == id).OrderByDescending(t => t.CreatedDate).ToListAsync();
            return myblogs;
        }

        public async Task<List<Models.Blog>> Search(string? filterQuery = null)
        {
            var result = _context.Blogs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filterQuery))
            {
                // Filter by Title
                var TitleFilter = result.Where(x => x.Title.Contains(filterQuery));

                // Filter by Detail
                //var DetailsFilter = result.Where(x => x.Details.Contains(filterQuery));
                // Filter by Id
                if (int.TryParse(filterQuery, out int IdFilter))
                {
                    result = result.Where(x => x.UserId == IdFilter);
                    return await result.ToListAsync();
                }
                // Combine all filters
                //result = TitleFilter.Union(DetailsFilter);
                result = TitleFilter;
            }

            return await result.ToListAsync();
        }
    }
}
