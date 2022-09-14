using OrientationAPI.Models;

namespace OrientationAPI.Data
{
    public class DecisionRepository : IAppRepository<Decision>
    {
        private OrientationContext _orientationContext;

        public DecisionRepository(OrientationContext orientationContext)
        {
            _orientationContext = orientationContext;
        }

        public void Add(Decision entity)
        {
            _orientationContext.Add(entity);
        }

        public Decision Get(int id)
        {
            return _orientationContext.decisions.FirstOrDefault(d => d.id == id);
        }

        public Decision getByDemandId(int id)
        {
            return _orientationContext.decisions.FirstOrDefault(d => d.demandId == id);
        }

        public List<Decision> GetList()
        {
            return _orientationContext.decisions.ToList();
        }

        public List<Decision> GetUncheckedDemandList()
        {

            return _orientationContext.decisions
                .Where(d => d.id == _orientationContext.demands.FirstOrDefault(a => a.isEvaluate == true).id).ToList();
        }

        public bool SaveAll()
        {
            return _orientationContext.SaveChanges() > 0;
        }
    }
}
