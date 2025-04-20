using ChildGTS_Group02.DAL.Entities;
using ChildGTS_Group02.DAL.Repositories;
using System.Collections.Generic;

namespace ChildGTS_Group02.BLL.Services
{
    public class FeedbackRatingService
    {
        private FeedbackRatingRepository _repository = new();

        public void AddRating(FeedbackRating rating)
        {
            _repository.AddRating(rating);
        }

        public List<FeedbackRating> GetRatingsByFeedbackId(int feedbackId)
        {
            return _repository.GetRatingsByFeedbackId(feedbackId);
        }

        public FeedbackRating GetUserRatingForFeedback(int feedbackId, int userId)
        {
            return _repository.GetUserRatingForFeedback(feedbackId, userId);
        }
    }
}
