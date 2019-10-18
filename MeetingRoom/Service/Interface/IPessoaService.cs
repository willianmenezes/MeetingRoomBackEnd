using MeetingRoom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingRoom.Service.Interface
{
    public interface IPessoaService
    {
        Pessoa UpdatePassword(string senhaAtual, string novaSenha, string email);
        Pessoa GetbyLogin(string login);
    }
}
