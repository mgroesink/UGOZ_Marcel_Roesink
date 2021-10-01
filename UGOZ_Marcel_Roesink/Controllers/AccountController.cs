using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UGOZ_Marcel_Roesink.Models;
using UGOZ_Marcel_Roesink.Utility;

namespace UGOZ_Marcel_Roesink.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AccountController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Login()
        {
            double pi = Math.PI;
            return View();
        }

        [HttpPost]
        public IActionResult Login(string Email , string Password)
        {

            return View();
        }
    }
}
