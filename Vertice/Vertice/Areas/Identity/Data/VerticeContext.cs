// <copyright file="VerticeContext.cs" company="Thomas Castonguay-Gagnon">
// Copyright (c) Thomas Castonguay-Gagnon. All rights reserved.
// Licensed under the GPL3 license. See LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Vertice.Areas.Identity.Data;
using Vertice.Models;

namespace Vertice.Data
{
    public class VerticeContext : IdentityDbContext<VerticeUser>
    {
        public VerticeContext(DbContextOptions<VerticeContext> options)
            : base(options)
        {
        }

        public DbSet<Vertice.Models.CharacterModel> CharacterModel { get; set; }

        public DbSet<Vertice.Models.InventoryModel> InventoryModel { get; set; }

        public DbSet<Vertice.Models.ItemModel> ItemModel { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
