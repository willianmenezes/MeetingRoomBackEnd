using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MeetingRoom.Models
{
    public partial class Reserva
    {
        public int NidReserva { get; set; }
        public int Nstatus { get; set; }
        public DateTime DdataHoraIni { get; set; }
        public DateTime DdataHoraFim { get; set; }
        public int NidSala { get; set; }
        public int NidPessoa { get; set; }
        public string Stitulo { get; set; }
        public string Sdescricao { get; set; }
        public int? NmotivoCancelamento { get; set; }

        public virtual Pessoa NidPessoaNavigation { get; set; }
        [JsonIgnore]
        public virtual Sala NidSalaNavigation { get; set; }
    }
}
