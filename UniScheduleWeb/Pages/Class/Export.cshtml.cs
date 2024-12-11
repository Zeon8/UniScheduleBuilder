using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PicoXLSX;
using UniScheduleBuilder;
using UniScheduleWeb.Data;

namespace UniScheduleWeb.Pages.Class
{
    public class ExportModel : PageModel
    {
        private readonly UniScheduleWebContext _context;

        public ExportModel(UniScheduleWebContext context)
        {
            _context = context;
        }

        public IActionResult OnGetCalendar()
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            GenerateCalendar(writer);
            writer.Flush();
            stream.Position = 0;
            return new FileStreamResult(stream, "text/ics")
            {
                FileDownloadName = $"schedule-{Guid.NewGuid()}.ics"
            };
        }

        public async Task<IActionResult> OnGetExcel()
        {
            var stream = new MemoryStream();
            await GenerateExcel(stream);
            stream.Position = 0;
            return new FileStreamResult(stream, "text/ics")
            {
                FileDownloadName = $"Навантаження-{Guid.NewGuid()}.xlsx"
            };
        }

        private void GenerateCalendar(StreamWriter writer)
        {
            var classes = _context.ClassModel
                .Include(c => c.Discipline);
            
            foreach (var @class in classes)
            {
                var classTime = ClassTime.GetClassTime(@class.ClassNumber);
                writer.WriteLine("BEGIN:VEVENT");
                writer.Write($"SUMMARY: {@class.ClassNumber}п {@class.Discipline.Name} {@class.Discipline.Type}-{@class.OrdinalNumber}");
                if (!string.IsNullOrEmpty(@class.Classroom))
                    writer.Write($", {@class.Classroom}");
                if (!string.IsNullOrEmpty(@class.Discipline.SubGroup))
                    writer.Write($" {@class.Discipline.SubGroup}г");
                writer.WriteLine();
                writer.WriteLine($"DTSTART:{new DateTime(@class.Date, classTime.StartTime): yyyyMMddTHHmmss}");
                writer.WriteLine($"DTEND:{new DateTime(@class.Date, classTime.EndTime): yyyyMMddTHHmmss}");
                writer.WriteLine("END:VEVENT");
            }
        }

        private Task GenerateExcel(Stream stream)
        {
            var workbook = new Workbook("Навантаження");
            var sheet = workbook.CurrentWorksheet;
            sheet.AddNextCell("Номер");
            sheet.AddNextCell("Дата");
            sheet.AddNextCell("Пара");
            sheet.AddNextCell("Аудиторія");
            sheet.AddNextCell("Вид");
            sheet.AddNextCell("Підгрупа");
            sheet.AddNextCell("Група");
            sheet.AddNextCell("Дисципліна");
            foreach (var @class in _context.ClassModel
                         .Include(c => c.Discipline))
            {
                sheet.GoToNextRow();
                sheet.AddNextCell(@class.OrdinalNumber);
                sheet.AddNextCell(@class.Date);
                sheet.AddNextCell(@class.ClassNumber);
                sheet.AddNextCell(@class.Classroom);
                sheet.AddNextCell(@class.Discipline.Type);
                sheet.AddNextCell(@class.Discipline.SubGroup);
                sheet.AddNextCell(@class.Discipline.Group);
                sheet.AddNextCell(@class.Discipline.Name);
            }

            return workbook.SaveAsStreamAsync(stream, leaveOpen: true);
        }
    }
}
