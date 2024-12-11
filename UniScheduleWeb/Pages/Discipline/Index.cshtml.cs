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
    public class IndexModel : PageModel
    {
        private readonly UniScheduleWeb.Data.UniScheduleWebContext _context;

        public IndexModel(UniScheduleWeb.Data.UniScheduleWebContext context)
        {
            _context = context;
        }

        public IList<DisciplineModel> Discipline { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Discipline = await _context.Discipline.ToListAsync();
        }
    }
}
