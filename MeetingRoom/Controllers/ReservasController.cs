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
                var reservas = _reservaService.GetByIdSala(id, DateTime.Now);

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

        // POST: api/Reservas/Data/5
        [HttpPost("Data/{id}")]
        [Authorize("Bearer")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Reserva>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetByIdSalaDate([FromRoute] int id, [FromBody] DateTime dataAgenda)
        {
            try
            {

                if (id == 0 || dataAgenda == null)
                {
                    throw new ArgumentException("Agenda ou data não fornecidos.");
                }

                if (dataAgenda.Date < DateTime.Now.Date)
                {
                    throw new Exception("Não é permitido buscar horários de dias anteriores.");
                }

                var reservas = _reservaService.GetByIdSala(id, dataAgenda);

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

        // POST: api/Reservas/Lista
        [HttpPost("Lista")]
        [Authorize("Bearer")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Reserva>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult PostReservasLista([FromBody] IEnumerable<Reserva> reservas)
        {
            try
            {
                if (reservas == null)
                {
                    throw new ArgumentException("Reservas não fornecidas.");
                }

                var reservasPos = _reservaService.Post(reservas);

                if (reservasPos == null)
                {
                    return NoContent();
                }

                return Ok(reservasPos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST: api/Reservas/Delete/5
        [HttpPost("Delete/{id}")]
        [Authorize("Bearer")]
        [ProducesResponseType(200, Type = typeof(Reserva))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Delete([FromRoute] int id, [FromBody] int idUsuarioExclusao)
        {
            try
            {

                if (id <= 0 || idUsuarioExclusao <= 0)
                {
                    throw new ArgumentException("Reserva ou usuário não fornecidos.");
                }

                var reserva = _reservaService.Delete(id, idUsuarioExclusao);

                if (reserva == null)
                {
                    return NoContent();
                }

                return Ok(reserva);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


    }
}