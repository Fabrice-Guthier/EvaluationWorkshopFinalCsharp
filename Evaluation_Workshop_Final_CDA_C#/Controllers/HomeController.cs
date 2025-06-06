using Evaluation_Workshop_Final_CDA_C_.Data;
using Evaluation_Workshop_Final_CDA_C_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Evaluation_Workshop_Final_CDA_C_.Controllers
{
    public class HomeController : Controller
    {
        private readonly Evaluation_Workshop_Final_CDA_C_Context _context;

        public HomeController(Evaluation_Workshop_Final_CDA_C_Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["RacesList"] = new SelectList(await _context.Race.ToListAsync(), "RaceId", "RaceName");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
