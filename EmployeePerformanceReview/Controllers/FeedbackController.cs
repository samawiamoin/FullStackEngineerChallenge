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
    [Route("api/feedback")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        IFeedbackRepository feedbackRepository;

        public FeedbackController(IFeedbackRepository _feedbackRepository)
        {
            feedbackRepository = _feedbackRepository;
        }

        [HttpPost]
        [Route("AddFeedback")]
        public async Task<IActionResult> AddFeedback(Feedback model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var feedbackId = await feedbackRepository.AddFeedback(model);
                    if (feedbackId > 0)
                    {
                        return Ok(feedbackId);
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
    }
}