using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        private IUserService _userService;
        private IDemandService _demandService;
        private IDocumentService _documentService;
        private IDecisionService _decisionService;

        public AdminsController(IUserService userService, IDemandService demandService, IDocumentService documentService, IDecisionService decisionService)
        {
            _userService = userService;
            _demandService = demandService;
            _documentService = documentService;
            _decisionService = decisionService; 
        }

        [HttpGet("demands")]
        public ActionResult getDemandsList()
        {
            //if ( LoginController.userRole != Constants.adminRole)
            //{
            //    return BadRequest("You must log in with an admin account.");
            //}
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
                return BadRequest("You must log in with an admin account.");
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
            return Ok(decision);
        }

        [HttpGet("decisions")]
        public ActionResult getDecisions()
        {
            List<Decision> decisions = _decisionService.GetAll();
            return Ok(decisions);
        }
    }
}
