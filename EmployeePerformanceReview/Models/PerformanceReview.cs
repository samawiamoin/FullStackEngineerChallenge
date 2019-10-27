using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceReview.Models
{
    public class PerformanceReview : Base
    {
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        public string Communication { get; set; }

        public string Technical { get; set; }

        public string TimeManagement { get; set; }

        public string Leadership { get; set; }

        public string Ownership { get; set; }

        public string OverallPerformance { get; set; }

        public bool IsCompleted { get; set; }

        public bool IsAssigned { get; set; }

        public bool RequireFeedback { get; set; }
    }
}
