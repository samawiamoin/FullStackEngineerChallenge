using EmployeePerformanceReview.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceReview.Interfaces
{
    public interface IPerformanceReviewRepository
    {
        Task<List<PerformanceReview>> GetPerformanceReviews();

        Task<PerformanceReview> GetPerformanceReview(int? prId);

        Task<int> AddPerformanceReview(PerformanceReview pr);

        Task UpdatePerformanceReview(PerformanceReview pr);

        Task<List<PerformanceReview>> GetPRsRequireFeedback(int selfEmployeeId);
    }
}
