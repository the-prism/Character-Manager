// <copyright file="InventoryController.cs" company="Thomas Castonguay-Gagnon">
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
using Vertice.Models;

namespace Vertice.Controllers
{
    public class InventoryController : Controller
    {
        private readonly VerticeContext _context;
        private readonly UserManager<VerticeUser> _userManager;

        public InventoryController(
            VerticeContext context,
            UserManager<VerticeUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: InventoryModels
        public async Task<IActionResult> Index()
        {
            var inventories = await _context.InventoryModel.Include(m => m.Character).Include(n => n.Items).ToListAsync();
            foreach (var item in inventories)
            {
                item.Character = await _context.CharacterModel.FirstOrDefaultAsync(q => q.CharacterId == item.CharacterId);
            }

            return View(inventories);
        }

        // GET: InventoryModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventoryModel = await _context.InventoryModel
                .FirstOrDefaultAsync(m => m.InventoryId == id);
            if (inventoryModel == null)
            {
                return NotFound();
            }

            return View(inventoryModel);
        }

        // GET: InventoryModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: InventoryModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CharacterId,Character")] InventoryModel inventoryModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inventoryModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(inventoryModel);
        }

        // GET: InventoryModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventoryModel = await _context.InventoryModel.FindAsync(id);
            if (inventoryModel == null)
            {
                return NotFound();
            }

            return View(inventoryModel);
        }

        // POST: InventoryModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CharacterId,Character")] InventoryModel inventoryModel)
        {
            if (id != inventoryModel.InventoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inventoryModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventoryModelExists(inventoryModel.InventoryId))
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

            return View(inventoryModel);
        }

        // GET: InventoryModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventoryModel = await _context.InventoryModel
                .FirstOrDefaultAsync(m => m.InventoryId == id);
            if (inventoryModel == null)
            {
                return NotFound();
            }

            return View(inventoryModel);
        }

        // POST: InventoryModels/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inventoryModel = await _context.InventoryModel.FindAsync(id);
            _context.InventoryModel.Remove(inventoryModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InventoryModelExists(int id)
        {
            return _context.InventoryModel.Any(e => e.InventoryId == id);
        }
    }
}
