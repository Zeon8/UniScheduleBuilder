using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using UniScheduleBuilder;
using UniScheduleWeb.Data;

namespace UniScheduleWeb.Pages.Discipline
{
    public class CreateModel : PageModel
    {
        private readonly UniScheduleWeb.Data.UniScheduleWebContext _context;

        public CreateModel(UniScheduleWeb.Data.UniScheduleWebContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public DisciplineModel Discipline { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Discipline.Add(Discipline);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
