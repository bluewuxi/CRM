using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CRM.Domain.Abstract;
using CRM.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace CRM.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //HttpContext.Session.SetString("AccountList", "");
            //string a = HttpContext.Session.GetString("AccountList");
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public string test()
        {
            return "Hello World!";
        }
    }
}
