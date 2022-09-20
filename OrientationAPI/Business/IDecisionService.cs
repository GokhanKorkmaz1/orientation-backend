using OrientationAPI.Models;

namespace OrientationAPI.Business
{
    public interface IDecisionService:IAppService<Decision>
    {
        Decision getByDemandId(int id);
        List<Decision> getDecisionsByUser(int userId);
    }
}
