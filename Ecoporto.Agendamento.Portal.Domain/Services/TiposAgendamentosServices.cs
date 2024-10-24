using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecoporto.Agendamento.Portal.Domain.Entities;
using Ecoporto.Agendamento.Portal.Domain.Interfaces.Repository;
using Ecoporto.Agendamento.Portal.Domain.Interfaces.Services;


namespace Ecoporto.Agendamento.Portal.Domain.Services
{
    public class TiposAgendamentosServices: ITiposAgendamentosServices
    {
        private readonly ITiposAgendamentosRepository _tiposAgendamentosRepository;

        public TiposAgendamentosServices(ITiposAgendamentosRepository tiposAgendamentosRepository)
        {
            _tiposAgendamentosRepository = tiposAgendamentosRepository; 
        }

        public IEnumerable<TiposAgendamentos> GetListarTiposAgendamentos()
        {
            return _tiposAgendamentosRepository.GetListarTiposAgendamentos();
        }
    }
}
