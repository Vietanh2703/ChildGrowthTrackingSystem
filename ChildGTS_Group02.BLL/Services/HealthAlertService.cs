using ChildGTS_Group02.DAL.Entities;
using ChildGTS_Group02.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildGTS_Group02.BLL.Services
{
    public class HealthAlertService
    {
        private HeathAlertRepository _repository = new();
        public List<HealthAlert> GetAllHealthAlerts()
        {
            return _repository.GetAllHealthAlerts();
        }
        public List<HealthAlert> GetAllHealthAlertsByChildId(int childId)
        {
            return _repository.GetAllHealthAlertsByChildId(childId);
        }
        public void Delete(HealthAlert healthAlert)
        {
            _repository.Delete(healthAlert);
        }
    }
}
