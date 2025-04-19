using ChildGTS_Group02.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildGTS_Group02.DAL.Repositories
{
   public  class GrowRecordRepository
    {

        private readonly ChildGrowthTrackingSystemDBContext _db;



        public GrowRecordRepository()
        {

            _db = new ChildGrowthTrackingSystemDBContext();

        }


        public List<GrowthRecord> GetRecordsByChildId(int childId)
        {
            return _db.GrowthRecords
                      .Where(gr => gr.ChildId == childId)
                      .ToList();
        }


        public async Task<GrowthRecord> CreateGrowthRecordAsync(GrowthRecord newRecord)
        {
            _db.GrowthRecords.Add(newRecord);
            await _db.SaveChangesAsync();
            return newRecord;
        }


        

    }
}
