using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeetingRoom.Models;
using MeetingRoom.Service;
using MeetingRoom.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeetingRoom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalasController : ControllerBase
    {

        private readonly ISalaService _salaService;

        public SalasController(ISalaService salaService)
        {
            _salaService = salaService;
        }

        // GET: api/Salas
        [HttpGet]
        [Authorize("Bearer")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Sala>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_salaService.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}