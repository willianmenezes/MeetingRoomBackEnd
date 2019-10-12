using MeetingRoom.Models;
using MeetingRoom.Repository;
using MeetingRoom.Repository.Interface;
using MeetingRoom.Security;
using MeetingRoom.Service.Interface;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoom.Service
{
    public class LoginService : ILoginService
    {

        private readonly IPessoaRepository _pessoaRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private SigninConfiguration _signingConfiguration;
        private TokenConfiguration _tokenConfiguration;

        private Pessoa usuario;

        public LoginService(IPessoaRepository repository,
                            SigninConfiguration signinConfiguration,
                            TokenConfiguration tokenConfiguration,
                            IRefreshTokenRepository refreshTokenRepository)
        {
            _pessoaRepository = repository;
            _signingConfiguration = signinConfiguration;
            _tokenConfiguration = tokenConfiguration;
            _refreshTokenRepository = refreshTokenRepository;
        }


        public object GetByLogin(CredencialAcesso credenciais)
        {
            try
            {
                bool validadeCredencial = false;

                if (credenciais != null && !string.IsNullOrWhiteSpace(credenciais.Usuario))
                {
                    usuario = _pessoaRepository.GetbyLogin(credenciais.Usuario);

                    if (credenciais.TipoConcessao.Equals("password"))
                    {

                        //convertendo a senha do usuário em base 64
                        var textBytes = Encoding.UTF8.GetBytes(credenciais.Senha);
                        string encodeText = Convert.ToBase64String(textBytes);
                        credenciais.Senha = encodeText;

                        //verificando se as credenciais do usuário são validas
                        validadeCredencial = (usuario != null && credenciais.Usuario == usuario.Slogin && credenciais.Senha == usuario.Ssenha);

                    }
                    else if (credenciais.TipoConcessao.Equals("refresh_token"))
                    {
                        if (!string.IsNullOrWhiteSpace(credenciais.RefreshToken))
                        {
                            //buscando no banco o refresh token do usuário
                            RefreshToken refreshTokenBase = _refreshTokenRepository.GetRefreshToken(usuario.NidPessoa);

                            //calculando nova data de expiração do token
                            DateTime dataExpiracao = DateTime.Parse(credenciais.Expiration);
                            dataExpiracao = dataExpiracao + TimeSpan.FromSeconds(_tokenConfiguration.FinalExpiration);

                            DateTime dtaAtual = DateTime.Now;

                            //verificando se o refresh token é valido
                            validadeCredencial = (refreshTokenBase != null &&
                                                  usuario.NidPessoa == refreshTokenBase.NidPessoa &&
                                                  refreshTokenBase.SrefreshToken == credenciais.RefreshToken &&
                                                  dataExpiracao >= dtaAtual);
                        }
                    }
                }

                if (validadeCredencial)
                {
                    ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(credenciais.Usuario, "Login"),
                        new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                            new Claim(JwtRegisteredClaimNames.UniqueName, usuario.NidPessoa.ToString()),
                            new Claim(JwtRegisteredClaimNames.UniqueName, usuario.Snome),
                            new Claim(JwtRegisteredClaimNames.UniqueName, usuario.Slogin),
                            new Claim(JwtRegisteredClaimNames.UniqueName, usuario.NidTipoPessoa.ToString()),
                        }
                    );

                    //cria a data de criação e expriração do token
                    DateTime dataCriacao = DateTime.Now;
                    DateTime dataExpiracao = dataCriacao + TimeSpan.FromSeconds(_tokenConfiguration.Seconds);

                    //calcula um tempo de sobre para a expricação do token
                    TimeSpan finalExpriracao = TimeSpan.FromSeconds(_tokenConfiguration.FinalExpiration);

                    JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                    string token = CreateToken(identity, dataCriacao, dataExpiracao, handler);

                    return SuccessObject(dataCriacao, dataExpiracao, token, finalExpriracao, usuario.NidPessoa);
                }
                else
                {
                    return ExceptionObject();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object SuccessObject(DateTime createDate, DateTime expirationDate, string token, TimeSpan finalExpiration, int NIdPessoa)
        {
            var resultado = new
            {
                autenticado = true,
                criado = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken = token,
                refreshToken = Guid.NewGuid().ToString().Replace("-", String.Empty),
                mensagem = "OK"
            };

            try
            {
                // Armazena o refresh token
                var refreshTokenData = new RefreshToken();
                refreshTokenData.SrefreshToken = resultado.refreshToken;
                refreshTokenData.NidPessoa = NIdPessoa;
                refreshTokenData.SfinalExpiration = finalExpiration.ToString();

                _refreshTokenRepository.SetRefreshToken(refreshTokenData);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resultado;
        }

        public string CreateToken(ClaimsIdentity identity, DateTime dataCriacao, DateTime dataExpiracao, JwtSecurityTokenHandler handler)
        {
            var tokenSeguro = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfiguration.Issuer,
                Audience = _tokenConfiguration.Audience,
                SigningCredentials = _signingConfiguration.SigningCredentials,
                Subject = identity,
                NotBefore = dataCriacao,
                Expires = dataExpiracao
            });

            var token = handler.WriteToken(tokenSeguro);

            return token;
        }

        public object ExceptionObject()
        {
            return new
            {
                autenticado = false,
                mensagem = "Failed to autheticate"
            };
        }
    }
}
