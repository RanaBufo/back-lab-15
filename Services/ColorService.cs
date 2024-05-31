using HandCrafter.DataBase;
using Microsoft.EntityFrameworkCore;

namespace HandCrafter.Services
{
    public class ColorService
    {
        private readonly ApplicationContext _db;

        public ColorService(ApplicationContext db) => (_db) = (db);

        public List<ColorDB> GetColorsService()
        {
            var allColors = _db.Colors.ToList();
            return allColors;
        }
        public void AddColorService(string? Name)
        {
            var newColor = new ColorDB
            {
                Name = Name ?? "NewColor"
            };
            _db.Colors.Add(newColor);
            _db.SaveChanges();
        }

        public void DeleteColorService(int? id)
        {
            var allColors = GetColorsService();
            foreach (var color in allColors)
            {
                if (color.Id == id)
                {
                    _db.Colors.Remove(color);
                    _db.SaveChanges();
                    break;
                }
            }
        }
    }
}
