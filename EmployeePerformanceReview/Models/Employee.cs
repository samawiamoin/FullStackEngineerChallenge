using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceReview.Models
{
    public class Employee : Base
    {

        [ForeignKey("AuthenticateUser")]
        public int AuthUserId { get; set; }

        public string FullName { get; set; }

        public string JobTitle { get; set; }

        //Can participate in the performance review of these employees
        public PROfEmployees PROfEmployees { get; set; }

        public virtual AuthenticateUser AuthenticateUser { get; set;}
    }
}
