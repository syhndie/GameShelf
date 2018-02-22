using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameShelf.Data;
using GameShelf.Models;
using GameShelf.Models.ViewModels;

namespace GameShelf.Controllers
{
    public class GamesController : Controller
    {
        private readonly GameShelfContext _context;

        public GamesController(GameShelfContext context)
        {
            _context = context;
        }

        // GET: Games
        public IActionResult Index(string titleFilter, int yearFilter, int minFilter, int maxFilter, string playTimeFilter, string ownerFilter, string designerFilter, string sort)
        {
            var indexVM = new GameIndexViewModel(_context, titleFilter, yearFilter, minFilter, maxFilter, playTimeFilter, ownerFilter, designerFilter, sort);

            return View(indexVM);
        }

        // GET: Games/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games
                .SingleOrDefaultAsync(m => m.ID == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // GET: Games/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,OrigPubYear,Edition")] Game game)
        {
            if (ModelState.IsValid)
            {
                _context.Add(game);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(game);
        }

        // GET: Games/Edit/5

        public IActionResult Edit(int id)
        {
            var editVM = new GameEditViewModel(_context, id);
            return View(editVM);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int id)
        {
            var gameToUpdate = _context.Games.Include(g => g.PlayTime).Single(g => g.ID == id);

            bool updated = await TryUpdateModelAsync<Game>(gameToUpdate, "GameWithPersonInfo", g => g.Title);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Games/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games
                .Include(g => g.PlayTime)
                .Include(g => g.GamePersonRelationships)
                .ThenInclude(gpr => gpr.Person)
                .SingleOrDefaultAsync(m => m.ID == id);

            var gpi = new GameWithPersonInfo(game);

            if (gpi == null)
            {
                return NotFound();
            }

            return View(gpi);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var game = await _context.Games.SingleOrDefaultAsync(m => m.ID == id);
            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(int id)
        {
            return _context.Games.Any(e => e.ID == id);
        }
    }
}
