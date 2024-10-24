using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecoporto.Agendamento.Portal.Application.Interfaces;
using Ecoporto.Agendamento.Portal.Domain.Interfaces.Services;
using Ecoporto.Agendamento.Portal.Domain.Entities;

namespace Ecoporto.Agendamento.Portal.Application.Services
{
    public class TiposAgendamentosAppServices: ITiposAgendamentosAppServices
    {
        private readonly ITiposAgendamentosServices _tiposAgendamentosServices;

        public TiposAgendamentosAppServices(ITiposAgendamentosServices tiposAgendamentosServices)
        {
            _tiposAgendamentosServices = tiposAgendamentosServices; 
        }

        public IEnumerable<TiposAgendamentos> GetListarTiposAgendamentos()
        {
            return _tiposAgendamentosServices.GetListarTiposAgendamentos();
        }

    }
}
