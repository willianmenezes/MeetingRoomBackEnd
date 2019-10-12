using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeetingRoom.Security;
using MeetingRoom.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeetingRoom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        // POST: api/Login
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public object Login([FromBody] CredencialAcesso credencial)
        {
            try
            {
                if (credencial == null)
                {
                    return BadRequest("Credenciais não fornecidas.");
                }

                return Ok(_loginService.GetByLogin(credencial));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);  
            }
        }
    }
}