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
    public class RacesController : Controller
    {
        private readonly Evaluation_Workshop_Final_CDA_C_Context _context;

        public RacesController(Evaluation_Workshop_Final_CDA_C_Context context)
        {
            _context = context;
        }

        // GET: Races
        public async Task<IActionResult> Index()
        {
            return View(await _context.Race.ToListAsync());
        }

        // GET: Races/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var race = await _context.Race
                .FirstOrDefaultAsync(m => m.RaceId == id);
            if (race == null)
            {
                return NotFound();
            }

            return View(race);
        }

        // GET: Races/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Races/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateRaceViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var race = new Race
                {
                    RaceName = viewModel.RaceName,
                    RaceDescription = viewModel.RaceDescription
                };

                _context.Add(race);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        // GET: Races/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var race = await _context.Race.FindAsync(id);
            if (race == null)
            {
                return NotFound();
            }

            return View(race);
        }

        // POST: Races/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RaceId,RaceName,RaceDescription")] Race raceFromForm)
        {
            if (id != raceFromForm.RaceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var raceToUpdate = await _context.Race.FindAsync(id);
                if (raceToUpdate == null)
                {
                    return NotFound("Impossible de trouver la race à mettre à jour.");
                }
                raceToUpdate.RaceName = raceFromForm.RaceName;
                raceToUpdate.RaceDescription = raceFromForm.RaceDescription;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RaceExists(raceFromForm.RaceId))
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
            return View(raceFromForm);
        }

        // GET: Races/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var race = await _context.Race
                .FirstOrDefaultAsync(m => m.RaceId == id);
            if (race == null)
            {
                return NotFound();
            }

            return View(race);
        }

        // POST: Races/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var race = await _context.Race.FindAsync(id);
            if (race != null)
            {
                _context.Race.Remove(race);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RaceExists(int id)
        {
            return _context.Race.Any(e => e.RaceId == id);
        }
    }
}
