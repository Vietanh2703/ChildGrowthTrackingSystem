using ChildGTS_Group02.DAL.Entities;
using ChildGTS_Group02.DAL;
using System.Collections.Generic;
using ChildGTS_Group02.DAL.Repositories;

namespace ChildGTS_Group02.Services
{
    public class GrowthRecordService
    {
        private GrowthRecordRepository _growthRecordRepository = new();

        // Lấy tất cả bản ghi GrowthRecord trong hệ thống
        public List<GrowthRecord> GetAllGrowthRecords()
        {
            return _growthRecordRepository.GetAll();
        }
    }
}
