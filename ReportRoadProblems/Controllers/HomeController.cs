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

            if (!ModelState.IsValid)
            {
                ViewData["success"] = 0;

                return View(report);
            }

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
