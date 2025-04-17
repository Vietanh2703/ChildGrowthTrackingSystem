using ChildGTS_Group02.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildGTS_Group02.DAL.Repositories
{
    public class ChildRepository
    {
        private ChildGrowthTrackingSystemDBContext _context = new();
        public List<Child> GetAllChildren()
        {
            return _context.Children.ToList();
        }
        public Child? GetChildById(int childId)
        {
            return _context.Children.FirstOrDefault(c => c.ChildId == childId);
        }
    }
}
