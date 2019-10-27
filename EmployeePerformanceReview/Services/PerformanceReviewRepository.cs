using EmployeePerformanceReview.Data;
using EmployeePerformanceReview.Interfaces;
using EmployeePerformanceReview.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceReview.Services
{
    public class PerformanceReviewRepository : IPerformanceReviewRepository
    {
        ApplicationDbContext db;

        public PerformanceReviewRepository(ApplicationDbContext _db)
        {
            db = _db;
        }

        public async Task<int> AddPerformanceReview(PerformanceReview pr)
        {
            if(pr != null)
            {
                pr.CreatedAt = DateTime.Now;
                pr.ModifiedAt = DateTime.Now;
                pr.IsAssigned = false;
                pr.RequireFeedback = false;
            }            
            if (db != null)
            {
                await db.PerformanceReviews.AddAsync(pr);
                await db.SaveChangesAsync();

                return pr.Id;
            }

            return 0;
        }

        public async Task<PerformanceReview> GetPerformanceReview(int? prId)
        {
            if (db != null)
            {
                return await db.PerformanceReviews.Where(x => x.Id == prId).FirstOrDefaultAsync();
            }

            return null;
        }

        public async Task<List<PerformanceReview>> GetPerformanceReviews()
        {
            if (db != null)
            {
                return await db.PerformanceReviews.ToListAsync();
            }

            return null;
        }

        public async Task UpdatePerformanceReview(PerformanceReview pr)
        {
            if (db != null)
            {
                //Delete that pr
                db.PerformanceReviews.Update(pr);

                //Commit the transaction
                await db.SaveChangesAsync();
            }
        }

        public async Task<List<PerformanceReview>> GetPRsRequireFeedback(int selfEmployeeId)
        {
            if (db != null)
            {
                return await db.PerformanceReviews.Where(x => x.RequireFeedback == false && x.EmployeeId != selfEmployeeId).ToListAsync();
            }

            return null;
        }
    }
}
