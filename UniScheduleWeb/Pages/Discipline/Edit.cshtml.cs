using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UniScheduleBuilder;
using UniScheduleWeb.Data;

namespace UniScheduleWeb.Pages.Discipline
{
    public class EditModel : PageModel
    {
        private readonly UniScheduleWeb.Data.UniScheduleWebContext _context;

        public EditModel(UniScheduleWeb.Data.UniScheduleWebContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DisciplineModel Discipline { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discipline =  await _context.Discipline.FirstOrDefaultAsync(m => m.Id == id);
            if (discipline == null)
            {
                return NotFound();
            }
            Discipline = discipline;
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

            _context.Attach(Discipline).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DisciplineExists(Discipline.Id))
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

        private bool DisciplineExists(int id)
        {
            return _context.Discipline.Any(e => e.Id == id);
        }
    }
}
