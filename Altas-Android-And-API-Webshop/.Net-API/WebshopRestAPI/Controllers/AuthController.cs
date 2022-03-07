using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebshopAPI.Core.ApplicationService;
using WebshopAPI.Core.Entity;

namespace WebshopRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthenticationService _authenticationService;
        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        //Post api/auth/register
        [HttpPost]
        [Route("register")]
        public User Register([FromBody] LoginInput User)
        {
            return _authenticationService.RegisterUser(User);
        }

        //Post api/auth/login
        [HttpPost]
        [Route("login")]
        public User Login([FromBody] LoginInput User)
        {
            return _authenticationService.LoginUser(User);
        }
    }
}