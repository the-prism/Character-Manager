using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Vertice.Areas.Identity.Data;

namespace Vertice
{
    public class AdditionalUserClaimsPrincipalFactory :
        UserClaimsPrincipalFactory<VerticeUser, IdentityRole>
    {
        public AdditionalUserClaimsPrincipalFactory(
        UserManager<VerticeUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IOptions<IdentityOptions> optionsAccessor)
        : base(userManager, roleManager, optionsAccessor)
        { }

		public async override Task<ClaimsPrincipal> CreateAsync(VerticeUser user)
		{
			var principal = await base.CreateAsync(user);
			var identity = (ClaimsIdentity)principal.Identity;

			var claims = new List<Claim>();
			if (user.IsAdmin)
			{
				claims.Add(new Claim("Role", "admin"));
			}
			else
			{
				claims.Add(new Claim("Role", "user"));
			}
			claims.Add(new Claim("DisplayName", user.DisplayName));

			identity.AddClaims(claims);
			return principal;
		}
	}
}
