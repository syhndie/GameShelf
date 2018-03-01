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
        private readonly GameShelfContext db;

        public GamesController(GameShelfContext context)
        {
            db = context;
        }

        // GET: Games
        public IActionResult Index(string titleFilter, int minFilter, int maxFilter, int playTimeFilter, string ownerFilter, string designerFilter, string sort)
        {
            var indexVM = new GameIndexViewModel(db, titleFilter, minFilter, maxFilter, playTimeFilter, ownerFilter, designerFilter, sort);

            return View(indexVM);
        }

        // GET: Games/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await db.Games
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
        public async Task<IActionResult> Create([Bind("ID,Title")] Game game)
        {
            if (ModelState.IsValid)
            {
                db.Add(game);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(game);
        }

        // GET: Games/Edit/5

        public IActionResult Edit(int id)
        {
            var editVM = new GameEditViewModel(db, id);
            return View(editVM);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int[] selectedOwners)
        {
            var gameToUpdate = db.Games
                .Include(g => g.PlayTime)
                .Include(g => g.GamePersonRelationships)
                .ThenInclude(gpi => gpi.Person)
                .Single(g => g.ID == id);

            bool updated = await TryUpdateModelAsync<Game>(gameToUpdate, "GameWithPersonInfo", g => g.Title, g => g.PlayTimeID, g => g.MinPlayers, g => g.MaxPlayers);

            gameToUpdate.UpdateGameOwners(selectedOwners, db);
            
            await db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Games/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await db.Games
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
            var game = await db.Games.SingleOrDefaultAsync(m => m.ID == id);
            db.Games.Remove(game);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(int id)
        {
            return db.Games.Any(e => e.ID == id);
        }
    }
}
