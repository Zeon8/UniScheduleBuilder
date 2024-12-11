using System.Text;
using UniScheduleBuilder;

const int DaysPerWeek = 7;

var filePath = args[0];
var content = File.ReadAllText(filePath);

string[] rows = content.Replace("\r", string.Empty).Split('\n');

var builder = new StringBuilder();
builder.AppendLine("BEGIN:VCALENDAR");
builder.AppendLine("NAME:Schedule");
for (int i = 1; i < rows.Length; i++)
{
    string row = rows[i];
    if (string.IsNullOrEmpty(row))
        continue;

    int pos = 0;
    string number = ReadUntil(row, ',', ref pos);
    string dateStr = ReadUntil(row, ',', ref pos);
    DateOnly date = DateOnly.Parse(dateStr);
    int order = int.Parse(ReadUntil(row, ',', ref pos));
    string classroom = ReadUntil(row, ',', ref pos);
    string group = ReadUntil(row, ',', ref pos);
    string type = ReadUntil(row, ',', ref pos);

    string name;
    if (row[pos] == '"')
    {
        pos++;
        name = ReadUntil(row, '"', ref pos);
    }
    else
        name = row[pos..row.Length];


    string summary = $"{order}п {name} {type}-{number}";
    if(!string.IsNullOrEmpty(classroom))
        summary += $", {classroom}";
    if (!string.IsNullOrEmpty(group))
        summary += $" {group}г";

    var classTime = ClassTime.GetClassTime(order);
    builder.AppendLine("BEGIN:VEVENT");
    builder.AppendLine($"SUMMARY: {summary}");
    builder.AppendLine($"DTSTART:{new DateTime(date, classTime.StartTime): yyyyMMddTHHmmss}");
    builder.AppendLine($"DTEND:{new DateTime(date, classTime.EndTime): yyyyMMddTHHmmss}");
    builder.AppendLine("END:VEVENT");


}
builder.AppendLine("END:VCALENDAR");


var path = Path.Combine(Path.GetDirectoryName(filePath) ?? string.Empty,
    Path.GetFileNameWithoutExtension(filePath) + ".ics");
File.WriteAllText(path, builder.ToString());

static string ReadUntil(string str, char separator, ref int i)
{
    int start = i;
    for (; i < str.Length; i++)
    {
        if (str[i] == separator)
        {
            return str[start..i++];
        }
    }
    return string.Empty;
}