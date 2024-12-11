using Microsoft.EntityFrameworkCore;
using UniScheduleBuilder;

namespace UniScheduleWeb.Data
{
    public class UniScheduleWebContext : DbContext
    {
        public UniScheduleWebContext (DbContextOptions<UniScheduleWebContext> options)
            : base(options)
        {
        }

        public DbSet<DisciplineModel> Discipline { get; set; }

        public DbSet<ClassModel> ClassModel { get; set; } 
    }
}
