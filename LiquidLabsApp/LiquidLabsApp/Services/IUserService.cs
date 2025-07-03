using LiquidLabsApp.Models;

namespace LiquidLabsApp.Services
{
    public interface IUserService
    {
        Task<User?> GetUserAsync(int id);
        Task<List<User>> GetAllUsersAsync();
    }
}
