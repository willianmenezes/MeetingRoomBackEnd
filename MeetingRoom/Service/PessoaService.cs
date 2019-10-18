using MeetingRoom.Models;
using MeetingRoom.Repository.Interface;
using MeetingRoom.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoom.Service
{
    public class PessoaService : IPessoaService
    {

        private readonly IPessoaRepository _pessoaRepository;

        public PessoaService(IPessoaRepository pessoaRepository)
        {
            _pessoaRepository = pessoaRepository;
        }
        public Pessoa GetbyLogin(string login)
        {
            try
            {
                return _pessoaRepository.GetbyLogin(login);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Pessoa UpdatePassword(string senhaAtual, string novaSenha, string email)
        {
            try
            {
                var pessoa = _pessoaRepository.GetbyLogin(email);

                if (pessoa == null)
                {
                    throw new Exception("Usuário não encontrado na base de dados!");
                }

                //convertendo a senha digitada do usuário em base 64
                var textBytes = Encoding.UTF8.GetBytes(novaSenha);
                string encodeText = Convert.ToBase64String(textBytes);
                novaSenha = encodeText;

                // convertendo a senha digitada do usuário em base 64
                textBytes = Encoding.UTF8.GetBytes(senhaAtual);
                encodeText = Convert.ToBase64String(textBytes);
                senhaAtual = encodeText;

                if (!pessoa.Ssenha.Equals(senhaAtual))
                {
                    throw new Exception("Senha atual inválida.");
                }

                if (pessoa.Ssenha.Equals(novaSenha))
                {
                    throw new Exception("A senha atual não pode ser igual a senha anterior.");
                }

                return _pessoaRepository.UpdatePassword(email, novaSenha);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
