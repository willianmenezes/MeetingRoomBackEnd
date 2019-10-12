using MeetingRoom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingRoom.Service.Interface
{
    public interface IPessoaService
    {
        IEnumerable<Pessoa> GetAll();
        Pessoa GetById(int idPessoa);
        Pessoa Delete(int idPessoa);
        Pessoa Update(Pessoa pessoa, int idPessoa);
        bool Exists(int idPessoa);
    }
}
