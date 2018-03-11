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

        public IActionResult Index(string titleFilter, int minFilter, int maxFilter, int playTimeFilter, string ownerFilter, string designerFilter, string sort)
        {
            var indexVM = new GameIndexViewModel(db, titleFilter, minFilter, maxFilter, playTimeFilter, ownerFilter, designerFilter, sort);

            return View(indexVM);
        }

        public IActionResult Create()
        {
            var editVM = new GameEditViewModel(db);
            return View(editVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GameEditViewModel editViewModel, int[] selectedOwners, int[] selectedDesigners)
        {
            var game = new Game()
            {
                Title = editViewModel.GameWithPersonInfo.Title,
                PlayTimeID = editViewModel.GameWithPersonInfo.PlayTimeID,
                MinPlayers =editViewModel.GameWithPersonInfo.MinPlayers,
                MaxPlayers =editViewModel.GameWithPersonInfo.MaxPlayers
            };

            db.Add(game);

            foreach (int selectedOwner in selectedOwners)
            {
                db.Add(new GamePersonRelationship { GameID = game.ID, PersonID = selectedOwner, Role = Role.Owner });
            }

            foreach (int selectedDesigner in selectedDesigners)
            {
                db.Add(new GamePersonRelationship { GameID = game.ID, PersonID = selectedDesigner, Role = Role.Designer });
            }

            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var editVM = new GameEditViewModel(db, id);
            return View(editVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int[] selectedOwners, int[] selectedDesigners)
        {
            var gameToUpdate = db.Games
                .Include(g => g.PlayTime)
                .Include(g => g.GamePersonRelationships)
                .ThenInclude(gpi => gpi.Person)
                .Single(g => g.ID == id);

            bool updated = await TryUpdateModelAsync<Game>(gameToUpdate, "GameWithPersonInfo", g => g.Title, g => g.PlayTimeID, g => g.MinPlayers, g => g.MaxPlayers);

            gameToUpdate.UpdateGamePeople(selectedOwners, db, Role.Owner);
            gameToUpdate.UpdateGamePeople(selectedDesigners, db, Role.Designer);
            
            await db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var game = await db.Games.SingleAsync(m => m.ID == id);
            db.Games.Remove(game);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
