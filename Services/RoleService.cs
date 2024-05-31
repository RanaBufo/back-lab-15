using HandCrafter.DataBase;
using Microsoft.EntityFrameworkCore;

namespace HandCrafter.Services
{
    public class RoleService
    {
        private readonly ApplicationContext _db;

        public RoleService(ApplicationContext db) => (_db) = (db);
        
        public async Task<List<RoleDB>> GetRolsService()
        {
            var allRoles = await _db.Roles.ToListAsync();
            return allRoles;
        }
        public async Task AddRoleService(string? Name) 
        {
            var newRole = new RoleDB
            {
                Name = Name ?? "NewRole"
            };
            _db.Roles.Add(newRole);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteRoleService(int? id)
        {
            var allRoles = await GetRolsService();
            foreach (var role in allRoles)
            {
                if (role.Id == id)
                {
                    _db.Roles.Remove(role);
                    await _db.SaveChangesAsync();
                    break;
                }
            }
        }

    }
}
