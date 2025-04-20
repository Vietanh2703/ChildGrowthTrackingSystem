using ChildGTS_Group02.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildGTS_Group02.DAL.Repositories
{
    public class HeathAlertRepository
    {
        private ChildGrowthTrackingSystemDBContext _context = new();

        public List<HealthAlert> GetAllHealthAlerts()
        {
            return _context.HealthAlerts.ToList();
        }
        public List<HealthAlert> GetAllHealthAlertsByChildId(int childId)
        {
            return _context.HealthAlerts
                .Where(ha => ha.ChildId == childId)
                .ToList();
        }
        public void Delete(HealthAlert healthAlert)
        {
            var existingHealthAlert = _context.HealthAlerts
                .FirstOrDefault(ha => ha.AlertId == healthAlert.AlertId);
            if (existingHealthAlert != null)
            {
                _context.HealthAlerts.Remove(existingHealthAlert);
                _context.SaveChanges();
            }
        }
    }
}
