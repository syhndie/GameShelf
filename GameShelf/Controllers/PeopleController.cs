using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameShelf.Data;
using GameShelf.Models;
using GameShelf.Models.ViewModels;

namespace GameShelf.Controllers
{
    public class PeopleController : Controller
    {
        private readonly GameShelfContext db;

        public PeopleController(GameShelfContext context)
        {
            db = context;
        }

        // GET: People
        public async Task<IActionResult> Index()
        {
            return View(await db.People.ToListAsync());
        }

        // GET: People/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await db.People
                .SingleOrDefaultAsync(m => m.ID == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: People/Create
        public IActionResult Create(int id)
        {
            var personVM = new PersonCreateViewModel(id);
            return View(personVM);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, int[] selectedOwners)
        //{
        //    var gameToUpdate = db.Games
        //        .Include(g => g.PlayTime)
        //        .Include(g => g.GamePersonRelationships)
        //        .ThenInclude(gpi => gpi.Person)
        //        .Single(g => g.ID == id);

        //    bool updated = await TryUpdateModelAsync<Game>(gameToUpdate, "GameWithPersonInfo", g => g.Title, g => g.PlayTimeID, g => g.MinPlayers, g => g.MaxPlayers);

        //    gameToUpdate.UpdateGameOwners(selectedOwners, db);

        //    await db.SaveChangesAsync();

        //    return RedirectToAction(nameof(Index));
        //}


        // POST: People/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,LastName,FirstName")] Person person, [Bind("GameID")] int gameID)
        {
            if (ModelState.IsValid)
            {
                db.Add(person);
                await db.SaveChangesAsync();
                return RedirectToAction("Edit", "Games", new { id = gameID });
            }
            var personVM = new PersonCreateViewModel(gameID);
            return View(personVM);
        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await db.People.SingleOrDefaultAsync(m => m.ID == id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,LastName,FirstName")] Person person)
        {
            if (id != person.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(person);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.ID))
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
            return View(person);
        }

        // GET: People/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await db.People
                .SingleOrDefaultAsync(m => m.ID == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var person = await db.People.SingleOrDefaultAsync(m => m.ID == id);
            db.People.Remove(person);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int id)
        {
            return db.People.Any(e => e.ID == id);
        }
    }
}
