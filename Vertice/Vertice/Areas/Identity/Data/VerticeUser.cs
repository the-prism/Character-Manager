// <copyright file="VerticeUser.cs" company="Thomas Castonguay-Gagnon">
// Copyright (c) Thomas Castonguay-Gagnon. All rights reserved.
// Licensed under the GPL3 license. See LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Vertice.Areas.Identity.Data
{
    /// <summary>
    /// Add profile data for application users by adding properties to the VerticeUser class
    /// </summary>
    public class VerticeUser : IdentityUser
    {
        [PersonalData]
        public string DisplayName { get; set; }

        public bool IsAdmin { get; set; }
    }
}
