﻿using Microsoft.AspNetCore.Mvc;
using MSIT155Site.Models;
using System.Diagnostics;

namespace MSIT155Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult JsonTest()
        {
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
        public IActionResult City()
        {
            return View();
        }
        public IActionResult Avator()
        {
            return View();
        }
        public IActionResult Spots()
        {
            return View();
        }
        public IActionResult spotsTitle()
        {
            return View();
        }

        public IActionResult Cors()
        {
            //Server端呼叫使用HttpClient套件
            return View();
        }
    }
}
