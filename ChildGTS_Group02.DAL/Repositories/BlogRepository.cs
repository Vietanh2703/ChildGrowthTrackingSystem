using ChildGTS_Group02.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildGTS_Group02.DAL.Repositories
{
    public class BlogRepository
    {
        private ChildGrowthTrackingSystemDBContext? _context = new();

        public List<Blog> GetAllBlogs()
        {
            _context = new ChildGrowthTrackingSystemDBContext();
            return _context.Blogs.Include(b => b.Author).ToList();
        }
        public List<Blog> GetAllBlogsByUserId(int userId)
        {
            _context = new ChildGrowthTrackingSystemDBContext();
            return _context.Blogs
                .Include(b => b.Author)
                .Where(b => b.AuthorId == userId)
                .ToList();
        }
        public void Delete(Blog blog)
        {
            _context = new ChildGrowthTrackingSystemDBContext();
            var existingBlog = _context.Blogs
                .Include(b => b.Author)
                .FirstOrDefault(b => b.BlogId == blog.BlogId);
            if (existingBlog != null)
            {
                _context.Blogs.Remove(existingBlog);
                _context.SaveChanges();
            }
        }
    }
}
