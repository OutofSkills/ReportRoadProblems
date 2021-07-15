using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReportRoadProblems.Models;
using ReportRoadProblems.Services;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace ReportRoadProblems.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMailService _mailService;

        public HomeController(ILogger<HomeController> logger, IMailService mailService)
        {
            _logger = logger;
            _mailService = mailService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Report()
        {
            ViewData["success"] = 0;

            return View();
        }

        [HttpPost]
        public IActionResult Report(Report report)
        {
            //set a flag for success message
            ViewData["success"] = 1;
            //If the model is not valid return back
            if (!ModelState.IsValid)
            {
                ViewData["success"] = 0;

                return View(report);
            }
            // If the model is valid send the email
            try
            {
                _mailService.SendEmail(report);
            }catch(Exception ex)
            {
                ViewData["success"] = 0;
                _logger.LogDebug(ex.Message);
            }

            return View();
        }

        [HttpGet]
        [Route("/Home/ProblemLocation/{latitude}/{longitude}")]
        public IActionResult ProblemLocation(string latitude, string longitude)
        {
            ViewBag.ErrorStatus = "";
            if (String.IsNullOrEmpty(latitude) || String.IsNullOrEmpty(longitude))
                ViewBag.ErrorStatus = "Wrong coordinates";

            if (!Double.TryParse(latitude, out var latit) || !Double.TryParse(longitude, out var longit))
                ViewBag.ErrorStatus = "Wrong coordinates";

            ViewBag.latitude = latitude;
            ViewBag.longitude = longitude;

            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
