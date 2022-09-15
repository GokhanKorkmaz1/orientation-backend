using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrientationAPI.Business;
using OrientationAPI.Data;
using OrientationAPI.Helpers;
using OrientationAPI.Models;
using OrientationAPI.Models.Dtos;
using System.IO.Compression;
using System.Reflection.Metadata;
using System.Text.Json;
using Document = OrientationAPI.Models.Document;

namespace OrientationAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private IDemandService _demandService;
        private IDocumentService _documentService;
        private IDecisionService _decisionService;
        private LoginController _loginController;

        public UsersController(IUserService userService, IDemandService demandService, IDocumentService documentService, LoginController loginController, IDecisionService decisionService)
        {
            _userService = userService;
            _demandService = demandService;
            _documentService = documentService;
            _loginController = loginController;
            _decisionService = decisionService;
        }

        [HttpPost("register")]
        public ActionResult Register([FromBody] RegisterUser registerUser)
        {
            //if(LoginController.userRole != Constants.guestRole)
            //{
            //    return BadRequest("You must log out of your account.");
            //}

            //if (_userService.GetByEmail(createUserDto.email) != null)
            //{
            //    return BadRequest("This address has already been taken.");
            //}

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
            LoginUserDto loginUserDto = new LoginUserDto
            {
                email = user.email,
                password = user.password
            };

            _loginController.Login(loginUserDto);
            return StatusCode(201,user);

        }

        [HttpGet("{id}")]
        public ActionResult get(int id)
        {
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

            //if (document.Length < 1)  
            //{
            //    return BadRequest("Document upload processing failed");
            //}

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
            //if (LoginController.userRole != Constants.userRole)
            //{
            //    return BadRequest("You must log in with a user account.");
            //}

            var demands = _demandService.GetListByUserId(userId);
                    
            return Ok(demands);
        }


        [HttpGet("decisions/{id}")]
        public ActionResult getDemandsWithDecisions(int id)
        {
            //if (LoginController.userRole != Constants.userRole)
            //{
            //    return BadRequest("You must log in with a user account.");
            //}

            List<ResponseDemandDecision> responseDemandDecisions = new List<ResponseDemandDecision>();
            var demands= _demandService.GetListByUserId(id);
            foreach(var demand in demands){
                var decision = _decisionService.getByDemandId(demand.id);
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

        //[HttpPost("document")]
        //public async Task<ActionResult> uploadDocument([FromForm] UploadDocumentDto uploadDocumentDto)
        //{

        //    Document uploadedDocument = await _documentService.UploadDocument(uploadDocumentDto);
        //    if(uploadedDocument == null)
        //    {
        //        return BadRequest("The document could not be loaded.");
        //    }

        //    _documentService.Add(uploadedDocument);
        //    return Ok(uploadedDocument);
        //}

    }
}
