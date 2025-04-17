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
        public Child? GetChildById(int childId)
        {
            return _childRepository.GetChildById(childId);
        }
        
    }
}
