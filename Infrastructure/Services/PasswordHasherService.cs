using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Services
{
    public class PasswordHasherService : IPasswordHasher
    {
        public string Hash(string password) => BCrypt.Net.BCrypt.HashPassword(password);

        public bool Verify(string password, string hashedPassword) => BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}
