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
    public class MotoristaAppServices : IMotoristaAppServices
    {
        private readonly IMotoristaServices _motoristaServices;

        public MotoristaAppServices(IMotoristaServices veiculosServices)
        {
            _motoristaServices = veiculosServices;
        }

        public int Cadastrar(Motorista motorista)
        {
            return _motoristaServices.Cadastrar(motorista);
        }
        public void Atualizar(Motorista motorista)
        {
            _motoristaServices.Atualizar(motorista);
        }
        public void Excluir(int id)
        {
            _motoristaServices.Excluir(id);
        }
        public IEnumerable<Motorista> ObterMotoristas(int transportadoraId)
        {
            return _motoristaServices.ObterMotoristas(transportadoraId);
        }
        public IEnumerable<Motorista> ObterMotoristasPorNomeOuCNH(string descricao, int transportadoraId)
        {
            return _motoristaServices.ObterMotoristasPorNomeOuCNH(descricao, transportadoraId);
        }
        public Motorista ObterMotoristaPorId(int id)
        {
            return _motoristaServices.ObterMotoristaPorId(id);
        }
        public Motorista ObterMotoristaPorCNH(string cnh, int transportadoraId)
        {
            return _motoristaServices.ObterMotoristaPorCNH(cnh, transportadoraId);
        }
        public Motorista ObterMotoristaPorCPF(string cpf, int transportadoraId)
        {
            return _motoristaServices.ObterMotoristaPorCPF(cpf, transportadoraId);
        }
        public IEnumerable<Motorista> ObterUltimos5MotoristasAgendados(int transportadoraId)
        {
            return _motoristaServices.ObterUltimos5MotoristasAgendados(transportadoraId);
        }
        public Motorista ObterMotoristaChronosPorCNH(string cnh)
        {
            return _motoristaServices.ObterMotoristaChronosPorCNH(cnh);
        }

        public IEnumerable<Paises> ObterPaises()
        {
            return _motoristaServices.ObterPaises();
        }

    }
}
