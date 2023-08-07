using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jwtTokens.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace jwtTokens.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JwtAuthenticationController : ControllerBase
    {
        IMyAuthenticationService _myAuthenticationService = null;

        public JwtAuthenticationController (IMyAuthenticationService authenticationService)
        {
            _myAuthenticationService = authenticationService;
        }

        [HttpPost]
        public IActionResult RequestToken(UserPassword up)
        {
            string token = null;
            if(_myAuthenticationService.isAuthenticated(up, out token))
            {
                return Ok(token);
            }

            return Ok();

        }
    }
}