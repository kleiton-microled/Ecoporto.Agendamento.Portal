using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecoporto.Agendamento.Portal.Domain.Entities;
using Ecoporto.Agendamento.Portal.Domain.Interfaces.Services;

namespace Ecoporto.Agendamento.Portal.Domain.Interfaces.Services
{
    public interface ITiposAgendamentosServices
    {
        IEnumerable<TiposAgendamentos> GetListarTiposAgendamentos();
    }
}
