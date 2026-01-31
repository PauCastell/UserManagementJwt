using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Services
{
    public class PasswordHasherService : IPasswordHasher
    {
        public string Hash(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(16);

            byte[] hashBytes = SHA256.HashData([.. Encoding.UTF8.GetBytes(password), .. salt]);

            byte[] hashWithSalt = salt.Concat(hashBytes).ToArray();

            return Convert.ToBase64String(hashWithSalt);
        }

        //TODO: Implementar este método
        public bool Verify(string password, string hashedPassword)
        {
            throw new NotImplementedException();
        }
    }
}
