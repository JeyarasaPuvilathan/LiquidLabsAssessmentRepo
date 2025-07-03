using LiquidLabsApp.Data;
using LiquidLabsApp.Models;

namespace LiquidLabsApp.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly SqlUserRepository _repository;

        public UserService(HttpClient httpClient, SqlUserRepository repository)
        {
            _httpClient = httpClient;
            _repository = repository;
        }

        public async Task<User?> GetUserAsync(int id)
        {
            try
            {
                var user = await _repository.GetUserByIdAsync(id);
                if (user != null) return user;

                //GenAI
                var fetchedUser = await _httpClient.GetFromJsonAsync<User>($"https://jsonplaceholder.typicode.com/users/{id}");

                if (fetchedUser == null)
                {
                    throw new HttpRequestException("User not found in public API.");
                }

                await _repository.SaveUserAsync(fetchedUser);
                return fetchedUser;
            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException("Failed to fetch data from public API.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unhandled error in GetUserAsync.", ex);
            }
        }

        public Task<List<User>> GetAllUsersAsync() => _repository.GetAllUsersAsync();
    }
}
