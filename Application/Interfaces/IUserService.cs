using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<UserResponseDto> RegisterAsync(CreateUserRequestDto request);
    }
}
