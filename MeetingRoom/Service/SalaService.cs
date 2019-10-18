using MeetingRoom.Models;
using MeetingRoom.Models.Enum;
using MeetingRoom.Repository;
using MeetingRoom.Repository.Interface;
using MeetingRoom.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingRoom.Service
{
    public class SalaService : ISalaService
    {
        private readonly ISalaRepository _salaRepository;

        public SalaService(ISalaRepository salaRepository)
        {
            _salaRepository = salaRepository;
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
                return _salaRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Sala GetById(int idSala)
        {
            try
            {
                return _salaRepository.GetById(idSala);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Sala InsertSala(Sala sala)
        {
            try
            {
                sala.Nstatus = (int)StatusEnum.Ativo;

               return _salaRepository.InsertSala(sala);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Sala Update(Sala sala, int idSala)
        {
            throw new NotImplementedException();
        }
    }
}
