using ChildGTS_Group02.DAL.Entities;
using ChildGTS_Group02.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildGTS_Group02.BLL.Services
{
    public class MembershipPackageService
    {
        private readonly MembershipPackageRepository _membershipPackageRepository = new();
        public List<MembershipPackage> GetAllMembershipPackages()
        {
            return _membershipPackageRepository.GetAllMembershipPackages();
        }

        public MembershipPackage? GetPackageById(int packageId)
        {
            return _membershipPackageRepository.GetMembershipPackageById(packageId);
        }
    }
}
