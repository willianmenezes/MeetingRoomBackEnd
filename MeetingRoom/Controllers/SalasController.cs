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
                var salas = _salaService.GetAll();

                if (salas == null)
                {
                    return NoContent();
                }

                return Ok(salas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET: api/Salas/6
        [HttpGet("{id}")]
        [Authorize("Bearer")]
        [ProducesResponseType(200, Type = typeof(Sala))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetById([FromRoute] int id)
        {
            try
            {
                var sala = _salaService.GetById(id);

                if (sala == null)
                {
                    return NoContent();
                }

                return Ok(sala);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // Post: api/Salas/
        [HttpPost]
        [Authorize("Bearer")]
        [ProducesResponseType(200, Type = typeof(Sala))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult PostSala([FromBody] Sala sala)
        {
            try
            {
                if (sala == null)
                {
                    throw new Exception("Sala não fornecida para insersão.");
                }

                var salaInserida = _salaService.InsertSala(sala);

                if (salaInserida.NidSala <= 0)
                {
                    return NoContent();
                }

                return Ok(salaInserida);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}