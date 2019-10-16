using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MeetingRoom.Models
{
    public partial class Pessoa
    {
        public Pessoa()
        {
            Reserva = new HashSet<Reserva>();
        }

        public int NidPessoa { get; set; }
        public string Snome { get; set; }
        public string Sapelido { get; set; }
        public int Nstatus { get; set; }
        public string Slogin { get; set; }
        public string Ssenha { get; set; }
        public int NidTipoPessoa { get; set; }

        [JsonIgnore]
        public virtual TipoPessoa NidTipoPessoaNavigation { get; set; }
        [JsonIgnore]
        public virtual RefreshToken RefreshToken { get; set; }
        [JsonIgnore]
        public virtual ICollection<Reserva> Reserva { get; set; }
    }
}
