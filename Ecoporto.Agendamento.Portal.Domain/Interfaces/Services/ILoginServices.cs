using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecoporto.Agendamento.Portal.Domain.Entities;
using Ecoporto.Agendamento.Portal.Domain.Interfaces.Repository;

namespace Ecoporto.Agendamento.Portal.Domain.Interfaces.Services
{
    public interface ILoginServices
    {
        Login GetLoginUsuario(int id);
    }
}
