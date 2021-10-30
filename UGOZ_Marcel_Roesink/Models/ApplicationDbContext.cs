using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UGOZ_Marcel_Roesink.Models
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {

        #region Properties
        /// <summary>
        /// Gets or sets the appointments.
        /// </summary>
        /// <value>
        /// The appointments.
        /// </value>
        public DbSet<Appointment> Appointments { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        } 
        #endregion

    }
}
