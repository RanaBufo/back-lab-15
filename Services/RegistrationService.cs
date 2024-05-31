using HandCrafter.Model;

namespace HandCrafter.Services
{
    public class RegistrationService
    {
        private readonly ApplicationContext _db;

        public RegistrationService(ApplicationContext db) => (_db) = (db);

        public int LoginService(ContactRequestModel contact)
        {
            var users = _db.Contacts
                .Where(c => c.Password == contact.Password)
                .Where(c => contact.Email == c.Email || contact.Phone == c.Phone || contact.Phone == c.Email || contact.Email == c.Phone)
                .ToList();
            if(users.Count == 0) { 
                return 0;
            }
            int id = users[0].IdUser;
            return id;
        }
        public int RegistrationUserService(ContactRequestModel contact)
        {
            var users = _db.Contacts
                .Where(c => c.Password == contact.Password)
                .Where(c => contact.Email == c.Email || contact.Phone == c.Phone)
                .ToList();
            int id = users[0].IdUser;
            return id;
        }
    }
}
