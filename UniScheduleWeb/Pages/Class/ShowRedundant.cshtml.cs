using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UniScheduleBuilder;
using UniScheduleWeb.Data;

namespace UniScheduleWeb.Pages.Classes
{
    public class ShowRedundantModel : PageModel
    {
        private readonly UniScheduleWebContext _context;

        public ShowRedundantModel(UniScheduleWebContext context) => _context = context;

        public IList<ClassModel>? ClassModel { get; set; }

        [BindProperty(SupportsGet = true)]
        [DisplayName("Дата завершення семестру")]
        public DateOnly? EndDate { get; init; }

        public async Task OnGetAsync()
        {
            if (ModelState.IsValid)
            {
                ClassModel = await _context.ClassModel
                    .Where(c => c.Date > EndDate)
                    .Include(c => c.Discipline)
                    .ToListAsync();
            }
        }
    }
}
