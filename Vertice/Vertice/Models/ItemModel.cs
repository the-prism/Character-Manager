// <copyright file="ItemModel.cs" company="Thomas Castonguay-Gagnon">
// Copyright (c) Thomas Castonguay-Gagnon. All rights reserved.
// Licensed under the GPL3 license. See LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Vertice.Models
{
    public class ItemModel
    {
        [Key]
        public int ItemId { get; set; }

        [ForeignKey("InventoryId")]
        public int Inventory { get; set; }

        public string Name { get; set; }

        public double Weight { get; set; }

        public int Value { get; set; }
    }
}
