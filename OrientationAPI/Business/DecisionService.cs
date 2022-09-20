using OrientationAPI.Data;
using OrientationAPI.Models;

namespace OrientationAPI.Business
{
    public class DecisionService : IDecisionService
    {
        private DecisionRepository _decisionRepository;
        private IDemandService _demandService;

        public DecisionService(DecisionRepository decisionRepository, IDemandService demandService)
        {
            _decisionRepository = decisionRepository;
            _demandService = demandService;
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

        public List<Decision> getDecisionsByUser(int userId)
        {
            var demands = _demandService.GetListByUserId(userId);
            List<Decision> decisions = new List<Decision>();
            foreach(var demand in demands)
            {
                if (demand.isEvaluate)
                {
                    decisions.Add(_decisionRepository.getByDemandId(demand.id));
                }
            }
            return decisions;
        }
    }
}
