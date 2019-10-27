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
    [Route("api/authenticate")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        IEmployeeRepository employeeRepository;

        public AuthenticateController(IEmployeeRepository _employeeRepository)
        {
            employeeRepository = _employeeRepository;

        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> GetLogin(AuthenticateUser authUser)
        {
            if (authUser == null)
            {
                return BadRequest();
            }

            // Check if Master Admin account exists
            // If not then create then validate user creds
            var authenticatedEmployee = await employeeRepository.GetEmployeeForLogin(authUser);
          
           
            if (authenticatedEmployee == null)
            {
                return NotFound();
            }

            return Ok(authenticatedEmployee);

        }
    }
}