using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeetingRoom.Models;
using MeetingRoom.Models.Helpers;
using MeetingRoom.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeetingRoom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoasController : ControllerBase
    {

        private readonly IPessoaService _pessoaService;
        public PessoasController(IPessoaService pessoaService)
        {
            _pessoaService = pessoaService;
        }

        // POST: api/Pessoas
        [HttpPost]
        [Authorize("Bearer")]
        [ProducesResponseType(200, Type = typeof(Pessoa))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult AlterarSenha([FromBody] PostUpdateSenha postUpdateSenha)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(postUpdateSenha.senhaAtual) || string.IsNullOrWhiteSpace(postUpdateSenha.novaSenha))
                {
                    throw new ArgumentException("Senha atual ou nova senha não fornecida.");
                }

                var pessoa = _pessoaService.UpdatePassword(postUpdateSenha.senhaAtual, postUpdateSenha.novaSenha, postUpdateSenha.email);

                if (pessoa == null)
                {
                    return NoContent();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}