using LiquidLabsApp.Exceptions;
using LiquidLabsApp.Models;
using System.Data.SqlClient;

namespace LiquidLabsApp.Data
{
    public class SqlUserRepository
    {
        private readonly string _connectionString;

        public SqlUserRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")!;
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            try
            {
                using var conn = new SqlConnection(_connectionString);
                await conn.OpenAsync();

                var cmd = new SqlCommand("SELECT Id, Name, Username, Email FROM Users WHERE Id = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);

                using var reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    return new User
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Username = reader.GetString(2),
                        Email = reader.GetString(3)
                    };
                }

                return null;
            }
            catch (SqlException ex)
            {
                throw new DataAccessException("Database error in GetUserByIdAsync.", ex);
            }
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            try
            {
                var users = new List<User>();

                using var conn = new SqlConnection(_connectionString);
                await conn.OpenAsync();

                var cmd = new SqlCommand("SELECT Id, Name, Username, Email FROM Users", conn);
                using var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    users.Add(new User
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Username = reader.GetString(2),
                        Email = reader.GetString(3)
                    });
                }

                return users;
            }
            catch (SqlException ex)
            {
                throw new DataAccessException("Database error in GetAllUsersAsync.", ex);
            }
        }

        public async Task SaveUserAsync(User user)
        {
            try
            {
                using var conn = new SqlConnection(_connectionString);
                await conn.OpenAsync();

                var cmd = new SqlCommand(
                    "INSERT INTO Users (Id, Name, Username, Email) VALUES (@Id, @Name, @Username, @Email)", conn);
                cmd.Parameters.AddWithValue("@Id", user.Id);
                cmd.Parameters.AddWithValue("@Name", user.Name);
                cmd.Parameters.AddWithValue("@Username", user.Username);
                cmd.Parameters.AddWithValue("@Email", user.Email);

                await cmd.ExecuteNonQueryAsync();
            }
            catch (SqlException ex)
            {
                throw new DataAccessException("Database error in SaveUserAsync.", ex);
            }
        }
    }
}
