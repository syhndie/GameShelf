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

        public IActionResult Create(int id, string origin)
        {
            var personVM = new PersonCreateViewModel(id, origin);
            return View(personVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PersonCreateViewModel createViewModel, int gameID, string origin)
        {
            var person = new Person() { FirstName = createViewModel.Person.FirstName, LastName = createViewModel.Person.LastName };

            if (ModelState.IsValid)
            {
                db.Add(person);
                await db.SaveChangesAsync();

                if (origin == "gamecreate")
                {
                    return RedirectToAction("Create", "Games");
                }
                else if (origin == "gameedit")
                {
                    return RedirectToAction("Edit", "Games", new { id = gameID });
                }
                else
                {
                    return RedirectToAction("Index", "People");
                }
            }

            var personVM = new PersonCreateViewModel(gameID, origin);
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
                db.Update(person);
                await db.SaveChangesAsync();
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
    }
}
