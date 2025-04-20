using ChildGTS_Group02.DAL.Entities;
using ChildGTS_Group02.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildGTS_Group02.BLL.Services
{
    public class ChildService
    {
        private ChildRepository _childRepository = new();
        public List<Child> GetAllChildren()
        {
            return _childRepository.GetAllChildren();
        }

        public List<Child> GetAllChildrenByUserId(int userId)
        {
            return _childRepository.GetAllChildrenByUserId(userId);
        }

        public List<Child> Search(string childName, int parentId)
        {
            return _childRepository.Search(childName, parentId);
        }

        public Child? GetChildById(int childId)
        {
            return _childRepository.GetChildById(childId);
        }

        public async Task<List<Child>> GetAllChildrenAndParent()
        {
            return await _childRepository.GetAllChildrenAndParentAsync();
        }

        public void Create(Child child)
        {
           _childRepository.Create(child);
        }

        public void Delete(Child child)
        {
            _childRepository.Delete(child);
        }

        public void Update(Child child)
        {
            _childRepository.Update(child);
        }



        public List<GrowthRecord> GetChildRelatedData(int childId)
        {
            return _childRepository.GetChildRelatedDataAsync(childId);


        }

    }
}
