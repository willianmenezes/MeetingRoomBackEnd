using MeetingRoom.Models;
using MeetingRoom.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingRoom.Repository
{
    public class PessoaRepository : IPessoaRepository
    {
        public Pessoa Delete(int idPessoa)
        {
            throw new NotImplementedException();
        }

        public bool Exists(int idPessoa)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Pessoa> GetAll()
        {
            throw new NotImplementedException();
        }

        public Pessoa GetById(int idPessoa)
        {
            throw new NotImplementedException();
        }

        public Pessoa Update(Pessoa pessoa, int idPessoa)
        {
            throw new NotImplementedException();
        }
    }
}
