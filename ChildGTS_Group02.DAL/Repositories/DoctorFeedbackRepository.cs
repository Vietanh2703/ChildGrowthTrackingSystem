using ChildGTS_Group02.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChildGTS_Group02.DAL.Repositories
{
    public class DoctorFeedbackRepository
    {
        private ChildGrowthTrackingSystemDBContext _context;

        public DoctorFeedbackRepository()
        {
            _context = new();
        }

        public void AddFeedback(DoctorFeedback feedback)
        {
            feedback.FeedbackDate = DateTime.Now;
            _context.DoctorFeedbacks.Add(feedback);
            _context.SaveChanges();
        }

        public List<DoctorFeedback> GetFeedbacksByRecordId(int recordId)
        {
            return _context.DoctorFeedbacks.Where(f => f.ShareId == recordId).ToList();
        }

        // Bổ sung: Lấy tất cả feedback (cho MemberWindow)
        public List<DoctorFeedback> GetAllFeedbacks()
        {
            return _context.DoctorFeedbacks.ToList();
        }
    }
}
