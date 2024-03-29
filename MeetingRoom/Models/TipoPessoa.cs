﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MeetingRoom.Models
{
    public partial class TipoPessoa
    {
        public TipoPessoa()
        {
            Pessoa = new HashSet<Pessoa>();
        }

        public int NidTipoPessoa { get; set; }
        public string Sdescricao { get; set; }

        [JsonIgnore]
        public virtual ICollection<Pessoa> Pessoa { get; set; }
    }
}
