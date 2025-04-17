using ChildGTS_Group02.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildGTS_Group02.DAL.Repositories
{
    public class MembershipPackageRepository
    {
        private ChildGrowthTrackingSystemDBContext _context = new();

        public List<MembershipPackage> GetAllMembershipPackages()
        {
            return _context.MembershipPackages.ToList();
        }

        public MembershipPackage? GetMembershipPackageById(int packageId)
        {
            return _context.MembershipPackages.FirstOrDefault(p => p.PackageId == packageId);
        }
    }
}
