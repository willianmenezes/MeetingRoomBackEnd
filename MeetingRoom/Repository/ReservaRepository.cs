using MeetingRoom.Models;
using MeetingRoom.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingRoom.Repository
{
    public class ReservaRepository : IReservaRepository
    {

        private Context _context;

        public ReservaRepository(Context context)
        {
            _context = context;
        }

        public Reserva Delete(int idReserva)
        {
            try
            {
                var reserva = _context.Reserva.SingleOrDefault(x => x.NidReserva == idReserva);

                if (reserva != null)
                {
                    _context.Remove(reserva);
                }

                _context.SaveChanges();

                return reserva;

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao excluir a reserva.", ex);
            }
        }

        public IEnumerable<Reserva> GetByIdSala(int idSala)
        {
            try
            {
                return _context.Reserva.Where(x => x.NidSala == idSala).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar as reservas da sala.", ex);
            }
        }

        public Reserva Post(Reserva reserva)
        {
            try
            {
                _context.Reserva.Add(reserva);

                _context.SaveChanges();

                return reserva;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao inserir a reserva na sala.", ex);
            }
        }

        public Reserva Update(Reserva reserva, int idReserva)
        {
            throw new NotImplementedException();
        }
    }
}
