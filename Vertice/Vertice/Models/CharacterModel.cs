// <copyright file="CharacterModel.cs" company="Thomas Castonguay-Gagnon">
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
    public class CharacterModel
    {
        public CharacterModel()
        {
            MainAttributes = new Attributes();
        }

        [Key]
        public int CharacterId { get; set; }

        // user ID from AspNetUser table.
        public string OwnerID { get; set; }

        public string CharacterName { get; set; }

        public Attributes MainAttributes { get; set; }
    }
}
