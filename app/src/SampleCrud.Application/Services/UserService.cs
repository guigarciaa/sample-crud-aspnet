using Microsoft.Extensions.Logging;
using SampleCrud.Domain.Entities;
using SampleCrud.Domain.Services;

namespace SampleCrud.Application.Services
{
    public class UserService : IUserService
    {
        #region Properties

        private readonly ILogger<UserService> _logger;
        private readonly IUserService _userRepository;

        #endregion

        #region Constructor
        public UserService(ILogger<UserService> logger, IUserService userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }
        #endregion

        #region Methods

        public void Add(User user)
        {
            try
            {
                _userRepository.Add(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding user.");
                throw;
            }
        }

        public void Update(User user)
        {
            try
            {
                _userRepository.Update(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user.");
                throw;
            }
        }

        #endregion
    }
}