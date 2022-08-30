using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Basketball_Workshop;
using Basketball_Workshop.Models;

namespace Basketball_Workshop.Controllers
{
    public class PlayerPositionsController : Controller
    {
        private readonly BasketballContext _context;

        public PlayerPositionsController(BasketballContext context)
        {
            _context = context;
        }

        // GET: PlayerPositions
        public async Task<IActionResult> Index()
        {
            var basketballContext = _context.PlayerPositions.Include(p => p.Player).Include(p => p.Position);
            return View(await basketballContext.ToListAsync());
        }

        // GET: PlayerPositions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PlayerPositions == null)
            {
                return NotFound();
            }

            var playerPosition = await _context.PlayerPositions
                .Include(p => p.Player)
                .Include(p => p.Position)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (playerPosition == null)
            {
                return NotFound();
            }

            return View(playerPosition);
        }

        // GET: PlayerPositions/Create
        public IActionResult Create()
        {
            ViewData["PlayerId"] = new SelectList(_context.Players, "Id", "Id");
            ViewData["PositionId"] = new SelectList(_context.Positions, "Id", "Id");
            return View();
        }

        // POST: PlayerPositions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PlayerId,PositionId")] PlayerPosition playerPosition)
        {
            if (ModelState.IsValid)
            {
                _context.Add(playerPosition);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlayerId"] = new SelectList(_context.Players, "Id", "Id", playerPosition.PlayerId);
            ViewData["PositionId"] = new SelectList(_context.Positions, "Id", "Id", playerPosition.PositionId);
            return View(playerPosition);
        }

        // GET: PlayerPositions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PlayerPositions == null)
            {
                return NotFound();
            }

            var playerPosition = await _context.PlayerPositions.FindAsync(id);
            if (playerPosition == null)
            {
                return NotFound();
            }
            ViewData["PlayerId"] = new SelectList(_context.Players, "Id", "Id", playerPosition.PlayerId);
            ViewData["PositionId"] = new SelectList(_context.Positions, "Id", "Id", playerPosition.PositionId);
            return View(playerPosition);
        }

        // POST: PlayerPositions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PlayerId,PositionId")] PlayerPosition playerPosition)
        {
            if (id != playerPosition.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(playerPosition);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerPositionExists(playerPosition.Id))
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
            ViewData["PlayerId"] = new SelectList(_context.Players, "Id", "Id", playerPosition.PlayerId);
            ViewData["PositionId"] = new SelectList(_context.Positions, "Id", "Id", playerPosition.PositionId);
            return View(playerPosition);
        }

        // GET: PlayerPositions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PlayerPositions == null)
            {
                return NotFound();
            }

            var playerPosition = await _context.PlayerPositions
                .Include(p => p.Player)
                .Include(p => p.Position)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (playerPosition == null)
            {
                return NotFound();
            }

            return View(playerPosition);
        }

        // POST: PlayerPositions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PlayerPositions == null)
            {
                return Problem("Entity set 'BasketballContext.PlayerPositions'  is null.");
            }
            var playerPosition = await _context.PlayerPositions.FindAsync(id);
            if (playerPosition != null)
            {
                _context.PlayerPositions.Remove(playerPosition);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlayerPositionExists(int id)
        {
          return (_context.PlayerPositions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
