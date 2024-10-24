using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecoporto.Agendamento.Portal.Domain.Entities;
using Ecoporto.Agendamento.Portal.Domain.Interfaces.Services;
using Ecoporto.Agendamento.Portal.Application.Interfaces; 

namespace Ecoporto.Agendamento.Portal.Application.Services
{
    public class LoginAppServices: ILoginAppServices 
    {
        private readonly ILoginServices _loginServices; 
        
        public LoginAppServices(ILoginServices loginServices)
        {
            _loginServices = loginServices;
        }

        public Login GetLoginUsuario(int id)
        {
            return _loginServices.GetLoginUsuario(id);
        }
    }
}
