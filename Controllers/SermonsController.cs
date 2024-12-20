using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KerazaWebApplication.Data;
using KerazaWebApplication.Models;
using KerazaWebApplication.ViewModel;
using Microsoft.Extensions.Logging;

namespace KerazaWebApplication.Controllers
{
    public class SermonsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SermonsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sermons
        [Route("sermons/index")]
        public async Task<IActionResult> Index()
        {
            ViewData["Filter"] = "Sermons";


            var applicationDbContext = _context.Sermons
                .Include(s => s.Event)
                    .ThenInclude(e=>e.Category)
                .Include(s => s.Language)
                .Include(s => s.Speaker)
                .Include(s => s.Topic);

            return View(await applicationDbContext.ToListAsync());
        }

        [Route("sermons/index/{filter}/{id}")]
        /*   [Route("/sermons/Index?filter={filter}&id={id}")]*/
        public async Task<IActionResult> Index(string filter, int id)
        {
            // Start building the base query
            var applicationDbContext = _context.Sermons
                .Include(s => s.Event)
                    .ThenInclude(e=>e.Category)
                .Include(s => s.Language)
                .Include(s => s.Speaker)
                .Include(s => s.Topic)
                .AsQueryable();  // Make it queryable to apply filters

            // Apply filter based on the "filter" parameter
            
            switch (filter?.ToLower())
            {
                case "event":
                    if (id > 0)
                    {
                        applicationDbContext = applicationDbContext.Where(s => s.Event.Id == id);
                        ViewData["Filter"] = applicationDbContext.FirstOrDefault(s => s.Event.Id == id).Event.Name;
                    }
                    break;

                case "category":
                    if (id > 0)
                    {
                        applicationDbContext = applicationDbContext.Where(s => s.Event.Category.Id == id);
                        ViewData["Filter"] = applicationDbContext.FirstOrDefault(s => s.Event.Category.Id == id).Event.Category.Name;

                    }
                    break;

                case "topic":
                    if (id > 0)
                    {
                        applicationDbContext = applicationDbContext.Where(s => s.Topic.Id == id);
                        ViewData["Filter"] = applicationDbContext.FirstOrDefault(s => s.Topic.Id == id).Topic.Name;

                    }
                    break;

                case "speaker":
                    if (id > 0)
                    {
                        applicationDbContext = applicationDbContext.Where(s => s.Speaker.Id == id);
                        ViewData["Filter"] = applicationDbContext.FirstOrDefault(s => s.Speaker.Id == id).Speaker.Name;

                    }
                    break;

                case "language":
                    if (id > 0)
                    {
                        applicationDbContext = applicationDbContext.Where(s => s.Language.Id == id);
                        ViewData["Filter"] = applicationDbContext.FirstOrDefault(s => s.Language.Id == id).Language.Name;

                    }
                    break;

                default:
                    ViewData["Filter"] = "Sermons";
                    break;

            }

            // Execute the query and return the filtered result
            var sermonsList = await applicationDbContext.ToListAsync();
            return View(sermonsList);
        }


