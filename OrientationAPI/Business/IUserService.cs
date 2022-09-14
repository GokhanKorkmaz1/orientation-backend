using OrientationAPI.Models;

namespace OrientationAPI.Business
{
    public interface IUserService:IAppService<User>
    {
        User GetByEmail(string email);
    }
}
