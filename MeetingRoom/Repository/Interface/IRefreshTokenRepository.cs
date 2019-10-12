using MeetingRoom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingRoom.Repository.Interface
{
    public interface IRefreshTokenRepository
    {
        void SetRefreshToken(RefreshToken refreshtoken);
        RefreshToken GetRefreshToken(int Nidpessoa);
    }
}
