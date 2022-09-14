using OrientationAPI.Data;
using OrientationAPI.Models;

namespace OrientationAPI.Business
{
    public class DecisionService : IDecisionService
    {
        private DecisionRepository _decisionRepository;

        public DecisionService(DecisionRepository decisionRepository)
        {
            _decisionRepository = decisionRepository;
        }
        public void Add(Decision entity)
        {
            _decisionRepository.Add(entity);
            _decisionRepository.SaveAll();
        }

        public Decision get(int id)
        {
            return _decisionRepository.Get(id);
        }

        public Decision getByDemandId(int id)
        {
            return _decisionRepository.getByDemandId(id);
        }

        public List<Decision> GetAll()
        {
            return _decisionRepository.GetList();
        }
    }
}
