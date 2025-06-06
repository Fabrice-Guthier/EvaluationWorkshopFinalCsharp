using Evaluation_Workshop_Final_CDA_C_.Data;
using Evaluation_Workshop_Final_CDA_C_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evaluation_Workshop_Final_CDA_C_.Controllers
{
    public class AnimalsController : Controller
    {
        private readonly Evaluation_Workshop_Final_CDA_C_Context _context;

        public AnimalsController(Evaluation_Workshop_Final_CDA_C_Context context)
        {
            _context = context;
        }

        // GET: Animals
        public async Task<IActionResult> Index(string searchString, int? raceId)
        {
            var animalsQuery = _context.Animal.Include(a => a.Race).AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                animalsQuery = animalsQuery.Where(a => a.AnimalName.Contains(searchString));
            }

            if (raceId.HasValue)
            {
                animalsQuery = animalsQuery.Where(a => a.RaceId == raceId);
            }

            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentRaceId"] = raceId;

            return View(await animalsQuery.ToListAsync());
        }


        // GET: Animals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var animal = await _context.Animal.Include(a => a.Race).FirstOrDefaultAsync(m => m.AnimalId == id);
            if (animal == null) return NotFound();
            return View(animal);
        }

        // GET: Animals/Create
        public async Task<IActionResult> Create()
        {
            var viewModel = new CreateAnimalViewModel
            {
                AvailableRaces = new SelectList(await _context.Race.ToListAsync(), "RaceId", "RaceName")
            };
            return View(viewModel);
        }

        // POST: Animals/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAnimalViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var animal = new Animal
                {
                    AnimalName = viewModel.AnimalName,
                    AnimalDescription = viewModel.AnimalDescription,
                    RaceId = viewModel.RaceId
                };
                _context.Add(animal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            viewModel.AvailableRaces = new SelectList(await _context.Race.ToListAsync(), "RaceId", "RaceName", viewModel.RaceId);
            return View(viewModel);
        }


        // GET: Animals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var animal = await _context.Animal.FindAsync(id);
            if (animal == null) return NotFound();
            ViewData["RaceId"] = new SelectList(await _context.Race.ToListAsync(), "RaceId", "RaceName", animal.RaceId);
            return View(animal);
        }


        // POST: Animals/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Animal animalFromForm)
        {
            if (id != animalFromForm.AnimalId) return NotFound();

            if (ModelState.IsValid)
            {
                var animalToUpdate = await _context.Animal.FindAsync(id);
                if (animalToUpdate == null) return NotFound();

                animalToUpdate.AnimalName = animalFromForm.AnimalName;
                animalToUpdate.AnimalDescription = animalFromForm.AnimalDescription;
                animalToUpdate.RaceId = animalFromForm.RaceId;
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimalExists(animalFromForm.AnimalId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["RaceId"] = new SelectList(await _context.Race.ToListAsync(), "RaceId", "RaceName", animalFromForm.RaceId);
            return View(animalFromForm);
        }

        // GET: Animals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var animal = await _context.Animal.Include(a => a.Race).FirstOrDefaultAsync(m => m.AnimalId == id);
            if (animal == null) return NotFound();
            return View(animal);
        }

        // POST: Animals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var animal = await _context.Animal.FindAsync(id);
            if (animal != null) _context.Animal.Remove(animal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimalExists(int id)
        {
            return _context.Animal.Any(e => e.AnimalId == id);
        }
    }
}