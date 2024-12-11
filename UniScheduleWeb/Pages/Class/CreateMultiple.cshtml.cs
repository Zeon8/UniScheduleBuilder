using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using UniScheduleBuilder;
using UniScheduleWeb.Data;

namespace UniScheduleWeb.Pages.Classes
{
    public class CreateMultipleModel : PageModel
    {
        private readonly UniScheduleWeb.Data.UniScheduleWebContext _context;

        public CreateMultipleModel(UniScheduleWeb.Data.UniScheduleWebContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["DisciplineId"] = _context.Discipline
                .Select(d => new SelectListItem($"{d.Name} {d.Type} {d.Group} {d.SubGroup}г", 
                    d.Id.ToString()));
            return Page();
        }

        [BindProperty]
        public ClassModel ClassModel { get; set; } = default!;

        [BindProperty]
        public int Count { get; set; }

        [BindProperty]
        public DateOnly StartDate { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            for (int i = 0; i < Count; i++)
            {
                var @class = new ClassModel()
                {
                    OrdinalNumber = i+1,
                    DisciplineId = ClassModel.DisciplineId,
                    ClassNumber = ClassModel.ClassNumber,
                    Classroom = ClassModel.Classroom,
                    Date = StartDate.AddDays(14 * i)
                };
                await _context.ClassModel.AddAsync(@class);
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
