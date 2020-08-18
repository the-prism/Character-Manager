// <copyright file="CharacterController.cs" company="Thomas Castonguay-Gagnon">
// Copyright (c) Thomas Castonguay-Gagnon. All rights reserved.
// Licensed under the GPL3 license. See LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vertice.Areas.Identity.Data;
using Vertice.Data;
using Vertice.Extensions;
using Vertice.Models;

namespace Vertice
{
    public class CharacterController : Controller
    {
        private readonly VerticeContext _context;
        private readonly UserManager<VerticeUser> _userManager;

        public CharacterController(
            VerticeContext context,
            UserManager<VerticeUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Character
        public async Task<IActionResult> Index()
        {
            if (User.Identity.GetRole() == "admin")
            {
                return View(await _context.CharacterModel.ToListAsync());
            }
            else
            {
                var verticeUser = await _userManager.GetUserAsync(User);
                var userId = await _userManager.GetUserIdAsync(verticeUser);
                return View(await _context.CharacterModel.Where(m => m.OwnerID == userId || m.Permission.ToLower() == "public").ToListAsync());
            }
        }

        // GET: Character/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var characterModel = await _context.CharacterModel
                .Include(m => m.MainAttributes)
                .FirstOrDefaultAsync(m => m.CharacterId == id);

            if (characterModel == null)
            {
                return NotFound();
            }

            return View(characterModel);
        }

        // GET: Character/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Character/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CharacterId,CharacterName,MainAttributes,Permission")] CharacterModel characterModel)
        {
            if (ModelState.IsValid)
            {
                var verticeUser = await _userManager.GetUserAsync(User);
                characterModel.OwnerID = await _userManager.GetUserIdAsync(verticeUser);

                characterModel.Inventory.Items.Add(new ItemModel() { Name = "First", Weight = 2.1, Value = 30, });

                _context.Add(characterModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(characterModel);
        }

        // GET: Character/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var characterModel = await _context.CharacterModel.FindAsync(id);
            if (characterModel == null)
            {
                return NotFound();
            }

            return View(characterModel);
        }

        // POST: Character/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CharacterId,OwnerID,CharacterName")] CharacterModel characterModel)
        {
            if (id != characterModel.CharacterId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existing = _context.CharacterModel.FirstOrDefault(m => m.CharacterId == id);

                    // Change existing stuff
                    existing.OwnerID = characterModel.OwnerID;
                    existing.CharacterName = characterModel.CharacterName;

                    _context.Update(existing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CharacterModelExists(characterModel.CharacterId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(characterModel);
        }

        // GET: Character/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var characterModel = await _context.CharacterModel
                .FirstOrDefaultAsync(m => m.CharacterId == id);
            if (characterModel == null)
            {
                return NotFound();
            }

            return View(characterModel);
        }

        // POST: Character/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var characterModel = await _context.CharacterModel.FindAsync(id);
            var inventoryModel = await _context.InventoryModel.Include(m => m.Items).FirstOrDefaultAsync(m => m.CharacterId == characterModel.CharacterId);

            foreach (var item in inventoryModel.Items)
            {
                _context.ItemModel.Remove(item);
            }

            _context.InventoryModel.Remove(inventoryModel);
            _context.CharacterModel.Remove(characterModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CharacterModelExists(int id)
        {
            return _context.CharacterModel.Any(e => e.CharacterId == id);
        }
    }
}
