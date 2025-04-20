using ChildGTS_Group02.DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ChildGTS_Group02.DAL.Repositories
{
    public class GrowthRecordRepository
    {
        private  ChildGrowthTrackingSystemDBContext _context;

        // Lấy tất cả bản ghi GrowthRecord theo DoctorId (join với Child để lấy các trẻ do bác sĩ phụ trách)
        public List<GrowthRecord> GetAll()
        {
            _context = new ChildGrowthTrackingSystemDBContext();
            return _context.GrowthRecords.ToList();
        }
    }
}
