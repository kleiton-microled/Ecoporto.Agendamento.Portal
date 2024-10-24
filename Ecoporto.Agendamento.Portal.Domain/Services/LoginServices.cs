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
    public class LoginServices : ILoginServices
    {
        private readonly ILoginRepository _loginRepository;

        public LoginServices(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        public Login GetLoginUsuario(int id)
        {
            return _loginRepository.GetLoginUsuario(id);
        }
    }
}
