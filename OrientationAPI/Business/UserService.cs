using OrientationAPI.Data;
using OrientationAPI.Models;

namespace OrientationAPI.Business
{
    public class UserService : IUserService
    {
        private UserRepository _userRepository;
        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public void Add(User entity)
        {
            if(entity.id != 0)
            {
                return;
            }
            if(_userRepository.GetByEmail(entity.email) != null)
            {
                return;
            }
            _userRepository.Add(entity);
            _userRepository.SaveAll();
        }

        public User get(int id)
        {
            return _userRepository.Get(id);
        }

        public List<User> GetAll()
        {
            return _userRepository.GetList();
        }

        public User GetByEmail(string email)
        {
            return _userRepository.GetByEmail(email);
        }
    }
}
