using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApi.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the ProjectCars2User class
    public class ProjectCars2User : IdentityUser<int>
    {
        public string GamerTag { get; set; }
    }

    public class Role : IdentityRole<int>
    {
    }
}
