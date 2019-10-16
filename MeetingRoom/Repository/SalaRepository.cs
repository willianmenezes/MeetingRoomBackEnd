using MeetingRoom.Models;
using MeetingRoom.Models.Enum;
using MeetingRoom.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingRoom.Repository
{
    public class SalaRepository : ISalaRepository
    {
        private readonly Context _context;

        public SalaRepository(Context context)
        {
            _context = context;
        }

        public Sala Delete(int idSala)
        {
            throw new NotImplementedException();
        }

        public bool Exists(int idSala)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Sala> GetAll()
        {
            try
            {
                var salas =  _context.Sala
                                .Where(s => s.Nstatus == (int)StatusEnum.Ativo)
                                .ToList();

                return salas;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar as todas as salas.", ex);
            }
        }

        public Sala GetById(int idSala)
        {
            try
            {
                return _context.Sala.SingleOrDefault(x => x.NidSala == idSala);
            }
            catch (Exception ex)
            {
                throw new Exception("Sala não encontrada.", ex);
            }
        }

        public Sala Update(Sala sala, int idSala)
        {
            throw new NotImplementedException();
        }
    }
}
