namespace Blogs.Exception
{
    public class EmailExist:ApplicationException
    {
        public EmailExist() { }
        public EmailExist(string message) : base(message) { }
    }
}
