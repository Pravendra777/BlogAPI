using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Blogs.Data;
using Blogs.Models;
using Microsoft.AspNetCore.Authorization;
using Blogs.Services;
using Blogs.Dto.Blog;

namespace Blogs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class BlogsController : ControllerBase
    {
        private readonly BlogContext _context;
        private readonly IBlogsService _serv;
        public BlogsController(BlogContext context, IBlogsService serv)
        {
            _context = context;
            _serv = serv;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Blogs.Models.Blog>>> GetBlogs()
        {
            return await _serv.GetBlogs();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Blogs.Models.Blog>> GetBlog(int id)
        {
            return await _serv.GetBlogById(id);
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutBlog(int id, Blogs.Models.Blog blog)
        {
            if (id != blog.Idblog)
            {
                return BadRequest();
            }
            await _serv.PutBlog(id, blog);
            return NoContent();
        }
        [HttpGet("/bytitle")]
        public async Task<IActionResult> Search([FromQuery] string? FilterQuery = null)
        {
            //var result = await _context.Products.ToListAsync();
            if(FilterQuery == null)
            {
                return NoContent();
            }
            var result = await _serv.Search(FilterQuery);
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<Blogs.Models.Blog>> PostBlog(PostBlog blog)
        {
            Models.Blog b=new Models.Blog();
            b.UserId = GetidFromToken();
            b.CreatedDate = DateTime.Now;
            b.Details=blog.Details; 
            b.Title = blog.Title;
            var result = await _serv.PostBlog(b);
            return CreatedAtAction("GetBlog", new { id = result.Idblog }, result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            return Ok(await _serv.DeleteBlog(id));
        }
        [HttpGet]
        [Route("getmypost")]
        public async Task<IActionResult> getMypost()
        {
            int myid = GetidFromToken();
            return Ok(await _serv.getMypost(myid));
        }
        private bool BlogExists(int id)
        {
            return _context.Blogs.Any(e => e.Idblog == id);
        }
        private int GetidFromToken()
        {
            var id = HttpContext.User.FindFirst("UserId").Value;
            return int.Parse(id);
        }
    }
    /*  public class BlogsController : ControllerBase
      {
          private readonly BlogContext _context;

          public BlogsController(BlogContext context)
          {
              _context = context;
          }

          // GET: api/Blogs
          [HttpGet]

          public async Task<ActionResult<IEnumerable<Blogs.Models.Blog>>> GetBlogs()
          {
              return await _context.Blogs.OrderByDescending(t=>t.CreatedDate).ToListAsync();
          }

          // GET: api/Blogs/5
          [HttpGet("{id}")]
          public async Task<ActionResult<Blogs.Models.Blog>> GetBlog(int id)
          {
              var blog = await _context.Blogs.FindAsync(id);

              if (blog == null)
              {
                  return NotFound();
              }

              return blog;
          }

          // PUT: api/Blogs/5
          // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
          [HttpPut("{id}")]
          [Authorize]
          public async Task<IActionResult> PutBlog(int id, Blogs.Models.Blog blog)
          {
              var blogre = new Blogs.Models.Blog();
              blogre.CreatedDate = blog.CreatedDate;
              blogre.UserId= GetidFromToken();
              blogre.Title = blog.Title;
              blogre.Details = blog.Details;
              blogre.Idblog = blog.Idblog;

              *//*var user = 
              blog.UserId = user;*//*

              if (id != blog.Idblog)
              {
                  return BadRequest();
              }

              _context.Entry(blogre).State = EntityState.Modified;

              try
              {
                  await _context.SaveChangesAsync();
              }
              catch (DbUpdateConcurrencyException)
              {
                  if (!BlogExists(id))
                  {
                      return NotFound();
                  }
                  else
                  {
                      throw;
                  }
              }

              return NoContent();
          }

          // POST: api/Blogs
          // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
          [HttpPost]
          public async Task<ActionResult<Blogs.Models.Blog>> PostBlog(Blogs.Models.Blog blog)
          {
             // var blogs

             // DateTime d = new DateTime();
              blog.UserId = GetidFromToken();
              blog.CreatedDate= DateTime.Now;
              _context.Blogs.Add(blog);
              await _context.SaveChangesAsync();

              return CreatedAtAction("GetBlog", new { id = blog.Idblog }, blog);
          }

          // DELETE: api/Blogs/5
          [HttpDelete("{id}")]
          public async Task<IActionResult> DeleteBlog(int id)
          {
              var blog = await _context.Blogs.FindAsync(id);
              if (blog == null)
              {
                  return NotFound();
              }

              _context.Blogs.Remove(blog);
              await _context.SaveChangesAsync();

              return NoContent();
          }
          [HttpGet]
          [Route("getmypost")]
          [Authorize(Roles ="User")]
          public async Task<IActionResult> getMypost()
          {
              int myid = GetidFromToken();
              var myblogs=  _context.Blogs.Where(t=>t.UserId==myid).OrderByDescending(t => t.CreatedDate).ToList();
              return Ok(myblogs);
          }

          private bool BlogExists(int id)
          {
              return _context.Blogs.Any(e => e.Idblog == id);
          }
          private int GetidFromToken()
          {
              var id = HttpContext.User.FindFirst("UserId").Value;
              return int.Parse(id);
          }
      }*/
}
