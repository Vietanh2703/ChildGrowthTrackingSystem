using ChildGTS_Group02.DAL.Entities;
using ChildGTS_Group02.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildGTS_Group02.BLL.Services
{
    public class DoctorPositionService
    {
        private DoctorPositionRepository _doctorPositionRepository = new();
        public List<DoctorPosition> GetAllDoctorPositions()
        {
            return _doctorPositionRepository.GetAllDoctorPositions();
        }
        public DoctorPosition? GetDoctorPositionById(int doctorPositionId)
        {
            return _doctorPositionRepository.GetDoctorPositionById(doctorPositionId);
        }
        public bool AddDoctorPosition(DoctorPosition doctorPosition)
        {
            return _doctorPositionRepository.AddDoctorPosition(doctorPosition);
        }
    }
}
