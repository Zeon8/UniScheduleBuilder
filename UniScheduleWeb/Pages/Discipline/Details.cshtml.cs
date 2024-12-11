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
    public class DetailsModel : PageModel
    {
        private readonly UniScheduleWeb.Data.UniScheduleWebContext _context;

        public DetailsModel(UniScheduleWeb.Data.UniScheduleWebContext context)
        {
            _context = context;
        }

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
    }
}
