using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeePerformanceReview.Interfaces;
using EmployeePerformanceReview.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeePerformanceReview.Controllers
{
    [Route("api/performancereview")]
    [ApiController]
    public class PerformanceReviewController : ControllerBase
    {
        IPerformanceReviewRepository prRepository;

        public PerformanceReviewController(IPerformanceReviewRepository _prRepository)
        {
            prRepository = _prRepository;
        }

        [HttpGet]
        [Route("GetPerformanceReviews")]
        public async Task<IActionResult> GetPerformanceReviews()
        {
            try
            {
                var prs = await prRepository.GetPerformanceReviews();
                if (prs == null)
                {
                    return NotFound();
                }

                return Ok(prs);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpGet]
        [Route("GetPerformanceReview/{prId}")]
        public async Task<IActionResult> GetPerformanceReview(int? prId)
        {
            if (prId == null)
            {
                return BadRequest();
            }

            try
            {
                var pr = await prRepository.GetPerformanceReview(prId);

                if (pr == null)
                {
                    return NotFound();
                }

                return Ok(pr);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetPRsRequireFeedback/{selfEmployeeId}")]
        public async Task<IActionResult> GetPRsRequireFeedback(string selfEmployeeId)
        {
            try
            {
                var pr = await prRepository.GetPRsRequireFeedback(Convert.ToInt32(selfEmployeeId));

                if (pr == null)
                {
                    return NotFound();
                }

                return Ok(pr);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("AddPerformanceReview")]
        public async Task<IActionResult> AddPerformanceReview(PerformanceReview model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var prId = await prRepository.AddPerformanceReview(model);
                    if (prId > 0)
                    {
                        return Ok(prId);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {

                    return BadRequest();
                }

            }

            return BadRequest();
        }

        [HttpPut]
        [Route("UpdatePerformanceReview")]
        public async Task<IActionResult> UpdatePost(PerformanceReview model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await prRepository.UpdatePerformanceReview(model);

                    return Ok();
                }
                catch (Exception ex)
                {
                    if (ex.GetType().FullName ==
                             "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                    {
                        return NotFound();
                    }

                    return BadRequest();
                }
            }

            return BadRequest();
        }
    }
}