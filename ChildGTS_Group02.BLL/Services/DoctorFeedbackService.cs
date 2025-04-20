using ChildGTS_Group02.DAL.Entities;
using ChildGTS_Group02.DAL.Repositories;
using System.Collections.Generic;

namespace ChildGTS_Group02.BLL.Services
{
    public class DoctorFeedbackService
    {
        private DoctorFeedbackRepository _repository = new();

        public void AddFeedback(DoctorFeedback feedback)
        {
            _repository.AddFeedback(feedback);
        }

        public List<DoctorFeedback> GetFeedbacksByShareId(int shareId)
        {
            return _repository.GetFeedbacksByRecordId(shareId);
        }

        // Bổ sung: Lấy tất cả feedback (cho MemberWindow)
        public List<DoctorFeedback> GetAllFeedbacks()
        {
            return _repository.GetAllFeedbacks();
        }
    }
}
