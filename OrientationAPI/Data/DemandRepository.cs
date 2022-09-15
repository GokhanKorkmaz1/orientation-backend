using OrientationAPI.Models;

namespace OrientationAPI.Data
{
    public class DemandRepository : IAppRepository<Demand>
    {
        private OrientationContext _orientationContext;

        public DemandRepository(OrientationContext orientationContext)
        {
            _orientationContext = orientationContext;
        }

        public void Add(Demand entity)
        {
            _orientationContext.Add(entity);
        }

        public void Update(Demand entity)
        {
            _orientationContext.Update(entity);
        }

        public Demand Get(int id)
        {
            return _orientationContext.demands.FirstOrDefault(d => d.id == id);
        }

        public List<Demand> GetListByUserId(int id)
        {
            return _orientationContext.demands.Where(d => d.userId == id).ToList();
        }

        public List<Demand> GetList()
        {
            return _orientationContext.demands.ToList();
        }

        public bool SaveAll()
        {
            return _orientationContext.SaveChanges() > 0;
        }
    }
}