        // GET: Sermons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sermon = await _context.Sermons
                .Include(s => s.Event)
                    .ThenInclude(e=>e.Category)
                .Include(s => s.Language)
                .Include(s => s.Speaker)
                .Include(s => s.Topic)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sermon == null)
            {
                return NotFound();
            }

            return View(sermon);
        }

        [HttpGet]
        [Route("sermons/create/{id}")]// Specify the route for the "Create" action with id
        public IActionResult Create(int id)
        {
            var selectedEvent = _context.Events.FirstOrDefault(e => e.Id == id);
            ViewData["EventId"] = new SelectList(new[] { selectedEvent }, "Id", "Name", id);
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Name");
            ViewData["SpeakerId"] = new SelectList(_context.Speakers, "Id", "Name");
            ViewData["TopicId"] = new SelectList(_context.Topics, "Id", "Name");
            return View();
        }

        [HttpGet]
        [Route("sermons/create/")] // Specify the route for the "Create" action without id
        public IActionResult Create()
        {
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Name");
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Name");
            ViewData["SpeakerId"] = new SelectList(_context.Speakers, "Id", "Name");
            ViewData["TopicId"] = new SelectList(_context.Topics, "Id", "Name");
            return View();
        }

        // POST: Sermons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title_ar,Title_en,Caption,Date,ImageURL,AddedBy,AddedOn,SpeakerId,TopicId,EventId,LanguageId")] Sermon sermon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sermon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Name", sermon.EventId);
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Name", sermon.LanguageId);
            ViewData["SpeakerId"] = new SelectList(_context.Speakers, "Id", "Name", sermon.SpeakerId);
            ViewData["TopicId"] = new SelectList(_context.Topics, "Id", "Name", sermon.TopicId);
            return View(sermon);
        }

        // GET: Sermons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sermon = await _context.Sermons.FirstOrDefaultAsync(s=> s.Id == id);
            if (sermon == null)
            {
                return NotFound();
            }
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Name", sermon.EventId);
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Name", sermon.LanguageId);
            ViewData["SpeakerId"] = new SelectList(_context.Speakers, "Id", "Name", sermon.SpeakerId);
            ViewData["TopicId"] = new SelectList(_context.Topics, "Id", "Name", sermon.TopicId);
            return View(sermon);
        }

        // POST: Sermons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title_ar,Title_en,Caption,Date,ImageURL,AddedBy,AddedOn,SpeakerId,TopicId,EventId,LanguageId")] Sermon sermon)
        {
            if (id != sermon.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sermon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SermonExists(sermon.Id))
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
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Name", sermon.EventId);
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Name", sermon.LanguageId);
            ViewData["SpeakerId"] = new SelectList(_context.Speakers, "Id", "Name", sermon.SpeakerId);
            ViewData["TopicId"] = new SelectList(_context.Topics, "Id", "Name", sermon.TopicId);
            return View(sermon);
        }

        // GET: Sermons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sermon = await _context.Sermons
                .Include(s => s.Event)
                .Include(s => s.Language)
                .Include(s => s.Speaker)
                .Include(s => s.Topic)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sermon == null)
            {
                return NotFound();
            }

            return View(sermon);
        }

        // POST: Sermons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sermon = await _context.Sermons.FindAsync(id);
            if (sermon != null)
            {
                _context.Sermons.Remove(sermon);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> SermonsSearch(HomeViewModel model)
        {
            // Start building the base query for Sermons
            var sermonsQuery = _context.Sermons
                .AsNoTracking()
                .Include(s => s.Event)
                    .ThenInclude(e=>e.Category)
                .Include(s => s.Language)
                .Include(s => s.Speaker)
                .Include(s => s.Topic).AsQueryable();

            // Apply filters based on the provided parameters

            // Filter by Event if specified
            if (model.EventId > 0)
            {
                sermonsQuery = sermonsQuery.Where(s => s.Event.Id == model.EventId);
            }


            // Filter by Topic if a specific topic is selected (i.e., topicId > 0)
            if (model.TopicId > 0)
            {
                sermonsQuery = sermonsQuery.Where(s => s.Topic.Id == model.TopicId);
            }

            // Filter by Speaker if specified
            if (model.SpeakerId > 0)
            {
                sermonsQuery = sermonsQuery.Where(s => s.Speaker.Id == model.SpeakerId);
            }

            // Filter by Language if specified
            if (model.LanguageId > 0)
            {
                sermonsQuery = sermonsQuery.Where(s => s.Language.Id == model.LanguageId);
            }

            // Search by the sermon name if a search word is provided
            if (!string.IsNullOrEmpty(model.searchWord))
            {
                sermonsQuery = sermonsQuery.Where(s => s.Title_en.ToLower().Contains(model.searchWord.ToLower()) || s.Title_ar.Contains(model.searchWord));
            }

            // Execute the query and get the results
            var sermonsList = await sermonsQuery.ToListAsync();



            HomeViewModel vm = new HomeViewModel
            {
                searchWord = model.searchWord,
                Sermons = sermonsList,

                EventId = model.EventId,
                CategoryId = model.CategoryId,
                SpeakerId = model.SpeakerId,
                LanguageId = model.LanguageId,
                TopicId = model.TopicId,


                Speakers = _context.Speakers
                .AsNoTracking()
                .Include(s => s.Sermons)
                .ToList(),

                Topics = _context.Topics
                .AsNoTracking()
                .Include(s => s.Sermons)
                .ToList(),

                Events = _context.Events
                .AsNoTracking()
                .Include(s => s.Sermons)
                .ToList(),

                Categories = _context.Categories
                .AsNoTracking()
                .ToList(),

                Languages = _context.Languages
                .AsNoTracking()
                .Include(s => s.Sermons)
                .ToList(),


            };

            return View(vm);
        }
        private bool SermonExists(int id)
        {
            return _context.Sermons.Any(e => e.Id == id);
        }
    }
}
