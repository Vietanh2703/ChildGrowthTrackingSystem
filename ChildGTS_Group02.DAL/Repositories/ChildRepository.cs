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
            return _context.Children.ToList();
        }
        public Child? GetChildById(int childId)
        {
            return _context.Children.FirstOrDefault(c => c.ChildId == childId);
        }


        // 
        public async Task<List<Child>> GetAllChildrenAndParentAsync()
        {
            return await _context.Children
                .Include(c => c.Parent)
                .ToListAsync();
        }


        public async Task<Child> AddChildAsync(Child child)
        {
            _context.Children.Add(child);
            await _context.SaveChangesAsync();
            return child;
        }


        public async Task<bool> DeleteChildAsync(int childId)
        {
            // Tìm bản ghi trong bảng Children
            var child = await _context.Children.FirstOrDefaultAsync(c => c.ChildId == childId);

            if (child == null)
            {
                // Nếu không tìm thấy bản ghi Child, trả về false
                return false;
            }

            // Xóa các bản ghi phụ thuộc (GrowthRecords, Appointment, v.v.)
            var growthRecords = await _context.GrowthRecords.Where(gr => gr.ChildId == childId).ToListAsync();
            foreach (var record in growthRecords)
            {
                _context.GrowthRecords.Remove(record);
            }

           
            // Xóa bản ghi chính (Child)
            _context.Children.Remove(child);

            // Lưu thay đổi vào cơ sở dữ liệu
            await _context.SaveChangesAsync();

            return true;
        }


        public async Task<Child> GetChilById(int id)
        {
            return await  _context.Children.FirstOrDefaultAsync(c => c.ChildId == id);
        }

        public async Task<Child> UpdateChildAsync(Child updatedChild)
        {
            // Tìm đối tượng Child trong cơ sở dữ liệu theo ChildId
            var child = await this.GetChilById(updatedChild.ChildId);

            if (child == null)
            {
             
                throw new Exception("Child not found.");
            }

            // Cập nhật các trường của Child
            child.FullName = updatedChild.FullName;
            child.DateOfBirth = updatedChild.DateOfBirth;
            child.Gender = updatedChild.Gender;
            child.BirthWeight = updatedChild.BirthWeight;
            child.BirthHeight = updatedChild.BirthHeight;
            child.ProfileImage = updatedChild.ProfileImage;
            child.CreatedDate = updatedChild.CreatedDate;
            child.LastCheckupDate = updatedChild.LastCheckupDate;
            child.MedicalNotes = updatedChild.MedicalNotes;
            child.ParentId = updatedChild.ParentId; 

            


            await _context.SaveChangesAsync();

            return child;
        }

        public async Task<List<GrowthRecord>> GetChildRelatedDataAsync(int childId)
        {
            var growthRecords = await _context.GrowthRecords
                .Where(gr => gr.ChildId == childId)
                .ToListAsync();

            return growthRecords;
        }


    }
}
