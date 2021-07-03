using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReportRoadProblems.Models
{
    public class Report
    {
        public int ID { get; set; }
        [Required]
        public IFormFile Picture { get; set; }
        [Required]
        public string ProblemSeverity { get; set; }
        [Required]
        public string Latitude { get; set; }
        [Required]
        public string Longitude { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
