using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecoporto.Agendamento.Portal.Domain.Entities;

namespace Ecoporto.Agendamento.Portal.Application.Interfaces
{
    public interface ILoginAppServices
    {
        Login GetLoginUsuario(int id);
    }
}
