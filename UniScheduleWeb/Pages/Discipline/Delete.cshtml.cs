using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UniScheduleBuilder;
using UniScheduleWeb.Data;

namespace UniScheduleWeb.Pages.Discipline
{
    public class DeleteModel : PageModel
    {
        private readonly UniScheduleWeb.Data.UniScheduleWebContext _context;

        public DeleteModel(UniScheduleWeb.Data.UniScheduleWebContext context)
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

            var discipline = await _context.Discipline.FirstOrDefaultAsync(m => m.Id == id);

            if (discipline == null)
            {
                return NotFound();
            }
            else
            {
                Discipline = discipline;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discipline = await _context.Discipline.FindAsync(id);
            if (discipline != null)
            {
                Discipline = discipline;
                _context.Discipline.Remove(Discipline);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
