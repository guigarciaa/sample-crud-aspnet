using SampleCrud.Domain.Entities;

namespace SampleCrud.Domain.Repositories
{
    public interface IUserRepository
    {
        /// <summary>
        /// Adds a new user.
        /// </summary>
        /// <param name="user">The user to add.</param>
        void Add(User user);

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="user">The user to update.</param>
        void Update(User user);
    }
}