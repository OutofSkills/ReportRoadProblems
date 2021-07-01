using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportRoadProblems.Models
{
    public class Report
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public IFormFile Picture { get; set; }
        public string ProblemSeverity { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string? Address { get; set; }
    }
}
