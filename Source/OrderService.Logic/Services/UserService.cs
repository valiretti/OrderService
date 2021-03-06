﻿using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FluentValidation;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OrderService.Model;
using OrderService.Model.Entities;

namespace OrderService.Logic.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _manager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IValidator<RegisterViewModel> _validator;

        public UserService(UserManager<User> manager, RoleManager<IdentityRole> roleManager, IValidator<RegisterViewModel> validator)
        {
            _manager = manager;
            _roleManager = roleManager;
            _validator = validator;
        }

        public async Task<string> Register(RegisterViewModel model)
        {
            var modelResult = _validator.Validate(model);
            if (!modelResult.IsValid)
            {
                throw new ValidationException(modelResult.Errors);
            }

            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email
            };

            var res = await _manager.FindByEmailAsync(model.Email);
            if (res != null)
            {
                throw new ValidationException($"The user with this email {model.Email} already exists");
            }

            var result = await _manager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                throw new ValidationException(result.Errors.First().Description);
            }

            await _manager.AddClaimAsync(user, new Claim(JwtClaimTypes.GivenName, user.FirstName));
            await _manager.AddClaimAsync(user, new Claim(JwtClaimTypes.FamilyName, user.LastName));
            await _manager.AddClaimAsync(user, new Claim(JwtClaimTypes.Email, user.Email));

            return user.Id;
        }

        public async Task SetExecutorRole(string id)
        {
            var role = await _roleManager.FindByNameAsync("Executor");
            if (role == null)
            {
                throw new ValidationException($"The role doesn't exist");
            }

            var user = await _manager.FindByIdAsync(id);
            if (user == null)
            {
                throw new ValidationException($"The user doesn't exist");
            }

            await _manager.AddToRoleAsync(user, role.Name);
        }

        public async Task ChangePassword(string userId, string password)
        {
            var user = await _manager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ValidationException($"The user doesn't exist");
            }

            user.PasswordHash = _manager.PasswordHasher.HashPassword(user, password);
            var result = await _manager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                throw new ValidationException(result.Errors.First().Description);
            }
        }

        public async Task ChangeProfile(UpdateProfileModel model)
        {
            var modelResult = _validator.Validate(model);
            if (!modelResult.IsValid)
            {
                throw new ValidationException(modelResult.Errors);
            }

            var user = await _manager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                throw new ValidationException($"The user doesn't exist");
            }

            var claims = await _manager.GetClaimsAsync(user);

            if (user.FirstName != model.FirstName)
            {
                await UpdateClaim(JwtClaimTypes.GivenName, model.FirstName);
                user.FirstName = model.FirstName;
            }

            if (user.LastName != model.LastName)
            {
                await UpdateClaim(JwtClaimTypes.FamilyName, model.LastName);
                user.LastName = model.LastName;
            }

            if (user.Email != model.Email)
            {
                var res = await _manager.FindByEmailAsync(model.Email);
                if (res != null)
                {
                    throw new ValidationException($"The user with this email {model.Email} already exists");
                }

                await UpdateClaim(JwtClaimTypes.Email, model.Email);
                user.Email = model.Email;
            }

            await _manager.UpdateAsync(user);

            async Task UpdateClaim(string claimType, string value)
            {
                var claim = claims.FirstOrDefault(c => c.Type == claimType);
                await _manager.RemoveClaimAsync(user, claim);
                await _manager.AddClaimAsync(user, new Claim(claimType, value));
            }
        }

        public async Task<int> GetExecutorIdByUserId(string userId)
        {
            var user = await _manager.Users
                .Include(u => u.Executor)
                .FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new ValidationException($"The user doesn't exist");
            }

            return user.Executor.Id;
        }
    }
}
