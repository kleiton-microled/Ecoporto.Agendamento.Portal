using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecoporto.Agendamento.Portal.Domain.Entities;

namespace Ecoporto.Agendamento.Portal.Domain.Interfaces.Repository
{
    public interface ITiposAgendamentosRepository
    {
        IEnumerable<TiposAgendamentos> GetListarTiposAgendamentos();
    }
}
