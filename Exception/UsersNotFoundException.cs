namespace Blogs.Exception
{
    public class UsersNotFoundException:ApplicationException
    {
         public UsersNotFoundException() { }

        public UsersNotFoundException(string msg) : base(msg) { }
    }
}
