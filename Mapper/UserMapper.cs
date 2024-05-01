using Blogs.Dto.User;
using Blogs.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Blogs.Mapper
{
    public static class UserMapper
    {
        public static UserProfileDto ToProfile(this User user)
        {
             return   new UserProfileDto
            {
                UserId=user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone
            };
        }
    }
}
