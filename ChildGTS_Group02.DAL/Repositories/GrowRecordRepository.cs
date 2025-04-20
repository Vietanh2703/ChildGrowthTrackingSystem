using ChildGTS_Group02.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildGTS_Group02.DAL.Repositories
{
   public class GrowRecordRepository
    {

        private ChildGrowthTrackingSystemDBContext _db = new();
        public List<GrowthRecord> GetAll()
        {
            return _db.GrowthRecords.Include(gr => gr.Child).ToList();
        }
        public List<GrowthRecord> GetRecordsByChildId(int childId)
        {
            return _db.GrowthRecords
                      .Where(gr => gr.ChildId == childId)
                      .ToList();
        }
        public GrowthRecord? GetRecordById(int? recordId)
        {
            return _db.GrowthRecords
                      .Include(gr => gr.Child)
                      .FirstOrDefault(gr => gr.RecordId == recordId);
        }
        public void Create(GrowthRecord newRecord)
        {
            _db.GrowthRecords.Add(newRecord);
            _db.SaveChanges();
        }
        public void Update(GrowthRecord updatedRecord)
        {
            _db.GrowthRecords.Update(updatedRecord);
            _db.SaveChanges();
        }
        public void Delete(GrowthRecord record)
        {
            var existingRecord = _db.GrowthRecords
                .FirstOrDefault(gr => gr.RecordId == record.RecordId);
            if (existingRecord != null)
            {
                _db.GrowthRecords.Remove(existingRecord);
                _db.SaveChanges();
            }
        }
    }
}
