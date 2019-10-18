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

        public Pessoa UpdatePassword(string email, string novaSenha)
        {
            try
            {
                var pessoa = GetbyLogin(email);

                pessoa.Ssenha = novaSenha;

                _context.SaveChanges();

                return pessoa;

            }
            catch (Exception ex)
            {

                throw new Exception("Erro ao alterar senha.", ex);
            }
        }
    }
}
