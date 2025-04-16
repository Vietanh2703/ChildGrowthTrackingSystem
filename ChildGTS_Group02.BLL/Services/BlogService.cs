using ChildGTS_Group02.DAL.Entities;
using ChildGTS_Group02.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildGTS_Group02.BLL.Services
{
    public class BlogService
    {
        private BlogRepository _blogRepository = new();

        public List<Blog>? GetAllBlogs()
        {
            return _blogRepository.GetAllBlogs();
        }
    }
}
