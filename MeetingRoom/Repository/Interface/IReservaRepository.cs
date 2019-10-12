using MeetingRoom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingRoom.Repository.Interface
{
    public interface IReservaRepository
    {
        IEnumerable<Reserva> GetAll();
        Reserva GetById(int idReserva);
        Reserva Delete(int idReserva);
        Reserva Update(Reserva reserva, int idReserva);
        bool Exists(int idReserva);
    }
}
