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
    public class ReservasController : ControllerBase
    {

        private readonly IReservaService _reservaService;

        public ReservasController(IReservaService reservaService)
        {
            _reservaService = reservaService;
        }

        // GET: api/Reservas
        [HttpGet]
        [Authorize("Bearer")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Reserva>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetAll()
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET: api/Reservas/5
        [HttpGet("{id}")]
        [Authorize("Bearer")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Reserva>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetByIdSala([FromRoute] int id)
        {
            try
            {
                var reservas = _reservaService.GetByIdSala(id);

                if (reservas == null)
                {
                    return NoContent();
                }

                return Ok(reservas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST: api/Reservas/
        [HttpPost]
        [Authorize("Bearer")]
        [ProducesResponseType(200, Type = typeof(Reserva))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Post([FromBody] Reserva Reserva)
        {
            try
            {
                var res = _reservaService.Post(Reserva);

                if (res == null)
                {
                    return NoContent();
                }

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


    }
}