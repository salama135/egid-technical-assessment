using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockExchange.Server.Authentication;
using StockExchange.Server.DTOs;
using StockExchange.Server.Model;
using StockExchange.Server.Services;

namespace StockExchange.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IJwtAuth jwtAuth;
        private readonly IUserService userService;

        public UserController(IJwtAuth jwtAuth, IUserService userService)
        {
            this.jwtAuth = jwtAuth;
            this.userService = userService;
        }

        // GET: api/<MembersController>
        [HttpGet]
        public ActionResult<IEnumerable<UserDto>> AllMembers()
        {
            return userService.GetMembers();
        }

        // GET api/<MembersController>/5
        [HttpGet("{id}")]
        public ActionResult<UserDto> MemberByid(Guid id)
        {
            var user = userService.GetMembers().Find(x => x.Id.Equals(id));
            
            if(user == null) return NotFound();

            return user;
        }

        [AllowAnonymous]
        // POST api/<MembersController>
        [HttpPost("authentication")]
        public IActionResult Authentication([FromBody] UserCredential userCredential)
        {
            var token = jwtAuth.Authentication(userCredential.Username, userCredential.Password);
            if (token == null)
                return Unauthorized();
            return Ok(token);
        }
    }
}
