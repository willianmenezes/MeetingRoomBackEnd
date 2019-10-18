﻿using MeetingRoom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingRoom.Service.Interface
{
    public interface ISalaService
    {
        IEnumerable<Sala> GetAll();
        Sala GetById(int idSala);
        Sala Delete(int idSala);
        Sala Update(Sala sala, int idSala);
        bool Exists(int idSala);
        Sala InsertSala(Sala sala);
    }
}
