using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UniScheduleBuilder;
using UniScheduleWeb.Data;

namespace UniScheduleWeb.Pages.Classes
{
    public class DeleteModel : PageModel
    {
        private readonly UniScheduleWeb.Data.UniScheduleWebContext _context;

        public DeleteModel(UniScheduleWeb.Data.UniScheduleWebContext context)
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

            var classmodel = await _context.ClassModel.FirstOrDefaultAsync(m => m.Id == id);

            if (classmodel == null)
            {
                return NotFound();
            }
            else
            {
                ClassModel = classmodel;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classmodel = await _context.ClassModel.FindAsync(id);
            if (classmodel != null)
            {
                ClassModel = classmodel;
                _context.ClassModel.Remove(ClassModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
