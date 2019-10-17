﻿using MeetingRoom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingRoom.Repository.Interface
{
    public interface IReservaRepository
    {
        Reserva Delete(int idReserva);
        IEnumerable<Reserva> GetByIdSala(int idSala);
        Reserva Post(Reserva reserva);
    }
}
