using Application.Dtos;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

// El servicio crea y orquesta entidades.
namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<UserResponseDto> RegisterAsync(CreateUserRequestDto request)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(request.Email);

            if(existingUser != null)
            {
                throw new EmailAlreadyExists($"El email {request.Email} ya está registrado.");
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email,
                PasswordHash = _passwordHasher.Hash(request.Password)
            };

            await _userRepository.AddUserAsync(user);

            return new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
        }

        public async Task<UserResponseDto?> GetUserByIdAsync(Guid id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);

            if (user == null) throw new UserNotFoundException($"No se encontró el usuario con id: {id}");

            return new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
        }


        public async Task<Loginresponse> LoginAsync(LoginRequestDto request)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.Email);
            if(user == null || !_passwordHasher.Verify(request.Password, user.PasswordHash))
            {
                throw new InvalidCredentialsException();
            }

            return new Loginresponse
            {
                UserId = user.Id,
                UserName = user.Name,
                Email = user.Email,
                Token = _jwtTokenGenerator.GenerateToken(user)
            };
        }


        public async Task<UserResponseDto> UpdateUserAsync(Guid id ,UpdateUserDto updatedUser)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if(user == null) throw new UserNotFoundException($"No se encontró el usuario con id: {id}");

            var existingUser = await _userRepository.GetUserByEmailAsync(updatedUser.Email);
            if (existingUser != null && existingUser.Id != id) throw new EmailAlreadyExists();
           
            user.Name = updatedUser.Name;
            user.Email = updatedUser.Email;

            await _userRepository.UpdateAsync(user);

            return new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
        }
    }
}
