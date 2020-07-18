using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Vertice.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the VerticeUser class
    public class VerticeUser : IdentityUser
    {
        [PersonalData]
        public string DisplayName { get; set; }
    }
}
