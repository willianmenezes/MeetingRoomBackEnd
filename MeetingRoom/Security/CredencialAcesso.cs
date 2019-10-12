using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingRoom.Security
{
    public class CredencialAcesso
    {
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public string RefreshToken { get; set; }
        public string TipoConcessao { get; set; }
        public string Expiration { get; set; }
    }
}
