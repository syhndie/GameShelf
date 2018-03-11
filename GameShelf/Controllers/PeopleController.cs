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

        public async Task<IActionResult> Index()
        {
            return View(await db.People.ToListAsync());
        }

        public IActionResult Create(int id)
        {
            var personVM = new PersonCreateViewModel(id);
            return View(personVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PersonCreateViewModel createViewModel, int gameID)
        {
            var person = new Person() { FirstName = createViewModel.Person.FirstName, LastName = createViewModel.Person.LastName };

            if (ModelState.IsValid)
            {
                db.Add(person);
                await db.SaveChangesAsync();

                if (gameID == 0)
                {
                    return RedirectToAction("Create", "Games");
                }
                else
                {
                    return RedirectToAction("Edit", "Games", new { id = gameID });
                }
            }

            var personVM = new PersonCreateViewModel(gameID);
            return View(personVM);
        }

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
