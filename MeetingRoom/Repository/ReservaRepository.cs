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
            throw new NotImplementedException();
        }

        public bool Exists(int idReserva)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Reserva> GetAll()
        {
            throw new NotImplementedException();
        }

        public Reserva GetById(int idReserva)
        {
            throw new NotImplementedException();
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
