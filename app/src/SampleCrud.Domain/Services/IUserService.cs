using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SampleCrud.Domain.Entities;

namespace SampleCrud.Domain.Services
{
    /// <summary>
    /// Represents a service for managing users.
    /// </summary>
    public interface IUserService
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