using Wuzzfny.Domain.Interfaces;
using Wuzzfny.Domain.Models.Users;

namespace Wuzzfny.Application
{
    public class RepositoryWrapper
    {
        private readonly IRepository<User, long> _userRepository;

        public RepositoryWrapper(IRepository<User, long> userRepository)
        {
            _userRepository = userRepository;
        }

        public IRepository<User, long> UserRepository => _userRepository;
    }
}
