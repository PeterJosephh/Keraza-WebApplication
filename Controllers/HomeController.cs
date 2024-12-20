using System.Diagnostics;
using KerazaWebApplication.Data;
using KerazaWebApplication.Models;
using KerazaWebApplication.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace KerazaWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger,ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Name");
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Name");
            ViewData["SpeakerId"] = new SelectList(_context.Speakers, "Id", "Name");
            ViewData["TopicId"] = new SelectList(_context.Topics, "Id", "Name");

            HomeViewModel vm = new HomeViewModel()
            {

                Sermons = _context.Sermons
                .Include(s => s.Event)
                    .ThenInclude(e => e.Category)
                .Include(s => s.Language)
                .Include(s => s.Speaker)
                .Include(s => s.Topic)
                .OrderByDescending(s => s.Id)
                .Take(9)
                .ToList(),

                Speakers = _context.Speakers
                .Include(s => s.Sermons)
                .ToList(),

                Topics = _context.Topics
                .Include(s => s.Sermons)
                .ToList(),

                Events = _context.Events
                .Include(s => s.Sermons)
                .ToList(),

                Categories = _context.Categories
                .ToList(),

                Languages = _context.Languages
                .Include(s => s.Sermons)
                .ToList(),


            };
            return View(vm);
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
