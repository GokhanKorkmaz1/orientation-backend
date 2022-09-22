using Microsoft.AspNetCore.Mvc;
using OrientationAPI.Business;
using OrientationAPI.Helpers;
using OrientationAPI.Models;
using OrientationAPI.Models.Dtos;

namespace OrientationAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private IDemandService _demandService;
        private IDecisionService _decisionService;

        public UsersController(IUserService userService, IDemandService demandService, IDecisionService decisionService)
        {
            _userService = userService;
            _demandService = demandService;
            _decisionService = decisionService;
        }

        [HttpPost("register")]
        public ActionResult Register([FromBody] RegisterUser registerUser)
        {
            if (LoginController.userRole != Constants.guestRole)
            {
                return Unauthorized("You must log out of your account.");
            }

            if (_userService.GetByEmail(registerUser.email) != null)
            {
                return BadRequest("This address has already been taken.");
            }

            User user = new User
            {
                name = registerUser.name,
                surname = registerUser.surname,
                email = registerUser.email,
                phoneNumber = registerUser.phoneNumber,
                password = registerUser.password,
                isAdmin = false
            };

            _userService.Add(user);
            return StatusCode(201,user);

        }

        [HttpGet("{id}")]
        public ActionResult get(int id)
        {
            if (LoginController.userRole == Constants.guestRole)
            {
                return Unauthorized("You must log in with an account.");
            }

            User user = _userService.get(id);
            if (user == null)
            {
                return NotFound("There is no user registered with this id.");
            }
            return Ok(user);
        }

        [HttpGet("demand/{id}")]
        public ActionResult getDemandById(int id)
        {
            var demand = _demandService.get(id);

            if(demand == null)
            {
                return NotFound("Demand not found");
            }


            return Ok(demand);
        }

        [HttpPost("demand")]
        public async Task<ActionResult> createDemand([FromForm] CreateDemandDto createDemandDto)
        {
            var document = await _demandService.UploadDocumentAsync(createDemandDto.document);

            if (document.Length < 1)
            {
                return BadRequest("Document upload processing failed");
            }

            Demand demand = new Demand
            {
                userId = LoginController.userId,
                name = createDemandDto.name,
                surname = createDemandDto.surname,
                description = createDemandDto.description,
                uploadTime = DateTime.Now,
                isEvaluate = false,
                document = document

            };

            _demandService.Add(demand);
            return Ok(demand);
        }

        [HttpGet("demands/{userId}")]
        public ActionResult getDemandsByUserId(int userId)
        {
            if (LoginController.userRole == Constants.guestRole)
            {
                return Unauthorized("You must log in with an account.");
            }

            var demands = _demandService.GetListByUserId(userId);
                    
            return Ok(demands);
        }


        [HttpGet("decisions/{id}")]
        public ActionResult getDemandsWithDecisions(int id)
        {
            if (LoginController.userRole == Constants.guestRole)
            {
                return Unauthorized("You must log in with an account.");
            }

            List<ResponseDemandDecision> responseDemandDecisions = new List<ResponseDemandDecision>();
            var demands= _demandService.GetListByUserId(id);
            foreach(var demand in demands){
                var decision = _decisionService.getByDemandId(demand.id);

                if (decision == null)
                    return NotFound("Decision not found.");

                ResponseDemandDecision responseDemandDecision = new ResponseDemandDecision
                {
                    demand = demand,
                    isApproved = decision.isApproved,
                    decisionTime = decision.decisionTime
                };
                responseDemandDecisions.Add(responseDemandDecision);
            }
            return Ok(responseDemandDecisions);
        }

    }
}
