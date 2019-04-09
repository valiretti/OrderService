using System.Threading.Tasks;
using OrderService.Model;

namespace OrderService.Logic.Services
{
    public interface IUserService
    {
        Task<string> Register(RegisterViewModel model);

        Task SetExecutorRole(string id);

        Task ChangePassword(string userId, string password);

        Task ChangeProfile(UpdateProfileModel model);

        Task<int> GetExecutorIdByUserId(string userId);
    }
}
