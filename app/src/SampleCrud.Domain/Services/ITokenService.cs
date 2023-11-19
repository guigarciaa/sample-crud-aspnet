using SampleCrud.Domain.Entities;

namespace SampleCrud.Domain.Services
{
    /// <summary>
    /// Represents a service for generating JWT tokens.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Generates a JWT token for the specified user.
        /// </summary>
        /// <param name="user">The user for whom the token is generated.</param>
        /// <returns>The generated JWT token.</returns>
        string GenerateJwtToken(User user);
    }
}