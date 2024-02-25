using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class MovieController : Controller
    {
        private readonly MvcMovieContext _context;

        public MovieController(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: Movie
        public async Task<IActionResult> Index()
        {
            var mvcMovieContext = _context.Movie.Include(m => m.Studio).Include(m => m.Artists);
            return View(await mvcMovieContext.ToListAsync());
        }

        // GET: Movie/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .Include(m => m.Studio)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movie/Create
        public IActionResult Create()
        {
            ViewData["StudioId"] = new SelectList(_context.Studio, "Id", "Name");
            ViewData["ArtistId"] = new SelectList(_context.Artist, "Id", "Name");
            return View();
        }

        // POST: Movie/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Price,StudioId,SelectedArtists")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                if (movie.SelectedArtists != null)
                {
                    foreach (var artistId in movie.SelectedArtists)
                    {
                        var artist = await _context.Artist.FindAsync(artistId);
                        if (artist != null)
                        {
                            movie.Artists.Add(artist);
                        }
                    }
                }

                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudioId"] = new SelectList(_context.Studio, "Id", "Name", movie.StudioId);
            ViewData["ArtistId"] = new MultiSelectList(_context.Artist, "Id", "Name");

            return View(movie);
        }


        // GET: Movie/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.Include(m => m.Artists).FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }
            ViewData["StudioId"] = new SelectList(_context.Studio, "Id", "Name", movie.StudioId);
            ViewData["ArtistId"] = new MultiSelectList(_context.Artist, "Id", "Name", movie.Artists.Select(a => a.Id).ToList());
            return View(movie);
        }

        // POST: Movie/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price,StudioId,SelectedArtists")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingMovie = await _context.Movie
                        .Include(m => m.Artists)
                        .FirstOrDefaultAsync(m => m.Id == id);

                    if (existingMovie == null)
                    {
                        return NotFound();
                    }

                    existingMovie.Title = movie.Title;
                    existingMovie.ReleaseDate = movie.ReleaseDate;
                    existingMovie.Genre = movie.Genre;
                    existingMovie.Price = movie.Price;
                    existingMovie.StudioId = movie.StudioId;

                    if (movie.SelectedArtists != null)
                    {
                        existingMovie.Artists.Clear();
                        foreach (var artistId in movie.SelectedArtists)
                        {
                            var artist = await _context.Artist.FindAsync(artistId);
                            if (artist != null)
                            {
                                existingMovie.Artists.Add(artist);
                            }
                        }
                    }

                    _context.Update(existingMovie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
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

            ViewData["StudioId"] = new SelectList(_context.Studio, "Id", "Name", movie.StudioId);
            ViewData["ArtistId"] = new MultiSelectList(_context.Artist, "Id", "Name", movie.SelectedArtists);

            return View(movie);
        }

        // GET: Movie/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .Include(m => m.Studio)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Movie == null)
            {
                return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
            }
            var movie = await _context.Movie.FindAsync(id);
            if (movie != null)
            {
                _context.Movie.Remove(movie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return (_context.Movie?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
