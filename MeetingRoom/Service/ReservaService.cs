using MeetingRoom.Models;
using MeetingRoom.Repository.Interface;
using MeetingRoom.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingRoom.Service
{
    public class ReservaService : IReservaService
    {

        private readonly IReservaRepository _reservaRepository;

        public ReservaService(IReservaRepository reservaRepository)
        {
            _reservaRepository = reservaRepository;
        }

        public Reserva Delete(int idReserva, int idUsuarioExclusao)
        {
            try
            {
                var reserva = _reservaRepository.GetById(idReserva);

                if (reserva == null)
                {
                    throw new Exception("Não existe esta reserva na base de dados.");
                }

                if (reserva.NidPessoa != idUsuarioExclusao)
                {
                    throw new Exception("Somente o usuário que cadastrou a reserva pode exclui-la.");
                }

                return _reservaRepository.Delete(idReserva);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Reserva> GetByIdSala(int idSala, DateTime dataAgenda)
        {
            try
            {
                var reservas = _reservaRepository.GetByIdSala(idSala, dataAgenda);

                List<Reserva> reservasDia = new List<Reserva>();

                //gerando todas as reservas do dia vazias de 30 em 30 minutos
                for (int i = 16; i < 48; i++)
                {
                    Reserva reserva = new Reserva();

                    reserva.DdataHoraIni = dataAgenda.Date;
                    reserva.DdataHoraIni = reserva.DdataHoraIni.AddMinutes(i * 30);

                    reserva.DdataHoraFim = dataAgenda.Date;
                    reserva.DdataHoraFim = reserva.DdataHoraFim.AddMinutes(i * 30 + 30);

                    var reservaCadastrada = reservas.Where(x => x.DdataHoraIni == reserva.DdataHoraIni && x.DdataHoraFim == reserva.DdataHoraFim).FirstOrDefault();

                    //inserindo as reservas do dia na lista
                    if (reservaCadastrada != null)
                    {
                        reservasDia.Add(reservaCadastrada);
                    }
                    else
                    {
                        reservasDia.Add(reserva);
                    }

                }

                return reservasDia;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Reserva Post(Reserva reserva)
        {
            try
            {
                return _reservaRepository.Post(reserva);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Reserva> Post(IEnumerable<Reserva> reserva)
        {
            try
            {
                return _reservaRepository.Post(reserva);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
