using ModelHolder.Dto;
using ModelHolder.Models;
using System;
using System.Linq;

namespace EventManager.Service.Parsers
{
    public class ParsersStorage
    {

        public static EventDto ParseEvent(string message)
        {
            var lines = message.Split('\n');

            string title = lines.First(l => l.StartsWith("Название:")).Replace("Название:", "").Trim();
            string? description = lines.First(l => l.StartsWith("Описание:")).Replace("Описание:", "").Trim();
            string locationText = lines.First(l => l.StartsWith("Локация:")).Replace("Локация:", "").Trim();
            var locationStr = lines.First(l => l.StartsWith("Локация")).Replace("Локация:", "").Trim();
            var location = new Location(decimal.Parse(locationStr.Split(' ')[0]), decimal.Parse(locationStr.Split(' ')[1]));
            DateTime date = DateTime.Parse(lines.First(l => l.StartsWith("Дата:")).Replace("Дата:", "").Trim());
            bool? isPaid = lines.First(l => l.StartsWith("Платно:")).Contains("да", StringComparison.OrdinalIgnoreCase);
            int limit = int.Parse(lines.First(l => l.StartsWith("Лимит:")).Replace("Лимит:", "").Trim());
            long categoryId = long.Parse(lines.First(l => l.StartsWith("Категория:")).Replace("Категория:", "").Trim());

            return new EventDto(title, description, location, date, isPaid, limit, categoryId);
        }
    }
}
