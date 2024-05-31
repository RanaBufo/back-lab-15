using System.Collections.Generic;
using System.Threading.Tasks;
using HandCrafter.DataBase;
using Microsoft.EntityFrameworkCore;

namespace HandCrafter.Services
{
    public class CategoryService
    {
        private readonly ApplicationContext _db;

        public CategoryService(ApplicationContext db) => (_db) = (db);

        public List<CategoryDB> GetCategoryService()
        {
            var allCategories =  _db.Categories.ToList();
            return allCategories;
        }
        public void AddCategoryService(string? Name)
        {
            var newCategory = new CategoryDB
            {
                Name = Name ?? "NewCategory"
            };
            _db.Categories.Add(newCategory);
            _db.SaveChanges();
        }

        public void DeleteCategoryService(int? id)
        {
            var allCategories = GetCategoryService();
            foreach (var category in allCategories)
            {
                if (category.Id == id)
                {
                    _db.Categories.Remove(category);
                    _db.SaveChanges();
                    break;
                }
            }
        }
    }
}