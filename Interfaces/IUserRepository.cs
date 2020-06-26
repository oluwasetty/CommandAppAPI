using System.Collections.Generic;
using ApprovalApp.Models;

namespace ApprovalApp.Interfaces
{
    public interface IUserRepository
    {
        bool SaveChanges();

        IEnumerable<User> GetAllUsers();
        User GetUserById(int id);
        User AuthenticateUser(string EmailAddress, string Password);
        void CreateUser(User user, string Password);
        void UpdateUser(User user);
        void DeleteUser(User user);
    }
}