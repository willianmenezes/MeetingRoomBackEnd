using MeetingRoom.Security;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MeetingRoom.Service.Interface
{
    public interface ILoginService
    {
        object GetByLogin(CredencialAcesso credenciais);
        string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate, JwtSecurityTokenHandler handler);
        object ExceptionObject();
        object SuccessObject(DateTime createDate, DateTime expirationDate, string token, TimeSpan finalExpiration, int NIdPessoa);
    }
}
