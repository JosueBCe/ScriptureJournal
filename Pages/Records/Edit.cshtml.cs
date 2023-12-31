﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoviesDatabase.Data;
using MoviesDatabase.Models;

namespace MoviesDatabase.Pages.Movies
{
    public class EditModel : PageModel
    {
        private readonly MoviesDatabase.Data.ScriptureJournalContext _context;

        public EditModel(MoviesDatabase.Data.ScriptureJournalContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Scriptured Movie { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Scriptured == null)
            {
                return NotFound();
            }

            var movie =  await _context.Scriptured.FirstOrDefaultAsync(m => m.ID == id);
            if (movie == null)
            {
                return NotFound();
            }
            Movie = movie;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(Movie.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MovieExists(int id)
        {
          return (_context.Scriptured?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
