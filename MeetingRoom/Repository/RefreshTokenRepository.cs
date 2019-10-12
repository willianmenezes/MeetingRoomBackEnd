using MeetingRoom.Models;
using MeetingRoom.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingRoom.Repository
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {

        private readonly Context _context;

        public RefreshTokenRepository(Context context)
        {
            _context = context;
        }

        public RefreshToken GetRefreshToken(int NidPessoa)
        {
            try
            {
                return _context.RefreshToken.FirstOrDefault(x => x.NidPessoa == NidPessoa);
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possivel buscar o refresh token.", ex);
            }
        }

        public void SetRefreshToken(RefreshToken refreshtoken)
        {
            try
            {
                var refresh = _context.RefreshToken.FirstOrDefault(x => x.NidPessoa == refreshtoken.NidPessoa);

                if (refresh != null)
                {
                    refresh.SrefreshToken = refreshtoken.SrefreshToken;
                    refresh.SfinalExpiration = refreshtoken.SfinalExpiration;
                }

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possivel salvar o refresh token.", ex);
            }
        }
    }
}
