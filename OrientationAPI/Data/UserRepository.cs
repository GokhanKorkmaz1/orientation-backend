using OrientationAPI.Models;

namespace OrientationAPI.Data
{
    public class UserRepository : IAppRepository<User>
    {
        private OrientationContext _orientationContext;

        public UserRepository(OrientationContext orientationContext)
        {
            _orientationContext = orientationContext;
        }

        public void Add(User entity)
        {
            _orientationContext.Add(entity);
        }

        public User Get(int id)
        {
            return _orientationContext.users.FirstOrDefault(u => u.id == id && u.isAdmin == false);
        }

        public User GetByEmail(string email)
        {
            return _orientationContext.users.FirstOrDefault(u => u.email == email);
        }

        public List<User> GetList()
        {
            return _orientationContext.users.Where(u => u.isAdmin == false).ToList();
        }

        public bool SaveAll()
        {
            return _orientationContext.SaveChanges() > 0;
        }
    }
}
