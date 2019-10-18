using MeetingRoom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingRoom.Repository.Interface
{
    public interface IPessoaRepository
    {
        Pessoa UpdatePassword(string email, string novaSenha);
        Pessoa GetbyLogin(string login);
    }
}
