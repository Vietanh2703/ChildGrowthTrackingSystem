using ChildGTS_Group02.DAL.Entities;
using ChildGTS_Group02.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildGTS_Group02.BLL.Services
{
   public  class GrowRecordService
    {

        private GrowRecordRepository _growRecordRepository;


        public GrowRecordService()
        {
            _growRecordRepository = new GrowRecordRepository();
        }


        public Task<GrowthRecord> AddRecord(GrowthRecord growthRecord) {
           return  _growRecordRepository.CreateGrowthRecordAsync(growthRecord);
        }


    }
}
