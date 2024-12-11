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

namespace UniScheduleWeb.Pages.Classes
{
    public class EditModel : PageModel
    {
        private readonly UniScheduleWeb.Data.UniScheduleWebContext _context;

        public EditModel(UniScheduleWeb.Data.UniScheduleWebContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ClassModel ClassModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classmodel =  await _context.ClassModel.FirstOrDefaultAsync(m => m.Id == id);
            if (classmodel == null)
            {
                return NotFound();
            }
            ClassModel = classmodel;
           ViewData["DisciplineId"] = new SelectList(_context.Discipline, "Id", "Name");
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

            _context.Attach(ClassModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassModelExists(ClassModel.Id))
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

        private bool ClassModelExists(int id)
        {
            return _context.ClassModel.Any(e => e.Id == id);
        }
    }
}
