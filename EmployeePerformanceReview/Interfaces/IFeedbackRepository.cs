using EmployeePerformanceReview.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceReview.Interfaces
{
    public interface IFeedbackRepository
    {
        Task<int> AddFeedback(Feedback feedback);
    }
}
