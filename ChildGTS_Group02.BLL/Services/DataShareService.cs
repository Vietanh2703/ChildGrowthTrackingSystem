using ChildGTS_Group02.DAL.Entities;
using ChildGTS_Group02.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildGTS_Group02.BLL.Services
{
   public  class DataShareService
    {

        private DataShareReposirory _dataShareRepository;


        public DataShareService()
        {
            _dataShareRepository = new DataShareReposirory();
        }


        public Task<DataShare> AddData(DataShare data)
        {
            return  _dataShareRepository.AddDataShareAsync(data);
        }

    }
}
