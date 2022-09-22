using Microsoft.AspNetCore.Mvc;
using OrientationAPI.Business;
using OrientationAPI.Helpers;
using OrientationAPI.Models;
using OrientationAPI.Models.Dtos;

namespace OrientationAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private IDemandService _demandService;
        private IDecisionService _decisionService;

        public AdminsController(IDemandService demandService, IDecisionService decisionService)
        {
            _demandService = demandService;
            _decisionService = decisionService; 
        }

        [HttpGet("demands")]
        public ActionResult getDemandsList()
        {
            if (LoginController.userRole != Constants.adminRole)
            {
                return Unauthorized("You must log in with an admin account.");
            }
            List<Demand> demands = _demandService.GetAll();
            return Ok(demands);
        }

        [HttpGet("demand/{id}")]
        public ActionResult getDemandDetailById(int id)
        {

            Demand demand = _demandService.get(id);
            if(demand == null)
            {
                return NotFound("Demand not found.");
            }
            return Ok(demand);
        }

        [HttpPost("decision")]
        public ActionResult decisionSelectedDemand([FromBody]CreateDecisionDto createDecisionDto)
        {
            if (LoginController.userRole != Constants.adminRole)
            {
                return Unauthorized("You must log in with an admin account.");
            }

            Demand demand = _demandService.get(createDecisionDto.demandId);
            if(demand == null)
            {
                return NotFound("Demand not found.");
            }
            
            Decision decision = new Decision
            {
                demandId = createDecisionDto.demandId,
                description = createDecisionDto.description,
                isApproved = createDecisionDto.isApproved,
                decisionTime = DateTime.Now
            };

            _decisionService.Add(decision);
            demand.isEvaluate = true;
            _demandService.Update(demand);
            return Ok(decision);
        }

        [HttpGet("decisions")]
        public ActionResult getDecisions()
        {
            if (LoginController.userRole == Constants.guestRole)
            {
                return Unauthorized("You must log in with an account.");
            }

            List<Decision> decisions = _decisionService.GetAll();
            return Ok(decisions);
        }

        [HttpGet("{id}/decisions")]
        public ActionResult getDecisionsById(int id)
        {
            if (LoginController.userRole == Constants.guestRole)
            {
                return Unauthorized("You must log in with an account.");
            }

            List<Decision> decisions = _decisionService.getDecisionsByUser(id);
            return Ok(decisions);
        }
    }
}
