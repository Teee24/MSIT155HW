﻿using Microsoft.AspNetCore.Mvc;

namespace MSIT155Site.Controllers
{
    public class HomeworkController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Travels()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
       
        public IActionResult RegisterAccount()
        {
            return View();
        } 
        public IActionResult TaipeiSpots()
        {
            return View();
        }
    }

}
