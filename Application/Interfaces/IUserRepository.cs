using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        Task AddUserAsync(User user);
        Task UpdateAsync(User user);
        Task<bool> DeleteUserAsync(User user);
        Task <User?> GetUserByIdAsync (Guid id);
        Task <User?> GetUserByEmailAsync (string email);  
    }
}
