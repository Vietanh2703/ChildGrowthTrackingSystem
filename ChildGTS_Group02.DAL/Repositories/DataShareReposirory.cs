using ChildGTS_Group02.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildGTS_Group02.DAL.Repositories
{
   public class DataShareReposirory
    {

        private readonly ChildGrowthTrackingSystemDBContext _db;



        public DataShareReposirory()
        {
            _db = new ChildGrowthTrackingSystemDBContext();
        }


        public async Task<DataShare> AddDataShareAsync(DataShare dataShare)
        {
            _db.DataShares.Add(dataShare);
            await _db.SaveChangesAsync();
            return dataShare;
        }


        public async Task TestAdd()
        {
            var db = new ChildGrowthTrackingSystemDBContext();
            var ds = new DataShare
            {
                ChildId = 1,
                DoctorId = 2,
                ShareDate = DateTime.Now,
                ExpiryDate = DateTime.Now.AddYears(1),
                AccessLevel = "Read",
                ShareStatus = "Active"
            };
            db.DataShares.Add(ds);
            var result = await db.SaveChangesAsync();
            Console.WriteLine($"Saved {result} rows to: " + db.Database.GetDbConnection().ConnectionString);
        }

    }
}
