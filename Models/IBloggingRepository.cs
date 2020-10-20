using System;
using System.Linq;

namespace Blogs.Models
{
    public interface IBloggingRepository
    {
        IQueryable<Blog> Blogs { get; }
        IQueryable<Post> Posts { get; }

        // TODO: UpdateBlog
        // TODO: UpdatePost
        void AddBlog(Blog blog);
        void DeleteBlog(Blog blog);
        void AddPost(Post post);
        void DeletePost(Post post);
    }
}
