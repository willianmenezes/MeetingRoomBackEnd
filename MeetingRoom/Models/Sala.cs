using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MeetingRoom.Models
{
    public partial class Sala
    {
        public Sala()
        {
            Reserva = new HashSet<Reserva>();
        }

        public int NidSala { get; set; }
        public string Snome { get; set; }
        public int Nstatus { get; set; }

        public virtual ICollection<Reserva> Reserva { get; set; }
    }
}
