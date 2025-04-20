using ChildGTS_Group02.DAL.Entities;
using ChildGTS_Group02.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildGTS_Group02.BLL.Services
{
   public class GrowRecordService
    {

        private GrowRecordRepository _growRecordRepository = new();

        public List<GrowthRecord> GetAllGrowthRecords()
        {
            return _growRecordRepository.GetAll();
        }
        public List<GrowthRecord> GetGrowthRecordsByChildId(int childId)
        {
            return _growRecordRepository.GetRecordsByChildId(childId);
        }
        public GrowthRecord? GetGrowthRecordById(int? recordId)
        {
            return _growRecordRepository.GetRecordById(recordId);
        }
        public void AddRecord(GrowthRecord growthRecord) {
            _growRecordRepository.Create(growthRecord);
        }
        public void UpdateRecord(GrowthRecord growthRecord)
        {
            _growRecordRepository.Update(growthRecord);
        }
        public void Delete(GrowthRecord growthRecord)
        {
            _growRecordRepository.Delete(growthRecord);
        }


    }
}
