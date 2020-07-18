// <copyright file="IdentityExtensions.cs" company="Thomas Castonguay-Gagnon">
// Copyright (c) Thomas Castonguay-Gagnon. All rights reserved.
// Licensed under the GPL3 license. See LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Vertice.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetDisplayName(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("DisplayName");
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string GetRole(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst(ClaimTypes.Role);
            return (claim != null) ? claim.Value : string.Empty;
        }
    }
}
