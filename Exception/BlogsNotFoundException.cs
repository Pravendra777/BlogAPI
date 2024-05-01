namespace Blogs.Exception
{
    public class BlogsNotFoundException : ApplicationException
    {
        public BlogsNotFoundException() { }

        public BlogsNotFoundException(string msg) : base(msg) { }
    }
}
