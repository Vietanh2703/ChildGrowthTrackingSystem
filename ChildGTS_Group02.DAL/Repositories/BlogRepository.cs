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
    }
}
