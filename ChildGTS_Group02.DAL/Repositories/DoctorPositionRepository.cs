using ChildGTS_Group02.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildGTS_Group02.DAL.Repositories
{
    public class DoctorPositionRepository
    {
        private ChildGrowthTrackingSystemDBContext? _context;
        public List<DoctorPosition> GetAllDoctorPositions()
        {
            _context = new ChildGrowthTrackingSystemDBContext();
            return _context.DoctorPositions.ToList();
        }
        public DoctorPosition? GetDoctorPositionById(int doctorPositionId)
        {
            _context = new ChildGrowthTrackingSystemDBContext();
            return _context.DoctorPositions
                .FirstOrDefault(dp => dp.PositionId == doctorPositionId);
        }
        public bool AddDoctorPosition(DoctorPosition doctorPosition)
        {
            _context = new ChildGrowthTrackingSystemDBContext();
            _context.DoctorPositions.Add(doctorPosition);
            return _context.SaveChanges() > 0;
        }
    }
}
