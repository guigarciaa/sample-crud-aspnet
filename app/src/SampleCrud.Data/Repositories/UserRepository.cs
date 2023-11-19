using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SampleCrud.Domain.Entities;
using SampleCrud.Domain.Repositories;
using SampleCrud.Infra.Data.Context;

namespace SampleCrud.Data.Repositories
{
    /// <summary>
    /// Represents a repository for managing user data.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;

        private readonly SampleCrudDbContext _context;

        public UserRepository(ILogger<UserRepository> logger, SampleCrudDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// Adds a new user to the repository.
        /// </summary>
        /// <param name="user">The user to be added.</param>
        /// <exception cref="Exception">Thrown when there is an error adding the user.</exception>
        public void Add(User user)
        {
            try
            {
                _logger.LogInformation($"Adding user: {user}");
                _context.User.Add(user);
                _context.SaveChanges();
                _logger.LogInformation($"User added: {user}");
            }
            catch (Exception e)
            {
                _logger.LogError($"Data: {user} Error: {e}");
                throw new Exception("Error adding user!");
            }
        }

        /// <summary>
        /// Retrieves a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>The user with the specified ID, or null if not found.</returns>
        public Task<User?> GetById(Guid? id)
        {
            try
            {
                _logger.LogInformation($"Getting user by id: {id}");
                var user = _context.User.FirstOrDefaultAsync(x => x.Id == id);
                _logger.LogInformation($"User found: {user}");
                return user;
            }
            catch (Exception e)
            {
                _logger.LogError($"Data: {id} Error: {e}");
                throw new Exception("Error getting user by id!");
            }
        }

        /// <summary>
        /// Retrieves a collection of users from the database.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the collection of users.</returns>
        public async Task<IEnumerable<User>> GetUsers()
        {
            try
            {
                _logger.LogInformation($"Getting users");
                var users = await _context.User.ToListAsync();
                _logger.LogInformation($"Users found: {users}");
                return users;
            }
            catch (Exception e)
            {
                _logger.LogError($"Data: {e}");
                throw new Exception("Error getting users!");
            }
        }

        /// <summary>
        /// Updates the specified user in the database.
        /// </summary>
        /// <param name="user">The user to be updated.</param>
        /// <exception cref="Exception">Thrown when there is an error updating the user.</exception>
        public void Update(User user)
        {
            try
            {
                _logger.LogInformation($"Updating user: {user}");
                _context.User.Update(user);
                _context.SaveChanges();
                _logger.LogInformation($"User updated: {user}");
            }
            catch (Exception e)
            {
                _logger.LogError($"Data: {user} Error: {e}");
                throw new Exception("Error updating user!");
            }
        }
    }
}