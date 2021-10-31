using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UGOZ_Marcel_Roesink.Models;
using UGOZ_Marcel_Roesink.Utility;

namespace UGOZ_Marcel_Roesink.DatabaseInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext db,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;

        }
        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex)
            {

            }
            if (_db.Roles.Any(r => r.Name == Helper.Admin))
            {
                return;
            }
            else
            {
                _roleManager.CreateAsync(new IdentityRole(Helper.Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(Helper.Doctor)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(Helper.Patient)).GetAwaiter().GetResult();
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "tvb-ugoz@gmail.com",
                    Email = "tvb-ugoz@gmail.com",
                    EmailConfirmed = true,
                    FirstName = "Tineke",
                    MiddleName = "van",
                    LastName = "Breukelen"
                }, "UgOz!2021").GetAwaiter().GetResult();

                ApplicationUser user = _db.Users.FirstOrDefault(u => u.UserName == "tvb-ugoz@gmail.com");
                _userManager.AddToRoleAsync(user, Helper.Admin).GetAwaiter().GetResult();
            }
        }
    }
}
