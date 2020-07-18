// <copyright file="AdditionalUserClaimsPrincipalFactory.cs" company="Thomas Castonguay-Gagnon">
// Copyright (c) Thomas Castonguay-Gagnon. All rights reserved.
// Licensed under the GPL3 license. See LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
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
        {
        }

        /// <summary>
        /// Add user claims on creation.
        /// </summary>
        /// <param name="user">Created user.</param>
        /// <returns>Return the user.</returns>
        public async override Task<ClaimsPrincipal> CreateAsync(VerticeUser user)
        {
            var principal = await base.CreateAsync(user);
            var identity = (ClaimsIdentity)principal.Identity;

            var claims = new List<Claim>();
            if (user.IsAdmin)
            {
                claims.Add(new Claim(ClaimTypes.Role, "admin"));
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, "user"));
            }

            claims.Add(new Claim("DisplayName", user.DisplayName));

            identity.AddClaims(claims);
            return principal;
        }
    }
}
