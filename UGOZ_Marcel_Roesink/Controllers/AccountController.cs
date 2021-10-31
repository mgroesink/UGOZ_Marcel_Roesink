using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UGOZ_Marcel_Roesink.Models;
using UGOZ_Marcel_Roesink.Models.ViewModels;
using UGOZ_Marcel_Roesink.Utility;

namespace UGOZ_Marcel_Roesink.Controllers
{
    public class AccountController : Controller
    {

        #region Fields
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager; 
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        /// <param name="userManager">The user manager.</param>
        /// <param name="roleManager">The role manager.</param>
        /// <param name="signInManager">The sign in manager.</param>
        public AccountController(ApplicationDbContext db,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _db = db;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _userManager = userManager;

        } 
        #endregion

        #region Methods
        /// <summary>
        /// Show login screen to the user.
        /// </summary>
        /// <returns></returns>
        public IActionResult Login()
        {
            return View();
        }



        /// <summary>
        /// Logs a user in.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, true);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(model.Email);
                    HttpContext.Session.SetString("userFullName", user.FullName);
                    return RedirectToAction("Index", "Appointment");
                }
                ModelState.AddModelError("", "Inloggen is mislukt");
            }
            return View();
        }

        /// <summary>
        /// Logs the current user off.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        /// <summary>
        /// Show register screen for new user.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Register()
        {
            return View();
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="model">The user data.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            // Check of model valide is
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    MiddleName = model.MiddleName,
                    LastName = model.LastName
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, model.RoleName);
                    if (!User.IsInRole(Helper.Admin))
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                    }
                    else
                    {
                        TempData["newAdminSignUp"] = user.FullName;
                    }
                    return RedirectToAction("Index", "Appointment");
                }
                // Add all errors to the modelstate
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }


            }
            return View();
        } 
        #endregion

    }
}
