using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KerazaWebApplication.Data;
using KerazaWebApplication.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace KerazaWebApplication.Controllers
{
    public class MediaFilesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MediaFilesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MediaFiles
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MediaFiles.Include(m => m.Sermon);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MediaFiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mediaFile = await _context.MediaFiles
                .Include(m => m.Sermon)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mediaFile == null)
            {
                return NotFound();
            }

            return View(mediaFile);
        }

        // GET: MediaFiles/Create
        public IActionResult Create(int id)
        {

            var mediaFile = new MediaFile()
            {
                SermonId=id,
            };

            return View(mediaFile);
            
        }

        // POST: MediaFiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Url,Type,AddedBy,AddedOn,SermonId")] MediaFile mediaFile)
        {
            if (_context.Sermons.Any(s=>s.Id == mediaFile.SermonId))
            {
                ModelState.AddModelError("","Sermon Not Found");
            }
            if (ModelState.IsValid)
            {
                _context.Add(mediaFile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mediaFile);
        }

        // GET: MediaFiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mediaFile = await _context.MediaFiles.FindAsync(id);
            if (mediaFile == null)
            {
                return NotFound();
            }
            ViewData["SermonId"] = new SelectList(_context.Sermons, "Id", "Title_ar", mediaFile.SermonId);
            return View(mediaFile);
        }

        // POST: MediaFiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Url,Type,AddedBy,AddedOn,SermonId")] MediaFile mediaFile)
        {
            if (id != mediaFile.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mediaFile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MediaFileExists(mediaFile.Id))
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
            ViewData["SermonId"] = new SelectList(_context.Sermons, "Id", "Title_ar", mediaFile.SermonId);
            return View(mediaFile);
        }

        // GET: MediaFiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mediaFile = await _context.MediaFiles
                .Include(m => m.Sermon)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mediaFile == null)
            {
                return NotFound();
            }

            return View(mediaFile);
        }

        // POST: MediaFiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mediaFile = await _context.MediaFiles.FindAsync(id);
            if (mediaFile != null)
            {
                _context.MediaFiles.Remove(mediaFile);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MediaFileExists(int id)
        {
            return _context.MediaFiles.Any(e => e.Id == id);
        }
    }
}
