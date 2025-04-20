using ChildGTS_Group02.DAL.Entities;
using Microsoft.EntityFrameworkCore;
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
            return _context.Children.Include(c => c.Parent).ToList();
        }

        public List<Child> GetAllChildrenByUserId(int userId)
        {
            return _context.Children
                .Include(c => c.Parent)
                .Where(c => c.ParentId == userId) 
                .ToList();
        }

        public List<Child> Search(string childName, int parentId)
        {
            return _context.Children
                .Include(c => c.Parent)
                .Where(c => c.ParentId == parentId && c.FullName.ToLower().Contains(childName.ToLower()))
                .ToList();
        }

        public Child? GetChildById(int childId)
        {
            return _context.Children.FirstOrDefault(c => c.ChildId == childId);
        }

        public async Task<List<Child>> GetAllChildrenAndParentAsync()
        {
            return await _context.Children
                .Include(c => c.Parent)
                .ToListAsync();
        }


        public void Create(Child child)
        {
            _context.Children.Add(child);
            _context.SaveChanges();
        }


        public void Delete(Child child)
        {
            var existingChild = _context.Children
                .Include(c => c.GrowthRecords)
                .FirstOrDefault(c => c.ChildId == child.ChildId);

            if (existingChild == null)
            {
                throw new Exception("Child not found.");
            }
            foreach (var growthRecord in existingChild.GrowthRecords.ToList())
            {
                _context.GrowthRecords.Remove(growthRecord);
            }
            _context.Children.Remove(existingChild);
            _context.SaveChanges();
        }



        public List<Child> GetChildrenByParentId(int parentId)
        {
            return _context.Children
                .Where(c => c.ParentId == parentId)
                .ToList();
        }

        public void Update(Child child)
        {
            _context.Children.Update(child);
            _context.SaveChanges();
        }

        public List<GrowthRecord> GetChildRelatedDataAsync(int childId)
        {
                 return _context.GrowthRecords
                .Where(gr => gr.ChildId == childId)
                .ToList();
        }


    }
}
