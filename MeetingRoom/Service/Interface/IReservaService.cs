using MeetingRoom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingRoom.Service.Interface
{
    public interface IReservaService
    {
        IEnumerable<Reserva> GetByIdSala(int idSala);
        Reserva Delete(int idReserva);
        Reserva Post(Reserva reserva);
    }
}
