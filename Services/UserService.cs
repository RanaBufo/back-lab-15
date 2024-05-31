using HandCrafter.DataBase;
using HandCrafter.Migrations;
using HandCrafter.Model;
using System.Data.Entity;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net;

namespace HandCrafter.Services
{
    public class UserService
    {
        private readonly ApplicationContext _db;
        public UserService(ApplicationContext db) => (_db) = (db);

        public void AddUserService(UseresRequestModel newUser)
        {
            var user = new UserDB
            {
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                Patronymic = newUser.Patronymic,
                Description = newUser.Description,
                Birthday = newUser.Birhday,
                Contact = new ContactDB
                {
                    Email = newUser.Contact.Email,
                    Password = newUser.Contact.Password,
                    Phone = newUser.Contact.Phone,
                    IdRole = newUser.Contact.IdRole
                },
                Address = newUser.Address
                ?.Select(a => new AddressDB
                {
                    Country = a.Country,
                    City = a.City,
                    Street = a.Street,
                    Region = a.Region,
                    House = a.House,
                    Entrance = a.Entrance,
                    Room = a.Room,
                    Intercom = a.Intercom

                }).ToList()
            };
            _db.Users.Add(user);
            _db.SaveChanges();
        }

        public List<UserDB> GetUsersService()
        {
            var users = _db.Users
            .Include(u => u.Contact)
            .Include(u => u.Address)
            .Select(u => new UserDB
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Patronymic = u.Patronymic,
                Description = u.Description,
                Birthday = u.Birthday,
                Contact = u.Contact != null ? new ContactDB
                {
                    Email = u.Contact.Email,
                    Phone = u.Contact.Phone,
                    IdRole = u.Contact.IdRole,
                    Role = u.Contact.Role
                } : null,
                Address = u.Address != null ? u.Address.Select(a => new AddressDB
                {
                    ID = a.ID,
                    Country = a.Country,
                    Region = a.Region,
                    City = a.City,
                    Street = a.Street,
                    House = a.House,
                    Entrance = a.Entrance,
                    Room = a.Room,
                    Intercom = a.Intercom
                }).ToList() : new List<AddressDB>()
            }).ToList();

            return users;
        }

        public UserDB GetUserService(int id)
        {
            var user = _db.Users
                .Include(u => u.Contact)
                .GroupBy(u => u.Id)
                .Select(u => new UserDB
                {
                    Id = u.Select(u => u.Id).FirstOrDefault(),
                    FirstName = u.Select(u => u.FirstName).FirstOrDefault(),
                    LastName = u.Select(u => u.LastName).FirstOrDefault(),
                    Patronymic = u.Select(u => u.Patronymic).FirstOrDefault(),
                    Description = u.Select(u => u.Description).FirstOrDefault(),
                    Birthday = u.Select(u => u.Birthday).FirstOrDefault(),
                    Contact = new ContactDB
                    {
                        Email = u.Select(u => u.Contact.Email).FirstOrDefault(),
                        Password = u.Select(u => u.Contact.Password).FirstOrDefault(),
                        Phone = u.Select(u => u.Contact.Phone).FirstOrDefault(),
                        IdRole = u.Select(u => u.Contact.IdRole).FirstOrDefault(),
                        Role = u.Select(u => u.Contact.Role).FirstOrDefault()
                    }
                }).FirstOrDefault(j => j.Id == id);
            return user;
        }

    }
}
