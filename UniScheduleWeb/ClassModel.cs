using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace UniScheduleBuilder
{
    public record DisciplineModel
    {
        [Key]
        public int Id { get; init; }
    
        [DisplayName("Ім'я")]
        public required string Name { get; init; }
        
        [DisplayName("Вид")]
        public required string Type { get; init; }
        
        [DisplayName("Група")]
        public required string Group { get; init; }

        [DisplayName("Під група")]
        public string? SubGroup { get; init; }

        [Display(Name = "Кількість пар")] 
        public int ClassesCount { get; init; }
    }

    public record ClassModel
    {
        [Key]
        public int Id { get; init; }

        public int DisciplineId { get; set; }

        public virtual DisciplineModel? Discipline { get; init; }

        [DisplayName("№")]
        public int OrdinalNumber { get; init; }
        
        [DisplayName("Аудиторія")]
        public required string Classroom { get; init; }

        [Display(Name = "Номер пари")]
        public int ClassNumber { get; init; }
    
        [DisplayName("Дата")]
        public DateOnly Date { get; init; }
    }

    public record ClassTime(TimeOnly StartTime, TimeOnly EndTime)
    {
        private static readonly Dictionary<int, ClassTime> s_classTime = new()
        {
            {1, new ClassTime(new TimeOnly(8,0,0), new TimeOnly(9,20,0))},
            {2, new ClassTime(new TimeOnly(9,30,0), new TimeOnly(10,50,0))},
            {3, new ClassTime(new TimeOnly(11,00,0), new TimeOnly(12,20,0))},
            {4, new ClassTime(new TimeOnly(12,40,0), new TimeOnly(14,00,0))},
            {5, new ClassTime(new TimeOnly(14,10,0), new TimeOnly(15,30,0))},
            {6, new ClassTime(new TimeOnly(15,40,0), new TimeOnly(17,0,0))},
            {7, new ClassTime(new TimeOnly(17,10,0), new TimeOnly(18,30,0))},
            {8, new ClassTime(new TimeOnly(18,40,0), new TimeOnly(20,00,0))},
        };
        
        public static ClassTime GetClassTime(int classOrder)
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThan(classOrder, s_classTime.Count, nameof(classOrder));
            return s_classTime[classOrder];
        }
    }
}
