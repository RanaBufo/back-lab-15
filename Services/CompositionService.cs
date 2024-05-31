using HandCrafter.DataBase;

namespace HandCrafter.Services
{
    public class CompositionService
    {
        private readonly ApplicationContext _db;

        public CompositionService(ApplicationContext db) => (_db) = (db);

        public List<CompositionDB> GetCompositionsService()
        {
            var allCompositions = _db.Compositions.ToList();
            return allCompositions;
        }
        public void AddCompositionService(string? Name)
        {
            var newComposition = new CompositionDB
            {
                Name = Name ?? "NewComposition"
            };
            _db.Compositions.Add(newComposition);
            _db.SaveChanges();
        }

        public void DeleteCompositionService(int? id)
        {
            var allCompositions = GetCompositionsService();
            foreach (var composition in allCompositions)
            {
                if (composition.Id == id)
                {
                    _db.Compositions.Remove(composition);
                    _db.SaveChanges();
                    break;
                }
            }
        }
    }
}
