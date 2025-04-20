using ChildGTS_Group02.DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ChildGTS_Group02.DAL.Repositories
{
    public class FeedbackRatingRepository
    {
        private ChildGrowthTrackingSystemDBContext _context;

        public FeedbackRatingRepository()
        {
            _context = new();
        }

        public void AddRating(FeedbackRating rating)
        {
            _context.FeedbackRatings.Add(rating);
            _context.SaveChanges();
        }

        public List<FeedbackRating> GetRatingsByFeedbackId(int feedbackId)
        {
            return _context.FeedbackRatings.Where(r => r.FeedbackId == feedbackId).ToList();
        }

        public FeedbackRating GetUserRatingForFeedback(int feedbackId, int userId)
        {
            return _context.FeedbackRatings.FirstOrDefault(r => r.FeedbackId == feedbackId && r.UserId == userId);
        }
    }
}
