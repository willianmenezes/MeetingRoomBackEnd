using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MeetingRoom.Models
{
    public partial class RefreshToken
    {
        public int NidPessoa { get; set; }
        public string SrefreshToken { get; set; }
        public string SfinalExpiration { get; set; }
        [JsonIgnore]
        public virtual Pessoa NidPessoaNavigation { get; set; }
    }
}
