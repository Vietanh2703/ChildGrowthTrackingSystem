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

        private readonly ChildGrowthTrackingSystemDBContext _db = new();

        public List<GrowthRecord> GetRecordsByChildId(int childId)
        {
            return _db.GrowthRecords
                      .Where(gr => gr.ChildId == childId)
                      .ToList();
        }


        public void Create(GrowthRecord newRecord)
        {
            _db.GrowthRecords.Add(newRecord);
            _db.SaveChanges();
        }
    }
}
