using EmployeePerformanceReview.Data;
using EmployeePerformanceReview.Interfaces;
using EmployeePerformanceReview.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceReview.Services
{
    public class FeedbackRepository : IFeedbackRepository
    {
        ApplicationDbContext db;

        public FeedbackRepository(ApplicationDbContext _db)
        {
            db = _db;
        }

        public async Task<int> AddFeedback(Feedback feedback)
        {
            feedback.PerformanceReview.RequireFeedback = true;
            if (db != null)
            {
                await db.Feedbacks.AddAsync(feedback);
                await db.SaveChangesAsync();

                return feedback.Id;
            }

            return 0;
        }
    }
}
