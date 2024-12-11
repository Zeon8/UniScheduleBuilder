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
    public class CreateModel : PageModel
    {
        private readonly UniScheduleWeb.Data.UniScheduleWebContext _context;

        public CreateModel(UniScheduleWeb.Data.UniScheduleWebContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["DisciplineId"] = _context.Discipline
                .Select(d => GetListItem(d));
            return Page();
        }

        public static SelectListItem GetListItem(DisciplineModel discipline)
        {
            string value = $"{discipline.Name} {discipline.Type} {discipline.Group}";
            if (discipline.SubGroup is not null)
                value += $"{discipline.SubGroup} г";

            return new SelectListItem(value, discipline.Id.ToString());
        }

        [BindProperty]
        public ClassModel ClassModel { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ClassModel.Add(ClassModel);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
