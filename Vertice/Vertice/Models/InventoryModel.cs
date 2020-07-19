// <copyright file="InventoryModel.cs" company="Thomas Castonguay-Gagnon">
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
    public class InventoryModel
    {
        public InventoryModel()
        {
            Items = new List<ItemModel>();
        }

        [Key]
        public int InventoryId { get; set; }

        public List<ItemModel> Items { get; set; }

        [NotMapped]
        public double TotalWeight { get; set; }

        [NotMapped]
        public int TotalValue { get; set; }

        public int CharacterId { get; set; }

        [ForeignKey("CharacterId")]
        public CharacterModel Character { get; set; }
    }
}
