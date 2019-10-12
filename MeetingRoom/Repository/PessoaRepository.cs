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

        private readonly Context _context;
        public PessoaRepository(Context context)
        {
            _context = context;
        }

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

        public Pessoa GetbyLogin(string login)
        {
            try
            {
                Pessoa pessoa = _context.Pessoa.SingleOrDefault(x => x.Slogin.Equals(login));

                return pessoa;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro os buscar e-mail de usuário.", ex);
            }
        }

        public Pessoa Update(Pessoa pessoa, int idPessoa)
        {
            throw new NotImplementedException();
        }
    }
}
