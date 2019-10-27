using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceReview.Models
{
    public class Feedback : Base
    {
        [ForeignKey("PerformanceReview")]
        public int PRId { get; set; }

        public string Comment { get; set; }

        public string FeedbackBy { get; set; }

        public string FeedbackTo { get; set; }

        public virtual PerformanceReview PerformanceReview { get; set; }
    }
}
