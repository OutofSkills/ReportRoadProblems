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
        private IMailService _mailService;

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
            return View();
        }
        [HttpPost]
        public IActionResult Report(Report report)
        {
            if (!ModelState.IsValid)
            {
                return View(report);
            }

            try
            {
                _mailService.SendEmail(report);
            }catch(Exception ex)
            {
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
