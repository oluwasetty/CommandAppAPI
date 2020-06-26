using System;
using System.Collections.Generic;
using System.Linq;
using ApprovalApp.Models;
using ApprovalApp.Helpers;
using ApprovalApp.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;

namespace ApprovalApp.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly ApprovalAppContext _context;

        public UserRepository(ApprovalAppContext context)
        {
            _context = context;
        }

        public User AuthenticateUser(string EmailAddress, string Password)
        {
            Console.WriteLine(Password);
            if (string.IsNullOrEmpty(EmailAddress) || string.IsNullOrEmpty(Password))
                return null;

            User user = _context.Users.FirstOrDefault(p => p.EmailAddress == EmailAddress);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!VerifyPassword(Password, user.PasswordHash, user.PasswordSalt))
                return null;

            // authentication successful
            return user;
        }

        public void CreateUser(User user, string Password)
        {
            Console.WriteLine(Password);
            // validation
            if (string.IsNullOrWhiteSpace(Password))
                throw new AppException("Password is required");

            if (_context.Users.Any(x => x.EmailAddress == user.EmailAddress))
                throw new AppException("Email \"" + user.EmailAddress + "\" is already taken");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(Password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Users.Add(user);
        }

        public void DeleteUser(User user)
        {
            if (user == null)
                throw new System.NotImplementedException();
            _context.Users.Remove(user);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateUser(User user)
        {
            // throw new System.NotImplementedException();
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)); // Create hash using password salt.
                for (int i = 0; i < computedHash.Length; i++)
                { // Loop through the byte array
                    if (computedHash[i] != passwordHash[i]) return false; // if mismatch
                }
            }
            return true; //if no mismatches.
        }
    }
}