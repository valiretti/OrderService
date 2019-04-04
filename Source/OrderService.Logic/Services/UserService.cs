using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FluentValidation;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
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
    }
}
